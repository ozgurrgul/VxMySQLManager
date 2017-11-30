namespace VxMySQLManager.VxEntityManager.Columns
{
    public abstract class Column : IColumn
    {
        public string Name { get; set; }
        public string NewName { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
        public int Decimals { get; set; } // use it when type is float | double | decimal
        public bool NotNull { get; set; }
        public bool PrimaryKey { get; set; }
        public bool AutoIncrement { get; set; }
        public string Default { get; set; }
        
        public abstract string GetCreateTableParameter();
        public abstract string GetChangeColumnParameter();
        public abstract string GetAddColumnSql();
        public abstract string GetNotNullString();
        
    }
}