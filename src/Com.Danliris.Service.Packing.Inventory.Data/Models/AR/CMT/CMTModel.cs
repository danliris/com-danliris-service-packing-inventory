using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.AR.CMT
{
    public class CMTModel : StandardEntity
    {
        [MaxLength(64)]
        public string InvoiceNo { get; set; }
        public DateTime TruckingDate { get; set; }
        public DateTime PEBDate { get; set; }
        [MaxLength(64)]
        public string ExpenditureGoodNo { get; set; }
        [MaxLength(20)]
        public string RONo { get; set; }
        public double Quantity { get; set; }
        public double Amount { get; set; }
        public double Kurs { get; set; }
        public double TotalAmount { get; set; }

        public int Month { get; set; }
    }
}
