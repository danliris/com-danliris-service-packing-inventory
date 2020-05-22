using FluentValidation;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create
{
    public class InputWarehouseCreateValidator : AbstractValidator<InputWarehouseCreateViewModel>
    {
        public InputWarehouseCreateValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default).WithMessage("Tanggal Harus Diisi!")
                .Must(s => s >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - s).TotalDays <= 1 && (DateTimeOffset.UtcNow - s).TotalDays >= 0)).WithMessage("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
        }
    }
}
