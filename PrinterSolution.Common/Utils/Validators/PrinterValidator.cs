using FluentValidation;
using PrinterSolution.Common.DTOs.Requests;

namespace PrinterSolution.Common.Utils.Validators
{
    public class PrinterValidator : BaseValidator<CreatePrinterModel>
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
