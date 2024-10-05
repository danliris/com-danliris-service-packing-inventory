using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.AR_ReportMutation
{
    public class AR_TempToCalculate
    {
        public string NoInvoice { get; set; }
        public double Amount { get; set; }
        public double Rate { get; set; }
        public double TotalAmount { get; set; }
        public int Bulan { get; set; }
    }
}
