using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote
{
    public interface IGarmentShippingDebitNoteService
    {
        Task<int> Create(GarmentShippingDebitNoteViewModel viewModel);
        Task<GarmentShippingDebitNoteViewModel> ReadById(int id);
        ListResult<GarmentShippingDebitNoteViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingDebitNoteViewModel viewModel);
        Task<int> Delete(int id);
        Task<FileResult> ReadPdfById(int id);
    }
}
