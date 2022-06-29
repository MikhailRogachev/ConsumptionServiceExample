namespace ConsumptionService.Models
{
    /// <summary>
    /// This class specifies properties for the calculation of the
    /// annual cost for the product B.
    /// </summary>
    public class ProductB: Product
    {
        /// <summary>
        /// Get; Set; The annual consumption limit.
        /// </summary>
        public decimal AnnualConsumption { get; set; }

        /// <summary>
        /// Get; Set; The fixed price for the <see cref="AnnualConsumption">limit</see>
        /// </summary>
        public decimal AnnualCost { get; set; }

        /// <summary>
        /// Get; Set; The coefficient to calculate the cost for the value exceed base.
        /// </summary>
        public decimal ExtraConsumptionCost { get; set; }
    }
}
