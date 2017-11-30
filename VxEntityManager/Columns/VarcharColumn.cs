namespace VxMySQLManager.VxEntityManager.Columns
{
    public class VarcharColumn : BaseColumn
    {
        public VarcharColumn()
        {
            Type = ColumnTypes.Varchar;
            
            if (Length == 0)
            {
                Length = 255;
            }
        }
        
        public override string GetNotNullString()
        {
            if (NotNull)
            {
                if (!string.IsNullOrEmpty(Default))
                {
                    return $"NOT NULL DEFAULT '{Default}'";
                }

                return "NOT NULL";
            }
            else
            {
                if (!string.IsNullOrEmpty(Default))
                {
                    return $"DEFAULT '{Default}'";
                }
            }
            
            return "";
        }
    }
}