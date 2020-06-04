using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory
{
    public class ProductPackingInventoryMovementModel : StandardEntity
    {
        public ProductPackingInventoryMovementModel()
        {

        }

        public ProductPackingInventoryMovementModel(
            int inventoryDocumentId,
            int productPackingId,
            int uomId,
            double quantity,
            string remark
            )
        {
            InventoryDocumentId = inventoryDocumentId;
            ProductSKUId = productPackingId;
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
