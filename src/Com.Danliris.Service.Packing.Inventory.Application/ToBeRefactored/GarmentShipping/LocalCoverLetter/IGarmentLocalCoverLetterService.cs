using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter
{
    public interface IGarmentLocalCoverLetterService
    {
        Task<int> Create(GarmentLocalCoverLetterViewModel viewModel);
        Task<GarmentLocalCoverLetterViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentLocalCoverLetterViewModel viewModel);
        Task<int> Delete(int id);
    }
}
