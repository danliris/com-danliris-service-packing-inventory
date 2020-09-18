using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract
{
    public interface IGarmentShippingLocalSalesContractService
    {
        Task<int> Create(GarmentShippingLocalSalesContractViewModel viewModel);
        Task<GarmentShippingLocalSalesContractViewModel> ReadById(int id);
        ListResult<GarmentShippingLocalSalesContractViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingLocalSalesContractViewModel viewModel);
        Task<int> Delete(int id);
    }
}
