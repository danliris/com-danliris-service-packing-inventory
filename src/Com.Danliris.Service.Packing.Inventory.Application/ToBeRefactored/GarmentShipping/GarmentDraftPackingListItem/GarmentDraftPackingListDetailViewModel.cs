using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListDetailViewModel : BaseViewModel
    {
        public double Carton1 { get; set; }
        public double Carton2 { get; set; }
        public string Style { get; set; }
        public string Colour { get; set; }
        public double CartonQuantity { get; set; }
        public double QuantityPCS { get; set; }
        public double TotalQuantity { get; set; }

        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double CartonsQuantity { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double NetNetWeight { get; set; }

        public int PackingListItemId { get; set; }

        public int Index { get; set; }

        public ICollection<GarmentDraftPackingListDetailSizeViewModel> Sizes { get; set; }
    }
}
