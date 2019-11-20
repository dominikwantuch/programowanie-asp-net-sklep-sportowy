using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Repositories;

namespace SportShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IProductRepository _productRepository;

        public AdminController(IManufacturerRepository manufacturerRepository, IProductRepository productRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            this.ViewBag.CurrentPage = "Index";
            return View(_productRepository.Products);
        }

        public IActionResult Edit(int id)
        {
            return View(_productRepository.Products.FirstOrDefault(x => x.ProductId == id));
        }

        public IActionResult Create()
        {
            this.ViewBag.CurrentPage = "Create";
            return View("Edit", new Product());
        }

        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid && product != null)
            {
                var result = _productRepository.SaveProduct(product);
            }

            return View("Index", _productRepository.Products);
        }

        public IActionResult Delete(int id)
        {
            var result = _productRepository.DeleteProduct(id);

            return View("Index", _productRepository.Products);
        }
    }
}