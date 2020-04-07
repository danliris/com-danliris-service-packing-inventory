using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionBalanceIM
{
    public class IndexViewModel
    {
        public string Material { get; set; }
        public string NoOrder { get; set; }
        public decimal SumOfAwal { get; set; }
        public decimal SumOfMasuk { get; set; }
        public decimal SumOfKeluar { get; set; }
        public decimal SumOfAkhir { get; set; }

    }
}
