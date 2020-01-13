using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;

namespace SportShop.Controllers
{
    /// <summary>
    /// Controller responsible for handling Manufacturer related requests
    /// </summary>
    [Authorize]
    [Route("admin/manufacturers")]
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
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CurrentPage = "Manufacturer";
            return View(_manufacturerRepository.Manufacturers?.ToList());
        }

        /// <summary>
        /// Redirect user to edit view of manufacturer with given id.
        /// </summary>
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var manufacturer = _manufacturerRepository.Manufacturers.FirstOrDefault(x => x.Id == id);
            
            if (manufacturer == null)
            {
                ViewData["Message"] = "Manufacturer with given id does not exist!";
                return View("Index");
            }
            
            return View("Edit",manufacturer);
        }

        /// <summary>
        /// Redirects to create view.
        /// </summary>
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.CurrentPage = "Create";
            return View("Edit", new Manufacturer());
        }
        
        /// <summary>
        /// Saves or edits given manufacturer. 
        /// </summary>
        [HttpPost("save")]
        public IActionResult Save(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid || manufacturer == null)
            {
                ViewData["Message"] = "Given data is not valid!";
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
                    ViewData["Message"] = "Manufacturer could not be added to the database.";
                }
            }

            return View("Index", _manufacturerRepository.Manufacturers);
        }
        
        /// <summary>
        /// Deletes manufacturer with given id.
        /// </summary>
        [HttpGet("delete")]
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
                ViewData["Message"] = "An unexpected error has occured while trying to delete product.";
            }

            return View("Index", _manufacturerRepository.Manufacturers);
        }
    }
}