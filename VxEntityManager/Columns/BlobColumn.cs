namespace VxMySQLManager.VxEntityManager.Columns
{
    public class BlobColumn : TextColumn
    {
        public BlobColumn()
        {
            Type = ColumnTypes.Blob;
        }
    }
}