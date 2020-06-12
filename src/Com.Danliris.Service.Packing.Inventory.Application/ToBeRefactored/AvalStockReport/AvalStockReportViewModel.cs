using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalStockReport
{
    public class AvalStockReportViewModel
    {
        public string AvalType { get; set; }
        public double StartAvalQuantity { get; set; }
        public double StartAvalWeightQuantity { get; set; }
        public double InAvalQuantity { get; set; }
        public double InAvalWeightQuantity { get; set; }
        public double OutAvalQuantity { get; set; }
        public double OutAvalWeightQuantity { get; set; }
        public double EndAvalQuantity { get; set; }
        public double EndAvalWeightQuantity { get; set; }
    }
}
