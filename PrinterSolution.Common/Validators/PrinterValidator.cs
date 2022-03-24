using FluentValidation;
using PrinterSolution.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Validators
{
    public class PrinterValidator : BaseValidator<Printer>
    {
        public PrinterValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Printer name cannot be empty.");

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("Printer address cannot be empty.");

            RuleFor(x => x.Depth)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Printer depth cannot be negative.");

            RuleFor(x => x.Width)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Printer width cannot be negative.");
            
            RuleFor(x => x.Height)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Printer height cannot be negative.");
        }
    }
}
