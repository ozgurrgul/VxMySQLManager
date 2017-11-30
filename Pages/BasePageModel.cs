using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VxMySQLManager.Pages
{
    public class BasePageModel : PageModel
    {        
        [TempData]
        public string ErrorMessage { get; set; }
        
        [TempData]
        public string SuccessMessage { get; set; }
        
        [TempData]
        public string ExecutedSql { get; set; }
        
        [BindProperty]
        public string DatabaseName { get; set; }
        
        [BindProperty]
        public string TableName { get; set; }

    }
}