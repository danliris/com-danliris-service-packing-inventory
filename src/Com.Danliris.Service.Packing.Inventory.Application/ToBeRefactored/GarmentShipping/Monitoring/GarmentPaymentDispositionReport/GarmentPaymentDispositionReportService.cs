using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionReport
{
    public class GarmentPaymentDispositionReportService : IGarmentPaymentDispositionReportService
    {
        private readonly IGarmentShippingPaymentDispositionRepository repository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentPaymentDispositionReportService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingPaymentDispositionRepository>();      
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentPaymentDispositionReportViewModel> GetDataQuery(string paymentType, string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
           var query = repository.ReadAll();
           var queryUnit = repository.ReadUnitAll();


            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            query = query.Where(w => w.PaymentDate.AddHours(offset).Date >= DateFrom.Date && w.PaymentDate.AddHours(offset).Date <= DateTo.Date);

            query = query.OrderBy(w => w.PaymentType).ThenBy(w => w.DispositionNo);

            var newQ = (from a in query
                        join b in queryUnit on a.Id equals b.PaymentDispositionId
                        where a.PaymentType == (string.IsNullOrWhiteSpace(paymentType) ? a.PaymentType : paymentType) &&  b.UnitCode == (string.IsNullOrWhiteSpace(unit) ? b.UnitCode : unit)
                              
                        select new GarmentPaymentDispositionReportViewModel
                        {
                            DispositionNo = a.DispositionNo,
                            PaymentDate = a.PaymentDate,
                            PaymentType = a.PaymentType,
                            PaymentMethod = a.PaymentMethod,
                            PaymentTerm = a.PaymentTerm,
                            BankName = a.Bank,
                            AccNumber = a.AccNo,
                            BuyerCode = a.BuyerAgentCode,
                            BuyerName = a.BuyerAgentName,
                            InvoiceNumber = a.InvoiceNumber,
                            InvoiceDate = a.InvoiceDate,
                            InvoiceTaxNumber = a.InvoiceTaxNumber,
                            BillValue = a.BillValue,
                            VatValue = a.VatValue,                            
                            IncomeTaxRate = a.IncomeTaxRate,
                            IncomeTaxValue = a.IncomeTaxValue,
                            TotalBill = a.TotalBill,
                            XpdcCode = a.PaymentType == "EMKL" ? a.EMKLCode : (a.PaymentType == "FORWARDER" ? a.ForwarderCode : (a.PaymentType == "COURIER" ? a.CourierCode : a.WareHouseCode)),
                            XpdcName = a.PaymentType == "EMKL" ? a.EMKLName : (a.PaymentType == "FORWARDER" ? a.ForwarderName : (a.PaymentType == "COURIER" ? a.CourierName : a.WareHouseName)),
                            UnitCode = b.UnitCode,
                            UnitPercentage = b.AmountPercentage,
                            UnitAmount = b.BillAmount,
                        });
            return newQ;
        }

        public List<GarmentPaymentDispositionReportViewModel> GetReportData(string paymentType, string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetDataQuery(paymentType, unit, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.PaymentType).ThenBy(b => b.DispositionNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string paymentType, string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetDataQuery(paymentType, unit, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Disposisi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Bayar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tipe Bayar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Metode Bayar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Termin Bayar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Ekspedisi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Ekspedisi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Bank", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Rekening", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Pajak Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Konfeksi", DataType = typeof(string) });            
            result.Columns.Add(new DataColumn() { ColumnName = "Tagihan %", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Tagihan", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else           
            {
                Dictionary<string, List<GarmentPaymentDispositionReportViewModel>> dataByXPDC = new Dictionary<string, List<GarmentPaymentDispositionReportViewModel>>();
                Dictionary<string, decimal> subTotalBill = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalPPN = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalPPH = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalNett = new Dictionary<string, decimal>();
 
                foreach (GarmentPaymentDispositionReportViewModel data in Query.ToList())
                {
                    string dispoNo = data.DispositionNo;

                    if (!dataByXPDC.ContainsKey(dispoNo)) dataByXPDC.Add(dispoNo, new List<GarmentPaymentDispositionReportViewModel> { });
                    dataByXPDC[dispoNo].Add(new GarmentPaymentDispositionReportViewModel
                    {
                        DispositionNo = data.DispositionNo,
                        PaymentDate = data.PaymentDate,
                        PaymentType = data.PaymentType,
                        PaymentMethod = data.PaymentMethod,
                        PaymentTerm = data.PaymentTerm,
                        BankName = data.BankName,
                        AccNumber = data.AccNumber,
                        BuyerCode = data.BuyerCode,
                        BuyerName = data.BuyerName,
                        InvoiceNumber = data.InvoiceNumber,
                        InvoiceDate = data.InvoiceDate,
                        InvoiceTaxNumber = data.InvoiceTaxNumber,                     
                        BillValue = data.BillValue,
                        VatValue = data.VatValue,
                        IncomeTaxRate = data.IncomeTaxRate,
                        IncomeTaxValue = data.IncomeTaxValue,
                        TotalBill = data.TotalBill,
                        XpdcCode = data.XpdcCode,
                        XpdcName = data.XpdcName,
                        UnitCode = data.UnitCode,
                        UnitPercentage = data.UnitPercentage,
                        UnitAmount = data.UnitAmount,
                    });

                    if (!subTotalBill.ContainsKey(dispoNo))
                    {
                        subTotalBill.Add(dispoNo, 0);
                    };

                    if (!subTotalPPN.ContainsKey(dispoNo))
                    {
                        subTotalPPN.Add(dispoNo, 0);
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
                    subTotalPPN[dispoNo] = data.VatValue;
                    subTotalPPH[dispoNo] = data.IncomeTaxValue;
                    subTotalNett[dispoNo] += data.UnitAmount;
                }

                decimal Total1 = 0;
                decimal Total2 = 0;
                decimal Total3 = 0;
                decimal Total4 = 0;

                int rowPosition = 12;

                foreach (KeyValuePair<string, List<GarmentPaymentDispositionReportViewModel>> dispoNo in dataByXPDC)
                {
                    string DispoNo = "";
                    int index = 0;
                    foreach (GarmentPaymentDispositionReportViewModel item in dispoNo.Value)
                    {
                        index++;

                        string InvDate = item.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : item.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string PayDate = item.PaymentDate == new DateTime(1970, 1, 1) ? "-" : item.PaymentDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                        string BillAmt = string.Format("{0:N2}", item.BillValue);
                        string ValAmt = string.Format("{0:N2}", item.VatValue);
                        string TaxRate = string.Format("{0:N2}", item.IncomeTaxRate);
                        string TaxAmt = string.Format("{0:N2}", item.IncomeTaxValue);
                        string TotBill = string.Format("{0:N2}", item.TotalBill);
                        string UnitPcnt = string.Format("{0:N2}", item.UnitPercentage);
                        string UnitAmt = string.Format("{0:N2}", item.UnitAmount);

                        result.Rows.Add(index, item.DispositionNo, PayDate, item.PaymentType, item.PaymentMethod, item.PaymentTerm, 
                                        item.XpdcCode, item.XpdcName, item.BankName, item.AccNumber, item.BuyerCode, item.BuyerName, 
                                        item.InvoiceNumber, InvDate, item.InvoiceTaxNumber, item.UnitCode, UnitPcnt, UnitAmt);

                        rowPosition += 1;
                        DispoNo = item.DispositionNo;
                    }

                    result.Rows.Add(".", ".", "NO DISPOSISI  :", DispoNo, ".", "NILAI TAGIHAN  :", Math.Round(subTotalBill[dispoNo.Key], 2), ".",
                                    "NILAI PPN  :", Math.Round(subTotalPPN[dispoNo.Key], 2), ".",
                                    "NILAI PPH  :", Math.Round(subTotalPPH[dispoNo.Key], 2), ".", 
                                    "TOTAL TAGIHAN :", Math.Round(subTotalPPH[dispoNo.Key], 2), ".", ".");

                    rowPosition += 1;
                    Total1 += subTotalBill[dispoNo.Key];
                    Total2 += subTotalPPN[dispoNo.Key];
                    Total3 += subTotalPPH[dispoNo.Key];
                    Total4 += subTotalNett[dispoNo.Key];      
                }
                result.Rows.Add(".", ".", "TOTAL :", ".", ".", "NILAI TAGIHAN  :", Math.Round(Total1, 2), ".", "NILAI PPN  :", Math.Round(Total2, 2), ".",
                                "NILAI PPH  :", Math.Round(Total3, 2), ".", "TOTAL TAGIHAN :", Math.Round(Total4, 2), ".", ".");
                
                rowPosition += 1;
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "SHEET1") }, true);
        }
    }
}
