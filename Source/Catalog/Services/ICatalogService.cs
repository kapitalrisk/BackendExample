using Catalog.Models.Ressources;

namespace Catalog.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogEntryRessource>> GetCatalog();
    }
}
