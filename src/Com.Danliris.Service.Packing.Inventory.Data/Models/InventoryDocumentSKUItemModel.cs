using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class InventoryDocumentSKUItemModel : StandardEntity
    {
        public InventoryDocumentSKUItemModel()
        {

        }

        public InventoryDocumentSKUItemModel(decimal quantity, int skuId, string uomUnit)
        {
            Quantity = quantity;
            SKUId = skuId;
            UOMUnit = uomUnit;
        }

        public decimal BeforeQuantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public int InventoryDocumentSKUId { get; set; }
        public decimal Quantity { get; set; }
        public int SKUId { get; set; }
        public string UOMUnit { get; set; }
    }
}