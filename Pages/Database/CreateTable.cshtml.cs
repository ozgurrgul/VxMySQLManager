using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

namespace VxMySQLManager.Pages.Database
{
    public class CreateTable : BasePageModel
    {
        [BindProperty]
        public new string TableName { get; set; }
        
        [BindProperty]
        public bool WithPrimaryKey { get; set; }
        
        [BindProperty]
        public bool WithTimestampColumns { get; set; }
        
        public async Task OnGetAsync(string database)
        {
            DatabaseName = database;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TempData.Clear();
            
            if (string.IsNullOrWhiteSpace(DatabaseName))
            {
                ModelState.AddModelError(nameof(DatabaseName), "Please select a database");
                return Page();
            }
            
            if (string.IsNullOrWhiteSpace(TableName))
            {
                ModelState.AddModelError(nameof(TableName), "Table name can't be empty");
                return Page();
            }
            
            var result = await new EntityManager(DatabaseName).CreateTable(TableName, null, WithPrimaryKey, WithTimestampColumns);
            ExecutedSql = result.ExecutedSql;
            
            if (result.Success)
            {
                SuccessMessage = "Table created";
                return RedirectToPage(new
                {
                    database = DatabaseName
                });
            }
            else
            {
                ErrorMessage = result.ErrorMessage;
                return Page();
            }
        }
    }
}