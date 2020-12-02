using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceAdjustmentDataUtil : BaseDataUtil<GarmentShippingInvoiceAdjustmentRepository, GarmentShippingInvoiceAdjustmentModel>
    {
        public GarmentShippingInvoiceAdjustmentDataUtil(GarmentShippingInvoiceAdjustmentRepository repository) : base(repository)
        {
        }

        public override GarmentShippingInvoiceAdjustmentModel GetModel()
        {
            var model = new GarmentShippingInvoiceAdjustmentModel(1,"", 100,1);

            return model;
        }

        public override GarmentShippingInvoiceAdjustmentModel GetEmptyModel()
        {
            var model = new GarmentShippingInvoiceAdjustmentModel(0, null, 0, 0);

            return model;
        }
    }
}
