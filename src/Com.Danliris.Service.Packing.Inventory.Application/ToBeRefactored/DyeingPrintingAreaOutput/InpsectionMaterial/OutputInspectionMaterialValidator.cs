using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public class OutputInspectionMaterialValidator : AbstractValidator<OutputInspectionMaterialViewModel>
    {
        public OutputInspectionMaterialValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!")
               .Must(s => s >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - s).TotalDays <= 1 && (DateTimeOffset.UtcNow - s).TotalDays >= 0)).WithMessage("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.Group).NotNull().WithMessage("Group Harus Diisi!");
            RuleFor(data => data.DestinationArea).NotNull().WithMessage("Tujuan Area Harus Diisi!");
            //RuleFor(data => data.InspectionMaterialProductionOrders).Must(s => s.All(d => d.Balance > 0)).WithMessage("Qty Keluar Harus Lebih Besar dari 0");
            RuleFor(data => data.InspectionMaterialProductionOrders)
                .Must(s => s.Count > 0).WithMessage("SPP harus Diisi");
            //RuleFor(data => data.InspectionMaterialProductionOrders)
            //    .Must(s => s.GroupBy(d => d.ProductionOrder.Id).All(e => e.Count() == 1))
            //    .WithMessage("SPP harus berbeda setiap detail!")
            //    .When(s => s.InspectionMaterialProductionOrders.All(d => d.ProductionOrder != null));
            RuleForEach(s => s.InspectionMaterialProductionOrders).ChildRules(d =>
            {
                d.RuleFor(data => data.Balance).Must(e => e > 0).WithMessage("Qty Keluar Harus Lebih Besar dari 0");
            });
        }
    }
}
