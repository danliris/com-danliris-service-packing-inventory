using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionBalanceIM
{
    public class InspectionBalanceIMViewModel
    {
        public string Material { get; set; }
        public string NoOrder { get; set; }
        public double SumOfAwal { get; set; }
        public double SumOfMasuk { get; set; }
        public double SumOfKeluar { get; set; }
        public double SumOfAkhir { get; set; }
    }
}
