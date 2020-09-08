using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDOItemModel : StandardEntity
    {
        public int LocalSalesDOId { get; private set; }
        public int LocalSalesNoteItemId { get; private set; }
        public int ProductId { get; private set; }
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }
        public string Description { get; private set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public double CartonQuantity { get; private set; }
        public double GrossWeight { get; private set; }
        public double NettWeight { get; private set; }
        public double Volume { get; private set; }

        public GarmentShippingLocalSalesDOItemModel(int localSalesDOId, int localSalesNoteItemId, int productId, string productCode, string productName, string description, double quantity, int uomId, string uomUnit, double cartonQuantity, double grossWeight, double nettWeight, double volume)
        {
            LocalSalesDOId = localSalesDOId;
            LocalSalesNoteItemId = localSalesNoteItemId;
            ProductId = productId;
            ProductCode = productCode;
            ProductName = productName;
            Description = description;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            CartonQuantity = cartonQuantity;
            GrossWeight = grossWeight;
            NettWeight = nettWeight;
            Volume = volume;
        }

        public void SetDescription(string description, string userName, string userAgent)
        {
            if (Description != description)
            {
                Description = description;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCartonQuantity(double cartonQuantity, string userName, string userAgent)
        {
            if (CartonQuantity != cartonQuantity)
            {
                CartonQuantity = cartonQuantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetGrossWeight(double grossWeight, string userName, string userAgent)
        {
            if (GrossWeight != grossWeight)
            {
                GrossWeight = grossWeight;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetNettWeight(double nettWeight, string userName, string userAgent)
        {
            if (NettWeight != nettWeight)
            {
                NettWeight = nettWeight;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetVolume(double volume, string userName, string userAgent)
        {
            if (Volume != volume)
            {
                Volume = volume;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
