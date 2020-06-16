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

        public int InventoryDocumentId { get; private set; }
        public int ProductSKUId { get; private set; }
        public int UOMId { get; private set; }
        public double Quantity { get; private set; }
        public string Remark { get; private set; }
    }
}
