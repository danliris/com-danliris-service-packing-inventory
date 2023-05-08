using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition.PaymentDispositionEMKLs
{
    public interface IGarmentShippingPaymentEMKLDispositionService
    {
        GarmentShippingPaymentDispositionEMKLViewModel MapToViewModel(GarmentShippingPaymentDispositionModel model);
        GarmentShippingPaymentDispositionModel MapToModel(GarmentShippingPaymentDispositionEMKLViewModel vm);
        Task<int> Create(GarmentShippingPaymentDispositionEMKLViewModel viewModel);
        Task<GarmentShippingPaymentDispositionEMKLViewModel> ReadById(int id);
        ListResult<GarmentShippingPaymentDispositionEMKLViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingPaymentDispositionEMKLViewModel viewModel);
        Task<int> Delete(int id);
    }
}
