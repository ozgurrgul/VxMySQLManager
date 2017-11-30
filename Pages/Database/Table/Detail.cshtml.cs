using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

namespace VxMySQLManager.Pages.Database.Table
{
    public class Detail : BasePageModel
    {
        public string CreateTableString { get; set; }
        
        public async Task OnGetAsync(string database, string tableName)
        {
            DatabaseName = database;
            TableName = tableName;
            
            try
            {
                CreateTableString = await new EntityManager(database).ShowCreateTable(tableName);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
        
        public async Task<IActionResult> OnPostDropTableAsync(string database, string tableName)
        {
            var result = await new EntityManager(database).DropTable(tableName);
            ExecutedSql = result.ExecutedSql;
            if (result.Success)
            {
                SuccessMessage = "Table dropped";
                return RedirectToPage("/Database/Detail", new
                {
                    database
                });
            }
            else
            {
                ErrorMessage = result.ErrorMessage;
                return RedirectToPage(new
                {
                    database,
                    tableName
                });
            }
        }
    }
}