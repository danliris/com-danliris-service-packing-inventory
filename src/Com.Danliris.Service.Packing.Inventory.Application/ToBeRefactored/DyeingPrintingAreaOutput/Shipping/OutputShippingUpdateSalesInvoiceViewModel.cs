using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingUpdateSalesInvoiceViewModel
    {
        public OutputShippingUpdateSalesInvoiceViewModel()
        {
            ItemIds = new HashSet<int>();
        }

        public bool HasSalesInvoice { get; set; }
        public IEnumerable<int> ItemIds { get; set; }
    }
}
