namespace VxMySQLManager.VxEntityManager.Columns.Helpers
{
    public class UpdatedAtColumn : DateTimeColumn
    {
        public UpdatedAtColumn()
        {
            Name = "UpdatedAt";
            UpdateTimestamp = true;
        }
    }
}