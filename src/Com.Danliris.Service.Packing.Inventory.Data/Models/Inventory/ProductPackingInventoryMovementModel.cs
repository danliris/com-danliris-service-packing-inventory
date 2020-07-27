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

        public int InventoryDocumentId { get; private set; }
        public int ProductSKUId { get; private set; }
        public int UOMId { get; private set; }
        public double Quantity { get; private set; }
        public string Remark { get; private set; }
    }
}
