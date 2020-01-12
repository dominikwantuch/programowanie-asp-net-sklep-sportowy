using System;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Repositories;

namespace SportShop.Controllers
{
    /// <summary>
    /// Controller responsible for handling Manufacturer related requests
    /// </summary>
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("admin/manufacturers")]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="manufacturerRepository"></param>
        public ManufacturerController(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }
        
        /// <summary>
        /// Main view with manufacturers list.
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Index()
        {
            ViewBag.CurrentPage = "Manufacturer";
            return View(_manufacturerRepository.Manufacturers?.ToList());
        }

        /// <summary>
        /// Redirect user to edit view of manufacturer with given id.
        /// </summary>
        [Microsoft.AspNetCore.Mvc.HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var manufacturer = _manufacturerRepository.Manufacturers.FirstOrDefault(x => x.Id == id);
            
            if (manufacturer == null)
            {
                TempData["Message"] = "Manufacturer with given id does not exist!";
                return View("Index");
            }
            
            return View(manufacturer);
        }

        /// <summary>
        /// Redirects to create view.
        /// </summary>
        [Microsoft.AspNetCore.Mvc.HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.CurrentPage = "Create";
            return View("Edit", new Manufacturer());
        }
        
        /// <summary>
        /// Saves or edits given manufacturer. 
        /// </summary>
        [Microsoft.AspNetCore.Mvc.HttpPost("save")]
        public IActionResult Save(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid || manufacturer == null)
            {
                TempData["Message"] = "Given data is not valid!";
                return View("Index", _manufacturerRepository.Manufacturers);
            }
            else
            {
                try
                {
                    var result = _manufacturerRepository.SaveManufacturer(manufacturer);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    TempData["Message"] = "Manufacturer could not be added to the database.";
                }
            }

            return RedirectToAction("Index", _manufacturerRepository.Manufacturers);
        }
        
        /// <summary>
        /// Deletes manufacturer with given id.
        /// </summary>
        [Microsoft.AspNetCore.Mvc.HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine(id);
            try
            {
                var result = _manufacturerRepository.DeleteManufacturer(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                TempData["Message"] = "An unexpected error has occured while trying to delete product.";
            }

            return RedirectToAction("Index", _manufacturerRepository.Manufacturers);
        }
    }
}