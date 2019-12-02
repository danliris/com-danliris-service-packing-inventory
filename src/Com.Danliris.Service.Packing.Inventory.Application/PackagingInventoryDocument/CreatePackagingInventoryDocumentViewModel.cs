using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;

namespace Com.Danliris.Service.Packing.Inventory.Application.PackagingInventoryDocument
{
    public class CreatePackagingInventoryDocumentViewModel
    {
        public string Composition { get; set; }
        public string Construction { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public string Design { get; set; }
        public string Grade { get; set; }
        public string Lot { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string ProductType { get; set; }
        public string Tags { get; set; }
        public UnitOfMeasurement UOM { get; set; }
        public string Width { get; set; }
        public string WovenType { get; set; }
        public string YarnType1 { get; set; }
        public string YarnType2 { get; set; }
    }
}