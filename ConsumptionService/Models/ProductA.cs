namespace ConsumptionService.Models
{
    /// <summary>
    /// This class specifies properties for the calculation of the
    /// annual cost for the product A.
    /// </summary>
    public class ProductA: Product
    {
        /// <summary>
        /// Get; Set; The Base price for the <see cref="BasePeriod">Base Period</see>.
        /// </summary>
        public decimal BasePrice { get; set; }

        /// <summary>
        /// Get; Set; The base period.
        /// </summary>
        public int BasePeriod { get; set; }

        /// <summary>
        /// Get; Set; The coefficient to calculate the cost for the value exceed base.
        /// </summary>
        public decimal ExtraConsumptionCost { get; set; }
    }
}
