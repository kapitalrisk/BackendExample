using Catalog.Models.Entities;
using InMemoryDatabase;
using InMemoryDatabase.UseCasePattern;

namespace Catalog.Repositories
{
    public class CatalogRepository : BaseRepository<CatalogEntryEntity>, ICatalogRepository
    {
        public CatalogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
