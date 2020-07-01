using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDO
{
    public interface IGarmentShippingLocalSalesDOService
    {
        Task<int> Create(GarmentShippingLocalSalesDOViewModel viewModel);
        Task<GarmentShippingLocalSalesDOViewModel> ReadById(int id);
        ListResult<GarmentShippingLocalSalesDOViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingLocalSalesDOViewModel viewModel);
        Task<int> Delete(int id);
    }
}
