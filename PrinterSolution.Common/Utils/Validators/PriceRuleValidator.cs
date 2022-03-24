using FluentValidation;
using PrinterSolution.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Utils.Validators
{
    public class PriceRuleValidator : BaseValidator<PriceRule>
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
