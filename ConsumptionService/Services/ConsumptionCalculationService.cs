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
        public IEnumerable<ConsumptionCostModel> GetAnnualConsumption(decimal consumptionValue)
        {
            var products = GetProductData();

            if(!products.IsAny())
            {
                _logger.LogWarning("There is no any products loaded.");
                return Array.Empty<ConsumptionCostModel>();
            }

            return Calculate(products, consumptionValue);
        }

        private IEnumerable<Product> GetProductData()
        {
            return _productRepository.GetProducts();
        }

        private IEnumerable<ConsumptionCostModel> Calculate(IEnumerable<Product> products, decimal value)
        {
            var consumptions = new List<ConsumptionCostModel>();

            foreach(var product in products)
            {
                var item = new ConsumptionCostModel
                {
                    Description = product.Description,
                    Name = product.Name,
                    TariffType = product.TariffType
                };

                switch(product)
                {
                    case ProductA productA:
                        item.AnualCost = productA.GetAnnualCost(value);                        
                        break;

                    case ProductB productB:
                        item.AnualCost = productB.GetAnnualCost(value);
                        break;

                    default:
                        break;
                }

                consumptions.Add(item);
            }

            if (!consumptions.IsAny())
            {
                _logger.LogWarning("There is no any annual costs calculated. {name}",
                    nameof(ConsumptionCalculationService.Calculate));
                return consumptions;
            }

            return consumptions.OrderBy(p => p.AnualCost);
        }
    }
}
