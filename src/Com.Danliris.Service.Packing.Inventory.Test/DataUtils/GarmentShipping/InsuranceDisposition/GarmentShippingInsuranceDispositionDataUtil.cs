using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionDataUtil : BaseDataUtil<GarmentShippingInsuranceDispositionRepository, GarmentShippingInsuranceDispositionModel>
    {
        public GarmentShippingInsuranceDispositionDataUtil(GarmentShippingInsuranceDispositionRepository repository) : base(repository)
        {
        }

        public override GarmentShippingInsuranceDispositionModel GetModel()
        {
            var items = new HashSet<GarmentShippingInsuranceDispositionItemModel> { new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 1, 1, "", "", 1, 1, 1, 1, 1, 1, 1), new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 2, 2, "", "", 2, 2, 2, 2, 2, 2, 2) };
            var model = new GarmentShippingInsuranceDispositionModel("","", DateTimeOffset.Now, "", 1, "", "", 1, "",  items);

            return model;
        }

        public override GarmentShippingInsuranceDispositionModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingInsuranceDispositionItemModel> { new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.MinValue, null, null, 0, 0, null, null, 0, 0, 0, 0, 0, 0, 0) };
            var model = new GarmentShippingInsuranceDispositionModel(null,null, DateTimeOffset.MinValue,null, 0, null, null, 0, null,  items);

            return model;
        }
    }
}
