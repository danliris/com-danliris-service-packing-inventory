using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ExportSalesDO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDODataUtil 
        : BaseDataUtil<GarmentShippingExportSalesDORepository, GarmentShippingExportSalesDOModel>
    {
        public GarmentShippingExportSalesDODataUtil(GarmentShippingExportSalesDORepository repository) : base(repository)
        {
        }

        public override GarmentShippingExportSalesDOModel GetModel()
        {
            var item = new HashSet<GarmentShippingExportSalesDOItemModel> { new GarmentShippingExportSalesDOItemModel(1, "", "", "", 1, 1, "", 1, 1, 1, 1) };
            var model = new GarmentShippingExportSalesDOModel("","",1,DateTimeOffset.Now,1,"","","","",1,"", "", "", "", item);

            return model;
        }

        public override GarmentShippingExportSalesDOModel GetEmptyModel()
        {
            var item = new HashSet<GarmentShippingExportSalesDOItemModel> { new GarmentShippingExportSalesDOItemModel(0, null, null, null, 0, 0, null, 0, 0, 0, 0) };
            var model = new GarmentShippingExportSalesDOModel( null, null, 0, DateTimeOffset.MinValue,0, null, null, null, null,0, null, "", "", "", item);

            return model;
        }
    }
}
