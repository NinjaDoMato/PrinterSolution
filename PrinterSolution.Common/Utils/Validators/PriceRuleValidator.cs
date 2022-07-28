using FluentValidation;
using PrinterSolution.Common.DTOs.Requests;

namespace PrinterSolution.Common.Utils.Validators
{
    public class PriceRuleValidator : BaseValidator<CreatePriceRuleModel>
    {
        public PriceRuleValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name cannot be empty.");

            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty()
                .WithMessage("Code cannot be empty.");
        }
    }
}
