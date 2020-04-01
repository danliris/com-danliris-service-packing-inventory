namespace Com.Danliris.Service.Packing.Inventory.Application.Product
{
    public class CreateProductPackAndSKUViewModel
    {
        // SKU Field
        public string Composition { get; set; }
        public string Construction { get; set; }
        public string Design { get; set; }
        public string Grade { get; set; }
        public string LotNo { get; set; }
        public string ProductType { get; set; }
        public string UOMUnit { get; set; }
        public string Width { get; set; }
        public string WovenType { get; set; }
        public string YarnType1 { get; set; }
        public string YarnType2 { get; set; }

        // Pack Field
        public decimal? Quantity { get; set; }
        public string PackType { get; set; }
    }
}
