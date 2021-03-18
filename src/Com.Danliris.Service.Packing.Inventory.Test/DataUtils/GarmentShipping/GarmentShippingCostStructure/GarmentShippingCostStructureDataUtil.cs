using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingCostStructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureDataUtil : BaseDataUtil<GarmentShippingCostStructureRepository, GarmentShippingCostStructureModel>
    {
        public GarmentShippingCostStructureDataUtil(GarmentShippingCostStructureRepository repository) : base(repository)
        {
        }

        public override GarmentShippingCostStructureModel GetModel()
        {
            var model = new GarmentShippingCostStructureModel("invoiceno", DateTimeOffset.Now, 1, "comodityCode", "comodityName", "hsCode", "destination", 1, "fabricType", 10000, 1);

            return model;
        }

        public GarmentShippingCostStructureModel GetModels()
        {
            var model = new GarmentShippingCostStructureModel("invoiceno", DateTimeOffset.Now, 1, "comodityCode", "comodityName", "hsCode", "destination", 1, "fabricType", 10000, 1);

            return model;
        }

        public override GarmentShippingCostStructureModel GetEmptyModel()
        {
            var model = new GarmentShippingCostStructureModel(null, DateTimeOffset.MinValue, 0, null, null, null, null, 0, null, 0, 0);

            return model;
        }

        public GarmentShippingCostStructureModel CopyModel(GarmentShippingCostStructureModel om)
        {
            var model = new GarmentShippingCostStructureModel(om.InvoiceNo, om.Date, om.ComodityId, om.ComodityCode, om.ComodityName, om.HsCode, om.Destination, om.FabricTypeId, om.FabricType, om.Amount, om.PackingListId){ Id = om.Id };

            return model;
        }
    }
}
