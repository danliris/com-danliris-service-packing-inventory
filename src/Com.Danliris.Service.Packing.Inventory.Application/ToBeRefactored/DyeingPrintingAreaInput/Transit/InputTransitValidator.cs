using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit
{
    public class InputTransitValidator : AbstractValidator<InputTransitViewModel>
    {
        public InputTransitValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!")
                .Must(s => s >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - s).TotalDays <= 1 && (DateTimeOffset.UtcNow - s).TotalDays >= 0)).WithMessage("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.Group).NotNull().WithMessage("Group Harus Diisi!");
            RuleFor(data => data.TransitProductionOrders)
                .Must(s => s.Count > 0).WithMessage("SPP harus Diisi");
            //RuleFor(data => data.TransitProductionOrders)
            //    .Must(s => s.GroupBy(d => d.ProductionOrder.Id).All(e => e.Count() == 1))
            //    .WithMessage("SPP harus berbeda setiap detail!")
            //    .When(s => s.TransitProductionOrders.All(d => d.ProductionOrder != null));
        }
    }
}
