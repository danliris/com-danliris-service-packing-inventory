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
        public string Group { get; set; }
    }
}
