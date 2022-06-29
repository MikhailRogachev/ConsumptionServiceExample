using ConsumptionService.Models;

namespace ConsumptionService.Repository
{
    /// <summary>
    /// This interface specifies functions for the repositories
    /// contained products collection.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// This function retrieves the product collection from the repositoty.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{Product}">Products Collection</see>.
        /// </returns>
        IEnumerable<Product> GetProducts();
    }
}
