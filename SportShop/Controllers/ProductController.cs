using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportShop.Repositories;

namespace SportShop.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        [HttpGet("/")]
        public ViewResult Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                this.ViewBag.CurrentPage = "Main";
                return View(_productRepository.Products);
            }
            else
            {
                this.ViewBag.CurrentPage = "Categories";
                return View(_productRepository.Products.Where(x => x.Category == name));
            }
        }
    }
}