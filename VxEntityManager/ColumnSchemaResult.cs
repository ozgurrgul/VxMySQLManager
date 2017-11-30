namespace VxMySQLManager.VxEntityManager
{
    public class ColumnSchemaResult
    {
        public string ColumnName { get; set; }
        public string ColumnType { get; set; } // decimal(16,8)
        public string DataType { get; set; } // decimal
        public string IsNullable { get; set; }
        public string Extra { get; set; }
        public string ColumnKey { get; set; }
        public dynamic NumericPrecision { get; set; }
        public dynamic NumericScale { get; set; }
        public dynamic MaxLength { get; set; }
    }
}