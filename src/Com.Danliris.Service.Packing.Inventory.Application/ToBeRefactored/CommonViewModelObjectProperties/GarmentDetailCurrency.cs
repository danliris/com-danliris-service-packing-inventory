using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties
{
    public class GarmentDetailCurrency
    {
        public int? id { get; set; }
        public string code { get; set; }
        public string symbol { get; set; }
        public DateTime date { get; set; }
        public double rate { get; set; }
    }
}
