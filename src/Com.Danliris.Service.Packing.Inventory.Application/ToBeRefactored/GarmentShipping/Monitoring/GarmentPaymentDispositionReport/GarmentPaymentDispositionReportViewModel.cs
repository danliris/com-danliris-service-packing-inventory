using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionReport
{
	public class GarmentPaymentDispositionReportViewModel
    {
        public string DispositionNo { get; set; }
        public string PaymentType { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTerm { get; set; }
        public string BankName { get; set; }
        public string AccNumber { get; set; }
        public string XpdcCode { get; set; }
        public string XpdcName { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public string InvoiceTaxNumber { get; set; }
        public decimal BillValue { get; set; }
        public decimal VatValue { get; set; }
        public decimal IncomeTaxRate { get; set; }
        public decimal IncomeTaxValue { get; set; }
        public decimal TotalBill { get; set; }
        public string UnitCode { get; set; }
        public decimal UnitPercentage { get; set; }
        public decimal UnitAmount { get; set; }
    }
}
