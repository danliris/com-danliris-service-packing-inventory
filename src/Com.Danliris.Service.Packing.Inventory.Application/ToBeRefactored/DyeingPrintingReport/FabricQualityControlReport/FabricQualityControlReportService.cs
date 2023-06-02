using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.FabricQualityControlReport
{
    public class FabricQualityControlReportService : IFabricQualityControlReportService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _productionOrderRepository;
        public FabricQualityControlReportService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();

        }
        public MemoryStream GenerateExcel(string orderNo, DateTime startdate, DateTime finishdate, int offset)
        {
            var list = GetReportQuery(orderNo, startdate, finishdate, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Keluar", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah (m)", DataType = typeof(double) });

            if (list.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", 0); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in list)
                {
                    index++;
                    string tglIn = item.dateIn == new DateTime(1970, 1, 1) ? "-" : item.dateIn.ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string tglOut = item.dateOut == new DateTime(1970, 1, 1) ? "-" : item.dateOut.ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                    result.Rows.Add(
                           index, item.bonNo, tglIn, item.productionOrderNo, item.orderType, item.buyer, item.color, item.motif, tglOut, item.grade, item.balance);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }

        

        public List<FabricQualityControlReportViewModel> GetReportData(string orderNo, DateTime startdate, DateTime finishdate, int offset)
        {
            var Query = GetReportQuery(orderNo, startdate, finishdate, offset);
            return Query.ToList();
        }

        private List<FabricQualityControlReportViewModel> GetReportQuery(string orderNo, DateTime startdate, DateTime finishdate, int offset)
        {
            var dateStart = startdate != DateTime.MinValue ? startdate.Date : DateTime.MinValue;
            var dateTo = finishdate != DateTime.MinValue ? finishdate.Date : DateTime.Now.Date;

            var query = from a in _repository.ReadAll()
                        where a.CreatedUtc.AddHours(offset).Date >= dateStart.Date && a.CreatedUtc.AddHours(offset).Date <= dateTo.Date
                        && a.Area == "INSPECTION MATERIAL"
                        select a;
            var joinQuery = from a in query
                            join b in _productionOrderRepository.ReadAll() on a.Id equals b.DyeingPrintingAreaOutputId
                            where b.ProductionOrderNo == (!string.IsNullOrEmpty(orderNo) ? orderNo : b.ProductionOrderNo)
                            select new FabricQualityControlReportViewModel
                            {
                                bonNo = a.BonNo,
                                productionOrderNo = b.ProductionOrderNo,
                                buyer = b.Buyer,
                                dateIn = b.DateIn.AddHours(offset),
                                color = b.Color,
                                dateOut = b.DateOut.AddHours(offset),
                                grade = b.Grade,
                                balance = b.Balance,
                                motif = b.Motif,
                                orderType = b.ProductionOrderType
                            };

            return joinQuery.ToList();
        }
    }
}
