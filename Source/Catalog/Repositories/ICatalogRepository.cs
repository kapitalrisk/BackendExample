using Catalog.Models.Entities;
using InMemoryDatabase;

namespace Catalog.Repositories
{
    public interface ICatalogRepository : IBaseRepository<CatalogEntryEntity>
    { }
}
