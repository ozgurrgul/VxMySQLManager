using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

// TODO: complete later
namespace VxMySQLManager.Pages.Database.Table
{
    public class ColumnEdit : BasePageModel
    {
        [BindProperty]
        public string ColumnName { get; set; }
        
        [BindProperty]
        public string DataType { get; set; }
        
        [BindProperty]
        public bool IsNullable { get; set; }
        
        [BindProperty]
        public string Extra { get; set; }
        
        [BindProperty]
        public dynamic NumericPrecision { get; set; }
        
        [BindProperty]
        public dynamic NumericScale { get; set; }
        
        [BindProperty]
        public dynamic MaxLength { get; set; }
        
        public ColumnSchemaResult ColumnSchema { get; set; }

        public async Task<IActionResult> OnGetAsync(string database, string tableName, string columnName)
        {
            DatabaseName = database;
            TableName = tableName;
            ColumnName = columnName;
            
            try
            {
                ColumnSchema = await new EntityManager(database).GetColumnSchema(tableName, columnName);
                DataType = ColumnSchema.DataType;
                IsNullable = ColumnSchema.IsNullable == "YES";
                Extra = ColumnSchema.Extra;
                NumericPrecision = ColumnSchema.NumericPrecision;
                NumericScale = ColumnSchema.NumericScale;
                MaxLength = ColumnSchema.MaxLength;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            
            return Page();
        }

    }
}