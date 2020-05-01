using FluentValidation;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalValidator : AbstractValidator<OutputAvalViewModel>
    {
        public OutputAvalValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.DestinationArea).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default).WithMessage("Tanggal Harus Diisi!");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.AvalItems).Must(s => s.Count > 0);
            RuleForEach(data => data.AvalItems).SetValidator(new OutputAvalItemValidator());
        }
    }
}
