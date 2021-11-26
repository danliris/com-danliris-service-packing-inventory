using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDraftPackingListItem
{
    public interface IGarmentDraftPackingListItemService
    {
        Task<int> Create(List<GarmentDraftPackingListItemViewModel> viewModels);
        Task<GarmentDraftPackingListItemViewModel> ReadById(int id);
        ListResult<GarmentDraftPackingListItemViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentDraftPackingListItemViewModel viewModel);
        Task<int> Delete(int id);
    }
}
