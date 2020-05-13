using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingValidator : AbstractValidator<OutputShippingViewModel>
    {
        public OutputShippingValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!")
                .Must(s => s >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - s).TotalDays <= 1 && (DateTimeOffset.UtcNow - s).TotalDays >= 0)).WithMessage("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.DestinationArea).NotNull().WithMessage("Tujuan Area Harus Diisi!");
            RuleFor(data => data.ShippingProductionOrders).Must(s => s.All(d => d.Qty > 0)).WithMessage("Qty Keluar Harus Lebih Besar dari 0");
            RuleForEach(s => s.ShippingProductionOrders).ChildRules(d =>
            {
                d.RuleFor(data => data.Remark).NotNull().WithMessage("Remark Harus Diisi!");

            });
        }
    }
}
