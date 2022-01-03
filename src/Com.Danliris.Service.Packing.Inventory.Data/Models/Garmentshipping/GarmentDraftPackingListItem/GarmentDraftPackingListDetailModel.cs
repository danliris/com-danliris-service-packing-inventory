using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListDetailModel : StandardEntity
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

        public double GrossWeight { get; private set; }
        public double NetWeight { get; private set; }
        public double NetNetWeight { get; private set; }

        public int Index { get; private set; }

        public ICollection<GarmentDraftPackingListDetailSizeModel> Sizes { get; private set; }

        public GarmentDraftPackingListDetailModel()
        {
            Sizes = new List<GarmentDraftPackingListDetailSizeModel>();
        }

        public GarmentDraftPackingListDetailModel(double carton1, double carton2, string style, string colour, double cartonQuantity, double quantityPCS, double totalQuantity, double length, double width, double height, double grossWeight, double netWeight, double netNetWeight, ICollection<GarmentDraftPackingListDetailSizeModel> sizes, int index)
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
            GrossWeight = grossWeight;
            NetWeight = netWeight;
            NetNetWeight = netNetWeight;
            Sizes = sizes;
            Index = index;
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

        public void SetGrossWeight(double newValue, string userName, string userAgent)
        {
            if (GrossWeight != newValue)
            {
                GrossWeight = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetNetWeight(double newValue, string userName, string userAgent)
        {
            if (NetWeight != newValue)
            {
                NetWeight = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetNetNetWeight(double newValue, string userName, string userAgent)
        {
            if (NetNetWeight != newValue)
            {
                NetNetWeight = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIndex(int newValue, string userName, string userAgent)
        {
            if (Index != newValue)
            {
                Index = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
