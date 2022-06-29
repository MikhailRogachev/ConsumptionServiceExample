using ConsumptionService.Models;

namespace ConsumptionService.Extensions
{
    public static class AnnualCostCalculationExtensions
    {
        /// <summary>
        /// This function returns the result of the calculation annual cost
        /// for the <see cref="ProductB">Product B</see>.
        /// </summary>
        /// <param name="product">Base <see cref="ProductB">Product B</see> to calculate an annual cost.</param>
        /// <param name="value">The <see cref="decimal">Annual Consumption</see> value (kWh/year)</param>
        /// <returns>
        ///     The <see cref="decimal">Annual Cost value</see> Calculated based on <see cref="ProductB"/>. 
        /// </returns>
        public static decimal GetAnnualCost(this ProductB product, decimal value)
        {

            if(value <= product.AnnualConsumption)
            {
                return product.AnnualCost;
            }

            return product.AnnualCost + ((value - product.AnnualConsumption) * product.ExtraConsumptionCost);
        }

        /// <summary>
        /// This function returns the result of the calculation annual cost
        /// for the <see cref="ProductA">Product A</see>.
        /// </summary>
        /// <param name="product">Base <see cref="ProductA">Product B</see> to calculate an annual cost.</param>
        /// <param name="value">The <see cref="decimal">Annual Consumption</see> value (kWh/year)</param>
        /// <returns>
        ///     The <see cref="decimal">Annual Cost value</see> Calculated based on <see cref="ProductA"/>. 
        /// </returns>
        public static decimal GetAnnualCost(this ProductA product, decimal value)
        {
            return (product.BasePrice * product.BasePeriod) + (value * product.ExtraConsumptionCost);
        }
    }
}
