using Asp.Versioning;
using CrudChallenge.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrudChallenge.API.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class ProductControllerV2 : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private IProductRepository _productRepository;

        public ProductControllerV2(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("products/{productId}", Name = "GetProducts")]
        public async Task<IActionResult> Get([FromRoute] string productId)
        {
            return Ok("Get product by id v2 not implemented");
        }
    }
}
