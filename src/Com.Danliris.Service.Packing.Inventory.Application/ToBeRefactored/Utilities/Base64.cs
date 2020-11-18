using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities
{
    public static class Base64
    {
        public static string GetBase64File(string encoded)
        {
            return encoded.Substring(encoded.IndexOf(',') + 1);
        }

    }
}
