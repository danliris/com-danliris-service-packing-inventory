using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDetailOmzetByUnitReport
{
    public class GarmentDetailOmzetByUnitReportService : IGarmentDetailOmzetByUnitReportService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityProvider _identityProvider;

        public GarmentDetailOmzetByUnitReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public List<GarmentDetailOmzetByUnitReportViewModel> GetData(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
            var quaryInvItem = itemrepository.ReadAll();
            var queryPL = plrepository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryInv = queryInv.OrderBy(w => w.BuyerAgentCode).ThenBy(b => b.InvoiceNo);
            List<GarmentDetailOmzetByUnitReportViewModel> omzetgmt = new List<GarmentDetailOmzetByUnitReportViewModel>();
            var Query = (from a in queryInv
                         join b in quaryInvItem on a.Id equals b.GarmentShippingInvoiceId
                         join c in queryPL on a.PackingListId equals c.Id
                         where a.IsDeleted == false && b.IsDeleted == false
                               && b.UnitCode == (string.IsNullOrWhiteSpace(unit) ? b.UnitCode : unit)
                               && a.PEBDate != DateTimeOffset.MinValue
                         select new GarmentDetailOmzetByUnitReportViewModel
                         {
                             Urutan = "A",
                             InvoiceNo = a.InvoiceNo,
                             PEBDate = a.PEBDate,
                             TruckingDate = c.TruckingDate,
                             BuyerAgentName = a.BuyerAgentCode + " - " + a.BuyerAgentName,
                             ComodityName = b.ComodityName,
                             UnitCode = b.UnitCode,
                             RONumber = b.RONo,
                             Quantity = b.Quantity,
                             UOMUnit = b.UomUnit,
                             CurrencyCode = b.CurrencyCode,
                             Amount = b.Amount,

                         }).ToList();

            //
            var currencyFilters = Query
                                    .GroupBy(o => new { o.PEBDate, o.CurrencyCode })
                                    .Select(o => new CurrencyFilter { date = o.Key.PEBDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime, code = o.Key.CurrencyCode })
                                    .ToList();

            var currencies = GetCurrecncies(currencyFilters).Result;

            decimal rate;

            foreach (var data in Query)
            {
                rate = Convert.ToDecimal(currencies.Where(q => q.code == data.CurrencyCode && q.date <= data.PEBDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime).Select(s => s.rate).LastOrDefault());

                data.Rate = rate;
                data.AmountIDR = rate * data.Amount;
            }
            //   
            foreach (GarmentDetailOmzetByUnitReportViewModel i in Query)
            {
                var data1 = GetExpenditureGood(i.RONumber);

                omzetgmt.Add(new GarmentDetailOmzetByUnitReportViewModel
                {
                    Urutan = i.Urutan,
                    InvoiceNo = i.InvoiceNo,
                    PEBDate = i.PEBDate,
                    TruckingDate = i.TruckingDate,
                    BuyerAgentName = i.BuyerAgentName,
                    ComodityName = i.ComodityName,
                    UnitCode = i.UnitCode,
                    RONumber = i.RONumber,
                    UOMUnit = i.UOMUnit,
                    Quantity = i.Quantity,
                    CurrencyCode = i.CurrencyCode,
                    Amount = i.Amount,
                    Rate = i.Rate,
                    AmountIDR = i.AmountIDR,
                    ArticleStyle = data1 == null || data1.Count == 0 ? "-" : data1.FirstOrDefault().Article,
                    ExpenditureGoodNo = data1 == null || data1.Count == 0 ? "-" : data1.FirstOrDefault().ExpenditureGoodNo,
                    QuantityInPCS = i.UOMUnit.Substring(0, 3) == "SET" || i.UOMUnit.Substring(0, 3) == "PAC" ? i.Quantity * 2 : i.Quantity,
                });

            };

            return omzetgmt;

        }

        public ListResult<GarmentDetailOmzetByUnitReportViewModel> GetReportData(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetData(unit, dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentDetailOmzetByUnitReportViewModel>(data, 1, total, total);
        }

        public MemoryStream GenerateExcel(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            var Query = GetData(unit, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "ITEM", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "STYLE   ORD/ART NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "QUANTITY", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "QUANTITY IN PCS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IN IDR", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "R/O", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TRUCKING", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO BON", DataType = typeof(string) });


            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "");
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "LAPORAN OMZET EXPORT GARMENT";
                    sheet.Cells[$"A1:L1"].Merge = true;
                    sheet.Cells[$"A1:L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:L1"].Style.Font.Bold = true;
                    sheet.Cells[$"A2:L2"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd MMM yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd MMM yyyy", new CultureInfo("id-ID")));
                    sheet.Cells[$"A2:L2"].Merge = true;
                    sheet.Cells[$"A2:L2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:L2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:L2"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:L3"].Value = string.Format("KONFEKSI : {0}", string.IsNullOrWhiteSpace(unit) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().UnitCode));
                    sheet.Cells[$"A3:L3"].Merge = true;
                    sheet.Cells[$"A3:L3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A3:L3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A3:L3"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells["A5"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string TruckDate = d.TruckingDate == new DateTime(1970, 1, 1) ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                    string Qty = string.Format("{0:N0}", d.Quantity);
                    string Qty1 = string.Format("{0:N0}", d.QuantityInPCS);
                    string AmtUSD = string.Format("{0:N2}", d.Amount);
                    string AmtIDR = string.Format("{0:N2}", d.AmountIDR);

                    result.Rows.Add(index, d.InvoiceNo, d.BuyerAgentName, d.ComodityName, d.ArticleStyle, Qty, d.UOMUnit, Qty1, AmtUSD, AmtIDR, d.RONumber, TruckDate, d.ExpenditureGoodNo);
                }

                string TotQty = string.Format("{0:N2}", Query.Sum(x => x.QuantityInPCS));
                string TotUSD = string.Format("{0:N2}", Query.Sum(x => x.Amount));
                string TotIDR = string.Format("{0:N2}", Query.Sum(x => x.AmountIDR));

                result.Rows.Add("", "", "", "", "", "", "  T  O  T  A  L  : ", TotQty, TotUSD, TotIDR, "", "", "");

                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "LAPORAN OMZET EXPORT GARMENT";
                    sheet.Cells[$"A1:L1"].Merge = true;
                    sheet.Cells[$"A1:L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:L1"].Style.Font.Bold = true;
                    sheet.Cells[$"A2:L2"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd MMM yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd MMM yyyy", new CultureInfo("id-ID")));
                    sheet.Cells[$"A2:L2"].Merge = true;
                    sheet.Cells[$"A2:L2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:L2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:L2"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:L3"].Value = string.Format("KONFEKSI : {0}", string.IsNullOrWhiteSpace(unit) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().UnitCode));
                    sheet.Cells[$"A3:L3"].Merge = true;
                    sheet.Cells[$"A3:L3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A3:L3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A3:L3"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells["A5"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;
        }
        async Task<List<GarmentCurrency>> GetCurrecncies(List<CurrencyFilter> filters)
        {
            string uri = "master/garment-currencies/by-code-before-date";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.SendAsync(HttpMethod.Get, $"{ApplicationSetting.CoreEndpoint}{uri}", new StringContent(JsonConvert.SerializeObject(filters), Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                List<GarmentCurrency> viewModel = JsonConvert.DeserializeObject<List<GarmentCurrency>>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return new List<GarmentCurrency>();
            }
        }

        public List<GarmentExpenditureGood> GetExpenditureGood(string RONo)
        {
            string expenditureUri = "expenditure-goods/byRO";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{ApplicationSetting.ProductionEndpoint}{expenditureUri}?RONo={RONo}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                List<GarmentExpenditureGood> viewModel;
                if (result.GetValueOrDefault("data") == null)
                {
                    viewModel = null;
                }
                else
                {
                    viewModel = JsonConvert.DeserializeObject<List<GarmentExpenditureGood>>(result.GetValueOrDefault("data").ToString());
                }
                return viewModel;
            }
            else
            {
                return null;
            }
        }
    }
}
