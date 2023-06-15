using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport
{
    public class OrderStatusReportService : IOrderStatusReportService
    {
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _productionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _productionOutRepository;
        public OrderStatusReportService(IServiceProvider serviceProvider)
        {
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _productionOutRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }
        public MemoryStream GenerateExcel(string orderType, string year)
        {
            var list = GetReportQuery(orderType, year);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "SPP", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Target Kirim Ke Buyer(m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sisa Belum Turun Kanban (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Belum Produksi (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sedang Produksi (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sedang QC (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sudah Produksi (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sudah Dikirim ke Gudang Jadi (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sudah Dikirim ke Buyer (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sisa Belum Kirim ke Buyer", DataType = typeof(double) });

            if (list.ToArray().Count() == 0)
                result.Rows.Add("", "", 0, 0, 0, 0, 0, 0, 0, 0, 0); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in list)
                {
                    index++;
                    result.Rows.Add(
                           index, item.productionOrderNo, item.targetQty, item.remainingQty, item.preProductionQty, item.inProductionQty, 
                           item.qcQty, item.producedQty, item.sentGJQty, item.sentBuyerQty, item.remainingSentQty);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }

        public List<OrderStatusReportViewModel> GetReportData(string orderType, string year)
        {
            var Query = GetReportQuery(orderType, year);
            return Query.ToList();
        }

        private List<OrderStatusReportViewModel> GetReportQuery(string orderType, string year)
        { 
            var joinQuery = from a in _productionOrderRepository.ReadAll()
                            join b in _productionOutRepository.ReadAll() on a.ProductionOrderId equals b.ProductionOrderId
                            where a.ProductionOrderType == (!string.IsNullOrEmpty(orderType) ? orderType : a.ProductionOrderType)
                            && a.CreatedUtc.Year== (!string.IsNullOrEmpty(year) ? Convert.ToInt32( year) : a.CreatedUtc.Year)
                            select new OrderStatusReportViewModel
                            {
                                productionOrderNo = b.ProductionOrderNo,
                                productionOrderId = b.ProductionOrderId,
                                sentBuyerQty= b.Balance,
                                targetQty= a.ProductionOrderOrderQuantity,
                                qcQty= a.InputQuantity,
                                producedQty= a.InputQuantity,
                                sentGJQty = a.Area=="GUDANG JADI"? a.InputQuantity : 0,
                            };

            return joinQuery.OrderByDescending(a => a.date).ToList();
        }
    }
}
