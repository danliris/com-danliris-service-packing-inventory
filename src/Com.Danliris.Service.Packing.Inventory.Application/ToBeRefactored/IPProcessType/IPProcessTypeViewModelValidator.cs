using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPProcessType;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPProcessType
{
    public class IPProcessTypeViewModelValidator : AbstractValidator<IPProcessTypeViewModel>
    {
        public IPProcessTypeViewModelValidator()
        {
            RuleFor(data => data.Code).NotNull().NotEmpty().WithMessage("Kode Harus Diisi!");
            RuleFor(data => data.ProcessType).NotNull().NotEmpty().WithMessage("Jenis Proses Harus Diisi!");
        }
    }
}
