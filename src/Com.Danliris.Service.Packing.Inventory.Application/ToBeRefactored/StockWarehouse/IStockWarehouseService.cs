using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse
{
    public interface IStockWarehouseService
    {
        List<ReportStockWarehouseViewModel> GetReportData(DateTimeOffset dateReport,string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string inventoryType);
        MemoryStream GenerateExcel(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string inventoryType);
        List<PackingDataViewModel> GetPackingData(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string grade);

    }
}
