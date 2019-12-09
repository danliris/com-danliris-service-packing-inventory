using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking
{
    public class CreateInventoryDocumentPackingViewModel
    {
        public ICollection<CreateInventoryDocumentPackingItemViewModel> Items { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public string Remark { get; set; }
        public Storage Storage { get; set; }
        public string Type { get; set; }
    }
}