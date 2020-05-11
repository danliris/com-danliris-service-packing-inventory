using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalViewModel : BaseViewModel
    {
        public InputAvalViewModel()
        {
            AvalItems = new HashSet<InputAvalItemViewModel>();
        }

        public string Area { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string BonNo { get; set; }
        public string Group { get; set; }
        public ICollection<InputAvalItemViewModel> AvalItems { get; set; }
        public List<InputAvalDyeingPrintingAreaMovementIdsViewModel> DyeingPrintingMovementIds { get; set; }
    }
}
