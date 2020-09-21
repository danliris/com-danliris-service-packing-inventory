using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Helper
{
    public static class CodeConstructionHelper
    {
        private static List<string> AlphabetList = new List<string>()
        {
            "A", "B", "C", "D", "E",
            "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O",
            "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y",
            "Z"
        };

        public static string GetYearCode(int year)
        {
            var alphabetIndex = (year - 2015) % 26;
            return AlphabetList[alphabetIndex];
        }

        //private static List<string> GetCodeList()
        //{

        //}
    }

    public class YearCode
    {
        public YearCode(int year, string code)
        {
            Year = year;
            Code = code;
        }

        public int Year { get; }
        public string Code { get; }
    }
}
