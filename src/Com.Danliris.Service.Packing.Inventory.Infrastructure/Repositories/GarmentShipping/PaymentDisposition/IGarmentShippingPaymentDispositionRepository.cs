using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition
{
    public interface IGarmentShippingPaymentDispositionRepository : IRepository<GarmentShippingPaymentDispositionModel>
    {
        IQueryable<GarmentShippingPaymentDispositionUnitChargeModel> ReadUnitAll();
        IQueryable<GarmentShippingPaymentDispositionInvoiceDetailModel> ReadInvAll();
    }
}

