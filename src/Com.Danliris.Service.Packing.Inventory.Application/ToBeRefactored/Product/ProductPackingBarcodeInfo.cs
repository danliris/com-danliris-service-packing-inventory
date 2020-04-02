using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Product
{
    public class ProductPackingBarcodeInfo
    {
        public ProductPackingBarcodeInfo(string packingCode, int skuId, decimal quantity, string uomUnitSKU, string packType, int packingId, string productSKUName)
        {
            PackingCode = packingCode;
            SKUId = skuId;
            Quantity = quantity;
            UOMUnitSKU = uomUnitSKU;
            PackingType = packType;
            PackingId = packingId;
            ProductSKUName = productSKUName;
        }

        public string PackingCode { get; set; }
        public int SKUId { get; set; }
        public decimal Quantity { get; set; }
        public string UOMUnitSKU { get; set; }
        public string PackingType { get; set; }
        public int PackingId { get; set; }
        public string ProductSKUName { get; set; }
    }
}
