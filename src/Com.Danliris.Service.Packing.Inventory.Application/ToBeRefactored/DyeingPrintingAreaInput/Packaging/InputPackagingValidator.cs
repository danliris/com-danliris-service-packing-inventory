﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging
{
    public class InputPackagingValidator : AbstractValidator<InputPackagingViewModel>
    {
        public InputPackagingValidator()
        {
            RuleFor(data => data.Area).NotNull().NotEmpty().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!")
                .Must(s=> s.Date.AddDays(1) >= DateTime.Now.Date).WithMessage("Tanggal Tidak Boleh Kurang Dari Hari Ini");
            RuleFor(data => data.Shift).NotNull().NotEmpty().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.Group).NotNull().NotEmpty().WithMessage("Group Harus Diisi!");
            //RuleFor(data => data.BonNo).NotNull().WithMessage("No. Bon Harus Diisi!");
        }
    }
}
