namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public class FormItemDto
    {
        public int ProductSKUId { get; internal set; }
        public int UOMId { get; internal set; }
        public double Quantity { get; internal set; }
        public string Remark { get; internal set; }
    }
}