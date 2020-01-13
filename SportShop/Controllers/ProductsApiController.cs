using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;

namespace SportShop.Controllers
{
    /// <summary>
    /// Controller responsible for product entity management.
    /// </summary>
    [Route("api/products")]
    public class ProductsApiController : CustomControllerBase
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
                var productsResponse = _productRepository.GetAll(category);

                return StatusCode(200, productsResponse.Data.Select(ProductModel.ToModel));
            }
            catch (Exception e)
            {
                return InternalServerError();
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
                var getProductResponse = _productRepository.GetById(id);

                if (getProductResponse.isStatusCodeSuccess() && getProductResponse.Data != null)
                    return StatusCode(getProductResponse.StatusCode, ProductModel.ToModel(getProductResponse.Data));
                else
                    return StatusCode(getProductResponse.StatusCode);
                
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Creates new product in database.
        /// </summary>
        /// <param name="model"><see cref="Product"/></param>
        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductModel model)
        {
            try
            {
                var createResult = _productRepository.Create(model.ToEntity());

                if (createResult.isStatusCodeSuccess() && createResult.Data != null)
                    return StatusCode(createResult.StatusCode, ProductModel.ToModel(createResult.Data));
                else
                    return StatusCode(createResult.StatusCode);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Updates existing product.
        /// </summary>
        /// <param name="model"><see cref="UpdateProductModel"/></param>
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] UpdateProductModel model)
        {
            try
            {
                var getProductResponse = _productRepository.GetById(model.ProductId);

                if (!getProductResponse.isStatusCodeSuccess() || getProductResponse.Data == null)
                    return NotFound();

                var updateResponse = _productRepository.Update(model.ToEntity(getProductResponse.Data));

                if (updateResponse.isStatusCodeSuccess() && updateResponse.Data != null)
                    return StatusCode(updateResponse.StatusCode, ProductModel.ToModel(updateResponse.Data));
                else
                    return StatusCode(updateResponse.StatusCode);
            }
            catch (Exception e)
            {
                return InternalServerError();
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
                var deleteResponse = _productRepository.Delete(id);

                return StatusCode(deleteResponse.StatusCode);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }
    }
}