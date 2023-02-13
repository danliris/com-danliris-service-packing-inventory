using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport
{
    public class GarmentShippingLeftOverExportSalesDOItemModel : StandardEntity
    {
        public int ExportSalesDOId { get; private set; }
        public int ExportSalesNoteItemId { get; private set; }
        public int ProductId { get; private set; }
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }
        public string Description { get; private set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public double PackQuantity { get; private set; }
        public int PackUomId { get; private set; }
        public string PackUomUnit { get; private set; }
        public double GrossWeight { get; private set; }
        public double NettWeight { get; private set; }

        public GarmentShippingLeftOverExportSalesDOItemModel(int exportSalesDOId, int exportSalesNoteItemId, int productId, string productCode, string productName, string description, double quantity, int uomId, string uomUnit, double packQuantity, int packUomId, string packUomUnit, double grossWeight, double nettWeight)
        {
            ExportSalesDOId = exportSalesDOId;
            ExportSalesNoteItemId = exportSalesNoteItemId;
            ProductId = productId;
            ProductCode = productCode;
            ProductName = productName;
            Description = description;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            PackQuantity = packQuantity;
            PackUomId = packUomId;
            PackUomUnit = packUomUnit;
            GrossWeight = grossWeight;
            NettWeight = nettWeight;
        }

        public void SetDescription(string description, string userName, string userAgent)
        {
            if (Description != description)
            {
                Description = description;
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

    }
}
