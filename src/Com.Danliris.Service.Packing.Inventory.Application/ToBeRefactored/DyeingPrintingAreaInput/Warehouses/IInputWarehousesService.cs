using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouses
{
    public interface IInputWarehousesService
    {
        Task<int> Create(InputWarehousesViewModel viewModel);
        Task<InputWarehousesViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<PreWarehouseIndexViewModel> ReadOutputPreWarehouse(DateTimeOffset searchDate,
                                                                      string searchShift,
                                                                      string searchGroup,
                                                                      int page,
                                                                      int size,
                                                                      string filter,
                                                                      string order,
                                                                      string keyword);
        //ListResult<InputWarehousesProductionOrdersViewModel> ReadProductionOrders(int page, int size, string filter, string order, string keyword);
        //ListResult<IndexViewModel> ReadBonOutToPack(int page, int size, string filter, string order, string keyword);
    }
}
