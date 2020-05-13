using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouses
{
    public class InputWarehousesValidator : AbstractValidator<InputWarehousesViewModel>
    {
        public InputWarehousesValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default).WithMessage("Tanggal Harus Diisi!")
                .Must(s => s.Date >= DateTime.Now.Date).WithMessage("Tanggal Tidak Boleh Kurang Dari Hari Ini");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            //RuleFor(data => data.BonNo).NotNull().WithMessage("No. Bon Harus Diisi!");
        }
    }
}
