using System.Collections.Generic;
using System.Linq;
using VxMySQLManager.VxEntityManager.Columns;
using VxMySQLManager.VxEntityManager.Extensions;

namespace VxMySQLManager.VxEntityManager.Sql
{
    public class MySqlGenerator : ISqlGenerator
    {
        public string CreateTableQuery(string tableName, List<IColumn> columns)
        {
            new { tableName }.ThrowIfNull();
            var sql = $"CREATE TABLE {tableName}";
            var hasPrimaryKey = columns.Exists(r => r.PrimaryKey);
            
            if(columns.Count > 0)
            {
                sql = sql + "(";
                
                foreach (var column in columns)
                {
                    sql += $"{column.GetCreateTableParameter()}, ";
                }
                
                sql = sql.Remove(sql.Length - 1); // remove space
                sql = sql.Remove(sql.Length - 1); // remove comma

                if (hasPrimaryKey)
                {
                    var primaryColumnsArray = columns.Where(r => r.PrimaryKey).Select(r => r.Name).ToArray();
                    var primaryColumnsStr = string.Join(",", primaryColumnsArray);
                    sql += $", PRIMARY KEY({primaryColumnsStr})";
                }
                
                sql = sql + ")";
            }

            return sql + ";";
        }

        public string DropTableQuery(string tableName)
        {
            new { tableName }.ThrowIfNull();
            return $"DROP TABLE {tableName};";
        }

        public string DropColumnQuery(string databaseName, string tableName, string columnName)
        {
            new { tableName }.ThrowIfNull();
            new { columnName }.ThrowIfNull();
            return $"USE `{databaseName}`; " +
                   $"ALTER TABLE {tableName} DROP COLUMN {columnName};";
        }

        public string AddColumnQuery(string databaseName, string tableName, IColumn column)
        {
            new { tableName }.ThrowIfNull();
            return $"USE `{databaseName}`; " +
                   $"ALTER TABLE {tableName} ADD COLUMN {column.GetAddColumnSql()};";
        }

        public string ChangeColumnQuery(string tableName, IColumn column)
        {
            new { tableName }.ThrowIfNull();
            return $"ALTER TABLE {tableName} CHANGE {column.GetChangeColumnParameter()};";
        }
    }
}