namespace VxMySQLManager.VxEntityManager.Columns
{
    public class BooleanColumn : BaseColumn
    {
        public BooleanColumn()
        {
            Type = ColumnTypes.Boolean;
        }
        
        public override string GetCreateTableParameter()
        {
            return $"{Name} {Type} {GetNotNullString()}";
        }
        
        public override string GetChangeColumnParameter()
        {
            if (string.IsNullOrEmpty(NewName))
            {
                NewName = Name;
            }
            
            return $"{Name} {NewName} {Type} {GetNotNullString()}";
        }

        public override string GetAddColumnSql()
        {
            return $"{Name} {Type} {GetNotNullString()}";
        }
        
        public override string GetNotNullString()
        {
            if (NotNull)
            {
                return $"NOT NULL DEFAULT {Default}";
            }
            
            return "";
        }
    }
}