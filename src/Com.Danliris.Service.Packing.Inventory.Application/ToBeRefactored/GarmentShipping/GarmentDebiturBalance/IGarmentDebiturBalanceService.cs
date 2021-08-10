using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDebiturBalance
{
    public interface IGarmentDebiturBalanceService
    {
        Task<int> Create(GarmentDebiturBalanceViewModel viewModel);
        Task<GarmentDebiturBalanceViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentDebiturBalanceViewModel viewModel);
        Task<int> Delete(int id);
    }
}
