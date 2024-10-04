using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.AR.OmzetCorrectionsModel
{
    public class OmzetCorrectionModel : StandardEntity
    {
        [MaxLength(30)]
        public string MemoNo { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        [MaxLength(10)]
        public string BuyerCode { get; set; }
        [MaxLength(64)]
        public string InvoiceNo { get; set; }
        public double Amount { get; set; }

        public double Kurs { get; set; }
        public double TotalAmount { get; set; }

        public int Month { get; set; }
    }
}
