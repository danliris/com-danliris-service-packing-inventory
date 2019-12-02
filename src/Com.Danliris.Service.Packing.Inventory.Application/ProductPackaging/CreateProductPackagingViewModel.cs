using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductPackaging
{
    public class CreateProductPackagingViewModel
    {
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public int? SKUId { get; set; }
        public CommonViewModelObjectProperties.ProductSKU SKU { get; set; }
        public string Tags { get; set; }
        public UnitOfMeasurement UOM { get; set; }
    }
}