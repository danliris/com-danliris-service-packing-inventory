using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Product
{
    public class ProductPackingBarcodeInfo
    {
        public ProductPackingBarcodeInfo(string code, int skuId, decimal quantity, string uomUnit, string packingType)
        {
            Code = code;
            SKUId = skuId;
            Quantity = quantity;
            UOMUnit = uomUnit;
            PackingType = packingType;
        }

        public string Code { get; set; }
        public int SKUId { get; set; }
        public decimal Quantity { get; set; }
        public string UOMUnit { get; set; }
        public string PackingType { get; set; }
    }
}
