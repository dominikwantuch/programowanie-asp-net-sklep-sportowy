using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportShop.Repositories;

namespace SportShop.Controllers
{
    /// <summary>
    /// Controller of products view.
    /// </summary>
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        /// <summary>
        /// Default application page. 
        /// </summary>
        [HttpGet]
        [HttpGet("/")]
        public ViewResult Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.CurrentPage = "Main";
                return View(_productRepository.Products);
            }
            else
            {
                ViewBag.CurrentPage = "Categories";
                return View(_productRepository.Products.Where(x => x.Category == name));
            }
        }
    }
}