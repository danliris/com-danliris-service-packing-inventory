using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse.StockOpnameWarehouseService;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
  public  interface IStockOpnameWarehouseService
    {
        Task<int> Create(StockOpnameWarehouseViewModel viewModel);
        //Task<int> Create(StockOpnameBarcodeFormDto form);
        //List<StockOpnameWarehouseProductionOrderViewModel> GetMonitoringScan(long productionOrderId, string barcode, string documentNo, string grade, string userFilter);
        List<BarcodeInfoViewModel> GetMonitoringScan(long productionOrderId, string barcode, string documentNo, string grade, string userFilter);
        Task<StockOpnameWarehouseViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword, bool isStockOpname);
        ListResult<IndexViewModel> Read(string keyword);
        Task<int> Delete(int bonId);
        Task<int> Update(int id, StockOpnameWarehouseViewModel viewModel);
        Task<MemoryStream> GenerateExcelDocumentAsync(int id, int offSet);
        MemoryStream GenerateExcelMonitoringScan(long productionOrderId, string barcode, string documentNo, string grade, string userFilter);
        List<StockOpnameWarehouseProductionOrderViewModel> getDatabyCode(string itemData, int trackId);
        List<ReportSOViewModel> GetReportDataSO(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, string barcode, int track, int offset);
        MemoryStream GenerateExcel(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, string barcode, int track, int offset);

        List<ReportSOViewModel> GetMonitoringSO(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int track, int offset);
        MemoryStream GenerateExcelMonitoring(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int track, int offset);
        List<StockOpnameWarehouseProductionOrderViewModel> getDatabyCodeSummary(string itemData, int trackId);

    }
}
