using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetterTS
{
    public interface IGarmentLocalCoverLetterTSService
    {
        Task<int> Create(GarmentLocalCoverLetterTSViewModel viewModel);
        Task<GarmentLocalCoverLetterTSViewModel> ReadById(int id);
        Task<GarmentLocalCoverLetterTSViewModel> ReadByLocalSalesNoteId(int localsalesnoteid);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentLocalCoverLetterTSViewModel viewModel);
        Task<int> Delete(int id);
    }
}
