namespace Com.Danliris.Service.Packing.Inventory.Application.ReceivingDispatchDocument
{
    public class CreateReceivingDispatchDocumentItemViewModel
    {
        public string Code { get; set; }
        public int? SKUId { get; set; }
        public decimal? Quantity { get; set; }
        public string UOMUnit { get; set; }
        public string PackingType { get; set; }
        public int? PackingId { get; set; }
    }
}