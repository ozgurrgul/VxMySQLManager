using System;

namespace VxMySQLManager.VxEntityManager.Columns
{
    public class DateTimeColumn : BaseColumn
    {
        public bool UpdateTimestamp { get; set; }
        public bool CreateTimestamp { get; set; }
        
        public DateTimeColumn()
        {
            Type = ColumnTypes.DateTime;
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
            if (UpdateTimestamp)
            {
                return "ON UPDATE CURRENT_TIMESTAMP";
            }
            
            if (CreateTimestamp)
            {
                return "NOT NULL DEFAULT CURRENT_TIMESTAMP";
            }
            
            if (NotNull)
            {
                return "NOT NULL";
            }
            
            throw new ApplicationException("Datetime not implemented correctly");
        }
    }
}