using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Product
{
    public class ProductPackingBarcodeInfo
    {
        public ProductPackingBarcodeInfo(string code, int skuId, decimal quantity, string uomUnit, string packType, int packingId)
        {
            Code = code;
            SKUId = skuId;
            Quantity = quantity;
            UOMUnit = uomUnit;
            PackType = packType;
            PackingId = packingId;
        }

        public string Code { get; set; }
        public int SKUId { get; set; }
        public decimal Quantity { get; set; }
        public string UOMUnit { get; set; }
        public string PackType { get; set; }
        public int PackingId { get; set; }
    }
}
