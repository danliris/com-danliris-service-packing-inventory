using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShipingNoteCreditAdvice;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceMonitoringService : IGarmentShippingNoteCreditAdviceMonitoringService
    {
        private readonly IGarmentShippingNoteCreditAdviceRepository carepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentShippingNoteCreditAdviceMonitoringService(IServiceProvider serviceProvider)
        {
            carepository = serviceProvider.GetService<IGarmentShippingNoteCreditAdviceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentShippingNoteCreditAdviceMonitoringViewModel> GetData(string buyerAgent, string noteType, string noteNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryCA = carepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryCA = queryCA.Where(w => w.BuyerCode == buyerAgent);
            }

            if (!string.IsNullOrWhiteSpace(noteType))
            {
                queryCA = queryCA.Where(w => w.NoteType == noteType);
            }

            if (!string.IsNullOrWhiteSpace(noteNo))
            {
                queryCA = queryCA.Where(w => w.NoteNo == noteNo);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryCA = queryCA.Where(w => w.PaymentDate.AddHours(offset).Date >= DateFrom.Date && w.PaymentDate.AddHours(offset).Date <= DateTo.Date);

            queryCA = queryCA.OrderBy(w => w.NoteNo);

            var Query = (from a in queryCA
                         where a.PaymentTerm == (string.IsNullOrWhiteSpace(paymentTerm) ? a.PaymentTerm : paymentTerm)

                         select new GarmentShippingNoteCreditAdviceMonitoringViewModel
                        {
                            CAId = a.Id,
                            NoteNo = a.NoteNo,
                            Date = a.Date,
                            PaymentDate = a.PaymentDate,
                            PaymentTerm = a.PaymentTerm,
                            Amount = a.Amount,
                            PaidAmount = a.PaidAmount,
                            BalanceAmount = a.BalanceAmount,
                            NettNego = a.NettNego,
                            BuyerName = a.BuyerName,
                            BankName = a.BankAccountName,
                            BankComission = a.BankComission,
                            CreditInterest = a.CreditInterest,
                            BankCharge = a.BankCharges,
                            InsuranceCharge = a.InsuranceCharge,                            
                        });
            return Query;
        }

        public List<GarmentShippingNoteCreditAdviceMonitoringViewModel> GetReportData(string buyerAgent, string noteType, string noteNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, noteType, noteNo, paymentTerm, dateFrom, dateTo, offset);
            Query = Query.OrderBy(w => w.NoteNo).ThenBy(w => w.CAId);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string noteType, string noteNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetData(buyerAgent, noteType, noteNo, paymentTerm, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Note", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Note", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Payment", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Payment Term", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Name", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Bank Name", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Paid Amount", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Balance Amount", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Bank Comission", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Credit Interest", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Bank Charges", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Insurance Charges", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nett Nego", DataType = typeof(double) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", 0, 0, 0, 0, 0, 0, 0, 0);
            else
            {
                Dictionary<string, List<GarmentShippingNoteCreditAdviceMonitoringViewModel>> dataByBrand = new Dictionary<string, List<GarmentShippingNoteCreditAdviceMonitoringViewModel>>();
             
                Dictionary<string, double> subTotalPaid = new Dictionary<string, double>();
                Dictionary<string, double> subTotalNett = new Dictionary<string, double>();
                Dictionary<string, double> subTotalComm = new Dictionary<string, double>();
                Dictionary<string, double> subTotalBank = new Dictionary<string, double>();
                Dictionary<string, double> subTotalIntr = new Dictionary<string, double>();
                Dictionary<string, double> subTotalInst = new Dictionary<string, double>();

                foreach (GarmentShippingNoteCreditAdviceMonitoringViewModel item in Query.ToList())
                {
                    string BrandName = item.NoteNo;
                    if (!dataByBrand.ContainsKey(BrandName)) dataByBrand.Add(BrandName, new List<GarmentShippingNoteCreditAdviceMonitoringViewModel> { });
                    dataByBrand[BrandName].Add(new GarmentShippingNoteCreditAdviceMonitoringViewModel
                    {
                        NoteNo = item.NoteNo,
                        Date = item.Date,
                        PaymentDate = item.PaymentDate,
                        PaymentTerm = item.PaymentTerm,
                        Amount = item.Amount,
                        PaidAmount = item.PaidAmount,
                        BalanceAmount = item.BalanceAmount,
                        BuyerName = item.BuyerName,
                        BankName = item.BankName,            
                        BankComission = item.BankComission,
                        CreditInterest = item.CreditInterest,
                        BankCharge = item.BankCharge,
                        InsuranceCharge = item.InsuranceCharge,
                        NettNego = item.NettNego,
                    });

                    if (!subTotalPaid.ContainsKey(BrandName))
                    {
                        subTotalPaid.Add(BrandName, 0);
                    };

                    if (!subTotalNett.ContainsKey(BrandName))
                    {
                        subTotalNett.Add(BrandName, 0);
                    };

                    if (!subTotalComm.ContainsKey(BrandName))
                    {
                        subTotalComm.Add(BrandName, 0);
                    };

                    if (!subTotalBank.ContainsKey(BrandName))
                    {
                        subTotalBank.Add(BrandName, 0);
                    };

                    if (!subTotalIntr.ContainsKey(BrandName))
                    {
                        subTotalIntr.Add(BrandName, 0);
                    };

                    if (!subTotalInst.ContainsKey(BrandName))
                    {
                        subTotalInst.Add(BrandName, 0);
                    };

                    subTotalPaid[BrandName] += item.PaidAmount;
                    subTotalNett[BrandName] += item.NettNego;
                    subTotalComm[BrandName] += item.BankComission;
                    subTotalBank[BrandName] += item.CreditInterest;
                    subTotalIntr[BrandName] += item.BankCharge;
                    subTotalInst[BrandName] += item.InsuranceCharge;
                }

                double Total1 = 0;
                double Total2 = 0;
                double Total3 = 0;
                double Total4 = 0;
                double Total5 = 0;
                double Total6 = 0;

                int rowPosition = 12;

                foreach (KeyValuePair<string, List<GarmentShippingNoteCreditAdviceMonitoringViewModel>> BuyerBrand in dataByBrand)
                {
                    string BrandCode = "";
                    int index = 0;
                    foreach (GarmentShippingNoteCreditAdviceMonitoringViewModel item in BuyerBrand.Value)
                    {
                        index++;

                        string CADate = item.Date == new DateTime(1970, 1, 1) ? "-" : item.Date.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string PayDate = item.PaymentDate == new DateTime(1970, 1, 1) ? "-" : item.PaymentDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                        result.Rows.Add(index, item.NoteNo, CADate, PayDate, item.PaymentTerm, item.BuyerName, item.BankName, item.Amount, item.PaidAmount, 
                                        item.BalanceAmount, item.BankComission, item.CreditInterest, item.BankCharge, item.InsuranceCharge, item.NettNego);

                        rowPosition += 1;
                        BrandCode = item.NoteNo;
                    }

                    rowPosition += 1;
                    Total1 += subTotalPaid[BuyerBrand.Key];
                    Total2 += subTotalNett[BuyerBrand.Key];
                    Total3 += subTotalComm[BuyerBrand.Key];
                    Total4 += subTotalBank[BuyerBrand.Key];
                    Total5 += subTotalIntr[BuyerBrand.Key];
                    Total6 += subTotalInst[BuyerBrand.Key];

                }

                result.Rows.Add("", "", "", "TOTAL :", "", "", "", 0, Math.Round(Total1, 2), 0, Math.Round(Total3, 2), Math.Round(Total4, 2), Math.Round(Total5, 2), Math.Round(Total6, 2), Math.Round(Total2, 2));

            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
