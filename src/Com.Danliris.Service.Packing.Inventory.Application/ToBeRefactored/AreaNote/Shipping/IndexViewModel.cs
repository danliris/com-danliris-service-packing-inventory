using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Shipping
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public string BonNo { get; set; }
        public string ProductionOrderNo { get; set; }
        public string Construction { get; set; }
        public string Motif { get; set; }
        public string Color { get; set; }
        public string Grade { get; set; }
        public decimal PackagingQty { get; set; }
        public string PackagingUnit { get; set; }
        public double MTRLength { get; set; }
        public double YDSLength { get; set; }
        //New Property
        //public double MassKg { get; set; }
    }
}
