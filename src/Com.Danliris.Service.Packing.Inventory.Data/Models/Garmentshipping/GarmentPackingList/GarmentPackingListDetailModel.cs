using Com.Moonlay.Models;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListDetailModel : StandardEntity
    {
        public int PackingListItemId { get; private set; }

        public double Carton1 { get; private set; }
        public double Carton2 { get; private set; }
        public string Style { get; private set; }
        public string Colour { get; private set; }
        public double CartonQuantity { get; private set; }
        public double QuantityPCS { get; private set; }
        public double TotalQuantity { get; private set; }

        public double Length { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public double CartonsQuantity { get; private set; }

        public double GrossWeight { get; private set; }
        public double NetWeight { get; private set; }
        public double NetNetWeight { get; private set; }

        public ICollection<GarmentPackingListDetailSizeModel> Sizes { get; private set; }

        public GarmentPackingListDetailModel()
        {
            Sizes = new List<GarmentPackingListDetailSizeModel>();
        }

        public GarmentPackingListDetailModel(double carton1, double carton2, string style, string colour, double cartonQuantity, double quantityPCS, double totalQuantity, double length, double width, double height, double cartonsQuantity, double grossWeight, double netWeight, double netNetWeight, ICollection<GarmentPackingListDetailSizeModel> sizes)
        {
            Carton1 = carton1;
            Carton2 = carton2;
            Style = style;
            Colour = colour;
            CartonQuantity = cartonQuantity;
            QuantityPCS = quantityPCS;
            TotalQuantity = totalQuantity;
            Length = length;
            Width = width;
            Height = height;
            CartonsQuantity = cartonsQuantity;
            GrossWeight = grossWeight;
            NetWeight = netWeight;
            NetNetWeight = netNetWeight;
            Sizes = sizes;
        }

        public void setPackingListItemId(int PackingListItemId, string userName, string userAgent)
        {
            if (this.PackingListItemId != PackingListItemId)
            {
                this.PackingListItemId = PackingListItemId;
                this.FlagForUpdate(userName, userAgent);
            }
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

        public void SetStyle(string newValue, string userName, string userAgent)
        {
            if (Style != newValue)
            {
                Style = newValue;
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

        public void SetLength(double newValue, string userName, string userAgent)
        {
            if (Length != newValue)
            {
                Length = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetWidth(double newValue, string userName, string userAgent)
        {
            if (Width != newValue)
            {
                Width = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetHeight(double newValue, string userName, string userAgent)
        {
            if (Height != newValue)
            {
                Height = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCartonsQuantity(double newValue, string userName, string userAgent)
        {
            if (CartonsQuantity != newValue)
            {
                CartonsQuantity = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
