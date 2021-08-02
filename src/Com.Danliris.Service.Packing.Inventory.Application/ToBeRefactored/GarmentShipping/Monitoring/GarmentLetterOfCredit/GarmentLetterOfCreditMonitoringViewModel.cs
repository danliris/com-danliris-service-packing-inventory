using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLetterOfCredit
{
    public class GarmentLetterOfCreditMonitoringViewModel
    {
        public string LCNo { get; set; }
        public DateTimeOffset LCDate { get; set; }
        public string IssuedBank { get; set; }
        public string ApplicantName { get; set; }
        public string ExpiredPlace { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
        public DateTimeOffset LatestShipment { get; set; }
        public string LCCondition { get; set; }
        public string InvoiceNo { get; set; }
        public decimal AmountToBePaid { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public double Quantity { get; set; }
        public string UomUnit { get; set; }
        public double AmountLC { get; set; }    
    }
}
