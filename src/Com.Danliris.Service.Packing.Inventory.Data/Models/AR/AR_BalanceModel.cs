using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.AR
{
    public class AR_BalanceModel : StandardEntity
    {
        public DateTime TruckingDate { get; set; } // TruckingDate
        [MaxLength(10)]
        public string BuyerAgentCode { get; set; } // BuyerAgentCode
        [MaxLength(30)]
        public string InvoiceNo { get; set; } // InvoiceNo
        public DateTime PEBDate { get; set; } // PEBDate

        public double Rate { get; set; } // Rate
        public double Amount { get; set; } // Sum of Amount
        public double AmountIDR { get; set; } // Sum of AmountIDR

        public int Month { get; set; }
    }
}
