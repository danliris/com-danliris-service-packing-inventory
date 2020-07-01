using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory
{
    public class ProductSKUInventoryMovementModel : StandardEntity
    {
        public ProductSKUInventoryMovementModel()
        {

        }

        public ProductSKUInventoryMovementModel(
            int inventoryDocumentId,
            int productSKUId,
            int uomId,
            int storageId,
            double quantity,
            string type,
            string remark
            )
        {
            InventoryDocumentId = inventoryDocumentId;
            ProductSKUId = productSKUId;
            StorageId = storageId;
            UOMId = uomId;
            Quantity = quantity;
            Type = type;
            Remark = remark;
        }

        public int InventoryDocumentId { get; private set; }
        public int ProductSKUId { get; private set; }
        public int UOMId { get; private set; }
        public int StorageId { get; private set; }
        public double Quantity { get; private set; }
        public double PreviousBalance { get; private set; }
        public double CurrentBalance { get; private set; }
        [MaxLength(32)]
        public string Type { get; private set; }
        public string Remark { get; private set; }

        public void SetPreviousBalance(double balance)
        {
            PreviousBalance = balance;
        }

        public void SetCurrentBalance(double quantity)
        {
            CurrentBalance = PreviousBalance + quantity;
        }

        public void AdjustCurrentBalance(double quantity)
        {
            CurrentBalance = quantity;
        }
    }
}
