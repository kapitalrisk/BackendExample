using Catalog.Models.Ressources;
using Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet(Name = "GetCatalog")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CatalogEntryRessource>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> GetCatalog()
        {
            try
            {
                var catalogRes = await _catalogService.GetCatalog();

                return Ok(catalogRes);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}