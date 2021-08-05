using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDebiturBalance
{
    public class IndexViewModel
    {
        public int id { get; set; }
        public int buyerAgentId { get; set; }
        public string buyerAgentCode { get; set; }
        public string buyerAgentName { get; set; }
        public DateTimeOffset? balanceDate { get; set; }
        public decimal balanceAmount { get; set; }
    }
}
