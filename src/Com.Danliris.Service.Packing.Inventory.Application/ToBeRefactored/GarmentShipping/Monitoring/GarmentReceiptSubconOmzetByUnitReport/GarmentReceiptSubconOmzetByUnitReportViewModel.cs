using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentReceiptSubconOmzetByUnitReport
{
    public class GarmentReceiptSubconOmzetByUnitReportViewModel
    {
        public string LocalSalesNoteNo { get; set; }
        public DateTimeOffset LocalSalesNoteDate { get; set; }
        public string LocalSalesContractNo { get; set; }
        public string BuyerName { get; set; }
        public string BuyerBrandName { get; set; }
        public string PaymentTerm { get; set; }
        public string RONo { get; set; }
        public string Article { get; set; }
        public string PackingOutNo { get; set; }
        public string SCNo { get; set; }
        public string ComodityName { get; set; }
        public string UnitCode { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
