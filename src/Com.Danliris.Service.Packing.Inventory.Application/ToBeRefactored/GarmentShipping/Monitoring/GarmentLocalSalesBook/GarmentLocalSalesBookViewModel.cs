using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesBook
{
	public class GarmentLocalSalesBookViewModel
    {
        public string LSNo { get; set; }
        public DateTimeOffset LSDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public decimal QtyTotal { get; set; }
        public decimal NettAmount { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal PPNAmount { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionType { get; set; }
        public decimal Qty1 { get; set; }
        public decimal Amount1 { get; set; }
        public decimal Qty2 { get; set; }
        public decimal Amount2 { get; set; }
        public decimal Qty3 { get; set; }
        public decimal Amount3 { get; set; }
        public decimal Qty4 { get; set; }
        public decimal Amount4 { get; set; }
        public decimal Qty5 { get; set; }
        public decimal Amount5 { get; set; }
    }

    public class GarmentLocalSalesBookTempViewModel
    {
        public string LSNo { get; set; }
        public DateTimeOffset LSDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionType { get; set; }
        public double Quantity { get; set; }
        public string UseVat { get; set; }
        public string IncludeVat { get; set; }
        public decimal DPP { get; set; }
        public decimal PPN { get; set; }
        public decimal Total { get; set; }
    }
}
