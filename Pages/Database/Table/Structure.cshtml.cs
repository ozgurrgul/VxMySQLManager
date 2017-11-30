using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

namespace VxMySQLManager.Pages.Database.Table
{
    public class Structure : BasePageModel
    {
        public List<ColumnResult> TableDescribeRows { get; set; }

        public async Task OnGetAsync(string database, string tableName)
        {
            DatabaseName = database;
            TableName = tableName;
            
            try
            {
                TableDescribeRows = await new EntityManager(database).DescribeTable(tableName);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
        
        public async Task<IActionResult> OnPostDropColumnAsync(string database, string tableName, string columnName)
        {
            
            var result = await new EntityManager(database).DropColumn(tableName, columnName);
            ExecutedSql = result.ExecutedSql;
            
            if (result.Success)
            {
                SuccessMessage = "Column dropped";
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