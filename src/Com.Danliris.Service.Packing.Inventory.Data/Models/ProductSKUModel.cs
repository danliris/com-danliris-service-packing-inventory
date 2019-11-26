using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class ProductSKUModel : StandardEntity
    {
        public ProductSKUModel()
        {

        }

        public ProductSKUModel(
            string Code,
            string Composition,
            string Construction,
            string Currency,
            string Description,
            string Design,
            string Grade,
            string Lot,
            string Name,
            decimal Price,
            string ProductType,
            string SKUId,
            string SKUCode,
            string Tags,
            string UOM,
            string Width,
            string WovenType,
            string YarnType1,
            string YarnType2
        )
        {
        }

        public string Code { get; set; }
        public string Composition { get; set; }
        public string Construction { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string Design { get; set; }
        public string Grade { get; set; }
        public string Lot { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public string SKUId { get; set; }
        public string SKUCode { get; set; }
        public string Tags { get; set; }
        public string UOM { get; set; }
        public string Width { get; set; }
        public string WovenType { get; set; }
        public string YarnType1 { get; set; }
        public string YarnType2 { get; set; }
    }
}