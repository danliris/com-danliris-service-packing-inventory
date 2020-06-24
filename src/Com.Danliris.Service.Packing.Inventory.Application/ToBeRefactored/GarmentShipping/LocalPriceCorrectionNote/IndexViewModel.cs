using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote
{
    public class IndexViewModel
    {
        public int id { get; set; }

        public string correctionNoteNo { get; set; }
        public SalesNote salesNote { get; set; }
    }

    public class SalesNote
    {
        public int id { get; set; }

        public string noteNo { get; set; }
        public Buyer buyer { get; set; }
        public DateTimeOffset? date { get; set; }
        public int tempo { get; set; }
        public string dispositionNo { get; set; }
    }
}
