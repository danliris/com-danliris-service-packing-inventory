using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition
{
    public interface IGarmentShippingInsuranceDispositionService
    {
        Task<int> Create(GarmentShippingInsuranceDispositionViewModel viewModel);
        Task<GarmentShippingInsuranceDispositionViewModel> ReadById(int id);
        ListResult<GarmentShippingInsuranceDispositionViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingInsuranceDispositionViewModel viewModel);
        Task<int> Delete(int id);
        Insurance GetInsurance(int id);
    }
}
