using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Persistence.Repositories;

namespace SportShop.Controllers
{
    /// <summary>
    /// Api controller for manufacturers.
    /// </summary>
    [Route("api/manufacturers")]
    public class ManufacturersApiController : CustomControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ManufacturersApiController(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        /// <summary>
        /// Return list of existing manufacturers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetManufacturers()
        {
            try
            {
                var manufacturers = _manufacturerRepository.Manufacturers.ToList();
                return StatusCode(200, manufacturers.Select(ManufacturerModel.ToModel));
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Returns manufacturer by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public IActionResult GetManufacturer(int id)
        {
            try
            {
                var manufacturerResponse = _manufacturerRepository.GetById(id);

                if (!manufacturerResponse.isStatusCodeSuccess())
                    return StatusCode(manufacturerResponse.StatusCode);
                else
                    return StatusCode(manufacturerResponse.StatusCode,
                        ManufacturerModel.ToModel(manufacturerResponse.Data));
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Creates manufacturer
        /// </summary>
        [HttpPost]
        public IActionResult CreateManufacturer([FromBody] CreateManufacturerModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createResponse = _manufacturerRepository.Create(model.ToEntity());
                    if (createResponse.StatusCode != 201)
                        return StatusCode(createResponse.StatusCode);
                    else
                        return StatusCode(createResponse.StatusCode, ManufacturerModel.ToModel(createResponse.Data));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Updates existing manufacturer.
        /// </summary>
        [HttpPut]
        public IActionResult UpdateManufacturer([FromBody] UpdateManufacturerModel model)
        {
            try
            {
                var getManufacturer = _manufacturerRepository.GetById(model.Id);

                if (!getManufacturer.isStatusCodeSuccess() || getManufacturer.Data == null)
                    return NotFound();

                var updateResponse = _manufacturerRepository.Update(model.ToEntity(getManufacturer.Data));

                if (updateResponse.isStatusCodeSuccess() && updateResponse.Data != null)
                    return StatusCode(updateResponse.StatusCode, ManufacturerModel.ToModel(updateResponse.Data));
                else
                    return StatusCode(updateResponse.StatusCode);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Removes manufacture with given ID from database.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteManufacturer(int id)
        {
            try
            {
                var deleteResponse = _manufacturerRepository.Delete(id);

                return StatusCode(deleteResponse.StatusCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected error has occured!");
            }
        }
    }
}