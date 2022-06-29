using FluentValidation.Results;
using System.Text;

namespace ConsumptionService.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// This function checks is the collection is not null and contains atleast 
        /// one item.
        /// </summary>
        /// <typeparam name="T">The type of the collection's entity.</typeparam>
        /// <param name="source">The collection to check items existance.</param>
        /// <returns>
        ///     <c>true</c> if the collection is not null and contains items.
        ///     Otherwise the function returns <c>false</c>. 
        /// </returns>
        public static bool IsAny<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        /// <summary>
        /// This function extracts messages from the 
        /// <see cref="IEnumerable{ValidationFailure}">Validation Failure</see> collection.
        /// </summary>
        /// <param name="failure">The <see cref="IEnumerable{ValidationFailure}">Validation Failure</see> collection</param>
        /// <returns>
        /// The <see cref="string">Error message</see>.
        /// </returns>
        public static string GetValidatorMessage(this List<ValidationFailure> failure)
        {
            StringBuilder builder = new StringBuilder();

            foreach(var failureItem in failure)
            {
                builder.AppendLine(failureItem.ErrorMessage);
            }

            return builder.ToString();
        }
    }
}
