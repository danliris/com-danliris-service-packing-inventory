using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse
{
    public interface IInputWarehouseService
    {
        Task<int> Create(InputWarehouseViewModel viewModel);
        Task<InputWarehouseViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        List<OutputPreWarehouseIndexViewModel> GetOutputPreWarehouseProductionOrders();
        Task<int> Reject(RejectedInputWarehouseViewModel viewModel);
    }
}
