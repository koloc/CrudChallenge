﻿using Asp.Versioning;
using CrudChallenge.Data.Repositories;
using CrudChallenge.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrudChallenge.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("products/{productId}", Name = "GetProducts")]
        public async Task<IActionResult> Get([FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(productId)) return BadRequest("ProductId must be set");

            _logger.LogDebug("Getting product: {0}", productId);

            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null) return NotFound("Product not found");

            return Ok(product);
        }

        [HttpPost("products", Name = "CreateProduct")]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest product)
        {
            var error = ValidateProduct(product);

            if(!string.IsNullOrEmpty(error)) return BadRequest(error);

            _logger.LogDebug("Creating product: {0}", JsonConvert.SerializeObject(product));

            var newProduct = await _productRepository.InsertProductAsync(product);

            return Created("products/" + newProduct.Id, "Product Created");
        }

        [HttpPut("products", Name = "UpdateProduct")]
        public async Task<IActionResult> Put([FromBody] UpdateProductRequest product)
        {
            var error = ValidateProduct(product);

            if (!string.IsNullOrEmpty(error)) return BadRequest(error);

            _logger.LogDebug("Updating product: {0}", JsonConvert.SerializeObject(product));

            await _productRepository.UpdateProductAsync(product);

            return Ok("Product Updated");
        }

        [HttpDelete("products/{productId}", Name = "DeleteProduct")]
        public async Task<IActionResult> Delete([FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(productId)) return BadRequest("ProductId must be set");

            _logger.LogDebug("Deleting product: {0}", productId);

            await _productRepository.DeleteProductAsync(productId);

            return Ok("Product Deleted");
        }

        [HttpGet("products/appexception", Name = "GetProductAppException")]
        public IActionResult GetAppException()
        {
            throw new ApplicationException("There was an exception!");
        }

        [HttpGet("products/exception", Name = "GetProductException")]
        public IActionResult GetException()
        {
            throw new Exception("There was an exception!");
        }

        private static string ValidateProduct(CreateProductRequest product)
        {
            if (string.IsNullOrWhiteSpace(product.Name)) return "Product name cannot be empty";

            if (string.IsNullOrWhiteSpace(product.Code)) return "Product code cannot be empty";

            return string.Empty;
        }
    }
}
