using System;

namespace VxMySQLManager.VxEntityManager.Columns
{
    public class IntColumn : BaseColumn
    {
        public IntColumn()
        {
            Type = ColumnTypes.Int;
            Length = 11;
        }
        
        public override string GetCreateTableParameter()
        {
            if (AutoIncrement && !PrimaryKey)
            {
                throw new ApplicationException($"AutoIncrement column [{Name}] must be defined as primary key");
            }
            
            var autoIncrementSql = AutoIncrement ? "AUTO_INCREMENT" : "";
            
            return $"{Name} {Type}({Length}) {GetNotNullString()} {autoIncrementSql}";
        }
        
        public override string GetNotNullString()
        {
            if (PrimaryKey)
            {
                return $"NOT NULL";
            }
            
            return $"NOT NULL DEFAULT {Default}";
        }
        
    }
}