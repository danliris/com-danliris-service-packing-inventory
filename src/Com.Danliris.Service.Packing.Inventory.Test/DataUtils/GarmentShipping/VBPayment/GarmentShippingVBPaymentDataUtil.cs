using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.VBPayment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentDataUtil : BaseDataUtil<GarmentShippingVBPaymentRepository, GarmentShippingVBPaymentModel>
    {
        public GarmentShippingVBPaymentDataUtil(GarmentShippingVBPaymentRepository repository) : base(repository)
        {
        }

        public override GarmentShippingVBPaymentModel GetModel()
        {
            var units = new HashSet<GarmentShippingVBPaymentUnitModel> { new GarmentShippingVBPaymentUnitModel(1,"","",1) };
            var invoices = new HashSet<GarmentShippingVBPaymentInvoiceModel> { new GarmentShippingVBPaymentInvoiceModel(1, "") };
            var model = new GarmentShippingVBPaymentModel("",DateTimeOffset.Now, "",1,"","",1,"","",1,"","","","",1,1,DateTimeOffset.Now,1,"",1,units,invoices);

            return model;
        }

        public override GarmentShippingVBPaymentModel GetEmptyModel()
        {
            var units = new HashSet<GarmentShippingVBPaymentUnitModel> { new GarmentShippingVBPaymentUnitModel(0,null,null,0) };
            var invoices = new HashSet<GarmentShippingVBPaymentInvoiceModel> { new GarmentShippingVBPaymentInvoiceModel(0,null) };
            var model = new GarmentShippingVBPaymentModel(null, DateTimeOffset.MinValue, null, 0, null, null, 0, null, null, 0, null, null, null, null, 0, 0, DateTimeOffset.MinValue, 0, null, 0, units, invoices);

            return model;
        }
    }
}
