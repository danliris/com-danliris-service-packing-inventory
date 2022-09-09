using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesBook
{
    public class GarmentLocalSalesBookService : IGarmentLocalSalesBookService
    {
        private readonly IGarmentShippingLocalSalesNoteRepository repository;
        private readonly IGarmentShippingLocalSalesNoteItemRepository itemrepository;
        private readonly IGarmentShippingLocalReturnNoteRepository rtrrepository;               
        private readonly IGarmentShippingLocalPriceCuttingNoteRepository cutrepository;
        private readonly IGarmentLocalCoverLetterRepository clrepository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentLocalSalesBookService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteItemRepository>();
            rtrrepository = serviceProvider.GetService<IGarmentShippingLocalReturnNoteRepository>();
            cutrepository = serviceProvider.GetService<IGarmentShippingLocalPriceCuttingNoteRepository>();
            clrepository = serviceProvider.GetService<IGarmentLocalCoverLetterRepository>();

            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentLocalSalesBookViewModel> GetDataQuery(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
           var query = repository.ReadAll();
           var queryItem = itemrepository.ReadAll();
           var queryrtr = rtrrepository.ReadItemAll();
           var querycut = cutrepository.ReadItemAll();
           var querycl = clrepository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            query = query.OrderBy(w => w.BuyerCode).ThenBy(w => w.Date);

            //  LOCAL SALES NOTE
            IQueryable<GarmentLocalSalesBookTempViewModel> d1 = from a in query
                                                                join b in queryItem on a.Id equals b.LocalSalesNoteId
                                                                join c in querycl on a.Id equals c.LocalSalesNoteId

                                                                select new GarmentLocalSalesBookTempViewModel
                                                           {
                                                               LSNo = a.NoteNo,
                                                               LSDate = a.Date,
                                                               CLDate = c.Date,
                                                               BuyerCode = a.BuyerCode,
                                                               BuyerName = a.BuyerName,
                                                               TransactionCode = a.TransactionTypeCode,
                                                               TransactionType = "01. PENJUALAN",
                                                               Quantity = b.Quantity,
                                                               DPP = Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price),
                                                               UseVat = a.UseVat == true ? "YA" : "TIDAK",
                                                               PPN = ((a.UseVat == true && a.KaberType == "KABER") || a.UseVat) == false ? 0 : (Convert.ToDecimal(a.VatRate) * Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price)) / 100,
                                                               Total = ((a.UseVat == true && a.KaberType == "KABER") || a.UseVat) == false ? Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price) : ((100 + Convert.ToDecimal(a.VatRate)) * Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price)) / 100,
                                                           };            
            //LOCAL RETURN NOTE
            IQueryable<GarmentLocalSalesBookTempViewModel> d2 = from a in queryrtr
                                                                join b in queryItem on a.SalesNoteItemId equals b.Id
                                                                join c in query on b.LocalSalesNoteId equals c.Id
                                                                join d in querycl on c.Id equals d.LocalSalesNoteId                                                            

                                                                select new GarmentLocalSalesBookTempViewModel
                                                           {
                                                               LSNo = c.NoteNo,
                                                               LSDate = c.Date,
                                                               CLDate = d.Date,
                                                               BuyerCode = c.BuyerCode,
                                                               BuyerName = c.BuyerName,
                                                               TransactionCode = c.TransactionTypeCode,
                                                               TransactionType = "02. RETUR",
                                                               Quantity = a.ReturnQuantity,
                                                               DPP = Convert.ToDecimal(a.ReturnQuantity) * Convert.ToDecimal(b.Price) * -1,
                                                               UseVat = c.UseVat == true ? "YA" : "TIDAK",
                                                               PPN = c.UseVat == false ? 0 : (Convert.ToDecimal(0.1) * Convert.ToDecimal(a.ReturnQuantity) * Convert.ToDecimal(b.Price)) * -1,
                                                               Total = c.UseVat == false ? Convert.ToDecimal(a.ReturnQuantity) * Convert.ToDecimal(b.Price) * -1 : (Convert.ToDecimal(1.1) * Convert.ToDecimal(a.ReturnQuantity) * Convert.ToDecimal(b.Price) * -1),
                                                           };
            //LOCAL CUTTING NOTE
            IQueryable<GarmentLocalSalesBookTempViewModel> d3 = from a in querycut
                                                                join b in query on a.SalesNoteId equals b.Id
                                                                join c in queryItem on b.Id equals c.LocalSalesNoteId
                                                                join d in querycl on c.Id equals d.LocalSalesNoteId

                                                                select new GarmentLocalSalesBookTempViewModel
                                                            {
                                                                LSNo = b.NoteNo,
                                                                LSDate = b.Date,
                                                                CLDate = d.Date,
                                                                BuyerCode = b.BuyerCode,
                                                                BuyerName = b.BuyerName,
                                                                TransactionCode = b.TransactionTypeCode,
                                                                TransactionType = "03. POTONGAN",
                                                                Quantity = 0,
                                                                UseVat = b.UseVat == true ? "YA" : "TIDAK",
                                                                IncludeVat = a.IncludeVat == true ? "YA" : "TIDAK",
                                                                DPP = a.IncludeVat == false ? Convert.ToDecimal(a.CuttingAmount) * -1 : Convert.ToDecimal(a.CuttingAmount/1.1) * -1,
                                                                PPN = b.UseVat == false ? 0 : (b.UseVat == true && a.IncludeVat == false ? Convert.ToDecimal(a.CuttingAmount * 0.1) * -1 : Convert.ToDecimal(a.CuttingAmount/11) * -1 ),
                                                                Total = b.UseVat == false ? Convert.ToDecimal(a.CuttingAmount) : (b.UseVat == true && a.IncludeVat == false ? Convert.ToDecimal(a.CuttingAmount * 1.1) * -1 : Convert.ToDecimal(a.CuttingAmount) * -1),
                                                            };

            List<GarmentLocalSalesBookTempViewModel> CombineData = d1.Union(d2).Union(d3).ToList();

            var Query = from data in CombineData
                        group data by new { data.TransactionCode, data.LSNo, data.LSDate, data.CLDate, data.BuyerCode, data.BuyerName, data.TransactionType } into groupData
                        select new GarmentLocalSalesBookViewModel
                        {
                            LSNo = groupData.Key.LSNo,
                            LSDate = groupData.Key.LSDate,
                            CLDate = groupData.Key.CLDate,
                            BuyerCode = groupData.Key.BuyerCode,
                            BuyerName = groupData.Key.BuyerName,
                            TransactionCode = groupData.Key.TransactionCode,
                            TransactionType = groupData.Key.TransactionType,
                            QtyTotal = groupData.Sum(s => (decimal)s.Quantity),
                            NettAmount = Math.Round(groupData.Sum(s => s.Total), 2),
                            SalesAmount = Math.Round(groupData.Sum(s => s.DPP), 2),
                            PPNAmount = Math.Round(groupData.Sum(s => s.PPN), 2),
                        };
            return Query.AsQueryable();        
        }

        public Tuple<List<GarmentLocalSalesBookViewModel>, int> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            List<GarmentLocalSalesBookViewModel> result = GetDataQuery(dateFrom, dateTo, offset).ToList();
            return Tuple.Create(result, result.Count);
        }

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
       {
            Tuple<List<GarmentLocalSalesBookViewModel>, int> Data = this.GetReportData(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Nota", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal SP", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Nota", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Penjualan + PPN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Penjualan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PPN", DataType = typeof(string) });            
            result.Columns.Add(new DataColumn() { ColumnName = "LBJ Qty", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "LBJ IDR", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AVL Qty", DataType = typeof(string) });            
            result.Columns.Add(new DataColumn() { ColumnName = "AVL IDR", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "LJS QTY", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "LJS IDR", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SBJ QTY", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SBJ IDR", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMR QTY", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMR IDR", DataType = typeof(string) });

            List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };

            if (Data.Item2 == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                Dictionary<string, List<GarmentLocalSalesBookViewModel>> dataBySupplier = new Dictionary<string, List<GarmentLocalSalesBookViewModel>>();
                Dictionary<string, decimal> subTotalNetSales = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalSales = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalPPN = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalQtyBJ = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmtBJ = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalQtyAVL = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmtAVL = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalQtyJS = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmtJS = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalQtySBJ = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmtSBJ = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalQtySMR = new Dictionary<string, decimal>();
                Dictionary<string, decimal> subTotalAmtSMR= new Dictionary<string, decimal>();

                foreach (GarmentLocalSalesBookViewModel data in Data.Item1)
                {
                    string SupplierName = data.TransactionType;
                    decimal AmountX = 0, AmountY = 0, AmountZ = 0, Qty1 = 0, Qty2 = 0, Qty3 = 0, Qty4 = 0, Qty5 = 0, Amount1 = 0, Amount2 = 0, Amount3 = 0, Amount4 = 0, Amount5 = 0;

                    switch (data.TransactionCode)
                    {
                        case "LBJ":
                            AmountX = data.NettAmount;
                            AmountY = data.SalesAmount;
                            AmountZ = data.PPNAmount;
                            
                            Qty1 = data.QtyTotal;
                            Qty2 = 0;
                            Qty3 = 0;
                            Qty4 = 0;
                            Qty5 = 0;

                            Amount1 = data.SalesAmount;
                            Amount2 = 0;
                            Amount3 = 0;
                            Amount4 = 0;
                            Amount5 = 0;
                            break;

                        case "LBL":
                            AmountX = data.NettAmount;
                            AmountY = data.SalesAmount;
                            AmountZ = data.PPNAmount;

                            Qty1 = 0;
                            Qty2 = data.QtyTotal;
                            Qty3 = 0;
                            Qty4 = 0;
                            Qty5 = 0;

                            Amount1 = 0;
                            Amount2 = data.SalesAmount;
                            Amount3 = 0;
                            Amount4 = 0;
                            Amount5 = 0;
                            break;

                       case "LBM":
                            AmountX = data.NettAmount;
                            AmountY = data.SalesAmount;
                            AmountZ = data.PPNAmount;

                            Qty1 = 0;
                            Qty2 = data.QtyTotal;
                            Qty3 = 0;
                            Qty4 = 0;
                            Qty5 = 0;

                            Amount1 = 0;
                            Amount2 = data.SalesAmount;
                            Amount3 = 0;
                            Amount4 = 0;
                            Amount5 = 0;
                            break;

                        case "LJS":
                            AmountX = data.NettAmount;
                            AmountY = data.SalesAmount;
                            AmountZ = data.PPNAmount;

                            Qty1 = 0;
                            Qty2 = 0;
                            Qty3 = data.QtyTotal;
                            Qty4 = 0;
                            Qty5 = 0;

                            Amount1 = 0;
                            Amount2 = 0;
                            Amount3 = data.SalesAmount;
                            Amount4 = 0;
                            Amount5 = 0;
                            break;

                        case "SBJ":
                            AmountX = data.NettAmount;
                            AmountY = data.SalesAmount;
                            AmountZ = data.PPNAmount;

                            Qty1 = 0;
                            Qty2 = 0;
                            Qty3 = 0;
                            Qty4 = data.QtyTotal;
                            Qty5 = 0;

                            Amount1 = 0;
                            Amount2 = 0;
                            Amount3 = 0;
                            Amount4 = data.SalesAmount;
                            Amount5 = 0;
                            break;

                        case "SMR":
                            AmountX = data.NettAmount;
                            AmountY = data.SalesAmount;
                            AmountZ = data.PPNAmount;

                            Qty1 = 0;
                            Qty2 = 0;
                            Qty3 = 0;
                            Qty4 = 0;
                            Qty5 = data.QtyTotal;

                            Amount1 = 0;
                            Amount2 = 0;
                            Amount3 = 0;
                            Amount4 = 0;
                            Amount5 = data.SalesAmount;
                            break;

                        default:
                            AmountX = 0;
                            AmountY = 0;
                            AmountZ = 0;

                            Qty1 = 0;
                            Qty2 = 0;
                            Qty3 = 0;
                            Qty4 = 0;
                            Qty5 = 0;

                            Amount1 = 0;
                            Amount2 = 0;
                            Amount3 = 0;
                            Amount4 = 0;
                            Amount5 = 0;
                            break;
                    }

                    if (!dataBySupplier.ContainsKey(SupplierName)) dataBySupplier.Add(SupplierName, new List<GarmentLocalSalesBookViewModel> { });
                    dataBySupplier[SupplierName].Add(new GarmentLocalSalesBookViewModel
                    {

                        LSDate = data.LSDate,
                        LSNo = data.LSNo,
                        CLDate = data.CLDate,
                        TransactionCode = data.TransactionCode,
                        TransactionType = data.TransactionType,
                        BuyerCode = data.BuyerCode,
                        BuyerName = data.BuyerName,
                        NettAmount = data.NettAmount,
                        SalesAmount = data.SalesAmount,
                        PPNAmount = data.PPNAmount,
                        Qty1 = Qty1,
                        Qty2 = Qty2,
                        Qty3 = Qty3,
                        Qty4 = Qty4,
                        Qty5 = Qty5,
                        Amount1 = Amount1,
                        Amount2 = Amount2,
                        Amount3 = Amount3,
                        Amount4 = Amount4,
                        Amount5 = Amount5,

                    });
            
                    if (!subTotalNetSales.ContainsKey(SupplierName))
                    {
                        subTotalNetSales.Add(SupplierName, 0);
                    };

                    if (!subTotalSales.ContainsKey(SupplierName))
                    {
                        subTotalSales.Add(SupplierName, 0);
                    };

                    if (!subTotalPPN.ContainsKey(SupplierName))
                    {
                        subTotalPPN.Add(SupplierName, 0);
                    };

                    if (!subTotalQtyBJ.ContainsKey(SupplierName))
                    {
                        subTotalQtyBJ.Add(SupplierName, 0);
                    };

                    if (!subTotalAmtBJ.ContainsKey(SupplierName))
                    {
                        subTotalAmtBJ.Add(SupplierName, 0);
                    };

                    if (!subTotalQtyAVL.ContainsKey(SupplierName))
                    {
                        subTotalQtyAVL.Add(SupplierName, 0);
                    };

                    if (!subTotalAmtAVL.ContainsKey(SupplierName))
                    {
                        subTotalAmtAVL.Add(SupplierName, 0);
                    };

                    if (!subTotalQtyJS.ContainsKey(SupplierName))
                    {
                        subTotalQtyJS.Add(SupplierName, 0);
                    };

                    if (!subTotalAmtJS.ContainsKey(SupplierName))
                    {
                        subTotalAmtJS.Add(SupplierName, 0);
                    };

                    if (!subTotalQtySBJ.ContainsKey(SupplierName))
                    {
                        subTotalQtySBJ.Add(SupplierName, 0);
                    };

                    if (!subTotalAmtSBJ.ContainsKey(SupplierName))
                    {
                        subTotalAmtSBJ.Add(SupplierName, 0);
                    };

                    if (!subTotalQtySMR.ContainsKey(SupplierName))
                    {
                        subTotalQtySMR.Add(SupplierName, 0);
                    };

                    if (!subTotalAmtSMR.ContainsKey(SupplierName))
                    {
                        subTotalAmtSMR.Add(SupplierName, 0);
                    };

                    subTotalNetSales[SupplierName] += AmountX;
                    subTotalSales[SupplierName] += AmountY;
                    subTotalPPN[SupplierName] += AmountZ;
                    subTotalQtyBJ[SupplierName] += Qty1;
                    subTotalAmtBJ[SupplierName] += Amount1;
                    subTotalQtyAVL[SupplierName] += Qty2;
                    subTotalAmtAVL[SupplierName] += Amount2;
                    subTotalQtyJS[SupplierName] += Qty3;
                    subTotalAmtJS[SupplierName] += Amount3;
                    subTotalQtySBJ[SupplierName] += Qty4;
                    subTotalAmtSBJ[SupplierName] += Amount4;
                    subTotalQtySMR[SupplierName] += Qty5;
                    subTotalAmtSMR[SupplierName] += Amount5;

                }

                decimal TotalNetSales = 0;
                decimal TotalSales = 0;
                decimal TotalPPN = 0;
                decimal TotalQtyBJ = 0;
                decimal TotalAmtBJ = 0;
                decimal TotalQtyAVL = 0;
                decimal TotalAmtAVL = 0;
                decimal TotalQtyJS = 0;
                decimal TotalAmtJS = 0;
                decimal TotalQtySBJ = 0;
                decimal TotalAmtSBJ = 0;
                decimal TotalQtySMR = 0;
                decimal TotalAmtSMR = 0;

                int rowPosition = 13;

                foreach (KeyValuePair<string, List<GarmentLocalSalesBookViewModel>> SupplName in dataBySupplier)
                {
                    string splCode = "";
                    int index = 0;
                    foreach (GarmentLocalSalesBookViewModel data in SupplName.Value)
                    {
                        index++;
                        string LSDate = data.LSDate == new DateTime(1970, 1, 1) ? "-" : data.LSDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        string CLDate = data.CLDate == new DateTime(1970, 1, 1) ? "-" : data.CLDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                        result.Rows.Add(index, LSDate, CLDate, data.LSNo, data.BuyerCode, data.BuyerName, Math.Round(data.NettAmount, 2), Math.Round(data.SalesAmount, 2), Math.Round(data.PPNAmount, 2), 
                                        Math.Round(data.Qty1, 2), Math.Round(data.Amount1, 2), Math.Round(data.Qty2, 2), Math.Round(data.Amount2, 2), Math.Round(data.Qty3, 2), Math.Round(data.Amount3, 2),
                                        Math.Round(data.Qty4, 2), Math.Round(data.Amount4, 2), Math.Round(data.Qty5, 2), Math.Round(data.Amount5, 2));
                        rowPosition += 1;
                        splCode = data.TransactionType;
                    }

                    result.Rows.Add("", "", "", "SUB TOTAL", splCode, ":", Math.Round(subTotalNetSales[SupplName.Key], 2), 
                                    Math.Round(subTotalSales[SupplName.Key], 2), Math.Round(subTotalPPN[SupplName.Key], 2), 
                                    Math.Round(subTotalQtyBJ[SupplName.Key], 2), Math.Round(subTotalAmtBJ[SupplName.Key], 2),
                                    Math.Round(subTotalQtyAVL[SupplName.Key], 2), Math.Round(subTotalAmtAVL[SupplName.Key], 2),
                                    Math.Round(subTotalQtyJS[SupplName.Key], 2), Math.Round(subTotalAmtJS[SupplName.Key], 2),
                                    Math.Round(subTotalQtySBJ[SupplName.Key], 2), Math.Round(subTotalAmtSBJ[SupplName.Key], 2),
                                    Math.Round(subTotalQtySMR[SupplName.Key], 2), Math.Round(subTotalAmtSMR[SupplName.Key], 2));

                    rowPosition += 1;
                    mergeCells.Add(($"A{rowPosition}:D{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Right, OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom));

                    TotalNetSales += subTotalNetSales[SupplName.Key];
                    TotalSales += subTotalSales[SupplName.Key];
                    TotalPPN += subTotalPPN[SupplName.Key];
                    TotalQtyBJ += subTotalQtyBJ[SupplName.Key];
                    TotalAmtBJ += subTotalAmtBJ[SupplName.Key];
                    TotalQtyAVL += subTotalQtyAVL[SupplName.Key];
                    TotalAmtAVL += subTotalAmtAVL[SupplName.Key];
                    TotalQtyJS += subTotalQtyJS[SupplName.Key];
                    TotalAmtJS += subTotalAmtJS[SupplName.Key];
                    TotalQtySBJ += subTotalQtySBJ[SupplName.Key];
                    TotalAmtSBJ += subTotalAmtSBJ[SupplName.Key];
                    TotalQtySMR += subTotalQtySMR[SupplName.Key];
                    TotalAmtSMR += subTotalAmtSMR[SupplName.Key];

                }

                result.Rows.Add("", "TOTAL", "PENJUALAN", "RETUR", "POTONGAN", ":", Math.Round(TotalNetSales, 2), Math.Round(TotalSales, 2), Math.Round(TotalPPN, 2),
                                Math.Round(TotalQtyBJ, 2), Math.Round(TotalAmtBJ, 2), Math.Round(TotalQtyAVL, 2), Math.Round(TotalAmtAVL, 2),
                                Math.Round(TotalQtyJS, 2), Math.Round(TotalAmtJS, 2), Math.Round(TotalQtySBJ, 2), Math.Round(TotalAmtSBJ, 2),
                                Math.Round(TotalQtySMR, 2), Math.Round(TotalAmtSMR, 2));

                result.Rows.Add("","", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                rowPosition += 1;
                mergeCells.Add(($"A{rowPosition}:D{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Right, OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom));
            }
        
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "LAP BUKU PENJUALAN LOKAL") }, true);
        }
    }
}
