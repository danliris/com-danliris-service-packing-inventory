using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Detail;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.List;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Reject;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.PreOutputWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse
{
    public interface IInputWarehouseService
    {
        Task<int> Create(InputWarehouseCreateViewModel viewModel);
        Task<InputWarehouseDetailViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        List<OutputPreWarehouseViewModel> GetOutputPreWarehouseProductionOrders();
        Task<int> Reject(RejectedInputWarehouseViewModel viewModel);
        Task<int> Delete(int bonId);
        Task<int> Update(int bonId, InputWarehouseCreateViewModel viewModel);
        MemoryStream GenerateExcelAll();
    }
}
