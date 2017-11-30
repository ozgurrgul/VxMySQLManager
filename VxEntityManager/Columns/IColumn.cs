namespace VxMySQLManager.VxEntityManager.Columns
{
    public interface IColumn
    {
        string Name { get; set; }

        string NewName { get; set; }
        string Type { get; set; }

        int Length { get; set; }
        int Decimals { get; set; } // if type is float | double | decimal
        bool NotNull { get; set; }
        bool PrimaryKey { get; set; }
        bool AutoIncrement { get; set; }
        string Default { get; set; }


        string GetCreateTableParameter();
        string GetChangeColumnParameter();
        string GetAddColumnSql();
        string GetNotNullString();
    }
}