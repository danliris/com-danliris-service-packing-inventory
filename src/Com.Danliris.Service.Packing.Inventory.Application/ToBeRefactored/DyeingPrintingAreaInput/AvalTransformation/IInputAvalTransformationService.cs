using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.AvalTransformation
{
    public interface IInputAvalTransformationService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, InputAvalTransformationViewModel viewModel);
        Task<int> Create(InputAvalTransformationViewModel viewModel);
        Task<InputAvalTransformationViewModel> ReadById(int id);
        ListResult<InputAvalTransformationViewModel> Read(int page, int size, string filter, string order, string keyword);
        List<InputAvalTransformationProductionOrderViewModel> GetInputAvalProductionOrders(string avalType);
    }
}
