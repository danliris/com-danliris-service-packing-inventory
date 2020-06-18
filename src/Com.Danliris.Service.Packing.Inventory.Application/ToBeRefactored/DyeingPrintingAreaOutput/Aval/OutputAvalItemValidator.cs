using FluentValidation;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalItemValidator : AbstractValidator<OutputAvalItemViewModel>
    {
        public OutputAvalItemValidator()
        {
            RuleFor(data => data.AvalOutSatuan).NotEmpty().NotNull().NotEqual(0).WithMessage("Harus Memiliki Qty Satuan!");
            RuleFor(data => data.AvalOutQuantity).NotEmpty().NotNull().NotEqual(0).WithMessage("Harus Memiliki Qty Kg!");
        }
    }
}
