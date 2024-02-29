using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.ShipmentLocalSalesNote
{
    public class ShipmentLocalSalesNoteVM
    {
        public string LocalCoverLetterNo { get; set; }
        public DateTimeOffset LocalCoverLetterDate { get; set; }
        public string BCNo { get; set; }
        public string LocalSalesNoteNo { get; set; }
        public DateTimeOffset LocalSalesNoteDate { get; set; }
        public string ShippingStaff { get; set; }
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate  { get; set; }
        public string BuyerName { get; set; }
        public string UnitName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public string UomUnit { get; set; }
        public double DPP { get; set; }
        public double PPn { get; set; }
        public double Total { get; set; }
    }
}
