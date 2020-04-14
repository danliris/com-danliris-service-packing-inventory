using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.AcceptingPackaging
{
    public class AcceptingPackagingValidator : AbstractValidator<AcceptingPackagingViewModel>
    {
        public AcceptingPackagingValidator()
        {
            RuleFor(x => x.NoBon).NotNull().WithMessage("No Bon Harus Diisi");
            RuleFor(x => x.Grade).NotNull().WithMessage("Grade Harus Diisi");
            RuleFor(x => x.Saldo).NotNull().WithMessage("Saldo Tidak boleh diisi");
        }       
    }
}
