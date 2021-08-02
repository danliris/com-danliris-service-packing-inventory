using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearSection
{
    public class OmzetYearSectionViewModel
    {
        public HashSet<string> sections { get; internal set; }
        public List<OmzetYearSectionTableViewModel> tables { get; set; }
        public Dictionary<string, decimal> totals { get; set; }
        public Dictionary<string, decimal> averages { get; set; }
    }

    public class OmzetYearSectionTableViewModel
    {
        public string month { get; set; }
        public Dictionary<string, decimal> items { get; set; }
    }

    class JoinedData
    {
        public DateTimeOffset month { get; set; }
        public int sectionId { get; set; }
        public string section { get; set; }
        public List<JoinedDataItem> items { get; set; }
    }

    class JoinedDataItem
    {
        public decimal amount { get; set; }
    }

    class SelectedData
    {
        public string month { get; set; }
        public string section { get; set; }
        public decimal amount { get; set; }
    }
}
