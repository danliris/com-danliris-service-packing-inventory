using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.AR.RecapOmzet
{
    public class RecapOmzetModel : StandardEntity
    {
        public DateTime TruckingDate { get; set; } // TruckingDate
        [MaxLength(10)]
        public string BuyerAgentCode { get; set; } // BuyerAgentCode
        [MaxLength(50)]
        public string Destination { get; set; } // Destination
        [MaxLength(30)]
        public string InvoiceNo { get; set; } // InvoiceNo
        public DateTime InvoiceDate { get; set; } // InvoiceDate
        [MaxLength(10)]
        public string PEBNo { get; set; } // PEBNo
        public DateTime PEBDate { get; set; } // PEBDate
        public double Quantity { get; set; } // Sum of Quantity
        [MaxLength(8)]
        public string UOMUnit { get; set; } // UOMUnit
        [MaxLength(10)]
        public string CurrencyCode { get; set; } // CurrencyCode
        public double Rate { get; set; } // Rate
        public double Amount { get; set; } // Sum of Amount
        public double AmountIDR { get; set; } // Sum of AmountIDR

        public int Month { get; set; }
    }
}
