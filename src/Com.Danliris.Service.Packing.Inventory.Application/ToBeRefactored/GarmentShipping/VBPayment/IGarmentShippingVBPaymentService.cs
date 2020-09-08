using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.VBPayment
{
    public interface IGarmentShippingVBPaymentService
    {
        Task<int> Create(GarmentShippingVBPaymentViewModel viewModel);
        Task<GarmentShippingVBPaymentViewModel> ReadById(int id);
        ListResult<GarmentShippingVBPaymentViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingVBPaymentViewModel viewModel);
        Task<int> Delete(int id);
    }
}
