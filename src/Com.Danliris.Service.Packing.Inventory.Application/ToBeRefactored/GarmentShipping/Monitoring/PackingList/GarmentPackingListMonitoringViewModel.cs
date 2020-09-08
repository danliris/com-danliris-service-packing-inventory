using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList
{
    public class GarmentPackingListMonitoringViewModel
    {
        public int id { get; set; }
        public string invoiceNo { get; set; }
        public DateTimeOffset date { get; set; }
        public string buyerAgentCode { get; set; }
        public string buyerAgentName { get; set; }
        public string sectionCode { get; set; }
        public DateTimeOffset truckingDate { get; set; }
        public DateTimeOffset exportEstimationDate { get; set; }
        public string destination { get; set; }
        public string lcNo { get; set; }
        public string issuedBy { get; set; }
        public double grossWeight { get; set; }
        public double nettWeight { get; set; }
        public double totalCarton { get; set; }
    }
}
