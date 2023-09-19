using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice
{
    public class GarmentBuyerReceivablesReportViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public string BuyerAgentName { get; set; }       
        public decimal ToBePaid { get; set; }
        public decimal DHLCharges { get; set; }
        public DateTimeOffset SailingDate { get; set; }
        public double PaymentDue { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public double BankCharges { get; set; }
        public double OtherCharges { get; set; }
        public double BankComission { get; set; }
        public double CreditInterest { get; set; }
        public double DiscrepancyFee { get; set; }
        public double ReceiptAmount { get; set; }
        public double OutStandingAmount { get; set; }
        public string BankDetail { get; set; }
        public string ReceiptNo { get; set; }
        public double OverDue { get; set; }
    }
}
