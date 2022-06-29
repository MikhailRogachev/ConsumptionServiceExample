using AutoMapper;
using ConsumptionService.Dto;
using ConsumptionService.Extensions;
using ConsumptionService.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ConsumptionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumptionController : ControllerBase
    {
        private readonly ILogger<ConsumptionController> _logger;
        private readonly IConsumptionCalculationService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<decimal> _inputValidator;

        public ConsumptionController(
            IConsumptionCalculationService service,
            IMapper mapper,
            IValidator<decimal> inputValidator,
            ILogger<ConsumptionController> logger 
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _inputValidator = inputValidator ?? throw new ArgumentNullException(nameof(inputValidator));
        }

        /// <summary>
        /// This method returns the result of the annual cost calculation. 
        /// </summary>
        /// <param name="consumption">The value of the consumption (kWh/year)</param>
        /// <returns>
        /// 200 - Request successfull, the annual cost calculated and result returned.
        /// 204 - No any calculations performed.
        /// 400 - Invalid input value, the error message returned.
        /// </returns>
        [HttpGet("{consumption:decimal}")]
        public IActionResult GetAnnualCost(decimal consumption)
        {
            _logger.LogTrace("Get Annual Cost for the consumption {value} kWh/year", consumption);

            var validity = _inputValidator.Validate(consumption);
            if (!validity.IsValid)
            {
                var errormessage = validity.Errors.GetValidatorMessage();
                _logger.LogWarning($"Invalid Input: {errormessage}");

                return BadRequest(errormessage);
            }

            var result = _service.GetAnnualConsumption(consumption);

            if (!result.IsAny())
            {
                _logger.LogWarning("The annual cost calculation is not performed.");
                return NoContent();
            }

            var response = _mapper.Map<IEnumerable<ResponseDto>>(result);

            _logger.LogTrace("The annual cost is calculated successfully.");

            return Ok(response);
        }
    }
}
 