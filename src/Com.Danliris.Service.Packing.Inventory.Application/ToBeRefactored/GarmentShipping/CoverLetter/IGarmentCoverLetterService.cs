using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter
{
    public interface IGarmentCoverLetterService
    {
        Task<int> Create(GarmentCoverLetterViewModel viewModel);
        Task<GarmentCoverLetterViewModel> ReadById(int id);
        Task<GarmentCoverLetterViewModel> ReadByInvoiceId(int invoiceId);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentCoverLetterViewModel viewModel);
        Task<int> Delete(int id);
    }
}
