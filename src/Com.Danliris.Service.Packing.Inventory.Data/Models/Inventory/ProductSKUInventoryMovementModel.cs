using Com.Moonlay.Models;

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
            double quantity,
            string remark
            )
        {
            InventoryDocumentId = inventoryDocumentId;
            ProductSKUId = productSKUId;
            UOMId = uomId;
            Quantity = quantity;
            Remark = remark;
        }

        public int InventoryDocumentId { get; }
        public int ProductSKUId { get; }
        public int UOMId { get; }
        public double Quantity { get; }
        public string Remark { get; }
    }
}
