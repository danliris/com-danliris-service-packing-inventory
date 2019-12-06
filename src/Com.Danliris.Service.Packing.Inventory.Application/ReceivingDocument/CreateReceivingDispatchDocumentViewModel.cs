using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ReceivingDocument
{
    public class CreateReceivingDispatchDocumentViewModel
    {
        public ICollection<CreateReceivingDispatchDocumentItemViewModel> Items { get; set; }
        public Storage Storage { get; set; }
    }
}
