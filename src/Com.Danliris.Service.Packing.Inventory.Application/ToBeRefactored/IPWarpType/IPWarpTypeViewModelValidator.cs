using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWarpType
{
    public class IPWarpTypeViewModelValidator : AbstractValidator<IPWarpTypeViewModel>
    {
        public IPWarpTypeViewModelValidator()
        {
            RuleFor(data => data.Code).NotNull().NotEmpty().WithMessage("Kode Harus Diisi!");
            RuleFor(data => data.WarpType).NotNull().NotEmpty().WithMessage("Jenis Lebar Harus Diisi!");
        }
    }
}
