using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public class GarmentFinanceDetailLocalSalesJournalTempViewModel
    {
        public string NoteNo { get; set; }
        public DateTimeOffset NoteDate { get; set; }
        public string BuyerName { get; set; }
        public string NoteType { get; set; }
        public string CurrencyCode { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double VatAmount { get; set; }
        public string COACode { get; set; }
        public string COAName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }

    public class GarmentFinanceDetailLocalSalesJournalViewModel
    {
        public string NoteNo { get; set; }
        public DateTimeOffset NoteDate { get; set; }
        public string BuyerName { get; set; }
        public string NoteType { get; set; }
        public string CurrencyCode { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double VatAmount { get; set; }
        public string COACode { get; set; }
        public string COAName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }

        public string UseVat { get; set; }
    }
}
