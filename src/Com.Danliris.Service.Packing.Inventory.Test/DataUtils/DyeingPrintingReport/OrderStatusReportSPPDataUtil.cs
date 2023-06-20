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
            IList<long> data = new List<long>();
            data.Add(1);

            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("data", data);
            return result;
        }

        public string GetResultFormatterOkString()
        {
            var result = GetResultFormatterOk();

            return JsonConvert.SerializeObject(result);
        }
    }
}
