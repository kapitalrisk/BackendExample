using AutoFixture;
using Moq;
using Shouldly;
using Stock.Models.Entities;
using Stock.Repositories;
using Stock.Services;

namespace Stock.UnitTests
{
    [TestClass]
    public class StockServiceTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public async Task ShouldGetByName()
        {
            // Arrange
            var dummyProduct = ArrangeDummyProductEntity();
            var productRepoMock = ArrangeProductRepository(dummyProduct.Name, dummyProduct);
            var productService = ArrangeProductService(productRepoMock);

            // Act
            var result = await productService.GetByName(dummyProduct.Name);

            // Assert
            productRepoMock.VerifyAll();
            result.ShouldNotBeNull();
            result.Name.ShouldBe(dummyProduct.Name);
        }

        [TestMethod]
        public async Task ShouldGetByNameReturnNull()
        {
            // Arrange
            var productName = _fixture.Create<string>();
            var productRepoMock = ArrangeProductRepository(productNameSearched: productName);
            var productService = ArrangeProductService(productRepoMock);

            // Act
            var result = await productService.GetByName(productName);

            // Assert
            productRepoMock.VerifyAll();
            result.ShouldBeNull();
        }

        [TestMethod]
        public async Task ShouldImportProducts()
        {
            // Arrange
            // TIP : to mock a service called multiple times you can use mock.SetupSequence instead of mock.Setup
            // This will need to be called for all the possible sequential calls made

            // Act

            // Assert
        }

        #region arrangers
        private ProductService ArrangeProductService(Mock<IProductRepository> productRepositoryMock)
        {
            return new ProductService(productRepositoryMock.Object);
        }

        private ProductEntity ArrangeDummyProductEntity(string? productName = null) => new ProductEntity
        {
            Name = productName ?? _fixture.Create<string>(),
            Description = _fixture.Create<string>(),
            StockAvailable = _fixture.Create<int>()
        };

        private Mock<IProductRepository> ArrangeProductRepository(string? productNameSearched = null, ProductEntity? productSearchedReturned = null)
        {
            var mock = new Mock<IProductRepository>(MockBehavior.Strict);

            if (!String.IsNullOrWhiteSpace(productNameSearched))
                mock.Setup(x => x.GetByName(productNameSearched)).ReturnsAsync(productSearchedReturned);

            return mock;
        }
        #endregion
    }
}