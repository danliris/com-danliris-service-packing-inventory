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

        public int ProductPackingId { get; }
        public int StorageId { get; }
        [MaxLength(64)]
        public string StorageCode { get; }
        [MaxLength(512)]
        public string StorageName { get; }
        public int UOMId { get; }
        public double Quantity { get; }
    }
}
