using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDetailViewModel : BaseViewModel
    {
        public double Carton1 { get; set; }
        public double Carton2 { get; set; }
        public string Colour { get; set; }
        public double CartonQuantity { get; set; }
        public double QuantityPCS { get; set; }
        public double TotalQuantity { get; set; }

        public List<DetailSize> Sizes { get; set; }
    }

    public class DetailSize
    {
        public string Size { get; set; }
        public double Quantity { get; set; }
    }
}
