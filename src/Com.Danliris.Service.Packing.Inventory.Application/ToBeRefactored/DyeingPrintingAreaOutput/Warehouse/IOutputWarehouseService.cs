using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse.InputSPPWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public interface IOutputWarehouseService
    {
        Task<int> Create(OutputWarehouseViewModel viewModel);
        Task<OutputWarehouseViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<IndexViewModel> Read(string keyword);
        Task<MemoryStream> GenerateExcel(int id);
        Task<MemoryStream> GenerateExcel(int id,int  offSet);
        List<InputWarehouseProductionOrderCreateViewModel> GetInputWarehouseProductionOrders();
        List<InputSppWarehouseViewModel> GetInputSppWarehouseItemList();
        List<InputSppWarehouseViewModel> GetInputSppWarehouseItemListV2(long productionOrderId, string grade);
        InputSppWarehouseItemListViewModel GetInputSppWarehouseItemListV2(string productPackingCode);
        List<InputSppWarehouseViewModel> GetInputSppWarehouseItemList(int bonId);
        Task<List<InputSppWarehouseViewModel>> GetOutputSppWarehouseItemListAsync(int bonId);
        Task<List<InputSppWarehouseViewModel>> GetOutputSppWarehouseItemListAsyncBon(int bonId);
        MemoryStream GenerateExcelAll(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, string type, int offSet);
        MemoryStream GenerateExcelAllBarcode(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
        Task<int> Delete(int bonId);
        Task<int> Update(int id, OutputWarehouseViewModel viewModel);
        ListResult<AdjWarehouseProductionOrderViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword);
        ListResult<InputWarehouseProductionOrderCreateViewModel> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword);

    }
}
