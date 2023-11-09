using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListDataUtil : BaseDataUtil<GarmentShippingPackingListRepository, GarmentShippingPackingListModel>
    {
        private readonly GarmentShippingPackingListRepository Service;

        public GarmentShippingPackingListDataUtil(GarmentShippingPackingListRepository repository) : base(repository)
        {
        }

        public override GarmentShippingPackingListModel GetModel()
        {
            var sizes = new HashSet<GarmentShippingPackingListDetailSizeModel> { new GarmentShippingPackingListDetailSizeModel(1, "", 1, 1) };
            var details = new HashSet<GarmentShippingPackingListDetailModel> { new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, sizes, 1) };
            var items = new HashSet<GarmentShippingPackingListItemModel> { new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", details) };
            var measurements = new HashSet<GarmentShippingPackingListMeasurementModel> { new GarmentShippingPackingListMeasurementModel(1, 1, 1, 1, "a") };
            var model = new GarmentShippingPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentShippingPackingListStatusEnum.CREATED, "", false, "", false, false, false, "");

            return model;
        }

        public override GarmentShippingPackingListModel GetEmptyModel()
        {
            var sizes = new HashSet<GarmentShippingPackingListDetailSizeModel> { new GarmentShippingPackingListDetailSizeModel(0, null, 0, 0) };
            var details = new HashSet<GarmentShippingPackingListDetailModel> { new GarmentShippingPackingListDetailModel(0, 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, sizes, 1) };
            var items = new HashSet<GarmentShippingPackingListItemModel> { new GarmentShippingPackingListItemModel(null, null, null, 0, null, 0, null, null, null, null, 0, 0, null, 0, 0, 0, 0, 0, null, 0, null, null, null, null, null, null, null, details) };
            var measurements = new HashSet<GarmentShippingPackingListMeasurementModel> { new GarmentShippingPackingListMeasurementModel(0, 0, 0, 0, "") };
            var model = new GarmentShippingPackingListModel(null, null, null, 0, null, DateTimeOffset.MinValue, null, null, DateTimeOffset.MinValue, null, 0, null, null, null, null, null, DateTimeOffset.MinValue, DateTimeOffset.MinValue, DateTimeOffset.MinValue, false, false, "", "", "", items, 0, 0, 0, 0, measurements, null, null, null, null, null, null, null, false, false, 0, null, GarmentShippingPackingListStatusEnum.CREATED, "", false, "", false, false, false, "");

            return model;
        }

        public GarmentShippingPackingListModel CopyModel(GarmentShippingPackingListModel om)
        {
            var items = new HashSet<GarmentShippingPackingListItemModel>();
            foreach (var i in om.Items)
            {
                var details = new HashSet<GarmentShippingPackingListDetailModel>();
                foreach (var d in i.Details)
                {
                    var sizes = new HashSet<GarmentShippingPackingListDetailSizeModel>();
                    foreach (var s in d.Sizes)
                    {
                        sizes.Add(new GarmentShippingPackingListDetailSizeModel(s.SizeId, s.Size, s.SizeIdx, s.Quantity) { Id = s.Id });
                    }
                    details.Add(new GarmentShippingPackingListDetailModel(d.Carton1, d.Carton2, d.Style, d.Colour, d.CartonQuantity, d.QuantityPCS, d.TotalQuantity, d.Length, d.Width, d.Height, d.GrossWeight, d.NetWeight, d.NetNetWeight, sizes, d.Index) { Id = d.Id });
                }
                items.Add(new GarmentShippingPackingListItemModel(i.ExpenditureGoodNo, i.RONo, i.SCNo, i.BuyerBrandId, i.BuyerBrandName, i.ComodityId, i.ComodityCode, i.ComodityName, i.ComodityDescription, i.MarketingName, i.Quantity, i.UomId, i.UomUnit, i.PriceRO, i.Price, i.PriceFOB, i.PriceCMT, i.Amount, i.Valas, i.UnitId, i.UnitCode, i.Article, i.OrderNo, i.Description, i.DescriptionMd, i.Remarks, i.RoType, details) { Id = i.Id });
            }
            var measurements = new HashSet<GarmentShippingPackingListMeasurementModel>();
            foreach (var measurement in om.Measurements)
            {
                measurements.Add(new GarmentShippingPackingListMeasurementModel(measurement.Length, measurement.Width, measurement.Height, measurement.CartonsQuantity, measurement.CreatedBy) { Id = measurement.Id });
            }

            var model = new GarmentShippingPackingListModel(om.InvoiceNo, om.PackingListType, om.InvoiceType, om.SectionId, om.SectionCode, om.Date, om.PaymentTerm, om.LCNo, om.LCDate, om.IssuedBy, om.BuyerAgentId, om.BuyerAgentCode, om.BuyerAgentName, om.Destination, om.FinalDestination, om.ShipmentMode, om.TruckingDate, om.TruckingEstimationDate, om.ExportEstimationDate, om.Omzet, om.Accounting, om.FabricCountryOrigin, om.FabricComposition, om.RemarkMd, items, om.GrossWeight, om.NettWeight, om.NetNetWeight, om.TotalCartons, measurements, om.SayUnit, om.ShippingMark, om.SideMark, om.Remark, om.ShippingMarkImagePath, om.SideMarkImagePath, om.RemarkImagePath, om.IsUsed, om.IsPosted, om.ShippingStaffId, om.ShippingStaffName, om.Status, om.Description, om.IsCostStructured, om.OtherCommodity, om.IsShipping, om.IsSampleDelivered, om.IsSampleExpenditureGood, om.SampleRemarkMd) { Id = om.Id };

            return model;
        }

        public GarmentShippingPackingListModel GetNewData()
        {
            var sizes = new HashSet<GarmentShippingPackingListDetailSizeModel> { new GarmentShippingPackingListDetailSizeModel(1, "", 1, 1) };
            var details = new HashSet<GarmentShippingPackingListDetailModel> { new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, sizes, 1) };
            var items = new HashSet<GarmentShippingPackingListItemModel> { new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", details) };
            var measurements = new HashSet<GarmentShippingPackingListMeasurementModel> { new GarmentShippingPackingListMeasurementModel(1, 1, 1, 1, "a") };
            var model = new GarmentShippingPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentShippingPackingListStatusEnum.CREATED, "", false, "", false, false, false, "");

            return model;
        }

        public async Task<GarmentShippingPackingListModel> GetTestData()
        {
            GarmentShippingPackingListModel model = GetNewData();
            await Service.InsertAsync(model);
            return await Service.ReadByIdAsync(model.Id);
        }

    }
}