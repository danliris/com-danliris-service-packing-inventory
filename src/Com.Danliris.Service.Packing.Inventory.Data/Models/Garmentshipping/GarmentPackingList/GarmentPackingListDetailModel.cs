using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListDetailModel : StandardEntity
    {
        public double Carton1 { get; private set; }
        public double Carton2 { get; private set; }
        public string Colour { get; private set; }
        public double CartonQuantity { get; private set; }
        public double QuantityPCS { get; private set; }
        public double TotalQuantity { get; private set; }

        public GarmentPackingListDetailModel()
        {
        }

        public GarmentPackingListDetailModel(double carton1, double carton2, string colour, double cartonQuantity, double quantityPCS, double totalQuantity)
        {
            Carton1 = carton1;
            Carton2 = carton2;
            Colour = colour;
            CartonQuantity = cartonQuantity;
            QuantityPCS = quantityPCS;
            TotalQuantity = totalQuantity;
        }
    }
}
