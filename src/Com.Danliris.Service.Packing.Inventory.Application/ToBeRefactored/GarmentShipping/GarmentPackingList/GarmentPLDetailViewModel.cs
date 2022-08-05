using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
	public class GarmentPLDetailViewModel
	{
		public string Style { get; set; }
		public string Colour { get; set; }
		public ICollection<GarmentPackingListDetailSizeViewModel> Sizes { get; set; }

	}
}
