using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesOmzet
{
    public class GarmentLocalSalesOmzetViewModel
    {
        public string LSNo { get; set; }
        public DateTimeOffset LSDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string UseVat { get; set; }
        public decimal DPP{ get; set; }
        public decimal PPN{ get; set; }
        public decimal Total { get; set; }
    }
}
