using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure
{
    public interface IGarmentShippingCostStructureService
    {
        Task<int> Create(GarmentShippingCostStructureViewModel viewModel);
        Task<GarmentShippingCostStructureViewModel> ReadById(int id);
        ListResult<GarmentShippingCostStructureViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingCostStructureViewModel viewModel);
        Task<int> Delete(int id);
        Task<MemoryStreamResult> ReadPdfById(int id);

    }
}
