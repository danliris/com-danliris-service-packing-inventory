using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class AvailableAvalIndexViewModel
    {
        public AvailableAvalIndexViewModel()
        {
            AvalItems = new HashSet<AvailableAvalItemViewModel>();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public ICollection<AvailableAvalItemViewModel> AvalItems { get; set; }
    }
}
