using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class AvalInputIndexViewModelDataUtil
    {
        public IndexViewModel GetModel()
        {
            return new IndexViewModel()
            {
                Id = 1,
                Date = DateTimeOffset.UtcNow,
                BonNo = "IM.GA.20.001",
                Shift = "PAGI"
            };
        }
    }
}
