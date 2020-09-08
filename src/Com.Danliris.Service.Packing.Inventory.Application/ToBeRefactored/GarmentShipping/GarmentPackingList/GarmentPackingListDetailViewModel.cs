using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
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

        public ICollection<GarmentPackingListDetailSizeViewModel> Sizes { get; set; }
    }
}
