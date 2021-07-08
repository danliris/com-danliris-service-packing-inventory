using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCoverLetter
{
    public class GarmentCoverLetterMonitoringViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset CLDate { get; set; }
        public DateTimeOffset BookingDate { get; set; }
        public DateTimeOffset ExportDate { get; set; }
        public string EMKLName { get; set; }
        public string ForwarderName { get; set; }
        public string Destination { get; set; }
        public string Address { get; set; }
        public string PIC { get; set; }
        public string ATTN { get; set; }
        public string Phone { get; set; }
        public string ShippingStaffName { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public double TotalCarton { get; set; }
        public string ContainerNo { get; set; }
        public string ShippingSeal { get; set; }
        public string DLSeal { get; set; }
        public double PCSQuantity { get; set; }
        public double SETSQuantity { get; set; }
        public double PACKQuantity { get; set; }
        public string Truck { get; set; }
        public string PlateNumber { get; set; }
        public string DriverName { get; set; }
        public string UnitName { get; set; }
    }
}
