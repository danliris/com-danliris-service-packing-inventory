using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalViewModel : BaseViewModel
    {
        public OutputAvalViewModel()
        {
            AvalItems = new HashSet<OutputAvalItemViewModel>();
        }

        public string Area { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public string Shift { get; set; }
        //public string BonNo { get; set; }
        public int InputAvalId { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public ICollection<OutputAvalItemViewModel> AvalItems { get; set; }
    }
}
