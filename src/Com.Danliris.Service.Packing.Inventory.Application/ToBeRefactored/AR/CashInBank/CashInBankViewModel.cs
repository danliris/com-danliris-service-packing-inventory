using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CashInBank
{
    public class CashInBankViewModel
    {
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string BuyerCode { get; set; }
        public int Month { get; set; }

        //Terima
        public ReceiptViewModel Receipt { get; set; }

        #region Piutang
        public string InvoiceNo { get; set; }

        //Jumlah Cair
        public LiquidAmountViewModel Liquid { get; set; }

        //Saldo Buku
        public BookBalanceViewModel BookBalance { get; set; }

        //Selisih Kurs
        public double DifferenceKurs { get; set; }
        #endregion

        #region Biaya/Penghasilan Lain2
        public CostViewModel Cost { get; set; }
        #endregion
    }

    public class LiquidAmountViewModel
    {
        public double LiquidAmount { get; set; }
        public double LiquidAmountKurs { get; set; }
        public double LiquidTotalAmount { get; set; }
    }

    public class BookBalanceViewModel
    {
        public double BookBalanceKurs { get; set; }
        public double BookBalanceTotalAmount { get; set; }
    }

    public class CostViewModel
    {
        public string COA { get; set; }
        public string UnitCode { get; set; }
        public string Remark { get; set; }
        public string SupportingDocument { get; set; }
        public double Amount { get; set; }
        public double TotalAmount { get; set; }
    }

    public class ReceiptViewModel
    {
        public double ReceiptAmount { get; set; }
        public double ReceiptKurs { get; set; }
        public double ReceiptTotalAmount { get; set; }
    }

}
