using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse
{
    public interface IStockWarehouseService
    {
        List<ReportStockWarehouseViewModel> GetReportData(DateTimeOffset dateReport,string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId);
        MemoryStream GenerateExcel(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId);

    }
}
