using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportShop.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}