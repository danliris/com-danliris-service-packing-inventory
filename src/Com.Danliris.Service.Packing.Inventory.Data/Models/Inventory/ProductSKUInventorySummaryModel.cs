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
            int uomId
            )
        {
            ProductSKUId = productSKUId;
            StorageId = storageId;
            UOMId = uomId;
        }

        public int ProductSKUId { get; private set; }
        public int StorageId { get; private set; }
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
