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
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel> { new GarmentShippingPaymentDispositionRecapItemModel(1, 10, 10, 10, 10, 10) };
            var model = new GarmentShippingPaymentDispositionRecapModel("", DateTimeOffset.Now, 1, "", "", "", "", items);

            return model;
        }

        public override GarmentShippingPaymentDispositionRecapModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel> { new GarmentShippingPaymentDispositionRecapItemModel(0, 10, 10, 10, 10, 10) };
            var model = new GarmentShippingPaymentDispositionRecapModel(null, DateTimeOffset.MinValue, 0, null, null, null, null, items);

            return model;
        }

        public GarmentShippingPaymentDispositionRecapModel CopyModel(GarmentShippingPaymentDispositionRecapModel om)
        {
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel>();
            foreach (var i in om.Items)
            {
                items.Add(new GarmentShippingPaymentDispositionRecapItemModel(i.PaymentDispositionId, i.Service, i.OthersPayment, i.TruckingPayment, i.VatService, i.AmountService) { Id = i.Id });
            }
            var model = new GarmentShippingPaymentDispositionRecapModel(om.RecapNo, om.Date, om.EmklId, om.EMKLCode, om.EMKLName, om.EMKLAddress, om.EMKLNPWP, items) { Id = om.Id };

            return model;
        }
    }
}
