using Catalog.Models.Ressources;
using Catalog.Repositories;

namespace Catalog.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<IEnumerable<CatalogEntryRessource>> GetCatalog()
        {
            return (await _catalogRepository.FindAsync()).Select(x => new CatalogEntryRessource(x));
        }
    }
}
