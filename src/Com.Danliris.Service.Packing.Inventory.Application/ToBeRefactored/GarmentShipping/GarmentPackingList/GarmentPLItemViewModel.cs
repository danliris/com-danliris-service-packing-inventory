using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
	public class GarmentPLItemViewModel
	{
		public string RONo { get; set; }
		public string Article { get; set; }

		public string Description { get; set; }
		public int ComodityId { get;  set; }
		public string ComodityCode { get;  set; }
		public string ComodityName { get;  set; }

	}
}
