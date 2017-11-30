using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

namespace VxMySQLManager.Pages.Database.Table
{
    public class Data : BasePageModel
    {
        public int DataPage { get; set; }
        public long TotalDataCount { get; set; }
        public DataListResult DataListResult { get; set; }
        public int DataLimit { get; set; }

        public async Task<IActionResult> OnGetAsync(string database, string tableName, int dataPage)
        {
            DataLimit = 10;
            DatabaseName = database;
            TableName = tableName;
            DataPage = dataPage;

            if (dataPage < 1)
            {
                dataPage = 1;
                return RedirectToPage(new
                {
                    database,
                    tableName,
                    dataPage
                });
            }

            try
            {
                DataListResult = await new EntityManager(database).ListData(tableName, dataPage - 1, DataLimit);
                TotalDataCount = await new EntityManager(database).CountData(tableName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                ErrorMessage = e.Message;
            }

            var list = new List<string>();
            
            return Page();
        }
    }
}