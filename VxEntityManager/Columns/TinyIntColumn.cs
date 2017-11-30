namespace VxMySQLManager.VxEntityManager.Columns
{
    public class TinyIntColumn : IntColumn
    {
        public TinyIntColumn()
        {
            Type = ColumnTypes.TinyInt;
            Length = 1;
        }
    }
}