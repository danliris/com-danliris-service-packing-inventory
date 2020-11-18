using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdvice
{
    public class GarmentCreditAdviceMonitoringViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public double Amount { get; set; }
        public double ToBePaid { get; set; }
        public double NettNego { get; set; }
        public string PaymentTerm { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public string BankName { get; set; }
        public DateTimeOffset DocUploadDate { get; set; }    
        // TT
        public double NettNegoTT { get; set; }
        public double BankChargeTT { get; set; }
        public double OtherChargeTT { get; set; }
        // LC
        public string SRNo { get; set; }
        public DateTimeOffset SRDate { get; set; }
        public string LCNo { get; set; }
        public double NettNegoLC { get; set; }
        public double BankChargeLC { get; set; }
        public double BankComissionLC { get; set; }
        public double DiscreapancyFeeLC { get; set; }
        public double CreditInterestLC { get; set; }
    }
}
