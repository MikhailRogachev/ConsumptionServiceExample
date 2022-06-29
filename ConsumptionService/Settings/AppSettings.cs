namespace ConsumptionService.Settings
{
    /// <summary>
    /// This class specifies properties to use by the application.
    /// It's coming from appsettings.json
    /// </summary>
    public class AppSettings
    {
        public const string SettingsName = "AppSettings";

        /// <summary>
        /// Get; Set; Maximum cunsumption value for the input.
        /// </summary>
        public decimal MaxConsumptionValue { get; set; } = 1000000M;
    }
}
