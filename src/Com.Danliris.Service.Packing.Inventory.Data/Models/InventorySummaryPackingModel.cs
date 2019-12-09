using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class InventorySummaryPackingModel : StandardEntity
    {
        public InventorySummaryPackingModel()
        {

        }

        public InventorySummaryPackingModel(
            int packingId,
            decimal quantity,
            int skuId,
            string storage,
            int storageId,
            string uomUnit
            )
        {
            PackingId = packingId;
            Quantity = quantity;
            SKUId = skuId;
            Storage = storage;
            StorageId = storageId;
            UOMUnit = uomUnit;
        }


        public int PackingId { get; private set; }
        public decimal Quantity { get; private set; }
        public int SKUId { get; private set; }
        public string Storage { get; private set; }
        public int StorageId { get; private set; }
        public string UOMUnit { get; private set; }
    }
}
