namespace VxMySQLManager.VxEntityManager.Columns
{
    public class BigIntColumn : IntColumn
    {
        public BigIntColumn()
        {
            Type = ColumnTypes.BigInt;
            Length = 20;
        }
    }
}