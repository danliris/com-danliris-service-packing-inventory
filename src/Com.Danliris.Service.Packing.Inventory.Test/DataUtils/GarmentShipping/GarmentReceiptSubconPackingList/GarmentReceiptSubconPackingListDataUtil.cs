using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListDataUtil : BaseDataUtil<GarmentReceiptSubconPackingListRepository, GarmentReceiptSubconPackingListModel>
    {
        public GarmentReceiptSubconPackingListDataUtil(GarmentReceiptSubconPackingListRepository repository) : base(repository)
        {
        }

        public override GarmentReceiptSubconPackingListModel GetModel()
        {
            var sizes = new HashSet<GarmentReceiptSubconPackingListDetailSizeModel> { new GarmentReceiptSubconPackingListDetailSizeModel(1, "", 1, 1,"", new Guid()) };
            var details = new HashSet<GarmentReceiptSubconPackingListDetailModel> { new GarmentReceiptSubconPackingListDetailModel(1, 1, "", 1, 1, 1, 1, 1, 1, 1, 1, 1, sizes,1) };
            var items = new HashSet<GarmentReceiptSubconPackingListItemModel> { new GarmentReceiptSubconPackingListItemModel("", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", details,1) };
            var model = new GarmentReceiptSubconPackingListModel(1, "", DateTimeOffset.Now, 1, "", 1, "", "", 1, "", "", "", "", true, true, items, 1, 1, 1, 1, true, false, "", DateTimeOffset.Now);

            return model;
        }

        public override GarmentReceiptSubconPackingListModel GetEmptyModel()
        {
            var sizes = new HashSet<GarmentReceiptSubconPackingListDetailSizeModel> { new GarmentReceiptSubconPackingListDetailSizeModel(0, null, 0, 0,null, new Guid()) };
            var details = new HashSet<GarmentReceiptSubconPackingListDetailModel> { new GarmentReceiptSubconPackingListDetailModel(0, 0, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, sizes,0) };
            var items = new HashSet<GarmentReceiptSubconPackingListItemModel> { new GarmentReceiptSubconPackingListItemModel(null, null, 0, null, 0, null, null, null, null, 0, 0, null, 0, 0, 0, 0, 0, null, 0, null, null, null, null, null, null, null, details,0) };
            var model = new GarmentReceiptSubconPackingListModel(0, null, DateTimeOffset.Now, 0, null, 0, null, null, 0, null, null, null, null, true, true, items, 0, 0, 0, 0, true, false, null, DateTimeOffset.Now);

            return model;
        }
    }
}
