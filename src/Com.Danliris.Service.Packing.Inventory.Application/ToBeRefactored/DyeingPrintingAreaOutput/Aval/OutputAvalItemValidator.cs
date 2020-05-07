using FluentValidation;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalItemValidator : AbstractValidator<OutputAvalItemViewModel>
    {
        public OutputAvalItemValidator()
        {
            RuleFor(data => data.AvalQuantity).NotEmpty().WithMessage("Harus Memiliki Qty Satuan!");
            RuleFor(data => data.AvalQuantityKg).NotEmpty().WithMessage("Harus Memiliki Qty Kg!");
        }
    }
}
