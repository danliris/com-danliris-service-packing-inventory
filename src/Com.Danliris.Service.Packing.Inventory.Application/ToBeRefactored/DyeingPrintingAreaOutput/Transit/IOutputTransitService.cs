using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Transit
{
    public interface IOutputTransitService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, OutputTransitViewModel viewModel);
        Task<int> Create(OutputTransitViewModel viewModel);
        Task<OutputTransitViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        List<InputTransitProductionOrderViewModel> GetInputTransitProductionOrders(long productionOrderId);
        ListResult<InputTransitProductionOrderViewModel> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword);
        MemoryStream GenerateExcel(OutputTransitViewModel viewModel);
        MemoryStream GenerateExcel();
    }
}
