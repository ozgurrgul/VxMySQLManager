namespace VxMySQLManager.VxEntityManager.Columns
{
    public class SmallIntColumn : IntColumn
    {
        public SmallIntColumn()
        {
            Type = ColumnTypes.SmallInt;
            Length = 5;
        }
    }
}