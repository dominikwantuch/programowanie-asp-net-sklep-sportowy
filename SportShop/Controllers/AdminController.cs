using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
using SportShop.Repositories;

namespace SportShop.Controllers
{
    /// <summary>
    /// Controller which manages products on admin page.
    /// </summary>
    [Authorize]
    [Route("admin/products")]
    public class AdminController : Controller
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AdminController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        /// <summary>
        /// Returns products view, allows to filter by product category.
        /// </summary>
        [HttpGet("{name?}")]
        public IActionResult Index(string name)
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

        /// <summary>
        /// Returns edit view of product with given id. 
        /// </summary>
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var product = _productRepository.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                ViewData["Message"] = "Product with given id does not exist!";
                return View("Index");
            }

            return View(product);
        }

        /// <summary>
        /// Returns view with new product form. 
        /// </summary>
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.CurrentPage = "Create";
            return View("Edit", new Product());
        }

        /// <summary>
        /// Saves or updates given product and redirects to index page.
        /// </summary>
        [HttpPost("save")]
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

        /// <summary>
        /// Removes product with given id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delete")]
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

            return RedirectToAction("Index", _productRepository.Products);
        }
    }
}