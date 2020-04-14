using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentAval
{
    public class InventoryDocumentAvalViewModel : BaseViewModel
    {
        //public int Id { get; set; }
        public string Area { get; set; }
        public string Shift { get; set; }
        public string UOMUnit { get; set; }
        public double ProductionOrderQuantity { get; set; }
        public double QtyKg { get; set; }
    }
}