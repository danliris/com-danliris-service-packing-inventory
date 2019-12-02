using System;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities
{
    public static class DocumentCodeGenerator
    {
        public static string ProductSKU(string latestCode)
        {
            if (!string.IsNullOrWhiteSpace(latestCode))
            {
                var yearCode = latestCode[0].ToString() + latestCode[1].ToString();
                var monthCode = latestCode[2].ToString() + latestCode[3].ToString();

                var yearNow = DateTime.Now.Year.ToString();
                var monthNowCode = DateTime.Now.Month.ToString().PadLeft(2, '0');
                var yearNowCode = latestCode[2].ToString() + latestCode[3].ToString();

                if (yearCode == yearNowCode && monthCode == monthNowCode)
                {
                    var parsedInt = int.Parse(latestCode);
                    parsedInt += 1;
                    return parsedInt.ToString();
                }

                return $"{yearNowCode}{monthNowCode}000001";
            }

            return DateTime.Now.Year.ToString("yy") + DateTime.Now.Month.ToString().PadLeft(2, '0') + 1.ToString().PadLeft(6, '0');
        }
    }
}