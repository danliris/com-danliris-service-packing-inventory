using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap
{
    public interface IPaymentDispositionRecapService
    {
        Task<int> Create(PaymentDispositionRecapViewModel viewModel);
        Task<PaymentDispositionRecapViewModel> ReadById(int id);
        ListResult<PaymentDispositionRecapViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, PaymentDispositionRecapViewModel viewModel);
        Task<int> Delete(int id);
    }
}
