using Com.Danliris.Service.Packing.Inventory.Application.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport
{
    public class SPPResult : BaseResult
    {
        public SPPResult()
        {
            data = new List<long>();
        }
        public IList<long> data { get; set; }
    }
    public class ProductionResult : BaseResult
    {
        public ProductionResult()
        {
            data = new List<ProductionViewModel>();
        }
        public IList<ProductionViewModel> data { get; set; }
    }
    public class ProductionViewModel
    {
        public double qtyin { get; set; }
        public string noorder { get; set; }
    }
}
