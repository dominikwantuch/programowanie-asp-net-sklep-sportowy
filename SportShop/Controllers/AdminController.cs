using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Repositories;

namespace SportShop.Controllers
{
    [Authorize]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IProductRepository _productRepository;

        public AdminController(IManufacturerRepository manufacturerRepository, IProductRepository productRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _productRepository = productRepository;
        }
        
        [Microsoft.AspNetCore.Mvc.HttpGet("index")]
        public IActionResult Index()
        {
            this.ViewBag.CurrentPage = "Index";
            return View(_productRepository.Products);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var product = _productRepository.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                TempData["Message"] = "Product with given id does not exist!";
            }

            return View(product);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("create")]
        public IActionResult Create()
        {
            this.ViewBag.CurrentPage = "Create";
            return View("Edit", new Product());
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("save")]
        public IActionResult Save(Product product)
        {
            if (!ModelState.IsValid || product == null)
            {
                TempData["Message"] = "Given data is not valid!";
                return View("Index", _productRepository.Products);
            }
            else
            {
                try
                {
                    var result = _productRepository.SaveProduct(product);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    TempData["Message"] = "Product could not be added to the database.";
                }
            }

            return RedirectToAction("Index", _productRepository.Products);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine(id);
            try
            {
                var result = _productRepository.DeleteProduct(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                TempData["Message"] = "An unexpected error has occured while trying to delete product.";
            }

            return RedirectToAction("Index", _productRepository.Products);
        }
    }
}