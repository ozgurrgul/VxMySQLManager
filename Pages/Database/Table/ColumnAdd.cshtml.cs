using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;
using VxMySQLManager.VxEntityManager.Columns;

namespace VxMySQLManager.Pages.Database.Table
{
    public class ColumnAdd : BasePageModel
    {
        [BindProperty]
        public string ColumnName { get; set; }
        
        [BindProperty]
        public string DataType { get; set; }
        
        //TODO: not null
        [BindProperty]
        public bool IsNullable { get; set; }
        
        [BindProperty]
        public string Extra { get; set; }
        
        [BindProperty]
        public int Length { get; set; }
        
        [BindProperty]
        public int Decimals { get; set; }
        
        [BindProperty]
        public string Default { get; set; }

        public async Task<IActionResult> OnGetAsync(string database, string tableName)
        {
            DatabaseName = database;
            TableName = tableName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string database, string tableName)
        {
            TempData.Clear();
            
            if (string.IsNullOrEmpty(ColumnName))
            {
                ModelState.AddModelError(nameof(ColumnName), "Column name can't be empty");
                return Page();
            }

            var column = ColumnTypes.GetColumnInstanceFromType(DataType);
            column.Name = ColumnName;
            column.NotNull = !IsNullable;
            column.Length = Length;
            column.Decimals = Decimals;
            column.Default = Default;

            var result = await new EntityManager(database).AddColumn(tableName, column);
            ExecutedSql = result.ExecutedSql;
            
            if (result.Success)
            {
                SuccessMessage = "Column added";
                return RedirectToPage(new
                {
                    database,
                    tableName
                });
            }
            else
            {
                ErrorMessage = result.ErrorMessage;
                return Page();
            }
            
            return Page();
        }
    }
}