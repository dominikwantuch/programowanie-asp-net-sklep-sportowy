using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Repositories;

namespace SportShop.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private IProductRepository _repository;

        public CategoriesViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_repository.Products.Select(x => x.Category).Distinct().ToList());
        }
    }
}