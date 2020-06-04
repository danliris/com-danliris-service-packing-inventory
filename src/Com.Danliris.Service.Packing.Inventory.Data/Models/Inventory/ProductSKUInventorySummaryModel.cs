using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory
{
    public class ProductSKUInventorySummaryModel : StandardEntity
    {
        public ProductSKUInventorySummaryModel()
        {

        }

        public ProductSKUInventorySummaryModel(
            int productSKUId,
            int storageId,
            string storageCode,
            string storageName,
            int uomId,
            double quantity
            )
        {
            ProductSKUId = productSKUId;
            StorageId = storageId;
            StorageCode = storageCode;
            StorageName = storageName;
            UOMId = uomId;
            Quantity = quantity;
        }

        public int ProductSKUId { get; }
        public int StorageId { get; }
        [MaxLength(64)]
        public string StorageCode { get; }
        [MaxLength(256)]
        public string StorageName { get; }
        public int UOMId { get; }
        public double Quantity { get; }
    }
}
