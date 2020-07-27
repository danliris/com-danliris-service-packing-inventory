using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWovenType
{
    public class IPWovenTypeViewModelValidator : AbstractValidator<IPWovenTypeViewModel>
    {
        public IPWovenTypeViewModelValidator()
        {
            RuleFor(data => data.Code).NotNull().NotEmpty().WithMessage("Kode Harus Diisi!");
            RuleFor(data => data.WovenType).NotNull().NotEmpty().WithMessage("Jenis Anyaman Harus Diisi!");
        }
    }
}
