using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionRecapReport
{
	public class GarmentPaymentDispositionRecapReportViewModel
    {
        public string RecapNo { get; set; }
        public DateTimeOffset RecapDate { get; set; }
        public string EmklCode { get; set; }
        public string EmklName { get; set; }
        public string EmklAddress { get; set; }
        public string EmklNPWP { get; set; }
        public int DispositionId { get; set; }
        public string DispositionNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceTaxNumber { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public decimal BillValue { get; set; }
        public double ServiceValue { get; set; }
        public decimal IncomeTaxValue { get; set; }
        public decimal TotalBill { get; set; }
    }
}
