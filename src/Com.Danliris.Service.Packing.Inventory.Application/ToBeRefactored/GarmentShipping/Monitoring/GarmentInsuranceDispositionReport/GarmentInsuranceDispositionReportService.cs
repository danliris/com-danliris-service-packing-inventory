using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInsuranceDispositionReport
{
    public class GarmentInsuranceDispositionReportService : IGarmentInsuranceDispositionReportService
    {
        private readonly IGarmentShippingInsuranceDispositionRepository repository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentInsuranceDispositionReportService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInsuranceDispositionRepository>();      
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentInsuranceDispositionReportViewModel> GetDataQuery(string policytype, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
           var query = repository.ReadAll();
           var queryItem = repository.ReadItemAll();
      
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            query = query.Where(w => w.PaymentDate.AddHours(offset).Date >= DateFrom.Date && w.PaymentDate.AddHours(offset).Date <= DateTo.Date);

            query = query.OrderBy(w => w.PolicyType).ThenBy(w => w.DispositionNo);

            var newQ = (from a in query
                        join b in queryItem on a.Id equals b.InsuranceDispositionId
                        where a.PolicyType == (string.IsNullOrWhiteSpace(policytype) ? a.PolicyType : policytype)

                        select new GarmentInsuranceDispositionReportViewModel
                        {                           
                            DispositionNo = a.DispositionNo,
                            PaymentDate = a.PaymentDate,
                            PolicyType = a.PolicyType,
                            BankName = a.BankName,
                            InsuranceCode = a.InsuranceCode,
                            InsuranceName = a.InsuranceName,
                            Rate = a.Rate,
                            PolicyNo = b.PolicyNo,
                            PolicyDate = b.PolicyDate,
                            InvoiceNo = b.InvoiceNo,
                            BuyerCode = b.BuyerAgentCode,
                            BuyerName = b.BuyerAgentName,
                            CurrencyRate = b.CurrencyRate,
                            Amount = b.Amount,
                            AmountC1A = b.Amount1A,
                            AmountC1B = b.Amount1B,
                            AmountC2A = b.Amount2A,
                            AmountC2B = b.Amount2B,
                            AmountC2C = b.Amount2C,
                            PremiAmount = a.PolicyType == "Piutang" ? (a.Rate / 100) * b.Amount: 0,                            
                        });
            return newQ;
        }

        public List<GarmentInsuranceDispositionReportViewModel> GetReportData(string policytype, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetDataQuery(policytype, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.PolicyType).ThenBy(b => b.DispositionNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string policytype, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetDataQuery(policytype, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Disposisi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Bayar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Bank", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Polis", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Rate %", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Asuransi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Asuransi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Polis", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Polis", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kurs", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Premi", DataType = typeof(string) });            
            result.Columns.Add(new DataColumn() { ColumnName = "Amount C1A", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount C1B", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount C2A", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount C2B", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount C2C", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                Dictionary<string, List<GarmentInsuranceDispositionReportViewModel>> dataBySupplier = new Dictionary<string, List<GarmentInsuranceDispositionReportViewModel>>();
                Dictionary<string, decimal> subTotalAmount = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalPremi = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmt1A = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmt1B = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmt2A = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmt2B = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmt2C= new Dictionary<string, decimal>();

                foreach (GarmentInsuranceDispositionReportViewModel data in Query.ToList())
                {
                    string policyType = data.PolicyType;
                   
                    if (!dataBySupplier.ContainsKey(policyType)) dataBySupplier.Add(policyType, new List<GarmentInsuranceDispositionReportViewModel> { });
                    dataBySupplier[policyType].Add(new GarmentInsuranceDispositionReportViewModel
                    {
                        DispositionNo = data.DispositionNo,
                        PaymentDate = data.PaymentDate,
                        PolicyType = data.PolicyType,
                        BankName = data.BankName,
                        InsuranceCode = data.InsuranceCode,
                        InsuranceName = data.InsuranceName,
                        Rate = data.Rate,
                        PolicyNo = data.PolicyNo,
                        PolicyDate = data.PolicyDate,
                        InvoiceNo = data.InvoiceNo,
                        BuyerCode = data.BuyerCode,
                        BuyerName = data.BuyerName,
                        CurrencyRate = data.CurrencyRate,
                        Amount = data.Amount,  
                        AmountC1A = data.AmountC1A,
                        AmountC1B = data.AmountC1B,
                        AmountC2A = data.AmountC2A,
                        AmountC2B = data.AmountC2B,
                        AmountC2C = data.AmountC2C,
                        PremiAmount = data.PremiAmount,
                    });
            
                    if (!subTotalAmount.ContainsKey(policyType))
                    {
                        subTotalAmount.Add(policyType, 0);
                    };

                    if (!subTotalPremi.ContainsKey(policyType))
                    {
                        subTotalPremi.Add(policyType, 0);
                    };

                    if (!subTotalAmt1A.ContainsKey(policyType))
                    {
                        subTotalAmt1A.Add(policyType, 0);
                    };

                    if (!subTotalAmt1B.ContainsKey(policyType))
                    {
                        subTotalAmt1B.Add(policyType, 0);
                    };

                    if (!subTotalAmt2A.ContainsKey(policyType))
                    {
                        subTotalAmt2A.Add(policyType, 0);
                    };

                    if (!subTotalAmt2B.ContainsKey(policyType))
                    {
                        subTotalAmt2B.Add(policyType, 0);
                    };

                    if (!subTotalAmt2C.ContainsKey(policyType))
                    {
                        subTotalAmt2C.Add(policyType, 0);
                    };

                    subTotalAmount[policyType] += data.Amount;
                    subTotalPremi[policyType] += data.PremiAmount;
                    subTotalAmt1A[policyType] += data.AmountC1A;
                    subTotalAmt1B[policyType] += data.AmountC1B;
                    subTotalAmt2A[policyType] += data.AmountC2A;
                    subTotalAmt2B[policyType] += data.AmountC2B;
                    subTotalAmt2C[policyType] += data.AmountC2C;
                    
                }

                decimal Total1 = 0;
                decimal Total2 = 0;
                decimal Total3 = 0;
                decimal Total4 = 0;
                decimal Total5 = 0;
                decimal Total6 = 0;
                decimal Total7 = 0;

                int rowPosition = 13;

                foreach (KeyValuePair<string, List<GarmentInsuranceDispositionReportViewModel>> policyType in dataBySupplier)
                {
                    string DispoType = "";
                    int index = 0;
                    foreach (GarmentInsuranceDispositionReportViewModel item in policyType.Value)
                    {
                        index++;

                        string PayDate = item.PaymentDate == new DateTime(1970, 1, 1) ? "-" : item.PaymentDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string PlcDate = item.PolicyDate == new DateTime(1970, 1, 1) ? "-" : item.PolicyDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                        string AmtTotal = string.Format("{0:N2}", item.Amount);
                        string AmtPremi = string.Format("{0:N2}", item.PremiAmount);
                        string AmtC1A = string.Format("{0:N2}", item.AmountC1A);
                        string AmtC1B = string.Format("{0:N2}", item.AmountC1B);
                        string AmtC2A = string.Format("{0:N2}", item.AmountC2A);
                        string AmtC2B = string.Format("{0:N2}", item.AmountC2B);
                        string AmtC2C = string.Format("{0:N2}", item.AmountC2C);
                        string RatePls = string.Format("{0:N2}", item.Rate);
                        string RateUSD= string.Format("{0:N2}", item.CurrencyRate);

                        result.Rows.Add(index, item.DispositionNo, PayDate, item.BankName, item.PolicyType, RatePls, item.InsuranceCode, item.InsuranceName, item.PolicyNo,
                                         PlcDate, item.InvoiceNo, item.BuyerCode, item.BuyerName, RateUSD, AmtTotal, AmtPremi, AmtC1A, AmtC1B, AmtC2A, AmtC2B, AmtC2C);
                        rowPosition += 1;
                        DispoType = item.PolicyType;
                    }

                    result.Rows.Add(".", ".", ".", ".", ".", ".", "SUB TOTAL", ".", ".", ".", DispoType, ".", ".", ":", Math.Round(subTotalAmt1A[policyType.Key], 2), Math.Round(subTotalAmt1B[policyType.Key], 2), Math.Round(subTotalAmt2A[policyType.Key], 2), Math.Round(subTotalAmt2B[policyType.Key], 2), Math.Round(subTotalAmt2C[policyType.Key], 2));

                    rowPosition += 1;
                    Total1 += subTotalAmount[policyType.Key];
                    Total2 += subTotalPremi[policyType.Key];
                    Total3 += subTotalAmt1A[policyType.Key];
                    Total4 += subTotalAmt1B[policyType.Key];
                    Total5 += subTotalAmt2A[policyType.Key];
                    Total6 += subTotalAmt2B[policyType.Key];
                    Total7 += subTotalAmt2C[policyType.Key];
                }
                result.Rows.Add(".", ".", ".", ".", ".", ".", "T O T A L :", ".", ".", ".", ".", ".", ".", ":", Math.Round(Total1, 2), Math.Round(Total2, 2), Math.Round(Total3, 2), Math.Round(Total4, 2), Math.Round(Total5, 2), Math.Round(Total6, 2), Math.Round(Total7, 2));
                rowPosition += 1;
            }        
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "SHEET1") }, true);
        }
    }
}
