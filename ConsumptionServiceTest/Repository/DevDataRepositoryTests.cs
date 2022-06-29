using ConsumptionService.Models;
using ConsumptionService.Repository;
using FluentAssertions;

namespace ConsumptionServiceTest.Repository
{
    public class DevDataRepositoryTests
    {
        /// <summary>
        /// This test checks the products collection returned by development repository.
        /// </summary>
        [Fact(DisplayName = "Expecting two-items collection returned by the repository.")]
        public void GetProduct_ReturnCollection()
        {
            // arrange
            var repository = new DevDataRepository();

            // act
            var result = repository.GetProducts();

            // assert
            result.Should().HaveCount(2);
            result.OfType<ProductB>().Should().HaveCount(1);
            result.OfType<ProductB>().Should().HaveCount(1);
        }
    }
}
