using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceDataUtil : BaseDataUtil<GarmentShippingInvoiceRepository, GarmentShippingInvoiceModel>
	{
		public GarmentShippingInvoiceDataUtil(GarmentShippingInvoiceRepository repository,GarmentPackingListDataUtil garmentPackingListDataUtil) : base(repository)
		{
		}

		public override GarmentShippingInvoiceModel GetModel()
		{
			var items = new HashSet<GarmentShippingInvoiceItemModel> { new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", "comodesc", "comodesc", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3) };
			var adjustments = new HashSet<GarmentShippingInvoiceAdjustmentModel> { new GarmentShippingInvoiceAdjustmentModel(1, "", 0, 1) };
            var units = new HashSet<GarmentShippingInvoiceUnitModel> { new GarmentShippingInvoiceUnitModel(1, "", 1,1), new GarmentShippingInvoiceUnitModel(2, "", 1, 1) };
            var model = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", items,1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, adjustments,100000,"","",units);
            
            return model;
		}

		public   GarmentShippingInvoiceModel GetModels()
		{
			var items = new HashSet<GarmentShippingInvoiceItemModel> {
				new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc","comodesc","comodesc","comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3),new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc","comodesc","comodesc","comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3) };
			var adjustments = new HashSet<GarmentShippingInvoiceAdjustmentModel> {
				new GarmentShippingInvoiceAdjustmentModel(1, "", 0,1),new GarmentShippingInvoiceAdjustmentModel(1,"ddd",1000,1) };
            var units = new HashSet<GarmentShippingInvoiceUnitModel> { new GarmentShippingInvoiceUnitModel(1, "", 1, 1), new GarmentShippingInvoiceUnitModel(2, "", 1, 1) };
            var model = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", items, 1000, "sss", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, adjustments, 100000, "","",units);

			return model;
		}

		public override GarmentShippingInvoiceModel GetEmptyModel()
		{
			var items = new HashSet<GarmentShippingInvoiceItemModel> { new GarmentShippingInvoiceItemModel(null, null, 0, null, 0, 0, null, null, null, null, null, null, 0, null, 0, 0, 0, null, 0, null, 0) };
			var adjustments = new HashSet<GarmentShippingInvoiceAdjustmentModel> { new GarmentShippingInvoiceAdjustmentModel(0, null, 0, 1) };
            var units = new HashSet<GarmentShippingInvoiceUnitModel> { new GarmentShippingInvoiceUnitModel(0, null, 0, 0) };
            var model = new GarmentShippingInvoiceModel(0, null, DateTimeOffset.MinValue, null, null, 0, null, null, null, null, null, 0, null, null, DateTimeOffset.MinValue, null, 0, null, 0, null, 0, null, 0, "", DateTimeOffset.MinValue, "", DateTimeOffset.MinValue, "", items, 0, "", "dsdsds", "memo", false, null, DateTimeOffset.MinValue, null, DateTimeOffset.MinValue, null, DateTimeOffset.MinValue, adjustments, 0,null,null,units);

			return model;
		}

        public GarmentShippingInvoiceModel CopyModel(GarmentShippingInvoiceModel om)
        {
            var items = new HashSet<GarmentShippingInvoiceItemModel>();
            foreach (var i in om.Items)
            {
                
                items.Add(new GarmentShippingInvoiceItemModel(i.RONo, i.SCNo, i.BuyerBrandId, i.BuyerBrandName, i.Quantity, i.ComodityId, i.ComodityCode, i.ComodityName, i.ComodityDesc,i.Desc2,i.Desc3,i.Desc4, i.UomId, i.UomUnit, i.Price, i.PriceRO, i.Amount, i.CurrencyCode, i.UnitId, i.UnitCode, i.CMTPrice) { Id = i.Id });
            }
            var adjs = new HashSet<GarmentShippingInvoiceAdjustmentModel>();
            foreach (var adj in om.GarmentShippingInvoiceAdjustment)
            {
                adjs.Add(new GarmentShippingInvoiceAdjustmentModel(om.Id,adj.AdjustmentDescription,adj.AdjustmentValue,adj.AdditionalChargesId) { Id = adj.Id });
            }
            var units = new HashSet<GarmentShippingInvoiceUnitModel>();
            foreach (var unit in om.GarmentShippingInvoiceUnit)
            {
                units.Add(new GarmentShippingInvoiceUnitModel(unit.UnitId,unit.UnitCode,unit.AmountPercentage,unit.QuantityPercentage) { Id = unit.Id });
            }
            var model = new GarmentShippingInvoiceModel(om.PackingListId, om.InvoiceNo,om.InvoiceDate,om.From,om.To,om.BuyerAgentId,om.BuyerAgentCode,om.BuyerAgentName,om.Consignee,
                om.LCNo,om.IssuedBy,om.SectionId,om.SectionCode,om.ShippingPer,om.SailingDate,om.ConfirmationOfOrderNo,om.ShippingStaffId,om.ShippingStaff,om.FabricTypeId,om.FabricType,
                om.BankAccountId,om.BankAccount,om.PaymentDue,om.PEBNo,om.PEBDate,om.NPENo, om.NPEDate,om.Description,items,om.AmountToBePaid,om.CPrice,"",om.Memo,om.IsUsed,om.BL,om.BLDate,
                om.CO,om.CODate,om.COTP,om.COTPDate,adjs, om.TotalAmount, om.ConsigneeAddress, om.DeliverTo, units) { Id = om.Id };

            return model;
        }
    }
}
