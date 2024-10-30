using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.DownPayment
{
    public class DownPaymentList
    {
        public int Id { get; set; }
        public DateTime LastModifiedUtc { get; set; }
        public string MemoNo { get; set; }
        public string Remark { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime OffsetDate { get; set; }
        public string InvoiceNo { get; set; }
        public double Amount { get; set; }
        public double Kurs { get; set; }
        public double TotalAmount { get; set; }

        //OmzetCorrection
        public string BuyerCode { get; set; }
        public int Month { get; set; }
    }
}
