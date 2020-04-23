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
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.DestinationArea).NotNull().WithMessage("Tujuan Area Harus Diisi!");
            RuleFor(data => data.InspectionMaterialProductionOrders).Must(s => s.Any(d => d.Balance > 0)).WithMessage("Minimal 1 SPP harus dikeluarkan Saldonya!");
            RuleFor(data => data.InspectionMaterialProductionOrders).Must(s => s.GroupBy(d => d.ProductionOrder.Id).All(e => e.Count() == 1)).WithMessage("SPP harus berbeda setiap detail!");
            RuleForEach(s => s.InspectionMaterialProductionOrders).ChildRules(d =>
            {
                d.RuleFor(data => data.Balance).LessThanOrEqualTo(e => e.PreviousBalance).WithMessage("Jumlah Saldo baru tidak boleh melebihi sebelumnya");
            });
        }
    }
}
