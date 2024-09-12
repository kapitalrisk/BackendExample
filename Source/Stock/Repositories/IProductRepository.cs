using InMemoryDatabase;
using Stock.Models.Entities;

namespace Stock.Repositories
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        Task<ProductEntity?> GetByName(string name);
    }
}
