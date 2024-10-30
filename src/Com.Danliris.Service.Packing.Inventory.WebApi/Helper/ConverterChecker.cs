using OfficeOpenXml;
using System.Globalization;
using System;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Helper
{
    public class ConverterChecker
    {
        public string GenerateValueString(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || string.IsNullOrWhiteSpace(value.Text) || value.Text.Trim().Equals("-"))
            {
                return null;
            }
            return value.Value.ToString().Trim();
        }

        public int GenerateValueInt(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || string.IsNullOrWhiteSpace(value.Text) || value.Text.Trim().Equals("-"))
            {
                return 0;
            }
            return Convert.ToInt32(value.Value.ToString().Trim());
        }

        public double? GenerateValueDouble(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || string.IsNullOrWhiteSpace(value.Text) || value.Text.Trim().Equals("-"))
            {
                return null;
            }
            return Convert.ToDouble(value.Value.ToString().Trim());
        }

        public decimal? GenerateValueDecimal(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || string.IsNullOrWhiteSpace(value.Text) || value.Text.Trim().Equals("-"))
            {
                return null;
            }
            return Convert.ToDecimal(value.Value.ToString().Trim());
        }

        public char? GenerateValueChar(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || string.IsNullOrWhiteSpace(value.Text) || value.Text.Trim().Equals("-"))
            {
                return null;
            }
            return Convert.ToChar(value.Value.ToString().Trim());
        }

        public DateTimeOffset? GenerateValueDate(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || string.IsNullOrWhiteSpace(value.Text) || value.Text.Trim().Equals("-"))
            {
                return null;
            }
            var date = DateTimeOffset.Parse(value.Text.ToString().Trim());
            var dateFormated = date.ToString("dd/MM/yyyy HH:mm:ss");
            return DateTimeOffset.ParseExact(dateFormated, "dd/MM/yyyy HH:mm:ss", null);
        }

        public string GeneratePureString(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || value.Text.Trim().Equals("-") || string.IsNullOrWhiteSpace(value.Text))
            {
                return null;
            }
            return value.Text;
        }
        public double? GeneratePureDouble(ExcelRangeBase value)
        {
            try
            {
                if (value.Value == null || value.Value.Equals("-") || /*value.Text.Trim().Equals("-") ||*/ string.IsNullOrWhiteSpace(value.Text))
                {
                    return 0;
                }

                if (value.Text.IndexOf('(') > 0 || value.Text.IndexOf('-') > 0)
                {
                    return Convert.ToDouble(value.Text);
                }
                else
                {
                    return Math.Round(Convert.ToDouble(value.Value), 2);
                }

            }
            catch
            {
                return 0;
            }
        }

        public DateTime? GeneratePureDateTime(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || value.Text.Trim().Equals("-") || string.IsNullOrWhiteSpace(value.Text))
            {
                return null;
            }
            var date = DateTime.Parse(value.Text.ToString().Trim());
            var dateFormated = date.ToString("dd/MM/yyyy");
            return DateTime.ParseExact(dateFormated, "dd/MM/yyyy", null);
        }

        public DateTime? GeneratePureTime(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || value.Text.Trim().Equals("-") || string.IsNullOrWhiteSpace(value.Text))
            {
                return null;
            }

            var richTime = DateTime.Parse(value.Text);
            var time = richTime.ToString("HH:mm");
            var anu = DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture);
            return DateTime.ParseExact(time, "HH:mm", null);
        }

        public string GenerateValueStringBC(ExcelRangeBase value)
        {
            if (value.Value == null || value.Value.Equals("-") || string.IsNullOrWhiteSpace(value.Text) || value.Text.Trim().Equals("-"))
            {
                return null;
            }

            var bc = "BC " + value.Value.ToString();

            return bc;
        }
    }
}
