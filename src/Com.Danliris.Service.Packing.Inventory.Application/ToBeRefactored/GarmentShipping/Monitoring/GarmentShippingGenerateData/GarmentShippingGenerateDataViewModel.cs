using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingGenerateData
{
    public class GarmentShippingGenerateDataViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string RONo { get; set; }
        public string SCNo { get; set; }
        public string PaymentTerm { get; set; }
        public string LCNo { get; set; }
        public string Destination { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Def { get; set; }
        public string Acc { get; set; }
        public string CurrencyCode { get; set; }
        public string ComodityCode { get; set; }
        public string ComodityName { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string PEBNo { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public DateTimeOffset SailingDate { get; set; }
        public decimal Amount { get; set; }
        public decimal ToBePaid { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubAmount { get; set; }
        public string UomUnit { get; set; }
    }
}
