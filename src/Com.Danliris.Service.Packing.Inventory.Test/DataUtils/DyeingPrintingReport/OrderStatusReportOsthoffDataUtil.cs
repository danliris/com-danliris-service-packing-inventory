using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.DyeingPrintingReport
{
    public class OrderStatusReportOsthoffDataUtil
    {
        public Dictionary<string, object> GetResultFormatterOk()
        {
            List<ProductionViewModel> datas = new List<ProductionViewModel>();
            ProductionViewModel data = new ProductionViewModel()
            {
                noorder="OrderNo",
                qtyin=1
            };
            datas.Add(data);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("data", datas);
            return result;
        }

        public string GetResultFormatterOkString()
        {
            var result = GetResultFormatterOk();

            return JsonConvert.SerializeObject(result);
        }
    }
}
