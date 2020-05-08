using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class AvailableAvalIndexViewModel
    {
        //public AvailableAvalIndexViewModel()
        //{
        //    AvailableAvalItems = new HashSet<AvailableAvalItemViewModel>();
        //}

        public int AvalInputId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Area { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string BonNo { get; set; }
        //public string Group { get; set; }
        public int AvalItemId { get; set; }
        public string AvalType { get; set; }
        public string AvalCartNo { get; set; }
        public string AvalUomUnit { get; set; }
        public double AvalQuantity { get; set; }
        public double AvalQuantityKg { get; set; }
        //public ICollection<AvailableAvalItemViewModel> AvailableAvalItems { get; set; }
        ////public List<OutputAvalDyeingPrintingAreaMovementIdsViewModel> DyeingPrintingMovementIds { get; set; }
    }
}
