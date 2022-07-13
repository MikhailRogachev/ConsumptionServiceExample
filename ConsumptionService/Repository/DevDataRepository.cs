using ConsumptionService.Models;

namespace ConsumptionService.Repository
{
    /// <summary>
    /// This repository contains of functions and methods to define the products collection
    /// in Development Mode.
    /// </summary>
    public class DevDataRepository : IRepository
    {
        /// <summary>
        /// This function retrieves the product collection from the repositoty.
        /// </summary>
        /// <returns>
        /// <see cref="IEnumerable{Product}">Products Collection</see>.
        /// </returns>
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await Task.Run(() =>
            {
                return new List<Product>()
                {
                    GetProductA(),
                    GetProductB()
                };
            });
        }

        private static ProductA GetProductA()
        {
            return new ProductA
            {
                TariffType = "Basic",
                Name = "basic electricity tariff",
                Description = "Calculation model: base costs per month 5 € + consumption costs 22 cent/kWh",
                BasePrice = 5M,
                BasePeriod = 12,
                ExtraConsumptionCost = 0.22M
            };
        }

        private static ProductB GetProductB()
        {
            return new ProductB
            {
                TariffType = "Packaged",
                Name = "Packaged tariff",
                Description = "Calculation model: 800 € for up to 4000 kWh/year and above 4000 kWh/year additionally 30 cent/kWh",
                AnnualConsumption = 4000M,
                AnnualCost = 800M,
                ExtraConsumptionCost = 0.3M
            };
        }
    }
}