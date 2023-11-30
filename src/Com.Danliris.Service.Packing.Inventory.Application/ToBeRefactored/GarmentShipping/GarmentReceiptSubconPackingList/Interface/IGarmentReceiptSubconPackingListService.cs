using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList
{
    public interface IGarmentReceiptSubconPackingListService
    {
        GarmentReceiptSubconPackingListViewModel MapToViewModel(GarmentReceiptSubconPackingListModel model);
        Task<string> Create(GarmentReceiptSubconPackingListViewModel viewModel);
        Task<GarmentReceiptSubconPackingListViewModel> ReadById(int id);
        ListResult<GarmentReceiptSubconPackingListViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Delete(int id);
        Task<int> Update(int id, GarmentReceiptSubconPackingListViewModel viewModel);

        Task<int> UpdateIsApproved(UpdateIsApprovedPackingListViewModel viewModel);

    }
}
