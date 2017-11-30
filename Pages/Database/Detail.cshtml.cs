using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

namespace VxMySQLManager.Pages.Database
{
    public class Detail : BasePageModel
    {
        public List<string> TableList { get; set; }
            
        public async Task OnGetAsync(string database)
        {
            DatabaseName = database;
            TableList = await new EntityManager(database).ListTables();
        }

        public async Task<IActionResult> OnPostDropTableAsync(string database, string tableName)
        {
            var result = await new EntityManager(database).DropTable(tableName);
            ExecutedSql = result.ExecutedSql;
            if (result.Success)
            {
                SuccessMessage = "Table dropped";
                return RedirectToPage(new
                {
                    database
                });
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