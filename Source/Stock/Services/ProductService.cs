using Dapper;
using Stock.Models.Entities;
using Stock.Models.Ressources;
using Stock.Repositories;

namespace Stock.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductRessource?> GetByName(string name)
        {
            var productEntity = await _productRepository.GetByName(name);

            return productEntity != null ? new ProductRessource(productEntity) : null;
        }

        public Task<IEnumerable<ProductRessource>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Import(IEnumerable<ImportProductRessource> productsToImport)
        {
            var cpt = 0;

            foreach (var productToImport in productsToImport)
            {
                var existingEntity = await _productRepository.GetByName(productToImport.Name);

                if (existingEntity != null) // product already exist and thus not imported
                    continue;

                var entityToCreate = new ProductEntity
                {
                    Name = productToImport.Name,
                    Description = productToImport.Description,
                    StockAvailable = productToImport.StockAvailable
                };
                await _productRepository.InsertAsync(entityToCreate);

                if (entityToCreate.Id != 0)
                    cpt++;
            }

            return cpt;
        }

        public Task<int> ImportButFaster(IEnumerable<ImportProductRessource> productsToImport)
        {
            throw new NotImplementedException();
        }
    }
}
