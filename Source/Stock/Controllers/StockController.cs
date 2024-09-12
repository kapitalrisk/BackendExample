using Microsoft.AspNetCore.Mvc;
using Stock.Models.Ressources;
using Stock.Services;
using System.ComponentModel.DataAnnotations;

namespace Stock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IProductService _productService;

        public StockController(ILogger<StockController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("{productName}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductRessource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> GetProduct(string productName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(productName))
                    return BadRequest();

                var productRes = await _productService.GetByName(productName);

                return productRes != null ? Ok(productRes) : NotFound(productName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occur trying to retrieve product with name {productName}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("all", Name = "GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductRessource>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        [HttpPost("import", Name  = "ImportProducts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> ImportProducts([FromBody][Required] IEnumerable<ImportProductRessource> productsToImport)
        {
            try
            {
                var result = await _productService.Import(productsToImport);

                return Ok(result);
            }
            catch (Exception e) 
            {
                _logger.LogError(e, $"An error occur trying to import products");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}