using CrudChallenge.Model;
using CrudChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrudChallenge.API.Controllers
{
    [ApiController]
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

            var product = await _productRepository.GetProductById(productId);

            if (product == null) return NotFound("Product not found");

            return Ok(product);
        }

        [HttpPost("products", Name = "CreateProduct")]
        public async Task<IActionResult> Post([FromBody] ProductDTO product)
        {
            var error = ValidateProduct(product);

            if(!string.IsNullOrEmpty(error)) return BadRequest(error);

            _logger.LogDebug("Creating product: {0}", JsonConvert.SerializeObject(product));

            var newProduct = await _productRepository.InsertProduct(product);

            return Created("products/" + newProduct.Id, "Product Created");
        }

        [HttpPut("products", Name = "UpdateProduct")]
        public async Task<IActionResult> Put([FromBody] ProductDTO product)
        {
            var error = ValidateProduct(product);

            if (!string.IsNullOrEmpty(error)) return BadRequest(error);

            _logger.LogDebug("Updating product: {0}", JsonConvert.SerializeObject(product));

            await _productRepository.UpdateProduct(product);

            return Ok("Product Updated");
        }

        [HttpDelete("products/{productId}", Name = "DeleteProduct")]
        public async Task<IActionResult> Delete([FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(productId)) return BadRequest("ProductId must be set");

            _logger.LogDebug("Deleting product: {0}", productId);

            await _productRepository.DeleteProduct(productId);

            return Ok("Product Deleted");
        }

        private static string ValidateProduct(ProductDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.Name)) return "Product name cannot be empty";

            if (string.IsNullOrWhiteSpace(product.Code)) return "Product code cannot be empty";

            return string.Empty;
        }
    }
}
