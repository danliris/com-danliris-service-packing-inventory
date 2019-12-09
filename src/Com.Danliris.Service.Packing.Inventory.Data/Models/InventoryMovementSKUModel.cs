using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class InventoryMovementSKUModel : StandardEntity
    {
        public InventoryMovementSKUModel()
        {

        }

        public InventoryMovementSKUModel(
            decimal afterQuantity,
            decimal beforeQuantity,
            decimal quantity,
            int skuId,
            string storage,
            int storageId,
            string type,
            string uomUnit
            )
        {
            AfterQuantity = afterQuantity;
            BeforeQuantity = beforeQuantity;
            Quantity = quantity;
            SKUId = skuId;
            Storage = storage;
            StorageId = storageId;
            Type = type;
            UOMUnit = uomUnit;
        }
        

        public decimal AfterQuantity { get; private set; }
        public decimal BeforeQuantity { get; private set; }
        public decimal Quantity { get; private set; }
        public int SKUId { get; private set; }
        public string Storage { get; private set; }
        public int StorageId { get; private set; }
        public string Type { get; private set; }
        public string UOMUnit { get; private set; }
    }
}
