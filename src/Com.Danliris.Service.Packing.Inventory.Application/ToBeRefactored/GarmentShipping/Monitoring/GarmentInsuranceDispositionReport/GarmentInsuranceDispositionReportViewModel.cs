using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInsuranceDispositionReport
{
	public class GarmentInsuranceDispositionReportViewModel
    {
        public string DispositionNo { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public string PolicyType { get; set; }
        public string BankName { get; set; }
        public string InsuranceCode { get; set; }
        public string InsuranceName { get; set; }
        public decimal Rate { get; set; }
        public string PolicyNo { get; set; }
        public DateTimeOffset PolicyDate { get; set; }
        public string InvoiceNo { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountC1A { get; set; }
        public decimal AmountC1B { get; set; }
        public decimal AmountC2A { get; set; }
        public decimal AmountC2B { get; set; }
        public decimal AmountC2C { get; set; }
        public decimal PremiAmount { get; set; }
    }
}
