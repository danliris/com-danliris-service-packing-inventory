using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class InventoryMovementPackingModel : StandardEntity
    {
        public InventoryMovementPackingModel()
        {

        }

        public InventoryMovementPackingModel(
            decimal afterQuantity,
            decimal beforeQuantity,
            int packingId,
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
            PackingId = packingId;
            Quantity = quantity;
            SKUId = skuId;
            Storage = storage;
            StorageId = storageId;
            Type = type;
            UOMUnit = uomUnit;
        }

        public decimal AfterQuantity { get; private set; }
        public decimal BeforeQuantity { get; private set; }
        public int PackingId { get; private set; }
        public decimal Quantity { get; private set; }
        public int SKUId { get; private set; }
        public string Storage { get; private set; }
        public int StorageId { get; private set; }
        public string Type { get; private set; }
        public string UOMUnit { get; private set; }
    }
}
