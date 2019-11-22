using System;
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
            var product = _productRepository.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                TempData["Message"] = "Product with given id does not exist!";
            }

            return View(product);
        }

        public IActionResult Create()
        {
            this.ViewBag.CurrentPage = "Create";
            return View("Edit", new Product());
        }

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

            return View("Index", _productRepository.Products);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var result = _productRepository.DeleteProduct(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                TempData["Message"] = "An unexpected error has occured while trying to delete product.";
            }

            return View("Index", _productRepository.Products);
        }
    }
}