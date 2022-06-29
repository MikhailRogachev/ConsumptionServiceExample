using ConsumptionService.Extensions;
using FluentAssertions;

namespace ConsumptionServiceTest.Extensions
{
    public class CollectionExtensionsTests
    {

        /// <summary>
        /// This method checks is collection empty and compare result
        /// to value expected.
        /// </summary>
        /// <param name="data">The collection to calculate.</param>
        /// <param name="expected">The expected value.</param>
        [Theory(DisplayName = "IsAny function test with different data."), MemberData(nameof(GetCollection))]
        public void IsAny_ReturnResult(IEnumerable<string> data, bool expected)
        {
            // act
            var result = data.IsAny();

            // assert
            result.Should().Be(expected);
        }

        public static TheoryData<IEnumerable<string>, bool> GetCollection()
        {
            return new TheoryData<IEnumerable<string>, bool>
            {
                {new List<string>(), false },
                {null, false },
                {new List<string>() {"Test 1", "Product 2"}, true }
            };
        }
    }
}
