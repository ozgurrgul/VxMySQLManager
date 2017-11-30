namespace VxMySQLManager.VxEntityManager
{
    public class ForeignKeyResult
    {
        public string ConstraintName { get; set; }
        public string ReferencedTableName { get; set; }
        public string ReferencedColumnName { get; set; }
    }
}