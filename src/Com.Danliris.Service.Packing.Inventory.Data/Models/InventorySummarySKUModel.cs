using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class InventorySummarySKUModel : StandardEntity
    {
        public InventorySummarySKUModel()
        {

        }

        public InventorySummarySKUModel(
            decimal quantity,
            int skuId,
            string storage,
            int storageId,
            string uomUnit
            )
        {
            Quantity = quantity;
            SKUId = skuId;
            Storage = storage;
            StorageId = storageId;
            UOMUnit = uomUnit;
        }


        public decimal Quantity { get; private set; }
        public int SKUId { get; private set; }
        public string Storage { get; private set; }
        public int StorageId { get; private set; }
        public string UOMUnit { get; private set; }
    }
}
