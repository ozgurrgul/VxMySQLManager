namespace VxMySQLManager.VxEntityManager.Columns
{
    public class MediumIntColumn : IntColumn
    {
        public MediumIntColumn()
        {
            Type = ColumnTypes.MediumInt;
            Length = 7;
        }
    }
}