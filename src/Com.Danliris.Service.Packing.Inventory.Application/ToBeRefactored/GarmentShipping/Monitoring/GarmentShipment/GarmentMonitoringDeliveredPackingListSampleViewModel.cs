using System;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment
{
    public class GarmentMonitoringDeliveredPackingListSampleViewModel
    {
        public string InvoiceNo { get; set; }
        public string PackingListType { get; set; }
        public string InvoiceType { get; set; }
        public string Section { get; set; }
        public DateTimeOffset Date { get; set; }
        public string PaymentTerm { get; set; }
        public string BuyerAgent { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string Destination { get; set; }
        public string CreatedBy { get; set; }

        public string RONo { get; set; }
        public string Article { get; set; }
        public string Comodity { get; set; }
        public double Quantity { get; set; }

        public int Index { get; set; }
        public double Carton1 { get; set; }
        public double Carton2 { get; set; }
        public string Style { get; set; }
        public string Colour { get; set; }
        public double CartonQuantity { get; set; }
        public double QuantityPCS { get; set; }

        public string Size { get; set; }
        public double SizeQuantity { get; set; }

    }
}
