using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory
{
    public class ProductPackingInventorySummaryModel : StandardEntity
    {
        public ProductPackingInventorySummaryModel()
        {

        }

        public ProductPackingInventorySummaryModel(
            int productPackingId,
            int storageId,
            string storageCode,
            string storageName,
            int uomId,
            double quantity
            )
        {
            ProductPackingId = productPackingId;
            StorageId = storageId;
            StorageCode = storageCode;
            StorageName = storageName;
            UOMId = uomId;
            Quantity = quantity;
        }

        public int ProductPackingId { get; private set; }
        public int StorageId { get; private set; }
        [MaxLength(64)]
        public string StorageCode { get; private set; }
        [MaxLength(512)]
        public string StorageName { get; private set; }
        public int UOMId { get; private set; }
        public double Quantity { get; private set; }
    }
}
