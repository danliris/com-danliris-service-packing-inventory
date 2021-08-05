using FluentValidation;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public class OutputWarehouseValidator : AbstractValidator<OutputWarehouseViewModel>
    {
        public OutputWarehouseValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default)
                                            .WithMessage("Tanggal Harus Diisi!")
                                      .Must(s => s >= DateTimeOffset.UtcNow ||
                                           ((DateTimeOffset.UtcNow - s).TotalDays <= 1 && (DateTimeOffset.UtcNow - s).TotalDays >= 0))
                                            .WithMessage("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.Group).NotNull().WithMessage("Group Harus Diisi!");
            RuleFor(data => data.DestinationArea).NotNull().WithMessage("Tujuan Area Harus Diisi!");
            RuleFor(data => data.WarehousesProductionOrders).Must(s => s.Count > 0).WithMessage("SPP harus Diisi");
            RuleForEach(s => s.WarehousesProductionOrders).ChildRules(d =>
            {
                d.RuleFor(data => data.Balance).Must(e => e > 0).WithMessage("Qty Keluar Harus Lebih Besar dari 0");
            });
        }
    }
}
