namespace VxMySQLManager.VxEntityManager.Columns.Helpers
{
    public class IdPrimaryKeyColumn : IntColumn
    {
        public IdPrimaryKeyColumn()
        {
            Name = "Id";
            PrimaryKey = true;
            AutoIncrement = true;
        }
    }
}