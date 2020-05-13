using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping
{
    public class InputShippingValidator : AbstractValidator<InputShippingViewModel>
    {
        public InputShippingValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!")
                .Must(s => s >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - s).TotalDays <= 1 && (DateTimeOffset.UtcNow - s).TotalDays >= 0)).WithMessage("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.ShippingProductionOrders)
                .Must(s => s.Count > 0).WithMessage("SPP harus Diisi")
                .Must(s => s.GroupBy(d => d.ProductionOrder.Id).All(e => e.Count() == 1)).WithMessage("SPP harus berbeda setiap detail!");
            RuleForEach(s => s.ShippingProductionOrders).ChildRules(d =>
            {
                d.RuleFor(data => data.DeliveryOrder).NotNull().WithMessage("DO Harus Diisi!");
                d.When(data => data.DeliveryOrder != null, () =>
                {
                    d.RuleFor(s => s.DeliveryOrder.Id).GreaterThan(0).WithMessage("SPP Tidak Valid!");
                    d.RuleFor(s => s.DeliveryOrder.No).NotNull().WithMessage("SPP Tidak Valid!");
                });

            });
        }
    }
}
