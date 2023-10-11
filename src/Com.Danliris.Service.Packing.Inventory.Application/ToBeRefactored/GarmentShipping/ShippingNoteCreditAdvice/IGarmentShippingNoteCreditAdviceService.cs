using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice
{
    public interface IGarmentShippingNoteCreditAdviceService
    {
        Task<int> Create(GarmentShippingNoteCreditAdviceViewModel viewModel);
        Task<GarmentShippingNoteCreditAdviceViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<GarmentShippingNoteCreditAdviceViewModel> ReadData(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingNoteCreditAdviceViewModel viewModel);
        Task<int> Delete(int id);
    }
}
