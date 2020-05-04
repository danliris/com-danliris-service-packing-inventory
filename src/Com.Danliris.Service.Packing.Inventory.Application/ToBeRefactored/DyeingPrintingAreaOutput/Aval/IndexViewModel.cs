using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string BonNo { get; set; }
        public string Shift { get; set; }
        public string CartNo { get; set; }
        public string AvalType { get; set; }
        public string UomUnit { get; set; }
        public double Qty { get; set; }
        public double QtyKg { get; set; }
    }
}
