using System.Collections.ObjectModel;

namespace VxMySQLManager.VxEntityManager
{
    public class DataListResult
    {
        public ReadOnlyCollection<DataRow> DataList { get; set; }
        public ReadOnlyCollection<ColumnResult> ColumnList { get; set; }
    }
}