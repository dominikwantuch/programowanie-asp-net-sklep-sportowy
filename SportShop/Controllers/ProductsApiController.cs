using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Repositories;

namespace SportShop.Controllers
{
    /// <summary>
    /// Controller responsible for product entity management.
    /// </summary>
    [Route("api/products")]
    public class ProductsApiController : Controller
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductsApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Fetches list of available products.
        /// </summary>
        [HttpGet]
        public IActionResult GetProducts([FromQuery] string category = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(category))
                    return Ok(_productRepository.Products.Where(x => x.Category == category));
                else
                    return Ok(_productRepository.Products.ToList());
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unexpected internal server error.");
            }
        }

        /// <summary>
        /// Fetches one product by given ID.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var prod = _productRepository.Products.FirstOrDefault(x => x.ProductId == id);

                if (prod == null)
                    return NotFound("Product with given ID does not exist!");
                else
                    return Ok(prod);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unexpected internal server error.");
            }
        }

        /// <summary>
        /// Creates new product in database.
        /// </summary>
        /// <param name="model"><see cref="Product"/></param>
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product model)
        {
            try
            {
                var prod = _productRepository.Products.FirstOrDefault(x => x.ProductId == model.ProductId);

                if (prod != null)
                    return Conflict("Product with given ID exists already.");

                var res = _productRepository.SaveProduct(model);

                if (res)
                    return CreatedAtAction(nameof(CreateProduct), new {id = model.ProductId}, model);
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unexpected internal server error.");
            }
        }

        /// <summary>
        /// Updates existing product.
        /// </summary>
        /// <param name="model"><see cref="Product"/></param>
        [HttpPut]
        public IActionResult EditProduct([FromBody] Product model)
        {
            try
            {
                var prod = _productRepository.Products.FirstOrDefault(x => x.ProductId == model.ProductId);

                if (prod == null)
                    return NotFound("Product with given ID does not exist!");

                var res = _productRepository.SaveProduct(model);

                if (res)
                    return Ok(model);
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unexpected internal server error.");
            }
        }

        /// <summary>
        /// Deletes specified product.
        /// </summary>
        /// <param name="id"> ID of the product. </param>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var prod = _productRepository.Products.FirstOrDefault(x => x.ProductId == id);

                if (prod == null)
                    return NotFound("Product with given ID does not exist!");

                var res = _productRepository.DeleteProduct(id);

                if (res)
                    return Ok();
                else
                    throw new Exception($"Unexpected internal server occured while trying to delete product with ID: {id}");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unexpected internal server error.");
            }
        }
    }
}