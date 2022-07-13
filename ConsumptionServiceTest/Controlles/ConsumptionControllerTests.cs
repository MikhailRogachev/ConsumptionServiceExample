using AutoFixture.Xunit2;
using AutoMapper;
using ConsumptionService.Automapper;
using ConsumptionService.Controllers;
using ConsumptionService.Dto;
using ConsumptionService.Models;
using ConsumptionService.Services;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConsumptionServiceTest.Controlles
{
    public class ConsumptionControllerTests
    {
        private readonly Mock<ILogger<ConsumptionController>> _logger;
        private readonly Mock<IConsumptionCalculationService> _service;
        private readonly Mock<IValidator<decimal>> _validator;
        private readonly IMapper _mapper;

        private ConsumptionController Sut => new ConsumptionController(_service.Object,
            _mapper,
            _validator.Object,
            _logger.Object);

        public ConsumptionControllerTests()
        {
            _logger = new Mock<ILogger<ConsumptionController>>();
            _service = new Mock<IConsumptionCalculationService>();
            _validator = new Mock<IValidator<decimal>>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ConsumptionModelProfile>()).CreateMapper();
        }

        /// <summary>
        /// This test checks the method response if the input value is invalid.
        /// Expecting - 400.
        /// </summary>
        [Fact(DisplayName = "Call Controller with an invalid input value - returns 400.")]
        public async Task GetAnnualCost_Result400()
        {
            // arrange
            var errorMessage = "Test Message"; 
            _validator.Setup(x => x.Validate(It.IsAny<decimal>()))
                .Returns(new ValidationResult
                {
                    Errors = new List<ValidationFailure>
                    {
                        {new ValidationFailure{ErrorMessage = errorMessage} }
                    }
                });

            // act
            var response = await Sut.GetAnnualCost(-100);

            // assert
            response.Should().BeAssignableTo<BadRequestObjectResult>();
            response.As<BadRequestObjectResult>().Value?.ToString().Should().StartWith(errorMessage);
        }

        /// <summary>
        /// This test checks the method response in case no any data calculated.
        /// Expecting - 204.
        /// </summary>
        [Fact(DisplayName = "Call Controller - no products found - returns 204.")]
        public async Task GetAnnualCost_Result204()
        {
            // arrange
            _service.Setup(p => p.GetAnnualConsumptionAsync(It.IsAny<decimal>()))
                .ReturnsAsync(Array.Empty<ConsumptionCostModel>());

            _validator.Setup(x => x.Validate(It.IsAny<decimal>()))
                .Returns(new ValidationResult());

            // act
            var response = await Sut.GetAnnualCost(100);

            // assert
            response.Should().BeAssignableTo<NoContentResult>();
        }

        /// <summary>
        /// This test checks the method response in case the calculation successfull.
        /// Expecting - 200.
        /// </summary>
        /// <param name="model">Fake models is returned by calculation service.</param>
        [Theory(DisplayName = "Call Controller - returns 200.")]
        [AutoData]
        public async Task GetAnnualCost_Result200(IEnumerable<ConsumptionCostModel> model)
        {
            // arrange
            _service.Setup(p => p.GetAnnualConsumptionAsync(It.IsAny<decimal>()))
               .ReturnsAsync(model);

            _validator.Setup(x => x.Validate(It.IsAny<decimal>()))
                .Returns(new ValidationResult());

            var response = await Sut.GetAnnualCost(100);

            // assert
            response.Should().BeAssignableTo<OkObjectResult>();

            var result = response.As<OkObjectResult>().Value.As<IEnumerable<ResponseDto>>();

            result.Should().HaveCount(model.Count());
        }
    }
}
