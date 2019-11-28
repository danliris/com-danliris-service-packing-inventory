using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class SKUInventoryDocumentItemModel : StandardEntity
    {
         public SKUInventoryDocumentItemModel()
        {

        }

        public SKUInventoryDocumentItemModel(
            decimal quantity,
            string remark,
            string sku,
            int skuId,
            int skuInventoryDocumentId
        )
        {
            Quantity = quantity;
            Remark = remark;
            SKU = sku;
            SKUId = skuId;
            SkuInventoryDocumentId = skuInventoryDocumentId;
        }

        public decimal Quantity { get; set; }
        public string Remark { get; set; }
        public string SKU { get; set; }
        public int SKUId { get; set; }
        public int SkuInventoryDocumentId { get; set; }
    }
}