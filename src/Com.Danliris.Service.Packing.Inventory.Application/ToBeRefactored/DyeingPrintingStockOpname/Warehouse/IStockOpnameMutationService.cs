using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public interface IStockOpnameMutationService 
    {
        Task<int> Create(StockOpnameMutationViewModel viewModel);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<StockOpnameMutationViewModel> ReadById(int id);
        List<ReportSOViewModel> GetMonitoringSO(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int track, int offset);
        MemoryStream GenerateExcelMonitoring(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int track, int offset);

    }
}
