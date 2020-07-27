using Com.Danliris.Service.Packing.Inventory.Application.DTOs;

namespace Com.Danliris.Service.Packing.Inventory.Application
{
    public class BarcodeInfo
    {
        public BarcodeInfo(ProductSKUDto product, ProductPackingDto productPacking)
        {
            SKUCode = product.Code;
            SKUName = product.Name;
            PackingCode = productPacking.Code;
            PackingSize = productPacking.PackingSize;
            PackingType = productPacking.UOM.Unit;
        }

        public string SKUCode { get; }
        public string SKUName { get; }
        public string PackingCode { get; }
        public double PackingSize { get; }
        public string PackingType { get; }
    }
}