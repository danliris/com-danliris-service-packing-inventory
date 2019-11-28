using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class PackagingInventoryDocumentItemModel : StandardEntity
    {
        public PackagingInventoryDocumentItemModel()
        {

        }

        public PackagingInventoryDocumentItemModel(
            string packaging,
            int packagingId,
            int packagingInventoryDocumentId,
            decimal quantity,
            string remark,
            string sku,
            int skuId
        )
        {
            Packaging = packaging;
            PackagingId = packagingId;
            PackagingInventoryDocumentId = packagingInventoryDocumentId;
            Quantity = quantity;
            Remark = remark;
            SKU = sku;
            SKUId = skuId;
        }

        public string Packaging { get; set; }
        public int PackagingId { get; set; }
        public int PackagingInventoryDocumentId { get; set; }
        public decimal Quantity { get; set; }
        public string Remark { get; set; }
        public string SKU { get; set; }
        public int SKUId { get; set; }
    }
}