using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalProductionOrderValidator : AbstractValidator<InputAvalProductionOrderViewModel>
    {
        public InputAvalProductionOrderValidator()
        {
            RuleFor(data => data.AvalType).NotEmpty().WithMessage("Harus Memiliki Jenis!");
            RuleFor(data => data.UomUnit).NotEmpty().WithMessage("Harus Memiliki Satuan!");
            RuleFor(data => data.Quantity).NotEmpty().WithMessage("Harus Memiliki Qty Satuan!");
            RuleFor(data => data.QuantityKg).NotEmpty().WithMessage("Harus Memiliki Qty Kg!");
        }
    }
}
