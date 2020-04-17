using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalInput
{
    public interface IAvalInputService
    {
        Task<int> Create(AvalInputViewModel viewModel);
        Task<int> Update(int id, AvalInputViewModel viewModel);
        Task<int> Delete(int id);
        Task<AvalInputViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
    }
}