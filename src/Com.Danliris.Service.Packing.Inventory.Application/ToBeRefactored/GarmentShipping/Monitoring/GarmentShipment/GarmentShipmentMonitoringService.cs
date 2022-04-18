using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment
{
    public class GarmentShipmentMonitoringService : IGarmentShipmentMonitoringService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IGarmentShippingInvoiceAdjustmentRepository adjrepository;
        private readonly IGarmentCoverLetterRepository clrepository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingCreditAdviceRepository carepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentShipmentMonitoringService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            adjrepository = serviceProvider.GetService<IGarmentShippingInvoiceAdjustmentRepository>();
            clrepository = serviceProvider.GetService<IGarmentCoverLetterRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            carepository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentShipmentMonitoringViewModel> GetData(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
            var queryItm = itemrepository.ReadAll();
            var queryAdj = adjrepository.ReadAll();
            var queryPL = plrepository.ReadAll();
            var queryCL = clrepository.ReadAll();
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
            
            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryInv = queryInv.OrderBy(w => w.InvoiceNo);

            var NewQ1 = (from a in queryInv
                        join b in queryAdj on a.Id equals b.GarmentShippingInvoiceId into cc
                        from InvAdj in cc.DefaultIfEmpty()
                        join c in queryPL on a.PackingListId equals c.Id
                        where a.IsDeleted == false && c.IsDeleted == false && InvAdj.IsDeleted == false 

                        select new GarmentShipmentMonitoringViewModel
                        {
                            InvoiceID = a.Id,
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = a.InvoiceDate,
                            TruckingDate = c.TruckingDate,
                            BuyerAgentName = a.BuyerAgentCode + " - " + a.BuyerAgentName,
                            ConsigneeName = a.Consignee,
                            SectionCode = a.SectionCode,
                            SailingDate = a.SailingDate,
                            CONo = a.CO,
                            PaymentDue = a.PaymentDue,
                            PEBNo = a.PEBNo,
                            PEBDate = a.PEBDate,
                            OriginPort = a.From,
                            DestinationPort = a.To,
                            ShippingStaffName = a.ShippingStaff,
                            AdjustmentValue = InvAdj == null ? 0 : InvAdj.AdjustmentValue,    
                        });

            var NewQ2 = (from aa in NewQ1

                         group new { AdjAmt = aa.AdjustmentValue } by new
                         {
                             aa.InvoiceID,
                             aa.InvoiceNo,
                             aa.InvoiceDate,
                             aa.TruckingDate,
                             aa.BuyerAgentName,
                             aa.ConsigneeName,
                             aa.SectionCode,
                             aa.SailingDate,
                             aa.CONo,
                             aa.PaymentDue,
                             aa.PEBNo,
                             aa.PEBDate,
                             aa.OriginPort,
                             aa.DestinationPort,
                             aa.ShippingStaffName, 
                         } into G


                         select new GarmentShipmentMonitoringViewModel
                         {
                             InvoiceID = G.Key.InvoiceID,
                             InvoiceNo = G.Key.InvoiceNo,
                             InvoiceDate = G.Key.InvoiceDate,
                             TruckingDate = G.Key.TruckingDate,
                             BuyerAgentName = G.Key.BuyerAgentName,
                             ConsigneeName = G.Key.ConsigneeName,
                             SectionCode = G.Key.SectionCode,
                             SailingDate = G.Key.SailingDate,
                             CONo = G.Key.CONo,
                             PaymentDue = G.Key.PaymentDue,
                             PEBNo = G.Key.PEBNo,
                             PEBDate = G.Key.PEBDate,
                             OriginPort = G.Key.OriginPort,
                             DestinationPort = G.Key.DestinationPort,
                             ShippingStaffName = G.Key.ShippingStaffName,
                             AdjustmentAmount = Math.Round(G.Sum(m => m.AdjAmt), 2),
                         });

            var NewQ3 = (from aa in NewQ2
                        join bb in queryItm on aa.InvoiceID equals bb.GarmentShippingInvoiceId
                        join cc in queryCL on aa.InvoiceID equals cc.InvoiceId into dd
                        from CL in dd.DefaultIfEmpty()
                        join ee in queryCA on aa.InvoiceID equals ee.InvoiceId into ff
                        from CA in ff.DefaultIfEmpty()
                        where bb.IsDeleted == false && CL.IsDeleted == false && CA.IsDeleted == false

                        select new GarmentShipmentMonitoringViewModel
                        {
                            InvoiceID = aa.InvoiceID,
                            InvoiceNo = aa.InvoiceNo,
                            InvoiceDate = aa.InvoiceDate,
                            TruckingDate = aa.TruckingDate,
                            BuyerAgentName = aa.BuyerAgentName,
                            ConsigneeName = aa.ConsigneeName,
                            BuyerBrandName = bb.BuyerBrandName,
                            ComodityName = bb.ComodityCode + " - " + bb.ComodityName,
                            SectionCode = aa.SectionCode,
                            SailingDate = aa.SailingDate,
                            BookingDate = CL == null ? new DateTime(1970, 1, 1) : CL.BookingDate,
                            ExpFactoryDate = CL == null ? new DateTime(1970, 1, 1) : CL.ExportEstimationDate,
                            CONo = aa.CONo,
                            PaymentDue = aa.PaymentDue,
                            PEBNo = aa.PEBNo,
                            PEBDate = aa.PEBDate,
                            OriginPort = aa.OriginPort,
                            DestinationPort = aa.DestinationPort,
                            ShippingStaffName = aa.ShippingStaffName,
                            Amount = bb.Amount,
                            CMTAmount = Convert.ToDecimal(bb.Quantity) * bb.CMTPrice,
                            CMTAmountSub = bb.CMTPrice != 0 ? (Convert.ToDecimal(bb.Quantity) * bb.CMTPrice) : bb.Amount,
                            LessfabricCost = bb.CMTPrice == 0 ? 0 : bb.Amount - (Convert.ToDecimal(bb.Quantity) * bb.CMTPrice),
                            AdjustmentValue = 0,
                            AdjustmentAmount = aa.AdjustmentAmount,
                            EMKLName = CL.Name ?? "-",
                            ForwarderName = CL.ForwarderName ?? "-",
                            DocSendDate = CA == null ? new DateTime(1970, 1, 1) : CA.DocumentSendDate,
                            PaymentDate = CA == null ? new DateTime(1970, 1, 1) : CA.PaymentDate,
                            DueDate = aa.SailingDate.AddDays(aa.PaymentDue),
                            DiffBDCL = CL == null ? 0 : (CL.ExportEstimationDate.ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date - CL.BookingDate.ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date).Days,
                            DiffETDDSD = CA == null ? 0 : (aa.SailingDate.AddDays(5).ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date - CA.DocumentSendDate.ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date).Days,
                            DiffDDPD = CA == null ? 0 : (aa.SailingDate.AddDays(aa.PaymentDue).ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date - CA.PaymentDate.ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date).Days,
                        });


            var Query = (from dd in NewQ3

                         group dd by new
                         {
                             dd.InvoiceID,
                             dd.InvoiceNo,
                             dd.InvoiceDate,
                             dd.TruckingDate,
                             dd.BuyerAgentName,
                             dd.ConsigneeName,
                             dd.BuyerBrandName,
                             dd.ComodityName,
                             dd.SectionCode,
                             dd.SailingDate,
                             dd.BookingDate,
                             dd.ExpFactoryDate,
                             dd.CONo,
                             dd.PaymentDue,
                             dd.PEBNo,
                             dd.PEBDate,
                             dd.OriginPort,
                             dd.DestinationPort,
                             dd.ShippingStaffName,
                             dd.Amount,
                             dd.CMTAmount,
                             dd.CMTAmountSub,
                             dd.LessfabricCost,
                             dd.AdjustmentValue,
                             dd.AdjustmentAmount,
                             dd.EMKLName,
                             dd.ForwarderName,
                             dd.DocSendDate,
                             dd.PaymentDate,
                             dd.DueDate,
                             dd.DiffBDCL,
                             dd.DiffETDDSD,
                             dd.DiffDDPD,
                         } into G


                         select new GarmentShipmentMonitoringViewModel
                         {
                             InvoiceID = G.Key.InvoiceID,
                             InvoiceNo = G.Key.InvoiceNo,
                             InvoiceDate = G.Key.InvoiceDate,
                             TruckingDate = G.Key.TruckingDate,
                             BuyerAgentName = G.Key.BuyerAgentName,
                             ConsigneeName = G.Key.ConsigneeName,
                             BuyerBrandName = G.Key.BuyerBrandName,
                             ComodityName = G.Key.ComodityName,
                             SectionCode = G.Key.SectionCode,
                             SailingDate = G.Key.SailingDate,
                             BookingDate = G.Key.BookingDate,
                             ExpFactoryDate = G.Key.ExpFactoryDate,
                             CONo = G.Key.CONo,
                             PaymentDue = G.Key.PaymentDue,
                             PEBNo = G.Key.PEBNo,
                             PEBDate = G.Key.PEBDate,
                             OriginPort = G.Key.OriginPort,
                             DestinationPort = G.Key.DestinationPort,
                             ShippingStaffName = G.Key.ShippingStaffName,
                             Amount = G.Key.Amount,
                             CMTAmount = G.Key.CMTAmount,
                             CMTAmountSub = G.Key.CMTAmountSub,
                             LessfabricCost = G.Key.LessfabricCost,
                             AdjustmentValue = 0,
                             AdjustmentAmount = G.Key.AdjustmentAmount,
                             EMKLName = G.Key.EMKLName,
                             ForwarderName = G.Key.ForwarderName,
                             DocSendDate = G.Key.DocSendDate,
                             PaymentDate = G.Key.PaymentDate,
                             DueDate = G.Key.DueDate,
                             DiffBDCL = G.Key.DiffBDCL,
                             DiffETDDSD = G.Key.DiffETDDSD,
                             DiffDDPD = G.Key.DiffDDPD,
                         });

            return Query;
        }

        public List<GarmentShipmentMonitoringViewModel> GetReportData(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, invoiceNo, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.InvoiceNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetData(buyerAgent, invoiceNo, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Trucking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Pelabuhan Muat", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Pelabuhan Bongkar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Consignee", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Brand", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Komoditi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Booking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Ex-Fty", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl ETD/Sailing", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jarak Tgl Booking - Tgl Ex-Fty(Hari)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Terbit COO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Terima BL/HAWB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Kirim/Upload Dokumen", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jarak ETD+Tempo-Kirim Dokumen", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tempo Pembayaran(Hari)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Jatuh Tempo", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Payment", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Jatuh Tempo - Tgl Payment", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nilai FOB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nilai CMT", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama EMKL /Trucking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Forwarder", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Staf Shipping", DataType = typeof(string) });


            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                Dictionary<string, List<GarmentShipmentMonitoringViewModel>> dataByBrand = new Dictionary<string, List<GarmentShipmentMonitoringViewModel>>();
                Dictionary<string, decimal> subTotalFOB = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalCMT = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalLFC = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalDHL = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalTBP = new Dictionary<string, decimal>();

                foreach (GarmentShipmentMonitoringViewModel item in Query.ToList())
                {
                    string BrandName = item.InvoiceNo;
                    if (!dataByBrand.ContainsKey(BrandName)) dataByBrand.Add(BrandName, new List<GarmentShipmentMonitoringViewModel> { });
                    dataByBrand[BrandName].Add(new GarmentShipmentMonitoringViewModel
                    {
                        InvoiceID = item.InvoiceID,
                        InvoiceNo = item.InvoiceNo,
                        InvoiceDate = item.InvoiceDate,
                        TruckingDate = item.TruckingDate,
                        BuyerAgentName = item.BuyerAgentName,
                        ConsigneeName = item.ConsigneeName,
                        BuyerBrandName = item.BuyerBrandName,
                        ComodityName = item.ComodityName,
                        SectionCode = item.SectionCode,
                        SailingDate = item.SailingDate,
                        BookingDate = item.BookingDate,
                        ExpFactoryDate = item.ExpFactoryDate,
                        CONo = item.CONo,
                        PaymentDue = item.PaymentDue,
                        PEBNo = item.PEBNo,
                        PEBDate = item.PEBDate,
                        OriginPort = item.OriginPort,
                        DestinationPort = item.DestinationPort,
                        ShippingStaffName = item.ShippingStaffName,
                        Amount = item.Amount,
                        CMTAmount = item.CMTAmount,
                        CMTAmountSub = item.CMTAmountSub,
                        LessfabricCost = item.LessfabricCost,
                        AdjustmentValue = item.AdjustmentValue,
                        AdjustmentAmount = item.AdjustmentAmount,
                        EMKLName = item.EMKLName,
                        ForwarderName = item.ForwarderName,
                        DocSendDate = item.DocSendDate,
                        PaymentDate = item.PaymentDate,
                        DueDate = item.DueDate,
                        DiffBDCL = item.DiffBDCL,
                        DiffETDDSD = item.DiffETDDSD,
                        DiffDDPD = item.DiffDDPD,
                    });

                    if (!subTotalFOB.ContainsKey(BrandName))
                    {
                        subTotalFOB.Add(BrandName, 0);
                    };

                    if (!subTotalCMT.ContainsKey(BrandName))
                    {
                        subTotalCMT.Add(BrandName, 0);
                    };

                    if (!subTotalLFC.ContainsKey(BrandName))
                    {
                        subTotalLFC.Add(BrandName, 0);
                    };

                    subTotalFOB[BrandName] += item.Amount;
                    subTotalCMT[BrandName] += item.CMTAmountSub;
                    subTotalLFC[BrandName] += item.LessfabricCost;
                    subTotalDHL[BrandName] = item.AdjustmentAmount;
                    subTotalTBP[BrandName] = subTotalFOB[BrandName] - subTotalLFC[BrandName] + subTotalDHL[BrandName];

                }

                int rowPosition = 1;
                foreach (KeyValuePair<string, List<GarmentShipmentMonitoringViewModel>> BuyerBrand in dataByBrand)
                {
                    string BrandCode = "";
                    int index = 0;
                    foreach (GarmentShipmentMonitoringViewModel item in BuyerBrand.Value)
                    {
                        index++;

                        string InvDate = item.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : item.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string SailDate = item.SailingDate == new DateTime(1970, 1, 1) ? "-" : item.SailingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string PEBDate = item.PEBDate == DateTimeOffset.MinValue ? "-" : item.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string TruckDate = item.TruckingDate == DateTimeOffset.MinValue ? "-" : item.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string BookDate = item.BookingDate == new DateTime(1970, 1, 1) ? "-" : item.BookingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string ExFtyDate = item.ExpFactoryDate == new DateTime(1970, 1, 1) ? "-" : item.ExpFactoryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                        string DocDate = item.DocSendDate == new DateTime(1970, 1, 1) ? "-" : item.DocSendDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string DueDate = item.DueDate == new DateTime(1970, 1, 1) ? "-" : item.DueDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string PayDate = item.PaymentDate == new DateTime(1970, 1, 1) ? "-" : item.PaymentDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                        string AmtFOB = string.Format("{0:N2}", item.Amount);
                        string AmtCMT = string.Format("{0:N2}", item.CMTAmount);

                        result.Rows.Add(index, item.InvoiceNo, InvDate, TruckDate, item.OriginPort, item.DestinationPort, item.BuyerAgentName, 
                                        item.ConsigneeName, item.BuyerBrandName, item.ComodityName, item.SectionCode, BookDate, ExFtyDate, 
                                        item.PEBNo, PEBDate, SailDate, item.DiffBDCL, item.CONo, DocDate,
                                        DocDate, item.DiffETDDSD, item.PaymentDue, DueDate, PayDate, item.DiffDDPD, AmtFOB, AmtCMT, item.EMKLName,
                                        item.ForwarderName,item.ShippingStaffName);

                        rowPosition += 1;
                        BrandCode = item.InvoiceNo;
                    }
                    if (subTotalFOB[BuyerBrand.Key] == subTotalCMT[BuyerBrand.Key])
                    {
                        result.Rows.Add("", "", "INVOICE NO :", BrandCode, "", "AMOUNT FOB :", Math.Round(subTotalFOB[BuyerBrand.Key], 2), "", "LESS FABRIC COST :", Math.Round(subTotalLFC[BuyerBrand.Key], 2), "", "AMOUNT CMT :", "0", "", "DHL CHARGES :", Math.Round(subTotalDHL[BuyerBrand.Key], 2), "", "AMOUNT TO BE PAID :", Math.Round(subTotalTBP[BuyerBrand.Key], 2));
                    }
                    else
                    {
                        result.Rows.Add("", "", "INVOICE NO :", BrandCode, "", "AMOUNT FOB :", Math.Round(subTotalFOB[BuyerBrand.Key], 2), "", "LESS FABRIC COST :", Math.Round(subTotalLFC[BuyerBrand.Key], 2), "", "AMOUNT CMT :", Math.Round(subTotalCMT[BuyerBrand.Key], 2), "", "DHL CHARGES :", Math.Round(subTotalDHL[BuyerBrand.Key], 2), "", "AMOUNT TO BE PAID :", Math.Round(subTotalTBP[BuyerBrand.Key], 2));
                    }

                    rowPosition += 1;
                }
            }          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
