using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Transit
{
    public class OutputTransitValidator : AbstractValidator<OutputTransitViewModel>
    {
        public OutputTransitValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!")
                .Must(s => s >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - s).TotalDays <= 1 && (DateTimeOffset.UtcNow - s).TotalDays >= 0)).WithMessage("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.Group).NotNull().WithMessage("Group Harus Diisi!");
            RuleFor(data => data.DestinationArea).NotNull().WithMessage("Tujuan Area Harus Diisi!");
            //RuleFor(data => data.TransitProductionOrders).Must(s => s.All(d => d.Balance > 0)).WithMessage("Qty Keluar Harus Lebih Besar dari 0");
            RuleFor(data => data.TransitProductionOrders)
                .Must(s => s.Count > 0).WithMessage("SPP harus Diisi");
            //RuleFor(data => data.InspectionMaterialProductionOrders)
            //    .Must(s => s.GroupBy(d => d.ProductionOrder.Id).All(e => e.Count() == 1))
            //    .WithMessage("SPP harus berbeda setiap detail!")
            //    .When(s => s.InspectionMaterialProductionOrders.All(d => d.ProductionOrder != null));
            RuleForEach(s => s.TransitProductionOrders).ChildRules(d =>
            {
                d.RuleFor(data => data.Balance).LessThanOrEqualTo(e => e.BalanceRemains).WithMessage("Jumlah Qty Keluar tidak boleh melebihi sisa saldo");
                d.RuleFor(data => data.Balance).Must(e => e > 0).WithMessage("Qty Keluar Harus Lebih Besar dari 0");
            });
        }
    }
}
