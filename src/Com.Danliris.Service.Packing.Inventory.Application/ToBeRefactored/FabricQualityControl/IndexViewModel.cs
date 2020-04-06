using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string CartNo { get; set; }
        public string Code { get; set; }
        public DateTimeOffset? DateIm { get; set; }
        public bool? IsUsed { get; set; }
        public string MachineNoIm { get; set; }
        public string OperatorIm { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductionOrderType { get; set; }
        public string ShiftIm { get; set; }
        public string DyeingPrintingAreaMovementBonNo { get; set; }
    }
}
