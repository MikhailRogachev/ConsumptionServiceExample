namespace ConsumptionService.Models
{
    /// <summary>
    /// This class contains of properties for the products 
    /// to calculate an annual cost.
    /// </summary>
    public abstract class Product
    {
        /// <summary>
        /// Get; Set; Tariff type of the product.
        /// </summary>
        public string TariffType { get; set; } = string.Empty;

        /// <summary>
        /// Get; Set; The product's Name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Get; Set; The product's description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        public abstract decimal GetAmount(decimal value);
    }
}
