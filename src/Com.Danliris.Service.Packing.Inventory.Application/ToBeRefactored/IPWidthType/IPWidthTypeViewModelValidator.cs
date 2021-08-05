using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWidthType
{
    public class IPWidthTypeViewModelValidator : AbstractValidator<IPWidthTypeViewModel>
    {
        public IPWidthTypeViewModelValidator()
        {
            RuleFor(data => data.Code).NotNull().NotEmpty().WithMessage("Kode Harus Diisi!");
            RuleFor(data => data.WidthType).NotNull().NotEmpty().WithMessage("Jenis Lebar Harus Diisi!");
        }
    }
}
