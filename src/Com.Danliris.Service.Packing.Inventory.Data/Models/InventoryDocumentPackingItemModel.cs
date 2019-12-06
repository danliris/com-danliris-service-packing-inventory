using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class InventoryDocumentPackingItemModel : StandardEntity
    {
        public InventoryDocumentPackingItemModel()
        {
                
        }

        public InventoryDocumentPackingItemModel(
            int packingId,
            decimal quantity,
            int skuId,
            string uomUnit
            )
        {
            PackingId = packingId;
            Quantity = quantity;
            SKUId = skuId;
            UOMUnit = uomUnit;
        }

        public int InventoryDocumentSKUId { get; set; }
        public int PackingId { get; set; }
        public decimal Quantity { get; set; }
        public int SKUId { get; set; }
        public string UOMUnit { get; set; }
    }
}