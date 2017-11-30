using System.Collections.Generic;
using VxMySQLManager.VxEntityManager.Columns;

namespace VxMySQLManager.VxEntityManager.Sql
{
    public interface ISqlGenerator
    {
        string CreateTableQuery(string tableName, List<IColumn> columns);
        string DropTableQuery(string tableName);
        string DropColumnQuery(string databaseName, string tableName, string columnName);
        string AddColumnQuery(string databaseName, string tableName, IColumn column);
        string ChangeColumnQuery(string tableName, IColumn column);
    }
}