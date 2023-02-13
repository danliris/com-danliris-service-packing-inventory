using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport
{
    public class IndexViewModel
    {
        public int id { get; set; }

        public string noteNo { get; set; }
        public string exportCoverLetterNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public Buyer buyer { get; set; }
        public string bcNo { get; set; }
        public DateTimeOffset? bcDate { get; set; }
    }
}
