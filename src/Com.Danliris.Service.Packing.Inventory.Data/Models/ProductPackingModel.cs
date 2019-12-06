using System;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class ProductPackingModel : StandardEntity
    {
        public ProductPackingModel()
        {

        }

        public ProductPackingModel(
            string code,
            string packType,
            decimal quantity,
            int skuId
        )
        {
            Code = code;
            Quantity = quantity;
            SKUId = skuId;
            PackType = packType;
        }

        public string Code { get; set; }
        public string PackType { get; set; }
        public decimal Quantity { get; private set; }
        public int SKUId { get; private set; }
    }
}