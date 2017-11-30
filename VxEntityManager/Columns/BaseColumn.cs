namespace VxMySQLManager.VxEntityManager.Columns
{
    public class BaseColumn : Column
    {
        public override string GetCreateTableParameter()
        {
            return $"{Name} {Type}({Length}) {GetNotNullString()}";
        }
        
        public override string GetChangeColumnParameter()
        {
            if (string.IsNullOrEmpty(NewName))
            {
                NewName = Name;
            }
            
            return $"{Name} {NewName} {Type}({Length}) {GetNotNullString()}";
        }

        public override string GetAddColumnSql()
        {
            return $"{Name} {Type}({Length}) {GetNotNullString()}";
        }

        public override string GetNotNullString()
        {
            return "";
        }
        
       
    }
}