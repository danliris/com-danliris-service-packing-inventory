using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdviceMII
{
    public class GarmentShippingNoteCreditAdviceMIIMonitoringViewModel
    {
        public DateTimeOffset CADate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string ReceiptNo { get; set; }
        public string BankName { get; set; }
        public string AccountBankNo { get; set; }
        public string NoteNo { get; set; }
        public decimal Amount { get; set; }
        public decimal BankComission { get; set; }
        public decimal CreditInterest { get; set; }
        public decimal BankCharges { get; set; }
        public decimal InsuranceCharges { get; set; }
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
