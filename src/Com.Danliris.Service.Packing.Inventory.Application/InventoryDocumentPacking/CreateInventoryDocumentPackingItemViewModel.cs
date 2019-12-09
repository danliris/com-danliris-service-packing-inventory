namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking
{
    public class CreateInventoryDocumentPackingItemViewModel
    {
        public int? PackingId { get; set; }
        public int? SKUId { get; set; }
        public decimal? Quantity { get; set; }
        public string UOMUnit { get; set; }
    }
}