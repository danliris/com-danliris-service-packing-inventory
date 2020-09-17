using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingInstruction
{
    public class GarmentShippingInstructionMonitoringViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset SIDate { get; set; }
        public string ForwarderCode { get; set; }
        public string ForwarderName { get; set; }
        public string ShippingStaffName { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public string CartonNo { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string PortOfDischarge { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string ContainerNo { get; set; }
        public double PCSQuantity { get; set; }
        public double SETSQuantity { get; set; }
        public double PACKQuantity { get; set; }
        public double GrossWeight { get; set; }
        public double NettWeight { get; set; }
    }
}
