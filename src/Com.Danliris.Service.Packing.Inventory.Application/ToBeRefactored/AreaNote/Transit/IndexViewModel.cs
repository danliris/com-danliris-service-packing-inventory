using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Transit
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Group { get; set; }
        public string UnitName { get; set; }
        public string SourceArea { get; set; }
        public string ProductionOrderNo { get; set; }
        public string CartNo { get; set; }
        public string MaterialName { get; set; }
        public string MaterialConstructionName { get; set; }
        public string MaterialWidth { get; set; }
        public string Grade { get; set; }
        public string Motif { get; set; }
        public string Color { get; set; }
        public double MeterLength { get; set; }
        public double YardsLength { get; set; }
        public string Remark { get; set; }
    }
}
