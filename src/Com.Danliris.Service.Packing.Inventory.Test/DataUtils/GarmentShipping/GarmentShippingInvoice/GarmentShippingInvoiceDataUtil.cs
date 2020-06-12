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
			var items = new HashSet<GarmentShippingInvoiceItemModel> { new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3) };
			var adjustments = new HashSet<GarmentShippingInvoiceAdjustmentModel> { new GarmentShippingInvoiceAdjustmentModel(1, "", 0) };
			var model = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", items,1000, 23, "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, adjustments,100000);

			return model;
		}

		public   GarmentShippingInvoiceModel GetModels()
		{
			var items = new HashSet<GarmentShippingInvoiceItemModel> {
				new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3),new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3) };
			var adjustments = new HashSet<GarmentShippingInvoiceAdjustmentModel> {
				new GarmentShippingInvoiceAdjustmentModel(1, "", 0),new GarmentShippingInvoiceAdjustmentModel(1,"ddd",1000) };
			var model = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", items, 1000, 23, "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, adjustments, 100000);

			return model;
		}

		public override GarmentShippingInvoiceModel GetEmptyModel()
		{
			var items = new HashSet<GarmentShippingInvoiceItemModel> { new GarmentShippingInvoiceItemModel(null, null, 0, null, 0, 0, null, null, null, 0, null, 0, 0, 0, null, 0, null, 0) };
			var adjustments = new HashSet<GarmentShippingInvoiceAdjustmentModel> { new GarmentShippingInvoiceAdjustmentModel(0, null, 0) };
			var model = new GarmentShippingInvoiceModel(0, null, DateTimeOffset.MinValue, null, null, 0, null, null, null, null, null, 0, null, null, DateTimeOffset.MinValue, null, 0, null, 0, null, 0, null, 0, "", DateTimeOffset.MinValue, "", DateTimeOffset.MinValue, "", items, 0, 0, "dsdsds", "memo", false, null, DateTimeOffset.MinValue, null, DateTimeOffset.MinValue, null, DateTimeOffset.MinValue, adjustments, 0);

			return model;
		}
	}
}
