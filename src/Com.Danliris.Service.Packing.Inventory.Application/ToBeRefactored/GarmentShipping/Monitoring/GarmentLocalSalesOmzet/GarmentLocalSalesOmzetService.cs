using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesOmzet
{
    public class GarmentLocalSalesOmzetService : IGarmentLocalSalesOmzetService
    {
        private readonly IGarmentShippingLocalSalesNoteRepository repository;
        private readonly IGarmentShippingLocalSalesNoteItemRepository itemrepository;
        private readonly IGarmentLocalCoverLetterRepository lclrepository;
        private readonly IGarmentShippingDetailLocalSalesNoteRepository hdrrepository;
        private readonly IGarmentShippingDetailLocalSalesNoteItemRepository detailrepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentLocalSalesOmzetService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteItemRepository>();
            lclrepository = serviceProvider.GetService<IGarmentLocalCoverLetterRepository>();
            hdrrepository = serviceProvider.GetService<IGarmentShippingDetailLocalSalesNoteRepository>();
            detailrepository = serviceProvider.GetService<IGarmentShippingDetailLocalSalesNoteItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentLocalSalesOmzetViewModel> GetData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
            var queryItem = itemrepository.ReadAll();
            var querylcl = lclrepository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            query = query.OrderBy(w => w.BuyerCode).ThenBy(w => w.Date);

            var newQ = (from a in query
                        join b in queryItem on a.Id equals b.LocalSalesNoteId
                        join c in querylcl on a.Id equals c.LocalSalesNoteId into dd
                        from CL in dd.DefaultIfEmpty()
                        where a.TransactionTypeCode != "SML" && a.TransactionTypeCode != "LMS"

                        //group new { Amt = Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price) } by new { a.NoteNo, a.Date, a.BuyerCode, a.BuyerName, a.UseVat } into G

                        select new GarmentLocalSalesOmzetViewModel
                        {
                            LSNo = a.NoteNo,
                            LSDate = a.Date,
                            BuyerCode = a.BuyerCode,
                            BuyerName = a.BuyerName,
                            KaberType = a.KaberType == null ? "NON KABER" : "KABER",
                            TransactionName = a.TransactionTypeName,
                            BCNo = CL == null ? "-" : CL.BCNo,
                            DispoNo = a.DispositionNo == null ? "-" : a.DispositionNo,
                            Tempo = a.Tempo,
                            ProductCode = b.ProductCode,
                            ProductName = b.ProductName,
                            Quantity = b.Quantity,
                            UomUnit = b.UomUnit,
                            DPP = Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price),
                            UseVat = a.UseVat == true ? "YA" : "TIDAK",
                            PPN = ((a.UseVat == true && a.KaberType == "KABER") || a.UseVat) == false ? 0 : (Convert.ToDecimal(a.VatRate) * Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price)) / 100,
                            Total = ((a.UseVat == true && a.KaberType == "KABER") || a.UseVat) == false ? Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price) : ((100 + Convert.ToDecimal(a.VatRate)) * Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price)) / 100,
                            LCLDate = CL == null ? new DateTime(1970, 1, 1) : CL.Date
                        });
            return newQ;
        }

        public List<GarmentLocalSalesOmzetViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.LSNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetData(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Status Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Nota", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Nota", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Surat Pengantar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Nota", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Disposisi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No BC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tempo", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Barang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Pakai PPN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "D P P", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "P P N", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "T O T A L", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","");
            else
            //{
            //    int index = 0;
            //    foreach (var d in Query)
            //    {
            //        index++;

            //        string GLSDate = d.LSDate == new DateTime(1970, 1, 1) ? "-" : d.LSDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
            //        string AmntDPP = string.Format("{0:N2}", d.DPP);
            //        string AmntPPN = string.Format("{0:N2}", d.PPN);
            //        string AmntTOT = string.Format("{0:N2}", d.Total);

            //        result.Rows.Add(index, d.LSNo, GLSDate, d.BuyerCode, d.BuyerName, d.UseVat, AmntDPP, AmntPPN, AmntTOT);
            //    }
            //}
            {
                Dictionary<string, List<GarmentLocalSalesOmzetViewModel>> dataByLSNo = new Dictionary<string, List<GarmentLocalSalesOmzetViewModel>>();
                Dictionary<string, double> subTotalQty = new Dictionary<string, double>();
                Dictionary<string, decimal> subTotalDPP = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalPPN = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmt = new Dictionary<string, decimal>();

                foreach (GarmentLocalSalesOmzetViewModel item in Query.ToList())
                {
                    string LSNumber = item.LSNo;
                    if (!dataByLSNo.ContainsKey(LSNumber)) dataByLSNo.Add(LSNumber, new List<GarmentLocalSalesOmzetViewModel> { });
                    dataByLSNo[LSNumber].Add(new GarmentLocalSalesOmzetViewModel
                    {
                        LSNo = item.LSNo,
                        LSDate = item.LSDate,
                        BuyerCode = item.BuyerCode,
                        BuyerName = item.BuyerName,
                        KaberType = item.KaberType,
                        TransactionName = item.TransactionName,
                        DispoNo = item.DispoNo,
                        BCNo = item.BCNo,
                        Tempo = item.Tempo,
                        UseVat = item.UseVat,
                        ProductCode = item.ProductCode,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        UomUnit = item.UomUnit,
                        DPP = item.DPP,
                        PPN = item.PPN,
                        Total = item.Total,
                        LCLDate = item.LCLDate
                    });

                    if (!subTotalQty.ContainsKey(LSNumber))
                    {
                        subTotalQty.Add(LSNumber, 0);
                    };

                    if (!subTotalDPP.ContainsKey(LSNumber))
                    {
                        subTotalDPP.Add(LSNumber, 0);
                    };

                    if (!subTotalPPN.ContainsKey(LSNumber))
                    {
                        subTotalPPN.Add(LSNumber, 0);
                    };

                    if (!subTotalAmt.ContainsKey(LSNumber))
                    {
                        subTotalAmt.Add(LSNumber, 0);
                    };

                    subTotalQty[LSNumber] += item.Quantity;
                    subTotalDPP[LSNumber] += item.DPP;
                    subTotalPPN[LSNumber] += item.PPN;
                    subTotalAmt[LSNumber] += item.Total;

                }

                double totalQty = 0;
                decimal totalDPP = 0;
                decimal totalPPN = 0;
                decimal totalAmount = 0;


                int rowPosition = 1;

                foreach (KeyValuePair<string, List<GarmentLocalSalesOmzetViewModel>> LSNumber in dataByLSNo)
                {
                    string NoteNo = "";
                    int index = 0;
                    foreach (GarmentLocalSalesOmzetViewModel item in LSNumber.Value)
                    {
                        index++;

                        string LSDate = item.LSDate == new DateTime(1970, 1, 1) ? "-" : item.LSDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string LCLDate = item.LCLDate == new DateTime(1970, 1, 1) ? "-" : item.LCLDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string QtyOrder = string.Format("{0:N2}", item.Quantity);
                        string DPP = string.Format("{0:N2}", item.DPP);
                        string PPN = string.Format("{0:N2}", item.PPN);
                        string Amount = string.Format("{0:N2}", item.Total);

                        result.Rows.Add(index, item.BuyerCode, item.BuyerName, item.KaberType, item.LSNo, LSDate, LCLDate, item.TransactionName, item.DispoNo, item.BCNo, item.Tempo, item.ProductCode, item.ProductName, item.UseVat, QtyOrder, item.UomUnit, DPP, PPN, Amount);
                        rowPosition += 1;
                        NoteNo = item.LSNo;
                    }

                    result.Rows.Add("", "", "SUB TOTAL", ".", ".", ".", ".", ".", "NOMOR NOTA :", NoteNo, ".", ".", ".", ".", Math.Round(subTotalQty[LSNumber.Key], 2), ".", Math.Round(subTotalDPP[LSNumber.Key], 2), Math.Round(subTotalPPN[LSNumber.Key], 2), Math.Round(subTotalAmt[LSNumber.Key], 2));

                    rowPosition += 1;
                    totalQty += subTotalQty[LSNumber.Key];
                    totalDPP += subTotalDPP[LSNumber.Key];
                    totalPPN += subTotalPPN[LSNumber.Key];
                    totalAmount += subTotalAmt[LSNumber.Key];
                }
                result.Rows.Add("", "", "T O T A L :", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", Math.Round(totalQty, 2), ".", Math.Round(totalDPP, 2), Math.Round(totalPPN, 2), Math.Round(totalAmount, 2));
                rowPosition += 1;
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
        //
        public IQueryable<GarmentDetailLocalSalesOmzetViewModel> GetDataDetail(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
            var queryDtl = hdrrepository.ReadAll();
            var queryItm = detailrepository.ReadAll();


            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            var newQ = (from a in query
                        join b in queryDtl on a.Id equals b.LocalSalesNoteId
                        join c in queryItm on b.Id equals c.LocalSalesNoteId 
                        where a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false && 
                              a.TransactionTypeCode != "SML" && a.TransactionTypeCode != "LMS"

                         select new GarmentDetailLocalSalesOmzetViewModel
                        {
                            LSNo = a.NoteNo,
                            LSDate = a.Date,
                            TransactionCode = a.TransactionTypeCode,
                            TransactionName = a.TransactionTypeName,
                            BuyerCode = a.BuyerCode,
                            BuyerName = a.BuyerName,
                            UnitCode = c.UnitCode,
                            UnitName = c.UnitName,
                            Tempo = a.Tempo,
                            DueDate = a.Date.AddDays(a.Tempo), 
                            Quantity = c.Quantity,
                            UomUnit = c.UomUnit,
                            CurrencyCode = "IDR",
                            Amount = c.Amount 
                        }).OrderBy(w => w.LSDate).ThenBy(w => w.LSNo).ThenBy(w => w.BuyerCode).ThenBy(w => w.UnitCode);
            return newQ;
        }
        //
        public MemoryStream GenerateExcelDetail(DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetDataDetail(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO NOTAr", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL NOTA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JENIS NOTA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JENIS TRANSAKSI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KODE BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KODE UNIT", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA UNIT", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JUMLAH", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "MATA UANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TOTAL AMOUNT", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "TEMPO", DataType = typeof(int) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL JT", DataType = typeof(string) });          

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", 0, "", "", 0, 0, "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string NoteDate = d.LSDate == new DateTime(1970, 1, 1) ? "-" : d.LSDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string JTDate = d.DueDate == new DateTime(1970, 1, 1) ? "-" : d.DueDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                 
                    result.Rows.Add(d.LSNo, NoteDate, d.TransactionCode, d.TransactionName, d.BuyerCode, d.BuyerName, d.UnitCode, d.UnitName, d.Quantity, d.UomUnit, d.CurrencyCode, d.Amount, d.Tempo, JTDate);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
