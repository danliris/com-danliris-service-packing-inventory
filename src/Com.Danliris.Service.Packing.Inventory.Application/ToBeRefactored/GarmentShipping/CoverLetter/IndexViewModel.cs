using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter
{
    public class IndexViewModel
    {
        public int id { get; set; }

        public string invoiceNo { get; set; }

        public DateTimeOffset? date { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string attn { get; set; }
        public string phone { get; set; }
        public DateTimeOffset? bookingDate { get; set; }

        public string order { get; set; }
    }
}
