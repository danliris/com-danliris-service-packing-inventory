using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.List;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.PreOutputWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN
{
    public interface IDPWarehouseInService
    {
        List<OutputPreWarehouseItemListViewModel> PreInputWarehouse(string packingCode);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Create(DPInputWarehouseCreateViewModel viewModel);
        Task<int> Reject(DPInputWarehouseCreateViewModel viewModel);
        Task<DPInputWarehouseCreateViewModel> ReadById(int id);
        List<DPInputWarehouseMonitoringViewModel> GetMonitoring(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId,  int offset);
        MemoryStream GenerateExcelMonitoring(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int offset);
        List<MonitoringPreInputWarehouseViewModel> GetMonitoringPreInput(int productionOrderId, string productPackingCode);
        MemoryStream GenerateExcelPreInput(int productionOrderId, string productPackingCode);

    }
}
