using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionRecapReport
{
    public class GarmentPaymentDispositionRecapReportService : IGarmentPaymentDispositionRecapReportService
    {
        private readonly IGarmentShippingPaymentDispositionRecapRepository repository;
        private readonly IGarmentShippingPaymentDispositionRepository payrepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentPaymentDispositionRecapReportService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingPaymentDispositionRecapRepository>();
            payrepository = serviceProvider.GetService<IGarmentShippingPaymentDispositionRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentPaymentDispositionRecapReportViewModel> GetDataQuery(string emkl, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
            var queryItem = repository.ReadItemAll();
            var queryPay = payrepository.ReadAll();
          
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            query = query.OrderBy(w => w.RecapNo);

            var newQ1 = (from a in query
                        join b in queryItem on a.Id equals b.RecapId
                        join c in queryPay on b.PaymentDispositionId equals c.Id
                        where a.EMKLCode == (string.IsNullOrWhiteSpace(emkl) ? a.EMKLCode : emkl)
                              && c.PaymentType == "EMKL"

                         group new { Bill = c.BillValue, Tax = c.IncomeTaxValue, Total = c.TotalBill, Fee = b.Service } by new
                         {                           
                             a.RecapNo,
                             a.Date,
                             a.EMKLCode,
                             a.EMKLName,
                             a.EMKLAddress,
                             a.EMKLNPWP,
                             c.Id,
                             c.DispositionNo,
                             c.InvoiceDate,
                             c.InvoiceNumber,
                             c.InvoiceTaxNumber,
                             c.BuyerAgentCode,
                             c.BuyerAgentName,
                         } into G

                         select new GarmentPaymentDispositionRecapReportViewModel
                         {
                             RecapNo = G.Key.RecapNo,
                             RecapDate = G.Key.Date,
                             EmklCode = G.Key.EMKLCode,
                             EmklName = G.Key.EMKLName,
                             EmklAddress = G.Key.EMKLAddress,
                             EmklNPWP = G.Key.EMKLNPWP,
                             DispositionId = G.Key.Id,
                             DispositionNo = G.Key.DispositionNo,
                             InvoiceDate = G.Key.InvoiceDate,
                             InvoiceNumber = G.Key.InvoiceNumber,
                             InvoiceTaxNumber = G.Key.InvoiceTaxNumber,
                             BuyerCode = G.Key.BuyerAgentCode,
                             BuyerName = G.Key.BuyerAgentName,
                             BillValue = Math.Round(G.Sum(m => m.Bill), 2),
                             ServiceValue = Math.Round(G.Sum(m => m.Fee), 2),
                             IncomeTaxValue = Math.Round(G.Sum(m => m.Tax), 2),
                             TotalBill = Math.Round(G.Sum(m => m.Total), 2),
                         });
            return newQ1;
        }

        public List<GarmentPaymentDispositionRecapReportViewModel> GetReportData(string emkl, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetDataQuery(emkl, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.RecapNo).ThenBy(b => b.DispositionNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string emkl, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetDataQuery(emkl, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Rekap", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Rekap", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode EMKL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama EMKL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Alamat EMKL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NPWP EMKL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Disposisi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Invoice / Tagihan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice / Tagihan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Faktur Pajak", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else           
            {
                Dictionary<string, List<GarmentPaymentDispositionRecapReportViewModel>> dataByXPDC = new Dictionary<string, List<GarmentPaymentDispositionRecapReportViewModel>>();
                Dictionary<string, decimal> subTotalBill = new Dictionary<string, decimal>();
                Dictionary<string, double> subTotalFee = new Dictionary<string, double>();
                Dictionary<string, decimal> subTotalPPH = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalNett = new Dictionary<string, decimal>();
 
                foreach (GarmentPaymentDispositionRecapReportViewModel data in Query.ToList())
                {
                    string dispoNo = data.RecapNo;

                    if (!dataByXPDC.ContainsKey(dispoNo)) dataByXPDC.Add(dispoNo, new List<GarmentPaymentDispositionRecapReportViewModel> { });
                    dataByXPDC[dispoNo].Add(new GarmentPaymentDispositionRecapReportViewModel
                    {
                        RecapNo = data.RecapNo,
                        RecapDate = data.RecapDate,
                        EmklCode = data.EmklCode,
                        EmklName = data.EmklName,
                        EmklAddress = data.EmklAddress,
                        EmklNPWP = data.EmklNPWP,
                        DispositionNo = data.DispositionNo,
                        InvoiceDate = data.InvoiceDate,
                        InvoiceNumber = data.InvoiceNumber,
                        InvoiceTaxNumber = data.InvoiceTaxNumber,
                        BuyerCode = data.BuyerCode,
                        BuyerName = data.BuyerName,
                        BillValue = data.BillValue,
                        ServiceValue = data.ServiceValue,
                        IncomeTaxValue = data.IncomeTaxValue,
                        TotalBill = data.TotalBill,
                    });

                    if (!subTotalBill.ContainsKey(dispoNo))
                    {
                        subTotalBill.Add(dispoNo, 0);
                    };

                    if (!subTotalFee.ContainsKey(dispoNo))
                    {
                        subTotalFee.Add(dispoNo, 0);
                    };

                    if (!subTotalPPH.ContainsKey(dispoNo))
                    {
                        subTotalPPH.Add(dispoNo, 0);
                    };

                    if (!subTotalNett.ContainsKey(dispoNo))
                    {
                        subTotalNett.Add(dispoNo, 0);
                    };

                    subTotalBill[dispoNo] = data.BillValue;
                    subTotalFee[dispoNo] = data.ServiceValue;
                    subTotalPPH[dispoNo] = data.IncomeTaxValue;
                    subTotalNett[dispoNo] = data.TotalBill;
                }

                decimal Total1 = 0;
                double Total2 = 0;
                decimal Total3 = 0;
                decimal Total4 = 0;

                int rowPosition = 12;

                foreach (KeyValuePair<string, List<GarmentPaymentDispositionRecapReportViewModel>> dispoNo in dataByXPDC)
                {
                    string DispoNo = "";
                    int index = 0;
                    foreach (GarmentPaymentDispositionRecapReportViewModel item in dispoNo.Value)
                    {
                        index++;

                        string RcpDate = item.RecapDate == new DateTime(1970, 1, 1) ? "-" : item.RecapDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string InvDate = item.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : item.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                        string BillAmt = string.Format("{0:N2}", item.BillValue);
                        string FeeAmt = string.Format("{0:N2}", item.ServiceValue);
                        string TaxAmt = string.Format("{0:N2}", item.IncomeTaxValue);
                        string TotBill = string.Format("{0:N2}", item.TotalBill);

                        result.Rows.Add(index, item.RecapNo, RcpDate, item.EmklCode, item.EmklName, item.EmklAddress, item.EmklNPWP, item.DispositionNo,
                                        InvDate, item.InvoiceNumber, item.InvoiceTaxNumber, item.BuyerCode, item.BuyerName);

                        rowPosition += 1;
                        DispoNo = item.RecapNo;
                    }

                    result.Rows.Add("NO REKAP EMKL  :", DispoNo, ".", "NILAI TAGIHAN  :", Math.Round(subTotalBill[dispoNo.Key], 2), ".",
                                    "NILAI JASA  :", Math.Round(subTotalFee[dispoNo.Key], 2), ".",
                                    "NILAI PPH  :", Math.Round(subTotalPPH[dispoNo.Key], 2), ".", 
                                    "TOTAL TAGIHAN :", Math.Round(subTotalPPH[dispoNo.Key], 2));

                    rowPosition += 1;
                    Total1 += subTotalBill[dispoNo.Key];
                    Total2 += subTotalFee[dispoNo.Key];
                    Total3 += subTotalPPH[dispoNo.Key];
                    Total4 += subTotalNett[dispoNo.Key];      
                }
                result.Rows.Add(".", "TOTAL :", ".", "NILAI TAGIHAN  :", Math.Round(Total1, 2), ".", "NILAI JASA  :", Math.Round(Total2, 2), ".",
                                "NILAI PPH  :", Math.Round(Total3, 2), ".", "TOTAL TERBAYAR :", Math.Round(Total4, 2));
                
                rowPosition += 1;
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "SHEET1") }, true);
        }
    }
}
