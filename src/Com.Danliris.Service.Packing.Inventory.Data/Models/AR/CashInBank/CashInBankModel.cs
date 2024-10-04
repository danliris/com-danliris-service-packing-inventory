using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.AR.CashInBank
{
    public class CashInBankModel : StandardEntity
    {
        [MaxLength(30)]
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        [MaxLength(10)]
        public string BuyerCode { get; set; }

        //Terima
        public double ReceiptAmount { get; set; }

        public double ReceiptKurs { get; set; }
        public double ReceiptTotalAmount { get; set; }

        #region Piutang
        [MaxLength(64)]
        public string InvoiceNo { get; set; }

        //Jumlah Cair
        public double LiquidAmount { get; set; }
        public double LiquidTotalAmount { get; set; }

        //Saldo Buku
        public double BookBalanceKurs { get; set; }
        public double BookBalanceTotalAmount { get; set; }

        //Selisih Kurs
        public double DifferenceKurs { get; set; }
        #endregion

        #region Biaya/Penghasilan Lain2
        [MaxLength(10)]
        public string COA { get; set; }
        [MaxLength(10)]
        public string UnitCode { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        [MaxLength(64)]
        public string SupportingDocument { get; set; }
        public double Amount { get; set; }
        public double TotalAmount { get; set; }
        #endregion

        public int Month { get; set; }


    }
}
