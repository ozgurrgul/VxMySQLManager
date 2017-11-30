using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VxMySQLManager.Pages.ViewComponents
{
    public class DatabaseLayoutTabsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}