using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string BonNo { get; set; }
        public string Shift { get; set; }
    }
}
