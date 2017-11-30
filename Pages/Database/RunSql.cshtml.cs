using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

namespace VxMySQLManager.Pages.Database
{
    public class RunSql : BasePageModel
    {
        public DataListResult DataListResult { get; set; }
        public string SqlQuery { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string database, string sqlQuery)
        {
            DatabaseName = database;

            if (string.IsNullOrEmpty(sqlQuery))
            {
                return Page();
            }

            try
            {
                DataListResult = await new EntityManager(database).ExecuteDataQuery(null, sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                ErrorMessage = e.Message;
            }
            
            return Page();
        }
        
        
    }
}