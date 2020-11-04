using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice
{
    public interface IGarmentShippingCreditAdviceService
    {
        Task<int> Create(GarmentShippingCreditAdviceViewModel viewModel);
        Task<GarmentShippingCreditAdviceViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<GarmentShippingCreditAdviceViewModel> ReadData(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingCreditAdviceViewModel viewModel);
        Task<int> Delete(int id);
    }
}
