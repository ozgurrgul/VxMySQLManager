namespace VxMySQLManager.VxEntityManager.Columns.Helpers
{
    public class CreatedAtColumn : DateTimeColumn
    {
        public CreatedAtColumn()
        {
            Name = "CreatedAt";
            CreateTimestamp = true;
        }
    }
}