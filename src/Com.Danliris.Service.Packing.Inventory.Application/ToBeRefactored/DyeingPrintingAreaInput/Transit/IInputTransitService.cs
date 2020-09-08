using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit
{
    public interface IInputTransitService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, InputTransitViewModel viewModel);
        Task<int> Create(InputTransitViewModel viewModel);
        Task<InputTransitViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<PreTransitIndexViewModel> ReadOutputPreTransit(int page, int size, string filter, string order, string keyword);
        List<OutputPreTransitProductionOrderViewModel> GetOutputPreTransitProductionOrders();
        Task<int> Reject(InputTransitViewModel viewModel);
        MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
    }
}
