using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.TransitInput
{
    public class TransitInputValidator : AbstractValidator<TransitInputViewModel>
    {
        public TransitInputValidator()
        {
            RuleFor(data => data.BonNo).NotNull().WithMessage("Harus Memilih No Bon!");
            RuleFor(data => data.Shift).NotNull().WithMessage("Harus Memilih Shift!");
        }
    }
}
