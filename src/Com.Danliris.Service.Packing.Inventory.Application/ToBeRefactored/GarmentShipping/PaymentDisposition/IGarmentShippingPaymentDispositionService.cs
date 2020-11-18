using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition
{
    public interface IGarmentShippingPaymentDispositionService
    {
        GarmentShippingPaymentDispositionViewModel MapToViewModel(GarmentShippingPaymentDispositionModel model);
        GarmentShippingPaymentDispositionModel MapToModel(GarmentShippingPaymentDispositionViewModel vm);
        Task<int> Create(GarmentShippingPaymentDispositionViewModel viewModel);
        Task<GarmentShippingPaymentDispositionViewModel> ReadById(int id);
        ListResult<GarmentShippingPaymentDispositionViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingPaymentDispositionViewModel viewModel);
        Task<int> Delete(int id);
    }
}
