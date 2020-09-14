using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities
{
    public static class NumberToTextEN
    {
        //static string[] th = { "", "Thousand", "Million", "Billion", "Trillion" };
        //static string[] dg = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        //static string[] tn = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        //static string[] tw = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        //public static string toWords(double s)
        //{

        //    Regex pattern = new Regex("[,]");
        //    s = Convert.ToDouble(s.ToString().Replace(",", ""));

        //    double number;
        //    bool isNumeric = double.TryParse(s.ToString(), out number);

        //    if (!isNumeric) return "not a number";
        //    var x = s.ToString().IndexOf(".");
        //    if (x == -1)
        //        x = s.ToString().Length;
        //    if (x > 15)
        //        return "too big";
        //    var n = s;
        //    var str = "";
        //    var sk = 0;
        //    for (var i = 0; i < x; i++)
        //    {
        //        if ((x - i) % 3 == 2)
        //        {
        //            if (n.ToString()[i] == '1')
        //            {
        //                str += tn[Convert.ToInt16(n.ToString()[i + 1].ToString())] + " ";
        //                i++;
        //                sk = 1;
        //            }
        //            else if (n.ToString()[i] != '0')
        //            {

        //                str += tw[Convert.ToInt16(n.ToString()[i].ToString()) - 2] + " ";
        //                sk = 1;
        //            }
        //        }
        //        else if (n.ToString()[i] != '0')
        //        { // 0235
        //            str += dg[Convert.ToInt16(n.ToString()[i].ToString())] + " ";
        //            if ((x - i) % 3 == 0) str += " Hundred ";
        //            sk = 1;
        //        }
        //        if ((x - i) % 3 == 1)
        //        {
        //            if (sk != 0)
        //                str += th[(x - i - 1) / 3] + " ";
        //            sk = 0;
        //        }
        //    }

        //    if (x != Convert.ToString(s).Length)
        //    {
        //        var y = s.ToString().Length;
        //        str += "and ";
        //        for (var i = x + 1; i < y; i++)
        //            str += dg[Convert.ToInt16(n.ToString()[i].ToString())] + " ";
        //    }
        //    return str;
        //}

        public static string toWords(double value)
        {
            string decimals = "";
            string input = Math.Round(value, 2).ToString();

            if (input.Contains("."))
            {
                decimals = input.Substring(input.IndexOf(".") + 1);
                // remove decimal part from input
                input = input.Remove(input.IndexOf("."));
            }

            // Convert input into words. save it into strWords
            string strWords = GetWords(input);


            if (decimals.Length > 0)
            {
                // if there is any decimal part convert it to words and add it to strWords.
                strWords += " and Cents " + GetWordDecimal(int.Parse(decimals));
            }

            return strWords;
        }

        private static string GetWords(string input)
        {
            // these are seperators for each 3 digit in numbers. you can add more if you want convert beigger numbers.
            string[] seperators = { "", " Thousand ", " Million ", " Billion " };

            // Counter is indexer for seperators. each 3 digit converted this will count.
            int i = 0;

            string strWords = "";

            while (input.Length > 0)
            {
                // get the 3 last numbers from input and store it. if there is not 3 numbers just use take it.
                string _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);
                // remove the 3 last digits from input. if there is not 3 numbers just remove it.
                input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

                int no = int.Parse(_3digits);
                // Convert 3 digit number into words.
                _3digits = GetWord(no);

                // apply the seperator.
                if (i == 0 && input.Length < 3)
                {
                    _3digits += " and ";
                }

                _3digits += seperators[i];

                // since we are getting numbers from right to left then we must append resault to strWords like this.
                strWords = _3digits + strWords;

                // 3 digits converted. count and go for next 3 digits
                i++;
            }
            return strWords;
        }

        // your method just to convert 3digit number into words.
        private static string GetWord(int no)
        {
            string[] Ones =
            {
            "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen"
        };

            string[] Tens = { "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string word = "";

            if (no > 99 && no < 1000)
            {
                int i = no / 100;
                word = word + Ones[i - 1] + " Hundred ";
                no = no % 100;
                //if(no > 0)
                //{
                //    word = word + "and ";
                //}
            }

            if (no > 19 && no < 100)
            {
                int i = no / 10;
                //if(!string.IsNullOrWhiteSpace(word) && !word.EndsWith("and "))
                //{
                //    word = word + "and ";
                //}
                word = word + Tens[i - 1] + " ";
                no = no % 10;
            }

            if (no > 0 && no < 20)
            {
                //if (!string.IsNullOrWhiteSpace(word) && !word.EndsWith("ty ") && !word.EndsWith("and "))
                //{
                //    word = word + "and ";
                //}
                word = word + Ones[no - 1];
            }

            return word;
        }

        private static string GetWordDecimal(int no)
        {
            string[] Ones =
            {
            "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen"
        };

            string[] Tens = { "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string word = "";
            if (no > 19 && no < 100)
            {
                int i = no / 10;
                word = word + Tens[i - 1] + " ";
                no = no % 10;
            }
            if (no > 0 && no < 20)
            {
                word = word + Ones[no - 1];
            }

            return word;
        }
    }
}
