using System.Collections.Generic;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;

namespace Com.Danliris.Service.Packing.Inventory.Application.ReceivingDocument
{
    public class CreateReceivingDocumentViewModel
    {
        public Storage Storage { get; set; }
        public List<ReceivingItem> Items { get; set; }
    }

    public class ReceivingItem
    {
        public ProductPackaging ProductPackaging { get; set; }
        public ProductSKU ProductSKU { get; set; }
    }

    public class ProductPackaging
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ProductSKU SKU { get; set; }
    }

    public class ProductSKU
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}