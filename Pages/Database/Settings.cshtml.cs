using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

namespace VxMySQLManager.Pages.Database
{
    public class Settings : BasePageModel
    {
        public async Task OnGetAsync(string database)
        {
            DatabaseName = database;
        }
        
        public async Task<IActionResult> OnPostDropDatabaseAsync(string database)
        {
            var result = await new EntityManager(database).DropDatabase();
            ExecutedSql = result.ExecutedSql;
            if (result.Success)
            {
                SuccessMessage = "Database dropped";
                return RedirectToPage("/");
            }
            else
            {
                ErrorMessage = result.ErrorMessage;
                return RedirectToPage(new
                {
                    database
                });
            }
        }
    }
}