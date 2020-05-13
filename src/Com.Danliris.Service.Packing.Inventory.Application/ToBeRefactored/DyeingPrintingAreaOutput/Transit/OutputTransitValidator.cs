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
            RuleFor(data => data.DestinationArea).NotNull().WithMessage("Tujuan Area Harus Diisi!");
            RuleFor(data => data.TransitProductionOrders).Must(s => s.All(d => d.Balance > 0)).WithMessage("Qty Keluar Harus Lebih Besar dari 0");
            RuleFor(data => data.TransitProductionOrders).Must(s => s.GroupBy(d => d.ProductionOrder.Id).All(e => e.Count() == 1)).WithMessage("SPP harus berbeda setiap detail!");
            RuleForEach(s => s.TransitProductionOrders).ChildRules(d =>
            {
                d.RuleFor(data => data.Balance).LessThanOrEqualTo(e => e.PreviousBalance).WithMessage("Jumlah Saldo baru tidak boleh melebihi sebelumnya");
            });
        }
    }
}
