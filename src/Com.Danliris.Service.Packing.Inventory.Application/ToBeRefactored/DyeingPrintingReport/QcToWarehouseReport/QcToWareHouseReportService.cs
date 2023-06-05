using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.QcToWarehouseReport
{
    public class QcToWarehouseReportService : IQcToWarehouseReportService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _productionOrderRepository;
        public QcToWarehouseReportService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            
        }
        public MemoryStream GenerateExcel(string bonNo, string orderNo, DateTime startdate, DateTime finishdate,int offset)
        {
            var list = GetReportQuery(bonNo, orderNo, startdate, finishdate,offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Warna yang Diminta", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kereta", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Panjang", DataType = typeof(double) });

            if (list.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", 0); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in list)
                {
                    index++;
                    string tgl = item.createdUtc == new DateTime(1970, 1, 1) ? "-" : item.createdUtc.ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                    result.Rows.Add(
                           index, tgl,   item.orderType,   item.inputQuantity);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }

        public List<QcToWarehouseReportViewModel> GetReportData(string bonNo, string orderNo, DateTime startdate, DateTime finishdate, int offset)
        {
            var Query = GetReportQuery(bonNo, orderNo, startdate, finishdate, offset);
            return Query.ToList();
        }

        private List<QcToWarehouseReportViewModel> GetReportQuery(string bonNo, string orderNo, DateTime startdate, DateTime finishdate, int offset)
        {
            var dateStart = startdate != DateTime.MinValue ? startdate.Date : DateTime.MinValue;
            var dateTo = finishdate != DateTime.MinValue ? finishdate.Date : DateTime.Now.Date;

            var query = from a in _repository.ReadAll()
                        where a.Date.AddHours(offset).Date >= dateStart.Date && a.Date.AddHours(offset).Date <= dateTo.Date
                        && a.Area == "GUDANG JADI"
                        select a;
            var joinQuery = from a in query
                            join b in _productionOrderRepository.ReadAll() on a.Id equals b.DyeingPrintingAreaInputId
                            where (b.ProductionOrderType == "solid" || b.ProductionOrderType == "dyeing")

                            select new QcToWarehouseReportViewModel
                            {
                                createdUtc = a.CreatedUtc,
                                inputQuantity = b.InputQuantity,
                                orderType = b.ProductionOrderType
                            };



            return joinQuery.OrderByDescending(a => a.createdUtc).ToList();
        }
    }
}
