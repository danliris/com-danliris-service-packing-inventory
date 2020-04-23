using FluentValidation;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalProductionOrderValidator : AbstractValidator<OutputAvalProductionOrderViewModel>
    {
        public OutputAvalProductionOrderValidator()
        {
            RuleFor(data => data.Quantity).NotEmpty().WithMessage("Harus Memiliki Qty Satuan!");
            RuleFor(data => data.QuantityKg).NotEmpty().WithMessage("Harus Memiliki Qty Kg!");
        }
    }
}
