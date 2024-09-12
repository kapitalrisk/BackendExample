using AutoFixture;
using InMemoryDatabase;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Stock.Models.Entities;
using Stock.Repositories;
using Stock.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Stock.IntegrationTests
{
    [TestClass]
    public class StockServiceIntegrationTests
    {
        private readonly Fixture _fixture = new Fixture();

        private static IProductService _productService;
        private static IProductRepository _productRepository;

        private static IEnumerable<int> _productIdsToDelete = new List<int>();

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var stockServiceCollection = new ServiceCollection();
            stockServiceCollection.AddStockServices();
            var serviceProvider = stockServiceCollection.BuildServiceProvider();

            // Obviously never do that, its for the sake of simplicity of the exemple
            var dbGenerator = serviceProvider.GetService<IDatabaseGenerator>();
            dbGenerator.RegisterTableEntity(typeof(ProductEntity));
            dbGenerator.GenerateDatabaseAsync();

            _productService = serviceProvider.GetService<IProductService>();
            _productRepository = serviceProvider.GetService<IProductRepository>();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            foreach (var productIdToDelete in _productIdsToDelete)
                _productRepository.DeleteAsync(new ProductEntity { Id = productIdToDelete });
        }

        // Should normally be part of dedicated repository integration tests but here for the sake of simplicity
        // Strange naming is also here to ensure it executes first before other tests in here
        [TestMethod]
        public async Task A_ShouldInsertProduct()
        {
            // Arrange
            var dummyProduct1 = ArrangeDummyProductEntity();
            var dummyProduct2 = ArrangeDummyProductEntity();

            // Act
            await _productRepository.InsertAsync(dummyProduct1);
            await _productRepository.InsertAsync(dummyProduct2);

            // Assert
            dummyProduct1.ShouldNotBeNull();
            dummyProduct1.Id.ShouldBe(1);
            dummyProduct2.ShouldNotBeNull();
            dummyProduct2.Id.ShouldBe(2);
        }

        [TestMethod]
        public async Task ShouldGetByName()
        {
            // Arrange
            var dummyProduct = ArrangeDummyProductEntity();
            await _productRepository.InsertAsync(dummyProduct);

            // Act
            var result = await _productService.GetByName(dummyProduct.Name);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(dummyProduct.Id);
            result.Name.ShouldBe(dummyProduct.Name);
            result.Description.ShouldBe(dummyProduct.Description);
            result.StockAvailable.ShouldBe(dummyProduct.StockAvailable);
        }

        #region arrange
        private ProductEntity ArrangeDummyProductEntity(string? productName = null) => new ProductEntity
        {
            Name = productName ?? _fixture.Create<string>(),
            Description = _fixture.Create<string>(),
            StockAvailable = _fixture.Create<int>()
        };
        #endregion
    }
}