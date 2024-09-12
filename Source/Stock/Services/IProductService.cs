using Stock.Models.Ressources;

namespace Stock.Services
{
    public interface IProductService
    {
        Task<ProductRessource?> GetByName(string name);
        Task<IEnumerable<ProductRessource>> GetAll();
        Task<int> Import(IEnumerable<ImportProductRessource> productsToImport);
        Task<int> ImportButFaster(IEnumerable<ImportProductRessource> productsToImport);
    }
}
