using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VxMySQLManager.VxEntityManager;

namespace VxMySQLManager.Pages.ViewComponents
{
    public class DatabaseListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var entityManager = new EntityManager("information_schema");
            var databaseList = await entityManager.ListDatabases();
            databaseList.Remove("information_schema");
            databaseList.Remove("sys");
            
            return View(databaseList);
        }
    }
}