using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPYarnType
{
    public class IPYarnTypeViewModelValidator : AbstractValidator<IPYarnTypeViewModel>
    {
        public IPYarnTypeViewModelValidator()
        {
            RuleFor(data => data.Code).NotNull().NotEmpty().WithMessage("Kode Harus Diisi!");
            RuleFor(data => data.YarnType).NotNull().NotEmpty().WithMessage("Jenis Benang Harus Diisi!");
        }
    }
}
