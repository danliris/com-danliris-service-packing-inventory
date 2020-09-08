using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl
{
    public class CriteriaViewModel
    {
        public int? Id { get; set; }
       
        public string Code { get; set; }
        public string Group { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Score Score { get; set; }
    }

    public class Score
    {
        public double? A { get; set; }
        public double? B { get; set; }
        public double? C { get; set; }
        public double? D { get; set; }
    }
}
