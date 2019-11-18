using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Repositories;

namespace SportShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult List(string name)
        {
            Console.WriteLine(name);
            if (string.IsNullOrWhiteSpace(name))
                return View(_productRepository.Products);
            else
                return View(_productRepository.Products.Where(x => x.Category == name));
        }
    }
}