using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOItemModel : StandardEntity
    {
        public int ExportSalesDOId { get; private set; }
        public int ComodityId { get; private set; }
        public string ComodityCode { get; private set; }
        public string ComodityName { get; private set; }
        public string Description { get; private set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public double CartonQuantity { get; private set; }
        public double GrossWeight { get; private set; }
        public double NettWeight { get; private set; }
        public double Volume { get; private set; }

        public GarmentShippingExportSalesDOItemModel(int comodityId, string comodityCode, string comodityName, string description, double quantity, int uomId, string uomUnit, double cartonQuantity, double grossWeight, double nettWeight, double volume)
        {
            ComodityId = comodityId;
            ComodityCode = comodityCode;
            ComodityName = comodityName;
            Description = description;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            CartonQuantity = cartonQuantity;
            GrossWeight = grossWeight;
            NettWeight = nettWeight;
            Volume = volume;
        }

        public void SetComodityId(int comodityId, string userName, string userAgent)
        {
            if (ComodityId != comodityId)
            {
                ComodityId = comodityId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetComodityCode(string comodityCode, string userName, string userAgent)
        {
            if (ComodityCode != comodityCode)
            {
                ComodityCode = comodityCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetComodityName(string comodityName, string userName, string userAgent)
        {
            if (ComodityName != comodityName)
            {
                ComodityName = comodityName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDescription(string description, string userName, string userAgent)
        {
            if (Description != description)
            {
                Description = description;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetQuantity(double quantity, string userName, string userAgent)
        {
            if (Quantity != quantity)
            {
                Quantity = quantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomId(int uomId, string userName, string userAgent)
        {
            if (UomId != uomId)
            {
                UomId = uomId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomUnit(string uomUnit, string userName, string userAgent)
        {
            if (UomUnit != uomUnit)
            {
                UomUnit = uomUnit;
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
