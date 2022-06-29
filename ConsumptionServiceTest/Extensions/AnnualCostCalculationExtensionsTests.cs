using ConsumptionService.Extensions;
using ConsumptionService.Models;
using ConsumptionServiceTest.Helpers;
using FluentAssertions;

namespace ConsumptionServiceTest.Extensions
{
    public class AnnualCostCalculationExtensionsTests
    {

        /// <summary>
        /// This test compares the anual cost calculated and expected
        /// for ProductB.
        /// </summary>
        /// <param name="value">The annual coas clculated.</param>
        /// <param name="expected">The annual cost expected.</param>
        [Theory(DisplayName = "Annual Cost Calculation Test for ProductB")]
        [InlineData(3500, 800)]
        [InlineData(4500, 950)]
        [InlineData(6000, 1400)]
        public void AnnualCostForProductB_Result(decimal value, decimal expected)
        {
            // arrange
            var productB = DataHelper.GetProduct<ProductB>();

            productB.Should().NotBeNull();

            // act
            var result = productB?.GetAnnualCost(value);

            // assert
            result.Should().Be(expected);
        }

        /// <summary>
        /// This test compares the anual cost calculated and expected
        /// for ProductA.
        /// </summary>
        /// <param name="value">The annual coas clculated.</param>
        /// <param name="expected">The annual cost expected.</param>
        [Theory(DisplayName = "Annual Cost Calculation Test for ProductA")]
        [InlineData(3500, 830)]
        [InlineData(4500, 1050)]
        [InlineData(6000, 1380)]
        public void AnnualCostForProductA_Result(decimal value, decimal expected)
        {
            // arrange
            var productA = DataHelper.GetProduct<ProductA>();

            productA.Should().NotBeNull();

            // act
            var result = productA?.GetAnnualCost(value);

            // assert
            result.Should().Be(expected);
        }

    }
}
