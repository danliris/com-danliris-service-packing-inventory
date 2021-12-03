using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping
{
    public interface IInputShippingService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, InputShippingViewModel viewModel);
        Task<int> Create(InputShippingViewModel viewModel);
        Task<InputShippingViewModel> ReadById(int id);
        Task<InputShippingViewModel> ReadByIdBon(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<PreShippingIndexViewModel> ReadOutputPreShipping(int page, int size, string filter, string order, string keyword);
        ListResult<InputShippingProductionOrderViewModel> ReadProductionOrders(int page, int size, string filter, string order, string keyword);
        List<OutputPreShippingProductionOrderViewModel> GetOutputPreShippingProductionOrders(long deliveryOrderSalesId);
        Task<int> Reject(InputShippingViewModel viewModel);
        MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, string type, int offSet);
        ListResult<OutputPreShippingProductionOrderViewModel> GetDistinctDO(int page, int size, string filter, string order, string keyword);
    }
}
