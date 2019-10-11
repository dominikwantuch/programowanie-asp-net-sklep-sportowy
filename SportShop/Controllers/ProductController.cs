using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public ViewResult List() => View(_productRepository.Products);
    }
}