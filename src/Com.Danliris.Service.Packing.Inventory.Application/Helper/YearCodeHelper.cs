using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Helper
{
    public static class YearCodeHelper
    {
        //public static string GetCode(int year)
        //{
        //    if (year < 2015)
        //        return "A";


        //}

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
