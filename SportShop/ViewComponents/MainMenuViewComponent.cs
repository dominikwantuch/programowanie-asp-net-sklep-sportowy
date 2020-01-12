using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportShop.Repositories;

namespace SportShop.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public MainMenuViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_productRepository.Products.Select(x=> x.Category).Distinct().ToList());
        }
    }
}