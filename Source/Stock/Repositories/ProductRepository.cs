using InMemoryDatabase;
using InMemoryDatabase.UseCasePattern;
using Stock.Models.Entities;

namespace Stock.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<ProductEntity?> GetByName(string name)
        {
            var productNameColumn = _entityType.GetColumnName(nameof(ProductEntity.Name));
            var result = await this.WhereAsync($"{productNameColumn} = @productNameParam", new { productNameParam = name });

            return result != null && result.Count() == 1 ? result.First() : null;
        }
    }
}
