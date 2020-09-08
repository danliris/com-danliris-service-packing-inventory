using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote
{
    public interface IGarmentShippingCreditNoteService
    {
        Task<int> Create(GarmentShippingCreditNoteViewModel viewModel);
        Task<GarmentShippingCreditNoteViewModel> ReadById(int id);
        ListResult<GarmentShippingCreditNoteViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingCreditNoteViewModel viewModel);
        Task<int> Delete(int id);
        Task<ExcelResult> ReadPdfById(int id);
    }
}
