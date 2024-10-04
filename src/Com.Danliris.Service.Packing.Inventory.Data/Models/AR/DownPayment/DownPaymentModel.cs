using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.AR.DownPayment
{
    public class DownPaymentModel : StandardEntity
    {
        [MaxLength(30)]
        public string MemoNo { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        [MaxLength(30)]
        public string ReceiptNo { get; set; }

        public DateTime Date { get; set; }
        public DateTime OffsetDate { get; set; }

        [MaxLength(64)]
        public string InvoiceNo { get; set; }
        public double Amount { get; set; }

        public double Kurs { get; set; }
        public double TotalAmount { get; set; }

        public int Month { get; set; }
    }
}