using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.PaymentDispositionRecap
{
    public class PaymentDispositionRecapDataUtil : BaseDataUtil<GarmentShippingPaymentDispositionRecapRepository, GarmentShippingPaymentDispositionRecapModel>
    {
        public PaymentDispositionRecapDataUtil(GarmentShippingPaymentDispositionRecapRepository repository) : base(repository)
        {
        }

        public override GarmentShippingPaymentDispositionRecapModel GetModel()
        {
            var details = new HashSet<GarmentShippingPaymentDispositionRecapDetailModel> { new GarmentShippingPaymentDispositionRecapDetailModel(1, 1, 10) };
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel> { new GarmentShippingPaymentDispositionRecapItemModel(1, details) };
            var model = new GarmentShippingPaymentDispositionRecapModel("", DateTimeOffset.Now, 1, "", "", "", "", items);

            return model;
        }

        public override GarmentShippingPaymentDispositionRecapModel GetEmptyModel()
        {
            var details = new HashSet<GarmentShippingPaymentDispositionRecapDetailModel> { new GarmentShippingPaymentDispositionRecapDetailModel(0, 0, 0) };
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel> { new GarmentShippingPaymentDispositionRecapItemModel(0, details) };
            var model = new GarmentShippingPaymentDispositionRecapModel(null, DateTimeOffset.MinValue, 0, null, null, null, null, items);

            return model;
        }

        public GarmentShippingPaymentDispositionRecapModel CopyModel(GarmentShippingPaymentDispositionRecapModel om)
        {
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel>();
            foreach (var i in om.Items)
            {
                var details = new HashSet<GarmentShippingPaymentDispositionRecapDetailModel>();
                foreach (var d in i.Details)
                {
                    details.Add(new GarmentShippingPaymentDispositionRecapDetailModel(d.PaymentDispositionInvoiceDetailId, d.InvoiceId, d.Service) { Id = d.Id });
                }
                items.Add(new GarmentShippingPaymentDispositionRecapItemModel(i.PaymentDispositionId, details) { Id = i.Id });
            }
            var model = new GarmentShippingPaymentDispositionRecapModel(om.RecapNo, om.Date, om.EmklId, om.EMKLCode, om.EMKLName, om.EMKLAddress, om.EMKLNPWP, items) { Id = om.Id };

            return model;
        }
    }
}
