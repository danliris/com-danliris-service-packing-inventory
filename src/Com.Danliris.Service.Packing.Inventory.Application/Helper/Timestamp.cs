using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.Helper
{
    public class Timestamp
    {
        private const string TIMESTAMP_FORMAT = "yyyyMMddHHmmssffff";
        public static string Generate(DateTime value)
        {
            return value.ToString(TIMESTAMP_FORMAT);
        }
    }
}
