using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using VxMySQLManager.VxEntityManager.Columns;
using VxMySQLManager.VxEntityManager.Columns.Helpers;
using VxMySQLManager.VxEntityManager.Sql;

namespace VxMySQLManager.VxEntityManager
{
    public class EntityManager
    {
        private readonly ISqlGenerator _sqlGenerator;
        private readonly string _databaseName;

        //TODO: sqlGenerator methods
        public EntityManager(string databaseName)
        {
            _databaseName = databaseName;
            _sqlGenerator = new MySqlGenerator();
        }
        
        public async Task<EntityManagerResult> CreateTable(string tableName, List<IColumn> columns, bool withPrimaryKey, bool withTimestampColumns)
        {
            if (columns == null)
            {
                columns = new List<IColumn>();
            }

            if (withPrimaryKey)
            {
                columns.Add(new IdPrimaryKeyColumn());
            }
            
            if (withTimestampColumns)
            {
                columns.Add(new CreatedAtColumn());
                columns.Add(new UpdatedAtColumn());
            }
            var sql = _sqlGenerator.CreateTableQuery(tableName, columns);
            var result = new EntityManagerResult {ExecutedSql = sql};

            try
            {
                await ExecuteNonQueryAsync(sql);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            
            return result;
        }
        
        public async Task<EntityManagerResult> DropTable(string tableName)
        {
            var sql = _sqlGenerator.DropTableQuery(tableName);
            var result = new EntityManagerResult {ExecutedSql = sql};
            try
            {
                await ExecuteNonQueryAsync(sql);
                result.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }
        
        public async Task<EntityManagerResult> DropDatabase()
        {
            var sql = $"DROP DATABASE {_databaseName};";
            var result = new EntityManagerResult {ExecutedSql = sql};
            try
            {
                await ExecuteNonQueryAsync(sql);
                result.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }
        
        public async Task<EntityManagerResult> DropColumn(string tableName, string columnName)
        {
            var sql = _sqlGenerator.DropColumnQuery(_databaseName, tableName, columnName);
            var result = new EntityManagerResult {ExecutedSql = sql};
            try
            {
                await ExecuteNonQueryAsync(sql);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }
        
        public async Task<EntityManagerResult> AddColumn(string tableName, IColumn column)  
        {
            var sql = _sqlGenerator.AddColumnQuery(_databaseName, tableName, column);
            var result = new EntityManagerResult {ExecutedSql = sql};
            try
            {
                await ExecuteNonQueryAsync(sql);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }
        
        public async Task<EntityManagerResult> ChangeColumn(string tableName, IColumn column) 
        {
            var sql = _sqlGenerator.ChangeColumnQuery(tableName, column);
            var result = new EntityManagerResult {ExecutedSql = sql};
            try
            {
                await ExecuteNonQueryAsync(sql);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }
        
        public async Task<List<string>> ListTables()
        {
            var sql = "SHOW TABLES;";
            var list = new List<string>();

            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                var reader = await cmd.ExecuteReaderAsync();
                
                using (reader)
                {
                    while (await reader.ReadAsync())
                    {
                        var tableName = await reader.GetFieldValueAsync<string>(0);
                        list.Add(tableName);
                    }
                }
            }

            return list;
        }
        
        public async Task<List<string>> ListDatabases()
        {
            var sql = "SHOW DATABASES;";
            var list = new List<string>();

            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                var reader = await cmd.ExecuteReaderAsync();
                
                using (reader)
                {
                    while (await reader.ReadAsync())
                    {
                        var tableName = await reader.GetFieldValueAsync<string>(0);
                        list.Add(tableName);
                    }
                }
            }

            return list;
        }
        
        public async Task<string> ShowCreateTable(string tableName)
        {
            var sql = $"SHOW CREATE TABLE {tableName};";
            var createTableString = "";

            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                var reader = await cmd.ExecuteReaderAsync();
                
                using (reader)
                {
                    while (await reader.ReadAsync())
                    {
                        // skip first column which is 'Table'
                        createTableString = await reader.GetFieldValueAsync<string>(1);
                    }
                }
            }

            return createTableString;
        }
        
        public async Task<List<ColumnResult>> DescribeTable(string tableName)
        {
            var sql = $"DESC {tableName};";
            var descRows = new List<ColumnResult>();

            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                var reader = await cmd.ExecuteReaderAsync();
                
                using (reader)
                {
                    while (await reader.ReadAsync())
                    {
                        var field = await reader.GetFieldValueAsync<dynamic>(0);
                        
                        descRows.Add(new ColumnResult
                        {
                            Field = field,
                            Type = await reader.GetFieldValueAsync<dynamic>(1),
                            Null = await reader.GetFieldValueAsync<dynamic>(2),
                            Key = await reader.GetFieldValueAsync<dynamic>(3),
                            Default = await reader.GetFieldValueAsync<dynamic>(4),
                            Extra = await reader.GetFieldValueAsync<dynamic>(5),
                            ForeignKeyResult = tableName == null ? null : await GetForeignKeysForColumn(tableName, field)
                        });
                    }
                }
            }

            return descRows;
        }

        public async Task<DataListResult> ExecuteDataQuery(string tableName, string sql)
        {
            var dataListResult = new DataListResult();
            var dataList = new List<DataRow>();
            var dataColumnList = new List<ColumnResult>();
            
            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                var reader = await cmd.ExecuteReaderAsync();
                
                using (reader)
                {
                    while (await reader.ReadAsync())
                    {
                        var dataRow = new DataRow();

                        foreach (var column in reader.GetColumnSchema())
                        {
                            var columnName = column.ColumnName;
                            var baseTableName = column.BaseTableName;
                            var ordinal = reader.GetOrdinal(columnName);
                            dataRow[columnName] = await reader.GetFieldValueAsync<dynamic>(ordinal);

                            if (dataColumnList.Any(r => r.Field == columnName) == false)
                            {
                                dataColumnList.Add(new ColumnResult
                                {
                                    Field = columnName,
                                    ForeignKeyResult = baseTableName != null 
                                        ? await GetForeignKeysForColumn(baseTableName, columnName) 
                                        : null
                                });
                            }
                        }
                        
                        dataList.Add(dataRow);
                    }
                }
            }

            dataListResult.DataList = dataList.AsReadOnly();
            dataListResult.ColumnList = dataColumnList.AsReadOnly();
           
            return dataListResult;
        }

        public async Task<DataListResult> ListData(string tableName, int page, int limit)
        {
            var offset = page * limit;
            var sql = $"USE `{_databaseName}`" +
                      $";SELECT * FROM {tableName} LIMIT {limit} OFFSET {offset};";
            return await ExecuteDataQuery(tableName, sql);
        }
        
        public async Task<dynamic> CountData(string tableName)
        {
            var sql = $"SELECT COUNT(1) AS total FROM {tableName};";
            dynamic total = 0;
            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                var reader = await cmd.ExecuteReaderAsync();
                
                using (reader)
                {
                    while (await reader.ReadAsync())
                    {
                        total = await reader.GetFieldValueAsync<dynamic>(0);
                    }
                }
            }

            return total;
        }
        
        public async Task<ColumnSchemaResult> GetColumnSchema(string tableName, string columnName)
        {
            var sql = $@"SELECT COLUMN_NAME, COLUMN_TYPE, IS_NULLABLE, EXTRA, COLUMN_KEY, 
                                NUMERIC_PRECISION, NUMERIC_SCALE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH
                        FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE COLUMN_NAME IN ('{columnName}')
                            AND TABLE_SCHEMA='{_databaseName}'
                            AND TABLE_NAME = '{tableName}';";
            ColumnSchemaResult result = null;
            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                var reader = await cmd.ExecuteReaderAsync();
                
                using (reader)
                {
                    while (await reader.ReadAsync())
                    {
                        result = new ColumnSchemaResult
                        {
                            ColumnName = await reader.GetFieldValueAsync<string>(0),
                            ColumnType = await reader.GetFieldValueAsync<string>(1),
                            IsNullable = await reader.GetFieldValueAsync<string>(2),
                            Extra = await reader.GetFieldValueAsync<string>(3),
                            ColumnKey = await reader.GetFieldValueAsync<string>(4),
                            NumericPrecision = await reader.GetFieldValueAsync<dynamic>(5),
                            NumericScale = await reader.GetFieldValueAsync<dynamic>(6),
                            DataType = await reader.GetFieldValueAsync<string>(7),
                            MaxLength = await reader.GetFieldValueAsync<dynamic>(8)
                        };
                    }
                }
            }

            return result;
        }
        
        public async Task<ForeignKeyResult> GetForeignKeysForColumn(string tableName, string columnName)
        {
            var sql = $@"SELECT 
                      CONSTRAINT_NAME, REFERENCED_TABLE_NAME,REFERENCED_COLUMN_NAME
                    FROM
                      INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                    WHERE
                      REFERENCED_TABLE_SCHEMA = '{_databaseName}' 
                      AND TABLE_NAME = '{tableName}' 
                      AND COLUMN_NAME = '{columnName}';";
            
            ForeignKeyResult result = null;
            
            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                var reader = await cmd.ExecuteReaderAsync();
                
                using (reader)
                {
                    while (await reader.ReadAsync())
                    {
                        result = new ForeignKeyResult
                        {
                            ConstraintName = await reader.GetFieldValueAsync<string>(0),
                            ReferencedTableName = await reader.GetFieldValueAsync<string>(1),
                            ReferencedColumnName = await reader.GetFieldValueAsync<string>(2)
                        };
                    }
                }
            }

            return result;
        }

        private async Task ExecuteNonQueryAsync(string sql)
        {
            using (var db = new Db(_databaseName))
            {
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}