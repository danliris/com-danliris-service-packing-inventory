using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdvice
{
    public class GarmentCreditAdviceMonitoringService : IGarmentCreditAdviceMonitoringService
    {
        private readonly IGarmentShippingCreditAdviceRepository carepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentCreditAdviceMonitoringService(IServiceProvider serviceProvider)
        {
            carepository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentCreditAdviceMonitoringViewModel> GetData(string buyerAgent, string invoiceNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryCA = carepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryCA = queryCA.Where(w => w.BuyerName == buyerAgent);
            }

            if (!string.IsNullOrWhiteSpace(invoiceNo))
            {
                queryCA = queryCA.Where(w => w.InvoiceNo == invoiceNo);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryCA = queryCA.Where(w => w.PaymentDate.AddHours(offset).Date >= DateFrom.Date && w.PaymentDate.AddHours(offset).Date <= DateTo.Date);

            queryCA = queryCA.OrderBy(w => w.InvoiceNo);

            var Query = (from a in queryCA
                         where a.PaymentTerm == (string.IsNullOrWhiteSpace(paymentTerm) ? a.PaymentTerm : paymentTerm)

                         select new GarmentCreditAdviceMonitoringViewModel
                        {
                            CAId = a.Id,
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = a.Date,
                            PaymentDate = a.PaymentDate,
                            DocUploadDate = a.DocumentSendDate,
                            PaymentTerm = a.PaymentTerm,
                            Amount = a.Amount,
                            ToBePaid = a.AmountToBePaid,
                            PaidAmount = a.AmountPaid,
                            BalanceAmount = a.BalanceAmount,
                            NettNego = a.NettNego + a.BankCharges + a.OtherCharge + a.BankComission + a.DiscrepancyFee + a.CreditInterest,
                            BuyerName = a.BuyerName,
                            BuyerAddress = a.BuyerAddress,
                            BankName = a.BankAccountName,
                            
                            NettNegoTT = a.PaymentTerm == "TT/OA" ? a.NettNego : 0,
                            BankChargeTT = a.PaymentTerm == "TT/OA" ? a.BankCharges : 0,
                            OtherChargeTT = a.PaymentTerm == "TT/OA" ? a.OtherCharge : 0,

                            SRNo = a.PaymentTerm == "LC" ? a.SRNo : "-",
                            SRDate = a.PaymentTerm == "LC" ? a.NegoDate : new DateTime(1970, 1, 1),
                            LCNo = a.PaymentTerm == "LC" ? a.LCNo : "-",
                            NettNegoLC = a.PaymentTerm == "LC" ? a.NettNego : 0,
                            BankChargeLC = a.PaymentTerm == "LC" ? a.BankCharges : 0,
                            BankComissionLC = a.PaymentTerm == "LC" ? a.BankComission : 0,
                            DiscreapancyFeeLC = a.PaymentTerm == "LC" ? a.DiscrepancyFee : 0,
                            CreditInterestLC = a.PaymentTerm == "LC" ? a.CreditInterest : 0,
                        });
            return Query;
        }

        public List<GarmentCreditAdviceMonitoringViewModel> GetReportData(string buyerAgent, string invoiceNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, invoiceNo, paymentTerm, dateFrom, dateTo, offset);
            Query = Query.OrderBy(w => w.InvoiceNo).ThenBy(w => w.CAId);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string invoiceNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetData(buyerAgent, invoiceNo, paymentTerm, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Payment", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Kirim Dokumen", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Payment Term", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Name", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Address", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Bank Name", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount To Be Paid", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Paid Amount", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Balance Amount", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nett Nego | TT", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Bank Charges | TT", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Other Charges | TT", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "No LC | LC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No SR | LC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal SR | LC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nett Nego | LC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Bank Comission | LC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Discreapancy Fee | LC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Credit Interest | LC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Bank Charges | LC", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                Dictionary<string, List<GarmentCreditAdviceMonitoringViewModel>> dataByBrand = new Dictionary<string, List<GarmentCreditAdviceMonitoringViewModel>>();
                Dictionary<string, double> subTotalAMT = new Dictionary<string, double>();
                Dictionary<string, double> subTotalTBP = new Dictionary<string, double>();
                Dictionary<string, double> subTotalNett = new Dictionary<string, double>();
                Dictionary<string, double> subTotalNettTT = new Dictionary<string, double>();
                Dictionary<string, double> subTotalNettLC = new Dictionary<string, double>();
                Dictionary<string, double> outStanding = new Dictionary<string, double>();
                
                foreach (GarmentCreditAdviceMonitoringViewModel item in Query.ToList())
                {
                    string BrandName = item.InvoiceNo;
                    if (!dataByBrand.ContainsKey(BrandName)) dataByBrand.Add(BrandName, new List<GarmentCreditAdviceMonitoringViewModel> { });
                    dataByBrand[BrandName].Add(new GarmentCreditAdviceMonitoringViewModel
                    {

                        InvoiceNo = item.InvoiceNo,
                        InvoiceDate = item.InvoiceDate,
                        PaymentDate = item.PaymentDate,
                        DocUploadDate = item.DocUploadDate,
                        PaymentTerm = item.PaymentTerm,
                        Amount = item.Amount,
                        ToBePaid = item.ToBePaid,
                        PaidAmount = item.PaidAmount,
                        BalanceAmount = item.BalanceAmount, 
                        BuyerName = item.BuyerName,
                        BuyerAddress = item.BuyerAddress,
                        BankName = item.BankName,
                        NettNego = item.NettNego,
                        NettNegoTT = item.NettNegoTT,
                        BankChargeTT = item.BankChargeTT,
                        OtherChargeTT = item.OtherChargeTT,

                        SRNo = item.SRNo,
                        SRDate = item.SRDate,
                        LCNo = item.LCNo,
                        NettNegoLC = item.NettNegoLC,
                        BankChargeLC = item.BankChargeLC,
                        BankComissionLC = item.BankComissionLC,
                        DiscreapancyFeeLC = item.DiscreapancyFeeLC,
                        CreditInterestLC = item.CreditInterestLC,                       
                    });

                    if (!subTotalNett.ContainsKey(BrandName))
                    {
                        subTotalNett.Add(BrandName, 0);
                    };

                    if (!subTotalNettTT.ContainsKey(BrandName))
                    {
                        subTotalNettTT.Add(BrandName, 0);
                    };

                    if (!subTotalNettLC.ContainsKey(BrandName))
                    {
                        subTotalNettLC.Add(BrandName, 0);
                    };
   
                    subTotalAMT[BrandName] = item.Amount;
                    subTotalTBP[BrandName] = item.ToBePaid;
                    subTotalNett[BrandName] += item.NettNego;
                    subTotalNettTT[BrandName] += item.NettNegoTT;
                    subTotalNettLC[BrandName] += item.NettNegoLC;
                    
                    outStanding[BrandName] = subTotalTBP[BrandName] - subTotalNett[BrandName];
                    
                }

                int rowPosition = 1;
                foreach (KeyValuePair<string, List<GarmentCreditAdviceMonitoringViewModel>> BuyerBrand in dataByBrand)
                {
                    string BrandCode = "";
                    int index = 0;
                    foreach (GarmentCreditAdviceMonitoringViewModel item in BuyerBrand.Value)
                    {
                        index++;
                    
                        string InvDate = item.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : item.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string DocDate = item.DocUploadDate == new DateTime(1970, 1, 1) ? "-" : item.DocUploadDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string SRDate = item.SRDate == new DateTime(1970, 1, 1) ? "-" : item.SRDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string PayDate = item.PaymentDate == new DateTime(1970, 1, 1) ? "-" : item.PaymentDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                        string AmtFOB = string.Format("{0:N2}", item.Amount);
                        string AmtPaid = string.Format("{0:N2}", item.ToBePaid);
                        string PaidAmt = string.Format("{0:N2}", item.PaidAmount);
                        string BlncAmt = string.Format("{0:N2}", item.BalanceAmount);

                        string NettTT = string.Format("{0:N2}", item.Amount);
                        string BChrgTT = string.Format("{0:N2}", item.ToBePaid);
                        string OChrgTT = string.Format("{0:N2}", item.Amount);

                        string CommLC = string.Format("{0:N2}", item.ToBePaid);
                        string FeeLC = string.Format("{0:N2}", item.Amount);
                        string NettLC = string.Format("{0:N2}", item.ToBePaid);
                        string IntLC = string.Format("{0:N2}", item.Amount);
                        string BChrgLC = string.Format("{0:N2}", item.ToBePaid);

                        result.Rows.Add(index, item.InvoiceNo, InvDate, PayDate, DocDate, item.PaymentTerm, item.BuyerName, item.BuyerAddress, item.BankName, 
                                        AmtFOB, AmtPaid, PaidAmt, BlncAmt, NettTT, BChrgTT, OChrgTT, item.LCNo, item.SRNo, SRDate, CommLC, FeeLC, NettLC, IntLC, BChrgLC);

                        rowPosition += 1;
                        BrandCode = item.InvoiceNo;
                    }

                    result.Rows.Add("", "", "INVOICE NO :", BrandCode, "", "", "AMOUNT :", Math.Round(subTotalAMT[BuyerBrand.Key], 2), "", "", "AMOUNT TO BE PAID :", Math.Round(subTotalTBP[BuyerBrand.Key], 2), "", "", "PAID AMOUNT :", Math.Round(subTotalNett[BuyerBrand.Key], 2), "", "", "OUTSTANDING AMOUNT :", Math.Round(outStanding[BuyerBrand.Key], 2), "");

                    rowPosition += 1;
                }
            }          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
