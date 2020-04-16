using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.ShipmentInput
{
    public class ShipmentInputValidator : AbstractValidator<ShipmentInputViewModel>
    {
        public ShipmentInputValidator()
        {
            RuleFor(data => data.BonNo).NotNull().WithMessage("Harus Memilih No Bon!");
            RuleFor(data => data.DeliveryOrderSales).NotNull().WithMessage("DO Harus Diisi!");
            When(data => data.DeliveryOrderSales != null, () =>
            {
                RuleFor(s => s.DeliveryOrderSales.Id).GreaterThan(0).WithMessage("DO Tidak Valid!");
                RuleFor(s => s.DeliveryOrderSales.No).NotNull().WithMessage("DO Tidak Valid!");
            });
        }
    }
}
