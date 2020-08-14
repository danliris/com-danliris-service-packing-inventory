using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesReportByBuyer
{
    public class GarmentLocalSalesReportByBuyerViewModel
    {
        public string BuyerName { get; set; }
        public List<GarmentLocalSalesReportByBuyerBuyerViewModel> LocalSalesNo { get; set; }
    }

    public class GarmentLocalSalesReportByBuyerBuyerViewModel
    {
        public string NoteNo { get; set; }
        public double Quantities { get; set; }
        public decimal Amounts { get; set; }

        public List<GarmentLocalSalesReportByBuyerDetailViewModel> Details { get; set; }
    }

    public class GarmentLocalSalesReportByBuyerDetailViewModel
    {
        public DateTimeOffset LSDate { get; set; }
        public string LSType { get; set; }
        public int Tempo { get; set; }
        public string DispoNo { get; set; }
        public string Remark { get; set; }
        public string UseVat { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public double Price { get; set; }
        public decimal Amount { get; set; }
    }

    public class GarmentLocalSalesReportByBuyerListViewModel
    {
        public string LSNo { get; set; }
        public DateTimeOffset LSDate { get; set; }
        public string LSType { get; set; }
        public string BuyerName { get; set; }
        public int Tempo { get; set; }
        public string DispoNo { get; set; }
        public string Remark { get; set; }
        public string UseVat { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public double Price { get; set; }
        public decimal Amount { get; set; }
    }
}
