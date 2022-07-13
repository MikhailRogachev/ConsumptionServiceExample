using ConsumptionService.Repository;

namespace ConsumptionServiceTest.Helpers
{
    /// <summary>
    /// This helper provides data samples to perform tests.
    /// </summary>
    internal static class DataHelper
    {
        private static readonly IRepository repository = new DevDataRepository();

        internal static IRepository Repository => repository;
        internal static async Task<T?> GetProduct<T>() where T:class
        {
            var response = await repository.GetProductsAsync();
            var product = response.FirstOrDefault(p => p is T);

            return product == null ? null : product as T;
        }
    }
}
