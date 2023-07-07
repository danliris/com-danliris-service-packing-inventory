using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public class GarmentFinanceDetailExportSalesJournalTempViewModel
    {
        public string invoicetype { get; set; }
        public string invoiceno { get; set; }
        public DateTimeOffset truckingdate { get; set; }
        public DateTimeOffset pebdate { get; set; }
        public string buyer { get; set; }
        public string pebno { get; set; }
        public string rono { get; set; }
        public string currencycode { get; set; }
        public decimal rate { get; set; }
        public double qty { get; set; }
        public decimal price { get; set; }
        public decimal amount { get; set; }
        public decimal vatamount { get; set; }
        public double amountcc { get; set; }        
    }

    public class GarmentFinanceDetailExportSalesJournalViewModel
    {
        public string invoicetype { get; set; }
        public string invoiceno { get; set; }
        public DateTimeOffset truckingdate { get; set; }
        public string buyer { get; set; }
        public string pebno { get; set; }
        public string currencycode { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
        public decimal vatamount { get; set; }
        public double amountcc { get; set; }
        public string coaname { get; set; }
        public string account { get; set; }
        public decimal debit { get; set; }
        public decimal credit { get; set; }
    }
}
