using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public class DocumentIndexDto
    {
        public DocumentIndexDto(IEnumerable<ProductSKUInventoryDocumentModel> documents, int page, int size, int total)
        {
            data = documents;
            this.page = page;
            this.size = size;
            this.total = total;
        }

        public IEnumerable<ProductSKUInventoryDocumentModel> data { get; private set; }
        public int page { get; private set; }
        public int size { get; private set; }
        public int total { get; private set; }
    }
}