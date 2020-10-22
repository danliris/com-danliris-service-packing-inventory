using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionDataUtil : BaseDataUtil<GarmentShippingPaymentDispositionRepository, GarmentShippingPaymentDispositionModel>
    {
        public GarmentShippingPaymentDispositionDataUtil(GarmentShippingPaymentDispositionRepository repository) : base(repository)
        {
        }

        public override GarmentShippingPaymentDispositionModel GetModel()
        {
            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel("",1) };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(1, "",1,1) };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel("",1,1,1,1,1,1,1) };
            var model = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", invoices, bills, units);

            return model;
        }

        public override GarmentShippingPaymentDispositionModel GetEmptyModel()
        {
            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel(null, 0) };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(0, null, 0, 0) };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel(null, 0, 0, 0, 0, 0, 0, 0) };
            var model = new GarmentShippingPaymentDispositionModel(null, null, null, null, null, 0, null, null, null, 0, null, null, 0, null, null, 0, null, null, null, null, null, DateTimeOffset.MinValue, null, 0, 0, 0, null, 0, 0, 0, DateTimeOffset.MinValue, null, null, true, null, null, DateTimeOffset.MinValue, null, invoices, bills, units);

            return model;
        }
    }
}
