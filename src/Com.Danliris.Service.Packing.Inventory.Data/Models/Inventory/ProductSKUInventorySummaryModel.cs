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
            int uomId
            )
        {
            ProductSKUId = productSKUId;
            StorageId = storageId;
            StorageCode = storageCode;
            StorageName = storageName;
            UOMId = uomId;
        }

        public int ProductSKUId { get; private set; }
        public int StorageId { get; private set; }
        [MaxLength(64)]
        public string StorageCode { get; private set; }
        [MaxLength(512)]
        public string StorageName { get; private set; }
        public int UOMId { get; private set; }
        public double Balance { get; private set; }

        public void AddBalance(double quantity)
        {
            Balance += quantity;
        }

        public void ReduceBalance(double quantity)
        {
            Balance -= quantity;
        }

        public void AdjustBalance(double quantity)
        {
            Balance = quantity;
        }

        public void SetBalance(double quantity)
        {
            Balance = quantity;
        }
    }
}
