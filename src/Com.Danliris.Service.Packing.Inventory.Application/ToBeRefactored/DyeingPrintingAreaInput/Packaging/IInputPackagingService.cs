using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging
{
    public interface IInputPackagingService
    {
        Task<int> CreateAsync(InputPackagingViewModel viewModel);
        Task<InputPackagingViewModel> ReadByIdAsync(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<InputPackagingProductionOrdersViewModel> ReadProductionOrders(int page, int size, string filter, string order, string keyword);
        ListResult<IndexViewModel> ReadBonOutToPack(int page, int size, string filter, string order, string keyword);
        ListResult<InputPackagingProductionOrdersViewModel> ReadInProducionOrders(int page, int size, string filter, string order, string keyword);
    }
}
