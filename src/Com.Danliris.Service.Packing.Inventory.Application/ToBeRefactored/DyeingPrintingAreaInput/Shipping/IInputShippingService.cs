using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping
{
    public interface IInputShippingService
    {
        Task<int> Create(InputShippingViewModel viewModel);
        Task<InputShippingViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<PreShippingIndexViewModel> ReadOutputPreShipping(int page, int size, string filter, string order, string keyword);
        ListResult<InputShippingProductionOrderViewModel> ReadProductionOrders(int page, int size, string filter, string order, string keyword);
        List<OutputPreShippingProductionOrderViewModel> GetOutputPreShippingProductionOrders();
    }
}
