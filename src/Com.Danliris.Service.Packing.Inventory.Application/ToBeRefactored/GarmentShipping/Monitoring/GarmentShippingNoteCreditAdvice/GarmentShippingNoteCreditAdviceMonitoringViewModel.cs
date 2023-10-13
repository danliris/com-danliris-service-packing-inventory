using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceMonitoringViewModel
    {
        public int CAId { get; set; }
        public string NoteNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Amount { get; set; }
        public double PaidAmount { get; set; }
        public double BalanceAmount { get; set; }
        public double BankComission { get; set; }
        public double CreditInterest { get; set; }
        public double BankCharge { get; set; }
        public double InsuranceCharge { get; set; }
        public double NettNego { get; set; }
        public string PaymentTerm { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public string BuyerName { get; set; }
        public string BankName { get; set; }
    }
}
