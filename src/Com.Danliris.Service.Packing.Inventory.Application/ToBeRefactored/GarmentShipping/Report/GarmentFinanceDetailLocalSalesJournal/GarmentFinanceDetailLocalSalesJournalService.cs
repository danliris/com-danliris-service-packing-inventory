using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public class GarmentFinanceDetailLocalSalesJournalService : IGarmentFinanceDetailLocalSalesJournalService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;

        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;

        public GarmentFinanceDetailLocalSalesJournalService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }
        private GarmentCurrency GetCurrencyPEBDate(string stringDate)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var currencyUri = ApplicationSetting.CoreEndpoint + $"master/garment-detail-currencies/sales-debtor-currencies-peb?stringDate={stringDate}";
            var currencyResponse = httpClient.GetAsync(currencyUri).Result.Content.ReadAsStringAsync();

            var currencyResult = new BaseResponse<GarmentCurrency>()
            {
                data = new GarmentCurrency()
            };
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(currencyResponse.Result);
            var json = result.Single(p => p.Key.Equals("data")).Value;
            var data = JsonConvert.DeserializeObject<GarmentCurrency>(json.ToString());

            return data;
        }
        public List<GarmentFinanceDetailLocalSalesJournalViewModel> GetReportQuery(DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            //DateTime dateFrom = new DateTime(year, month, 1);
            //int nextYear = month == 12 ? year + 1 : year;
            //int nextMonth = month == 12 ? 1 : month + 1;
            //DateTime dateTo = new DateTime(nextYear, nextMonth, 1);

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            List<GarmentFinanceDetailLocalSalesJournalViewModel> data = new List<GarmentFinanceDetailLocalSalesJournalViewModel>();
            List<GarmentFinanceDetailLocalSalesJournalTempViewModel> data1 = new List<GarmentFinanceDetailLocalSalesJournalTempViewModel>();

            var queryInv = repository.ReadAll();
            var queryInvItm = itemrepository.ReadAll();

            var queryPL = plrepository.ReadAll()
                .Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom && w.TruckingDate.AddHours(offset).Date <= DateTo.Date
                    && w.PackingListType == "LOKAL" && (w.InvoiceType == "AG" || w.InvoiceType == "DS" || w.InvoiceType == "AGR" || w.InvoiceType == "SMR"));

            var joinQuery = from a in queryInv
                            join c in queryInvItm on a.Id equals c.GarmentShippingInvoiceId
                            join b in queryPL on a.PackingListId equals b.Id
                            where a.IsDeleted == false && b.IsDeleted == false

                            select new GarmentFinanceDetailLocalSalesJournalTempViewModel
                            {
                                invoicetype = b.InvoiceType,
                                invoiceno = a.InvoiceNo,
                                truckingdate = b.TruckingDate,
                                buyer = a.BuyerAgentCode + "-" + a.BuyerAgentName,
                                pebno = a.PEBNo,
                                rono = c.RONo,
                                currencycode = "IDR",
                                rate = 1,
                                qty = c.Quantity,
                                price = c.Price,
                                amount = Convert.ToDecimal(c.Quantity) * c.Price,
                                vatamount = (Convert.ToDecimal(c.Quantity) * c.Price) * 11/100,                               
                                amountcc = 0,
                            };
            //
            foreach (GarmentFinanceDetailLocalSalesJournalTempViewModel i in joinQuery)
            {
                var data2 = GetCostCalculation(i.rono);

                data1.Add(new GarmentFinanceDetailLocalSalesJournalTempViewModel
                {
                    invoicetype = i.invoicetype,
                    invoiceno = i.invoiceno,
                    truckingdate = i.truckingdate,
                    buyer = i.buyer,
                    pebno = i.pebno,
                    rono = i.rono,
                    currencycode = "IDR",
                    rate = 1,
                    qty = i.qty,
                    price = i.price,
                    amount = i.amount,
                    vatamount = i.vatamount,                  
                    amountcc = data2 == null || data2.Count == 0 ? 0 : data2.FirstOrDefault().AmountCC * i.qty,
                });
            };
            //

            var joinQuery1 = from a in data1
                            
                             group new { AmtInv = a.amount, VatAmt = a.vatamount, AmtCC=a.amountcc } by new

                             { a.invoicetype, a.invoiceno, a.truckingdate, a.pebno, a.buyer } into G

                             select new GarmentFinanceDetailLocalSalesJournalViewModel
                             {
                                 invoicetype = G.Key.invoicetype,
                                 invoiceno = G.Key.invoiceno,
                                 truckingdate = G.Key.truckingdate,
                                 buyer = G.Key.buyer,
                                 pebno = G.Key.pebno,
                                 currencycode = "IDR",
                                 rate = 1,
                                 amount = Math.Round(G.Sum(c => c.AmtInv), 2),
                                 vatamount = Math.Round(G.Sum(c => c.VatAmt), 2),
                                 amountcc = Math.Round(G.Sum(c => c.AmtCC), 2),
                                 coaname = "-",
                                 account = "-",
                                 debit = 0,
                                 credit = 0,
                             };
            //

            foreach (GarmentFinanceDetailLocalSalesJournalViewModel x in joinQuery1.OrderBy(x => x.invoiceno))
            {
                var debit = new GarmentFinanceDetailLocalSalesJournalViewModel
                {
                    invoicetype = x.invoicetype,
                    invoiceno = x.invoiceno,
                    truckingdate = x.truckingdate,
                    buyer = x.buyer,
                    pebno = x.pebno,
                    currencycode = "IDR",
                    rate = 1,
                    amount = 0,
                    vatamount = 0,
                    amountcc = 0,
                    coaname = "PIUTANG USAHA LOKAL(AG2)",
                    account = "112.00.2.000",
                    debit = x.amount + x.vatamount,
                    credit = 0,
                };

                data.Add(debit);

                var credit1 = new GarmentFinanceDetailLocalSalesJournalViewModel
                {
                   invoicetype = x.invoicetype,
                    invoiceno = x.invoiceno,
                    truckingdate = x.truckingdate,
                    buyer = x.buyer,
                    pebno = x.pebno,
                    currencycode = "IDR",
                    rate = 1,
                    amount = 0,
                    vatamount = 0,
                    amountcc = 0,
                    coaname = x.invoicetype == "AG" || x.invoicetype == "DS" ? "       PENJUALAN LOKAL (AG2)" : "       PENJUALAN LAIN-LAIN LOKAL (AG2)",
                    account = x.invoicetype == "AG" || x.invoicetype == "DS" ? "411.00.2.000" : "411.00.2.000",
                    debit = 0,
                    credit = x.amount,
                };

                data.Add(credit1);

                var credit2 = new GarmentFinanceDetailLocalSalesJournalViewModel
                {
                    invoicetype = x.invoicetype,
                    invoiceno = x.invoiceno,
                    truckingdate = x.truckingdate,
                    buyer = x.buyer,
                    pebno = x.pebno,
                    currencycode = "IDR",
                    rate = 1,
                    amount = 0,
                    vatamount = 0,
                    amountcc = 0,
                    coaname = "       PPN KELUARAN (AG2)",
                    account = "217.01.2.000",
                    debit = 0,
                    credit = x.vatamount,
                };

                data.Add(credit2);

                ///

                var debit3 = new GarmentFinanceDetailLocalSalesJournalViewModel
                {
                    invoicetype = x.invoicetype,
                    invoiceno = x.invoiceno,
                    truckingdate = x.truckingdate,
                    buyer = x.buyer,
                    pebno = x.pebno,
                    currencycode = "IDR",
                    rate = 1,
                    amount = 0,
                    vatamount = 0,
                    amountcc = 0,
                    coaname = "HARGA POKOK PENJUALAN(AG2)",
                    account = "500.00.2.000",
                    debit = Convert.ToDecimal(x.amountcc),
                    credit = 0,
                };
                data.Add(debit3);

                //
                var stock = new GarmentFinanceDetailLocalSalesJournalViewModel
                {
                    invoicetype = x.invoicetype,
                    invoiceno = x.invoiceno,
                    truckingdate = x.truckingdate,
                    buyer = x.buyer,
                    pebno = x.pebno,
                    currencycode = "IDR",
                    rate = 1,
                    amount = 0,
                    vatamount = 0,
                    amountcc = 0,
                    coaname = "       PERSEDIAAN BARANG JADI (AG2)",
                    account = "114.01.2.000",
                    debit = 0,
                    credit = Convert.ToDecimal(x.amountcc),
                };
                data.Add(stock);

            }

            var total = new GarmentFinanceDetailLocalSalesJournalViewModel
            {
                invoicetype = "",
                invoiceno = "",
                truckingdate = DateTimeOffset.MinValue,
                buyer = "",
                pebno = "",
                currencycode = "",
                rate = 0,
                amount = 0,
                vatamount = 0,
                amountcc = 0,
                coaname = "",
                account = "J U M L A H",
                debit = joinQuery1.Sum(a => a.amount) + joinQuery1.Sum(a => a.vatamount) + Convert.ToDecimal(joinQuery1.Sum(a => a.amountcc)),
                credit = joinQuery1.Sum(a => a.amount) + joinQuery1.Sum(a => a.vatamount) + Convert.ToDecimal(joinQuery1.Sum(a => a.amountcc)),
            };
          
            data.Add(total);

            return data;
        }

        public List<CostCalculationGarmentForJournal> GetCostCalculation(string RO_Number)
        {
            string costcalcUri = "cost-calculation-garments/dataforjournal";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{ApplicationSetting.SalesEndpoint}{costcalcUri}?RO_Number={RO_Number}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                List<CostCalculationGarmentForJournal> viewModel;
                if (result.GetValueOrDefault("data") == null)
                {
                    viewModel = null;
                }
                else
                {
                    viewModel = JsonConvert.DeserializeObject<List<CostCalculationGarmentForJournal>>(result.GetValueOrDefault("data").ToString());
                }
                return viewModel;
            }
            else
            {
                return null;
            }
        }

        //public List<GarmentFinanceDetailLocalSalesJournalViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        //{
        //    var Query = GetReportQuery(dateFrom, dateTo, offset);
        //    return Query.ToList();
        //}

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            var Query = GetReportQuery(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL TRUCKING", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO AKUN ", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA AKUN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO PEB", DataType = typeof(string) });
            
            result.Columns.Add(new DataColumn() { ColumnName = "MATA UANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KURS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "DEBET", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KREDIT", DataType = typeof(string) });
            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {
                result.Rows.Add("", "", 0, 0);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    sheet.Column(1).Width = 15;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 15;
                    sheet.Column(4).Width = 50;
                    sheet.Column(5).Width = 50;
                    sheet.Column(6).Width = 15;

                    sheet.Column(9).Width = 15;
                    sheet.Column(10).Width = 15;
                    sheet.Column(11).Width = 20;
                    sheet.Column(12).Width = 20;

                    #region KopTable
                    sheet.Cells[$"A1:D1"].Value = "PT AMBASSADOR GARMINDO";
                    sheet.Cells[$"A1:D1"].Merge = true;
                    sheet.Cells[$"A1:D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:D1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:D2"].Value = "ACCOUNTING DEPT.";
                    sheet.Cells[$"A2:D2"].Merge = true;
                    sheet.Cells[$"A2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:D2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:D2"].Style.Font.Bold = true;

                    sheet.Cells[$"A4:D4"].Value = "RINCIAN JURNAL";
                    sheet.Cells[$"A4:D4"].Merge = true;
                    sheet.Cells[$"A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.Font.Bold = true;

                    sheet.Cells[$"C5"].Value = "BUKU HARIAN";
                    sheet.Cells[$"C5"].Style.Font.Bold = true;
                    sheet.Cells[$"D5"].Value = ": PENJUALAN LOKAL";
                    sheet.Cells[$"D5"].Style.Font.Bold = true;

                    sheet.Cells[$"C6"].Value = "PERIODE";
                    sheet.Cells[$"C6"].Style.Font.Bold = true;
                    sheet.Cells[$"D6"].Value = ": " + DateFrom.ToString("dd-MM-yyyy") + " S/D " + DateTo.ToString("dd-MM-yyyy");
                    sheet.Cells[$"D6"].Style.Font.Bold = true;

                    #endregion
                    sheet.Cells["A8"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;
             
                    result.Rows.Add( d.invoiceno, d.truckingdate, d.account, d.coaname, d.buyer, d.pebno, d.currencycode, d.rate, d.debit, d.credit);
                }

                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);


                    sheet.Column(1).Width = 15;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 15;
                    sheet.Column(4).Width = 50;
                    sheet.Column(5).Width = 50;
                    sheet.Column(6).Width = 15;

                    sheet.Column(9).Width = 15;
                    sheet.Column(10).Width = 15;
                    sheet.Column(11).Width = 20;
                    sheet.Column(12).Width = 20;

                    #region KopTable
                    sheet.Cells[$"A1:D1"].Value = "PT AMBASSADOR GARMINDO";
                    sheet.Cells[$"A1:D1"].Merge = true;
                    sheet.Cells[$"A1:D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:D1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:D2"].Value = "ACCOUNTING DEPT.";
                    sheet.Cells[$"A2:D2"].Merge = true;
                    sheet.Cells[$"A2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:D2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:D2"].Style.Font.Bold = true;

                    sheet.Cells[$"A4:D4"].Value = "RINCIAN JURNAL";
                    sheet.Cells[$"A4:D4"].Merge = true;
                    sheet.Cells[$"A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.Font.Bold = true;

                    sheet.Cells[$"C5"].Value = "BUKU HARIAN";
                    sheet.Cells[$"C5"].Style.Font.Bold = true;
                    sheet.Cells[$"D5"].Value = ": PENJUALAN LOKAL";
                    sheet.Cells[$"D5"].Style.Font.Bold = true;

                    sheet.Cells[$"C6"].Value = "PERIODE";
                    sheet.Cells[$"C6"].Style.Font.Bold = true;
                    sheet.Cells[$"D6"].Value = ": " + DateFrom.ToString("dd-MM-yyyy") + " S/D " + DateTo.ToString("dd-MM-yyyy");
                    sheet.Cells[$"D6"].Style.Font.Bold = true;

                    #endregion
                    sheet.Cells["A8"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }
        
    }

    //public class BaseResponse<T>
    //{
    //    public string apiVersion { get; set; }
    //    public int statusCode { get; set; }
    //    public string message { get; set; }
    //    public T data { get; set; }

    //    public static implicit operator BaseResponse<T>(BaseResponse<string> v)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
   
}
