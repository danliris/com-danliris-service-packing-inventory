using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public class DocumentIndexDto
    {
        public DocumentIndexDto(IEnumerable<ProductSKUInventoryDocumentModel> documents, int page, int size)
        {
            Data = documents;
            Page = page;
            Size = size;
        }

        public IEnumerable<ProductSKUInventoryDocumentModel> Data { get; }
        public int Page { get; }
        public int Size { get; }
    }
}