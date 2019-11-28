using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class ProductPackagingModel : StandardEntity
    {
        public ProductPackagingModel()
        {

        }

        public ProductPackagingModel(
            string code,
            string composition,
            string construction,
            int currencyId,
            string currency,
            string description,
            string design,
            string grade,
            string lot,
            string name,
            string packagingCode,
            decimal price,
            string productType,
            string skuId,
            string sku,
            string tags,
            int uomId,
            string uom,
            string width,
            string wovenType,
            string yarnType1,
            string yarnType2
        )
        {
            Code = code;
            Composition = composition;
            Construction = construction;
            Currency = currency;
            CurrencyId = currencyId;
            Description = description;
            Design = design;
            Grade = grade;
            Lot = lot;
            Name = name;
            PackagingCode = packagingCode;
            Price = price;
            ProductType = productType;
            SKU = sku;
            SKUId = skuId;
            Tags = tags;
            UOM = uom;
            UOMId = uomId;
            Width = width;
            WovenType = wovenType;
            YarnType1 = yarnType1;
            YarnType2 = yarnType2;
        }

        public string Code { get; set; }
        public string Composition { get; set; }
        public string Construction { get; set; }
        public string Currency { get; set; }
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public string Design { get; set; }
        public string Grade { get; set; }
        public string Lot { get; set; }
        public string Name { get; set; }
        public string PackagingCode { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public string SKU { get; set; }
        public string SKUId { get; set; }
        public string Tags { get; set; }
        public string UOM { get; set; }
        public int UOMId { get; set; }
        public string Width { get; set; }
        public string WovenType { get; set; }
        public string YarnType1 { get; set; }
        public string YarnType2 { get; set; }
    }
}