using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote
{
    public interface IGarmentShippingLocalPriceCuttingNoteService
    {
        Task<int> Create(GarmentShippingLocalPriceCuttingNoteViewModel viewModel);
        Task<GarmentShippingLocalPriceCuttingNoteViewModel> ReadById(int id);
        ListResult<GarmentShippingLocalPriceCuttingNoteViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Delete(int id);
    }
}
