using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalItemValidator : AbstractValidator<InputAvalItemViewModel>
    {
        public InputAvalItemValidator()
        {
            RuleFor(data => data.AvalType).NotEmpty().WithMessage("Harus Memiliki Jenis!");
            RuleFor(data => data.AvalCartNo).NotEmpty().WithMessage("Harus Memiliki No. Kereta!");
            //RuleFor(data => data.AvalUomUnit).NotEmpty().WithMessage("Harus Memiliki Satuan!");
            //RuleFor(data => data.AvalQuantity).NotEmpty().WithMessage("Harus Memiliki Qty Satuan!");
            //RuleFor(data => data.AvalQuantityKg).NotEmpty().WithMessage("Harus Memiliki Qty Kg!");
        }
    }
}
