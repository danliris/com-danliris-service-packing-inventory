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
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, sizes) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "");

            return model;
        }

        public override GarmentPackingListModel GetEmptyModel()
        {
            var sizes = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(0, null, 0) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(0, 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, sizes) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel(null, null, 0, null, 0, null, null, null, 0, 0, null, 0, 0, 0, 0, 0, null, 0, null, null, null, null, null, details, 0, 0) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(0, 0, 0, 0) };
            var model = new GarmentPackingListModel(null, null, null, 0, null, DateTimeOffset.MinValue, null, null, DateTimeOffset.MinValue, null, 0, null, null, null, null, null, DateTimeOffset.MinValue, DateTimeOffset.MinValue, DateTimeOffset.MinValue, false, false, "", "", "", items, 0, 0, 0, measurements, null, null, null, null, null, null, null, false, false, 0, null, GarmentPackingListStatusEnum.CREATED, "");

            return model;
        }

        public GarmentPackingListModel CopyModel(GarmentPackingListModel om)
        {
            var items = new HashSet<GarmentPackingListItemModel>();
            foreach (var i in om.Items)
            {
                var details = new HashSet<GarmentPackingListDetailModel>();
                foreach (var d in i.Details)
                {
                    var sizes = new HashSet<GarmentPackingListDetailSizeModel>();
                    foreach (var s in d.Sizes)
                    {
                        sizes.Add(new GarmentPackingListDetailSizeModel(s.SizeId, s.Size, s.Quantity) { Id = s.Id });
                    }
                    details.Add(new GarmentPackingListDetailModel(d.Carton1, d.Carton2, d.Style, d.Colour, d.CartonQuantity, d.QuantityPCS, d.TotalQuantity, d.Length, d.Width, d.Height, d.GrossWeight, d.NetWeight, d.NetNetWeight, d.CartonsQuantity, sizes) { Id = d.Id });
                }
                items.Add(new GarmentPackingListItemModel(i.RONo, i.SCNo, i.BuyerBrandId, i.BuyerBrandName, i.ComodityId, i.ComodityCode, i.ComodityName, i.ComodityDescription, i.Quantity, i.UomId, i.UomUnit, i.PriceRO, i.Price, i.PriceFOB, i.PriceCMT, i.Amount, i.Valas, i.UnitId, i.UnitCode, i.Article, i.OrderNo, i.Description, i.DescriptionMd, details, i.AVG_GW, i.AVG_NW) { Id = i.Id });
            }
            var measurements = new HashSet<GarmentPackingListMeasurementModel>();
            foreach (var measurement in om.Measurements)
            {
                measurements.Add(new GarmentPackingListMeasurementModel(measurement.Length, measurement.Width, measurement.Height, measurement.CartonsQuantity) { Id = measurement.Id });
            }
            var model = new GarmentPackingListModel(om.InvoiceNo, om.PackingListType, om.InvoiceType, om.SectionId, om.SectionCode, om.Date, om.PaymentTerm, om.LCNo, om.LCDate, om.IssuedBy, om.BuyerAgentId, om.BuyerAgentCode, om.BuyerAgentName, om.Destination, om.FinalDestination, om.ShipmentMode, om.TruckingDate, om.TruckingEstimationDate, om.ExportEstimationDate, om.Omzet, om.Accounting, om.FabricCountryOrigin, om.FabricComposition, om.RemarkMd, items, om.GrossWeight, om.NettWeight, om.TotalCartons, measurements, om.SayUnit, om.ShippingMark, om.SideMark, om.Remark, om.ShippingMarkImagePath, om.SideMarkImagePath, om.RemarkImagePath, om.IsUsed, om.IsPosted, om.ShippingStaffId, om.ShippingStaffName, om.Status, om.Description) { Id = om.Id };

            return model;
        }

    }
}
