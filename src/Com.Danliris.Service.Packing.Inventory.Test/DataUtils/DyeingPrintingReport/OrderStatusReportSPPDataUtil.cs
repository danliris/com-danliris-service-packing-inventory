using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.DyeingPrintingReport
{
    public class OrderStatusReportSPPDataUtil
    {

        public Dictionary<string, object> GetResultFormatterOk()
        {
            List<OrderQuantityForStatusOrder> datas = new List<OrderQuantityForStatusOrder>();
            OrderQuantityForStatusOrder data = new OrderQuantityForStatusOrder()
            {
                OrderId=1,
                OrderQuantity=1
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
