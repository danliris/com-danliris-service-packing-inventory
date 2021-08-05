using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment
{
    public class GarmentShipmentMonitoringViewModel
    {
        public int InvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string OriginPort { get; set; }
        public string DestinationPort { get; set; }
        public string BuyerAgentName { get; set; }
        public string ConsigneeName { get; set; }
        public string BuyerBrandName { get; set; }
        public string ComodityName { get; set; }    
        public string SectionCode { get; set; }
        public DateTimeOffset BookingDate { get; set; }
        public DateTimeOffset ExpFactoryDate { get; set; }    
        public int DiffBDCL { get; set; }
        public string PEBNo { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public DateTimeOffset SailingDate { get; set; }
        public string CONo { get; set; }
        public DateTimeOffset DocSendDate { get; set; }
        public int DiffETDDSD { get; set; }
        public int PaymentDue { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public int DiffDDPD { get; set; }
        public decimal Amount { get; set; }     
        public decimal CMTAmount { get; set; }
        public decimal CMTAmountSub { get; set; }
        public decimal LessfabricCost { get; set; }
        public decimal AdjustmentValue { get; set; }
        public string EMKLName { get; set; }
        public string ForwarderName { get; set; }
        public string ShippingStaffName { get; set; }
        public decimal AdjustmentAmount { get; set; }
    }
}
