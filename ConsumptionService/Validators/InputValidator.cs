using ConsumptionService.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace ConsumptionService.Validators
{
    /// <summary>
    /// This class specifies rules to validate the consumption input.
    /// </summary>
    public class InputValidator : AbstractValidator<decimal>
    {
        public InputValidator(IOptionsSnapshot<AppSettings> settings)
        {
            var maxValue = settings.Value.MaxConsumptionValue;

            RuleFor(p => p).GreaterThanOrEqualTo(0).WithMessage("The consumption value mustn't be less then 0.");
            RuleFor(p => p).LessThan(maxValue).WithMessage($"The consumption value must be less then {maxValue}");
        }
    }
}
