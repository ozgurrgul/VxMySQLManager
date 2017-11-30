using System;
using System.Collections.Generic;

namespace VxMySQLManager.VxEntityManager.Columns
{
    public static class ColumnTypes
    {
        public static readonly string Boolean = "BOOLEAN";
        public static readonly string Char = "CHAR";
        public static readonly string Varchar = "VARCHAR";
        public static readonly string Text = "TEXT";
        public static readonly string TinyText = "TINYTEXT";
        public static readonly string MediumText = "MEDIUMTEXT";
        public static readonly string LongText = "LONGTEXT";
        public static readonly string Blob = "BLOB";
        public static readonly string TinyBlob = "TINYBLOB";
        public static readonly string MediumBlob = "MEDIUMBLOB";
        public static readonly string LongBlob = "LONGBLOB";

        public static readonly string Int = "INT";
        public static readonly string TinyInt = "TINYINT";
        public static readonly string SmallInt = "SMALLINT";
        public static readonly string MediumInt = "MEDIUMINT";
        public static readonly string BigInt = "BIGINT";
        public static readonly string Float = "FLOAT";
        public static readonly string Double = "DOUBLE";
        public static readonly string Decimal = "DECIMAL";

        public static readonly string Date = "DATE";
        public static readonly string DateTime = "DATETIME";
        public static readonly string Timestamp = "TIMESTAMP";
        public static readonly string Time = "TIME";

        public static List<String> GetColumns()
        {
            return new List<string>()
            {
                Boolean,
                Char,
                Varchar,
                Text,
                TinyText,
                MediumText,
                LongText,
                Blob,
                TinyBlob,
                MediumBlob,
                LongBlob,
                Int,
                TinyInt,
                SmallInt,
                MediumInt,
                BigInt,
                Float,
                Double,
                Decimal,
                Date,
                DateTime,
                Timestamp,
                Time
            };
        }

        public static IColumn GetColumnInstanceFromType(string dataType)
        {
            if (dataType == Boolean) return new BooleanColumn();
            if (dataType == Char) return new BooleanColumn();
            if (dataType == Varchar) return new VarcharColumn();
            if (dataType == Text) return new TextColumn();
            if (dataType == TinyText) return new TinyTextColumn();
            if (dataType == MediumText) return new MediumTextColumn();
            if (dataType == LongText) return new LongTextColumn();
            if (dataType == Text) return new TextColumn();
            if (dataType == Blob) return new BlobColumn();
            if (dataType == TinyBlob) return new TinyBlobColumn();
            if (dataType == MediumBlob) return new MediumBlobColumn();
            if (dataType == LongBlob) return new LongBlobColumn();
            if (dataType == Int) return new IntColumn();
            if (dataType == TinyInt) return new TinyIntColumn();
            if (dataType == SmallInt) return new SmallIntColumn();
            if (dataType == MediumInt) return new MediumIntColumn();
            if (dataType == BigInt) return new BigIntColumn();
            if (dataType == Float) return new VarcharColumn();
            if (dataType == Double) return new VarcharColumn();
            if (dataType == Decimal) return new DecimalColumn();
            if (dataType == Date) return new VarcharColumn();
            if (dataType == DateTime) return new DateTimeColumn();
            if (dataType == Timestamp) return new VarcharColumn();
            if (dataType == Time) return new VarcharColumn();

            throw new ApplicationException($"Unsupported dataType: {dataType}");
        }
    }
}