namespace VxMySQLManager.VxEntityManager
{
    // use schemaResult instead?
    public class ColumnResult
    {
        public string Field { get; set; }
        public string Type { get; set; }
        public string Null { get; set; }
        public dynamic Key { get; set; }
        public dynamic Default { get; set; }
        public dynamic Extra { get; set; }
        public ForeignKeyResult ForeignKeyResult { get; set; }
    }
}