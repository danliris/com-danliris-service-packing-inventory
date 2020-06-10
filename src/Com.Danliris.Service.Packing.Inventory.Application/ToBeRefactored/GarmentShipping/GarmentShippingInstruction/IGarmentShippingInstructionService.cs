using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction
{
    public interface IGarmentShippingInstructionService
    {
        Task<int> Create(GarmentShippingInstructionViewModel viewModel);
        Task<GarmentShippingInstructionViewModel> ReadById(int id);
        ListResult<GarmentShippingInstructionViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingInstructionViewModel viewModel);
        Task<int> Delete(int id);
    }
}
