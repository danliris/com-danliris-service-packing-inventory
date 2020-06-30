using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote
{
    public interface IGarmentShippingLocalPriceCorrectionNoteService
    {
        Task<int> Create(GarmentShippingLocalPriceCorrectionNoteViewModel viewModel);
        Task<GarmentShippingLocalPriceCorrectionNoteViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Delete(int id);
    }
}
