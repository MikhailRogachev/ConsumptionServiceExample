namespace ConsumptionService.Models
{
    /// <summary>
    /// This class specifies properties for the collection with 
    /// annual cost values calculated.
    /// </summary>
    public class ConsumptionCostModel
    {
        /// <summary>
        /// Get; Set; The tariff type of the product.
        /// </summary>
        public string TariffType { get; set; } = string.Empty;

        /// <summary>
        /// Get; Set; The name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Get; Set; The product's description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Get; Set; The annual cost calculated base on <see cref="Name">Product</see>.
        /// </summary>
        public decimal AnualCost { get; set; }
    }
}
