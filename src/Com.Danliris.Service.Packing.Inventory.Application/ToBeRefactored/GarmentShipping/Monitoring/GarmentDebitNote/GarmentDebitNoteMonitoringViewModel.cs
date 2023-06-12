using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDebitNote
{
    public class GarmentDebitNoteMonitoringViewModel
    {
        public string DNNo { get; set; }
        public DateTimeOffset DNDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public double Amount { get; set; }
    }

    public class GarmentDebitNoteMIIMonitoringViewModel
    {
        public DateTimeOffset DNDate { get; set; }
        public DateTime DNDate1 { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string ReceiptNo { get; set; }
        public string BankName { get; set; }
        public string AccountBankNo { get; set; }
        public string InvoiceNo { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal AmountIDR { get; set; }
    }

    class CurrencyFilter
    {
        public DateTime date { get; set; }
        public string code { get; set; }
    }
}
