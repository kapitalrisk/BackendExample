using InMemoryDatabase;
using Stock.Models.Entities;
using Stock.Repositories;

namespace Stock.Services.HostedServices
{
    public class LowStockAlertingHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<LowStockAlertingHostedService> _logger;
        private readonly IProductRepository _productRepository;

        private Timer? _timer = null;
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private bool _disposed;

        public LowStockAlertingHostedService(ILogger<LowStockAlertingHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _productRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IProductRepository>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(LowStockAlertingHostedService)} starting");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10)); // value to put in config obviously
        }

        private async void DoWork(object? state)
        {
            try
            {
                // We cannot use lock() or Monitor.Enter() methods to ensure only one instance of this method is running (because of async calls inside it) we use semaphore signals as a replacement
                await semaphoreSlim.WaitAsync();

                // Get low stock products
                var productStockAvailableColumn = typeof(ProductEntity).GetColumnName(nameof(ProductEntity.StockAvailable));
                var lowStockProducts = await _productRepository.WhereAsync($"{productStockAvailableColumn} < @minimalProductStockParam", new { minimalProductStockParam = 10 }); // obviously the alerting threshold should be part of configuration as well

                foreach (var lowStockProduct in lowStockProducts)
                    _logger.LogWarning($"Product {lowStockProduct.Name} is in low quantities ({lowStockProduct.StockAvailable})"); // Here you would typically provide an email or chat alert message call
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occur trying to check stock state for alerting");
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _timer?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
