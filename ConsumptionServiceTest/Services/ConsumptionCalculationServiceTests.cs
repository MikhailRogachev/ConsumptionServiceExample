using ConsumptionService.Models;
using ConsumptionService.Services;
using ConsumptionServiceTest.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConsumptionServiceTest.Services
{
    public class ConsumptionCalculationServiceTests
    {
        /// <summary>
        /// This test received the annual cost collection calculated by the service and compare
        /// result to expected segregated by product.
        /// </summary>
        /// <param name="consumption">The consumption value.</param>
        /// <param name="expected">Expected value segregated by product.</param>
        [Theory(DisplayName = "Consumption Service Calculation compare to expected values."),
            MemberData(nameof(GetConsumptions))]
        public async Task CunsumptionCalculation_Result(decimal consumption, 
            Dictionary<string, decimal> expected)
        {
            // arrange
            var repository = DataHelper.Repository;
            var service = new ConsumptionCalculationService(repository,
                new Mock<ILogger<ConsumptionCalculationService>>().Object);

            // act
            var result = await service.GetAnnualConsumptionAsync(consumption);

            // assert
            result.Should().BeAssignableTo<IEnumerable<ConsumptionCostModel>>().And.HaveCount(2);

            foreach(var item in expected)
            {
                var expectedAmount = item.Value;
                var realAmount = result.FirstOrDefault(p => p.TariffType == item.Key);

                realAmount?.AnualCost.Should().Be(expectedAmount);
            }

            result.First().AnualCost.Should().BeLessThan(result.Last().AnualCost);
        }

        public static TheoryData<decimal, Dictionary<string, decimal>> GetConsumptions()
        {
            return new TheoryData<decimal, Dictionary<string, decimal>>
            {
                { 
                    3500, 
                    new Dictionary<string, decimal>
                    {
                        {"Packaged", 800 },
                        {"Basic", 830 }
                    }
                },
                {
                    4500,
                    new Dictionary<string, decimal>
                    {
                        {"Packaged", 950 },
                        {"Basic", 1050 }
                    }
                },
                {
                    6000,
                    new Dictionary<string, decimal>
                    {
                        {"Packaged", 1400 },
                        {"Basic", 1380 }
                    }
                }
            };
        }
    }
}
