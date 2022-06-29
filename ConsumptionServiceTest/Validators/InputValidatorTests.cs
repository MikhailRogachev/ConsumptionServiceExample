using ConsumptionService.Settings;
using ConsumptionService.Validators;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace ConsumptionServiceTest.Validators
{
    public class InputValidatorTests
    {
        /// <summary>
        /// This test checks validation results of input value and compare
        /// it to expectation.
        /// </summary>
        /// <param name="input">The input consumption value.</param>
        /// <param name="maxLimit">The max limit</param>
        /// <param name="expected">The result expected.</param>
        [Theory]
        [InlineData(-1, 10000, false)]
        [InlineData(10000, 100, false)]
        [InlineData(1000, 1000000, true)]
        [InlineData(0.1, 1000000, true)]
        public void Validator_Result(decimal input, decimal maxLimit, bool expected)
        {
            // arrange
            var options = new Mock<IOptionsSnapshot<AppSettings>>();
            options.SetupGet(p => p.Value).Returns(
                new AppSettings
                {
                    MaxConsumptionValue = maxLimit
                });

            var validator = new InputValidator(options.Object);

            // act
            var validity = validator.Validate(input);

            // assert
            validity.IsValid.Should().Be(expected);
        }
    }
}
