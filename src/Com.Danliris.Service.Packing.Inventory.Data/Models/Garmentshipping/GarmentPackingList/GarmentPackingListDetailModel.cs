using Com.Moonlay.Models;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListDetailModel : StandardEntity
    {
        public int PackingListItemId { get; private set; }

        public double Carton1 { get; private set; }
        public double Carton2 { get; private set; }
        public string Colour { get; private set; }
        public double CartonQuantity { get; private set; }
        public double QuantityPCS { get; private set; }
        public double TotalQuantity { get; private set; }

        public ICollection<GarmentPackingListDetailSizeModel> Sizes { get; private set; }

        public GarmentPackingListDetailModel()
        {
            Sizes = new List<GarmentPackingListDetailSizeModel>();
        }

        public GarmentPackingListDetailModel(double carton1, double carton2, string colour, double cartonQuantity, double quantityPCS, double totalQuantity, ICollection<GarmentPackingListDetailSizeModel> sizes)
        {
            Carton1 = carton1;
            Carton2 = carton2;
            Colour = colour;
            CartonQuantity = cartonQuantity;
            QuantityPCS = quantityPCS;
            TotalQuantity = totalQuantity;
            Sizes = sizes;
        }

        public void SetCarton1(double newValue, string userName, string userAgent)
        {
            if (Carton1 != newValue)
            {
                Carton1 = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCarton2(double newValue, string userName, string userAgent)
        {
            if (Carton2 != newValue)
            {
                Carton2 = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetColour(string newValue, string userName, string userAgent)
        {
            if (Colour != newValue)
            {
                Colour = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCartonQuantity(double newValue, string userName, string userAgent)
        {
            if (CartonQuantity != newValue)
            {
                CartonQuantity = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetQuantityPCS(double newValue, string userName, string userAgent)
        {
            if (QuantityPCS != newValue)
            {
                QuantityPCS = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTotalQuantity(double newValue, string userName, string userAgent)
        {
            if (TotalQuantity != newValue)
            {
                TotalQuantity = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
