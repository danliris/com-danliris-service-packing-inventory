using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice
{
    public class GarmentBuyerReceivablesReportService : IGarmentBuyerReceivablesReportService
    {
        private readonly IGarmentShippingInvoiceRepository repository;     
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingCreditAdviceRepository carepository;      
        private readonly IIdentityProvider _identityProvider;

        public GarmentBuyerReceivablesReportService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();           
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            carepository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();            
        }

        public IQueryable<GarmentBuyerReceivablesReportViewModel> GetData(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();         
            var queryPL = plrepository.ReadAll();
            var queryCA = carepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryInv = queryInv.Where(w => w.BuyerAgentCode == buyerAgent);
            }

            if (!string.IsNullOrWhiteSpace(invoiceNo))
            {
                queryInv = queryInv.Where(w => w.InvoiceNo == invoiceNo);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            //queryInv = queryInv.Where(w => w.SailingDate.AddHours(offset).Date >= DateFrom.Date && w.SailingDate.AddHours(offset).Date <= DateTo.Date);
            
            queryPL = queryPL.Where(w => w.Omzet == true);
            queryPL = queryPL.Where(w => w.OtherCommodity != "NEGO23");

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryInv = queryInv.OrderBy(w => w.BuyerAgentCode).ThenBy(b => b.InvoiceNo);

            var newQ = (from a in queryInv
                        join c in queryPL on a.PackingListId equals c.Id
                        join d in queryCA on a.Id equals d.InvoiceId into dd
                        from CA in dd.DefaultIfEmpty()
                        where a.IsDeleted == false && c.IsDeleted == false && CA.IsDeleted == false
    
                        select new GarmentBuyerReceivablesReportViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = a.InvoiceDate,
                            BuyerAgentName = a.BuyerAgentName,                            
                            ToBePaid = CA == null ? a.AmountToBePaid : Convert.ToDecimal(CA.AmountToBePaid),
                            DHLCharges = CA == null ? a.DHLCharges : Convert.ToDecimal(CA.DHLCharges),
                            SailingDate = c.TruckingDate,
                            PaymentDue = a.PaymentDue,
                            DueDate = a.SailingDate.AddDays(a.PaymentDue),
                            PaymentDate = CA == null ? new DateTime(1970, 1, 1) : CA.PaymentDate,
                            PaymentAmount = CA == null ? 0 : CA.AmountPaid,
                            BankCharges = CA == null ? 0 : CA.BankCharges,
                            BankComission = CA == null ? 0 : CA.BankComission,
                            CreditInterest = CA == null ? 0 : CA.CreditInterest,
                            OtherCharges = CA == null ? 0 : CA.PaymentTerm == "TT/OA" ? CA.OtherCharge : 0,
                            DiscrepancyFee = CA == null ? 0 : CA.PaymentTerm == "TT/OA" ? 0 : CA.DiscrepancyFee,
                            //OtherCharges = CA == null ? 0 : CA.PaymentTerm == "TT/OA" ? (CA.BankComission + CA.CreditInterest + CA.OtherCharge) : (CA.BankComission + CA.DiscrepancyFee + CA.CreditInterest),
                            ReceiptAmount = CA == null ? 0 : CA.PaymentTerm == "TT/OA" ? CA.AmountPaid - (CA.BankCharges + CA.OtherCharge + CA.BankComission + CA.CreditInterest) : CA.AmountPaid - (CA.BankCharges + CA.DiscrepancyFee + CA.BankComission + CA.CreditInterest), 
                            OutStandingAmount = CA == null ? Convert.ToDouble(a.AmountToBePaid + a.DHLCharges) : (CA.AmountToBePaid + CA.DHLCharges) - CA.AmountPaid,
                            BankDetail = CA == null ? "-" : CA.BankAccountName,
                            ReceiptNo = CA == null ? "-" : CA.ReceiptNo,
                            OverDue = CA == null ? 0 : (CA.PaymentDate.ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date - a.SailingDate.AddDays(a.PaymentDue).ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date).Days,
                        });
            return newQ;
        }

        public List<GarmentBuyerReceivablesReportViewModel> GetReportData(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, invoiceNo, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.InvoiceNo).ThenBy(b => b.PaymentDate);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            var Query = GetData(buyerAgent, invoiceNo, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL TRUCKING", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER AGENT", DataType = typeof(string) });       
            
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT TO BE PAID (US$)", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "DHL CHARGES (US$)", DataType = typeof(decimal) });

            result.Columns.Add(new DataColumn() { ColumnName = "ETD DATE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TEMPO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "DUE DATE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PAYMENT DATE", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "PAYMENT AMOUNT (US$)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "BANK COMISSION (US$)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "CREDIT INTEREST (US$)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "DISCREPANCY FEE (US$)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "BANK CHARGES (US$)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "OTHER CHARGES (US$)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT RECEIPT (US$)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "OUTSTANDING AMOUNT (US$)", DataType = typeof(double) });

            result.Columns.Add(new DataColumn() { ColumnName = "BANK DETAILS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "RECEIPT NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "OVERDUE", DataType = typeof(string) });

            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
               {
                     result.Rows.Add("", "", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 0, 0, 0, 0, "", "", "");
                     bool styling = true;

                    foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                    {
                        var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:Q1"].Value = "MONITORING SALDO PIUTANG BUYER";
                    sheet.Cells[$"A1:Q1"].Merge = true;
                    sheet.Cells[$"A1:Q1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:Q1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:Q1"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:Q3"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd-MM-yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd-MM-yyyy", new CultureInfo("id-ID")));
                    sheet.Cells[$"A3:Q3"].Merge = true;
                    sheet.Cells[$"A3:Q3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A3:Q3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A3:Q3"].Style.Font.Bold = true;
                    sheet.Cells[$"A4:Q4"].Value = string.Format("Buyer : {0}", string.IsNullOrWhiteSpace(buyerAgent) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().BuyerAgentName));
                    sheet.Cells[$"A4:Q4"].Merge = true;
                    sheet.Cells[$"A4:Q4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A4:Q4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:Q4"].Style.Font.Bold = true;
                    sheet.Cells[$"A5:Q5"].Value = string.Format("Invoice No : {0}", string.IsNullOrWhiteSpace(invoiceNo) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().InvoiceNo));
                    sheet.Cells[$"A5:Q5"].Merge = true;
                    sheet.Cells[$"A5:Q5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A5:Q5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A5:Q5"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells["A7"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string SailDate = d.SailingDate == new DateTime(1970, 1, 1) ? "-" : d.SailingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string JTDate = d.DueDate == new DateTime(1970, 1, 1) ? "-" : d.DueDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string PayDate = d.PaymentDate == new DateTime(1970, 1, 1) ? "-" : d.PaymentDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    string TBPaid = string.Format("{0:N2}", d.ToBePaid);
                    string AmtPaid = string.Format("{0:N2}", d.PaymentAmount);
                    string BCharges = string.Format("{0:N2}", d.BankCharges);
                    string OCharges = string.Format("{0:N2}", d.OtherCharges);
                    string RcptAmt = string.Format("{0:N2}", d.ReceiptAmount);
                    string OutStdg = string.Format("{0:N2}", d.OutStandingAmount);

                    result.Rows.Add(index, d.InvoiceNo, InvDate, SailDate, d.BuyerAgentName, d.ToBePaid, d.DHLCharges, SailDate, d.PaymentDue, JTDate, PayDate, d.PaymentAmount, d.BankComission, d.CreditInterest, d.DiscrepancyFee, d.BankCharges, d.OtherCharges, d.ReceiptAmount, d.OutStandingAmount, d.BankDetail, d.ReceiptNo, d.OverDue);
                }

                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:Q1"].Value = "MONITORING SALDO PIUTANG BUYER";
                    sheet.Cells[$"A1:Q1"].Merge = true;
                    sheet.Cells[$"A1:Q1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:Q1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:Q1"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:Q3"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd-MM-yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd-MM-yyyy", new CultureInfo("id-ID")));
                    sheet.Cells[$"A3:Q3"].Merge = true;
                    sheet.Cells[$"A3:Q3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A3:Q3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A3:Q3"].Style.Font.Bold = true;
                    sheet.Cells[$"A4:Q4"].Value = string.Format("Buyer : {0}", string.IsNullOrWhiteSpace(buyerAgent) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().BuyerAgentName));
                    sheet.Cells[$"A4:Q4"].Merge = true;
                    sheet.Cells[$"A4:Q4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A4:Q4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:Q4"].Style.Font.Bold = true;
                    sheet.Cells[$"A5:Q5"].Value = string.Format("Invoice No : {0}", string.IsNullOrWhiteSpace(invoiceNo) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().InvoiceNo));
                    sheet.Cells[$"A5:Q5"].Merge = true;
                    sheet.Cells[$"A5:Q5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A5:Q5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A5:Q5"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells["A7"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;

            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
