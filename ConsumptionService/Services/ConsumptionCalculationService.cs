using ConsumptionService.Extensions;
using ConsumptionService.Models;
using ConsumptionService.Repository;

namespace ConsumptionService.Services
{
    /// <summary>
    /// This class contains of functions and methods for the annuql cost calculation service
    /// based on products.
    /// </summary>
    public class ConsumptionCalculationService : IConsumptionCalculationService
    {
        private readonly IRepository _productRepository;
        private readonly ILogger<ConsumptionCalculationService> _logger;

        public ConsumptionCalculationService(
            IRepository repository,
            ILogger<ConsumptionCalculationService> logger
            )
        {
            _productRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// This method returns the <see cref="IEnumerable{ConsumptionCostModel}">result</see> of the
        /// annual cost calculation based on products seleted from <see cref="IRepository">Repository</see>.
        /// </summary>
        /// <param name="consumptionValue">The annual consumption value (kWh/year)</param>
        /// <returns>
        /// The <see cref="IEnumerable{ConsumptionCostModel}">result</see> 
        /// of the annual cost calculation.
        /// </returns>
        public async Task<IEnumerable<ConsumptionCostModel>> GetAnnualConsumptionAsync(decimal consumptionValue)
        {
            var products = await GetProductDataAsync();

            if(!products.IsAny())
            {
                _logger.LogWarning("There is no any products loaded.");
                return Array.Empty<ConsumptionCostModel>();
            }

            return Calculate(products, consumptionValue);
        }

        private async Task<IEnumerable<Product>> GetProductDataAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        private IEnumerable<ConsumptionCostModel> Calculate(IEnumerable<Product> products, decimal value)
        {
            var consumptions = new List<ConsumptionCostModel>();

            foreach(var product in products)
            {
                consumptions.Add(new ConsumptionCostModel
                {
                    Description = product.Description,
                    Name = product.Name,
                    TariffType = product.TariffType,
                    AnnualCost = product.GetAmount(value)
                });
            }

            if (!consumptions.IsAny())
            {
                _logger.LogWarning("There is no any annual costs calculated. {name}",
                    nameof(ConsumptionCalculationService.Calculate));
                return consumptions;
            }

            return consumptions.OrderBy(p => p.AnnualCost);
        }
    }
}
