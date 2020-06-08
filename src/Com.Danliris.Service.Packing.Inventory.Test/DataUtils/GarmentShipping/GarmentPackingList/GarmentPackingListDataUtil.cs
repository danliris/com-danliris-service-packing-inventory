using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDataUtil : BaseDataUtil<GarmentPackingListRepository, GarmentPackingListModel>
    {
        public GarmentPackingListDataUtil(GarmentPackingListRepository repository) : base(repository)
        {
        }

        public override GarmentPackingListModel GetModel()
        {
            var sizes = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "", 1) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", 1, 1, 1, sizes) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, "", 1, "", "", "", "", details, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", 1, "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, false, false, items, 1, 1, 1, measurements, "", "", "");

            return model;
        }

        public override GarmentPackingListModel GetEmptyModel()
        {
            var sizes = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(0, null, 0) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(0, 0, null, 0, 0, 0, sizes) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel(null, null, 0, null, 0, null, null, null, 0, 0, null, 0, 0, 0, null, 0, null, null, null, null, details, 0, 0) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(0, 0, 0, 0) };
            var model = new GarmentPackingListModel(null, null, null, 0, null, DateTimeOffset.MinValue, null, null, 0, null, null, null, DateTimeOffset.MinValue, DateTimeOffset.MinValue, false, false, items, 0, 0, 0, measurements, null, null, null);

            return model;
        }
    }
}
