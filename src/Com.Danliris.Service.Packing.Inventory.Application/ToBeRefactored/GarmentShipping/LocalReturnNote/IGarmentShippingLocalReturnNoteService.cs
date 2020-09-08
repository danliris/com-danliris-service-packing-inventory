using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote
{
    public interface IGarmentShippingLocalReturnNoteService
    {
        Task<int> Create(GarmentShippingLocalReturnNoteViewModel viewModel);
        Task<GarmentShippingLocalReturnNoteViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Delete(int id);
        Buyer GetBuyer(int id);
    }
}
