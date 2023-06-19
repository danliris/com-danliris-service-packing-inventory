using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Helper
{
    public class BaseResult
    {
        public string apiVersion { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
    }
}
