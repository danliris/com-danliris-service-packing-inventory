using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace om.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CMT
{
    public class CMTViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTime TruckingDate { get; set; }
        public DateTime PEBDate { get; set; }
        public string ExpenditureGoodNo { get; set; }
        public string RONo { get; set; }
        public double Quantity { get; set; }
        public double Amount { get; set; }
        public double Kurs { get; set; }
        public double TotalAmount { get; set; }
        public int Month { get; set; }
    }
}
