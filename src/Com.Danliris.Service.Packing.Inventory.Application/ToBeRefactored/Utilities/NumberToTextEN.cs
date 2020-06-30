using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities
{
    public class NumberToTextEN
    {
        static string[] th = { "", "Thousand", "Million", "Billion", "Trillion" };
        static string[] dg = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] tn = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] tw = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static string toWords(double s)
        {

            Regex pattern = new Regex("[,]");
            s = Convert.ToDouble(s.ToString().Replace(",", ""));

            double number;
            bool isNumeric = double.TryParse(s.ToString(), out number);

            if (!isNumeric) return "not a number";
            var x = s.ToString().IndexOf(".");
            if (x == -1)
                x = s.ToString().Length;
            if (x > 15)
                return "too big";
            var n = s;
            var str = "";
            var sk = 0;
            for (var i = 0; i < x; i++)
            {
                if ((x - i) % 3 == 2)
                {
                    if (n.ToString()[i] == '1')
                    {
                        str += tn[Convert.ToInt16(n.ToString()[i + 1].ToString())] + " ";
                        i++;
                        sk = 1;
                    }
                    else if (n.ToString()[i] != '0')
                    {

                        str += tw[Convert.ToInt16(n.ToString()[i].ToString()) - 2] + " ";
                        sk = 1;
                    }
                }
                else if (n.ToString()[i] != '0')
                { // 0235
                    str += dg[Convert.ToInt16(n.ToString()[i].ToString())] + " ";
                    if ((x - i) % 3 == 0) str += " Hundred ";
                    sk = 1;
                }
                if ((x - i) % 3 == 1)
                {
                    if (sk != 0)
                        str += th[(x - i - 1) / 3] + " ";
                    sk = 0;
                }
            }

            if (x != Convert.ToString(s).Length)
            {
                var y = s.ToString().Length;
                str += "point ";
                for (var i = x + 1; i < y; i++)
                    str += dg[Convert.ToInt16(n.ToString()[i].ToString())] + " ";
            }
            return str;
        }
    }
}
