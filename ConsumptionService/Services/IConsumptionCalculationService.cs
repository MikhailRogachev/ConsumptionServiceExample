using ConsumptionService.Models;

namespace ConsumptionService.Services
{
    /// <summary>
    /// This interface specifies functions and methods for the 
    /// consumption calculation services
    /// </summary>
    public interface IConsumptionCalculationService
    {
        /// <summary>
        /// This method returns the <see cref="IEnumerable{ConsumptionCostModel}">result</see> of the
        /// annual cost calculation.
        /// </summary>
        /// <param name="consumptionValue">The annual consumption value (kWh/year)</param>
        /// <returns>
        /// The <see cref="IEnumerable{ConsumptionCostModel}">result</see> 
        /// of the annual cost calculation.
        /// </returns>
        IEnumerable<ConsumptionCostModel> GetAnnualConsumption(decimal consumptionValue);
    }
}
