namespace ConsumptionService.Dto
{
    /// <summary>
    /// This class specifies properties for the output collection.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Get; Set; The product's tariff name.
        /// </summary>
        public string TariffName { get; set; } = string.Empty;

        /// <summary>
        /// Get; set; The annual cost.
        /// </summary>
        public decimal AnnualCost { get; set; }
    }
}
