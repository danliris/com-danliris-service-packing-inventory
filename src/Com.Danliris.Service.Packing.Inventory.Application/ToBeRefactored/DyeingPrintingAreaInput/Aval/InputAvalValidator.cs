using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalValidator : AbstractValidator<InputAvalViewModel>
    {
        public InputAvalValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.BonNo).NotNull().WithMessage("No. Bon Harus Diisi!");
            RuleFor(data => data.AvalProductionOrders).Must(s => s.Count >  0);
            RuleForEach(data => data.AvalProductionOrders).SetValidator(new InputAvalProductionOrderValidator());
        }
    }
}
