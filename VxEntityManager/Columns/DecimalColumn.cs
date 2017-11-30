namespace VxMySQLManager.VxEntityManager.Columns
{
    public class DecimalColumn : BaseColumn
    {
        public DecimalColumn()
        {
            Type = ColumnTypes.Decimal;
            Length = 10;
            Decimals = 2;
        }
        
        public override string GetCreateTableParameter()
        {
            return $"{Name} {Type}({Length},{Decimals}) {GetNotNullString()}";
        }
        
        public override string GetChangeColumnParameter()
        {
            if (string.IsNullOrEmpty(NewName))
            {
                NewName = Name;
            }
            
            return $"{Name} {NewName} {Type}({Length},{Decimals}) {GetNotNullString()}";
        }

        public override string GetAddColumnSql()
        {
            return $"{Name} {Type}({Length},{Decimals}) {GetNotNullString()}";
        }
        
        public override string GetNotNullString()
        {
            if (PrimaryKey)
            {
                return "NOT NULL";
            }
            
            return $"NOT NULL DEFAULT {Default}";
        }
    }
}