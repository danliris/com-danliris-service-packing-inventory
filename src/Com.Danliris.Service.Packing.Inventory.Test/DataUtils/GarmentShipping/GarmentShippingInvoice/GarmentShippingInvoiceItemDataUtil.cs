using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceItemDataUtil : BaseDataUtil<GarmentShippingInvoiceItemRepository, GarmentShippingInvoiceItemModel>
    {
        public GarmentShippingInvoiceItemDataUtil(GarmentShippingInvoiceItemRepository repository) : base(repository)
        {
        }

        public override GarmentShippingInvoiceItemModel GetModel()
        {
            var model = new GarmentShippingInvoiceItemModel("", "", "", 1, "", 1, 1, "", "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C10", 1, 1);

            return model;
        }

        public override GarmentShippingInvoiceItemModel GetEmptyModel()
        {
            var model = new GarmentShippingInvoiceItemModel(null, null, null, 0, null, 0, 0, null, null, null, null, null, null, null, 0, null, 0, 0, 0, null, 0, null, 0, 0);

            return model;
        }
    }
}
