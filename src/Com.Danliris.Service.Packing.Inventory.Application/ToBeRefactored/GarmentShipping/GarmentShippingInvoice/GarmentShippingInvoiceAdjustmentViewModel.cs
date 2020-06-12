using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceAdjustmentViewModel : BaseViewModel
	{
		public int GarmentShippingInvoiceId { get; set; }
		public string AdjustmentDescription { get; set; }
		public decimal AdjustmentValue { get; set; }
	}
}
