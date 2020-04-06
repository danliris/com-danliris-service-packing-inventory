using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl
{
    public class FabricGradeTestViewModel : BaseViewModel
    {
        public double? AvalLength { get; set; }
        public List<CriteriaViewModel> Criteria { get; set; }
        public double? FabricGradeTest { get; set; }
        public double? FinalArea { get; set; }
        public double? FinalGradeTest { get; set; }
        public double? FinalLength { get; set; }
        public double? FinalScore { get; set; }
        public string Grade { get; set; }
        public double? InitLength { get; set; }
        public string PcsNo { get; set; }
        public double? PointLimit { get; set; }
        public double? PointSystem { get; set; }
        public double? SampleLength { get; set; }
        public double? Score { get; set; }
        public string Type { get; set; }
        public double? Width { get; set; }
    }
}
