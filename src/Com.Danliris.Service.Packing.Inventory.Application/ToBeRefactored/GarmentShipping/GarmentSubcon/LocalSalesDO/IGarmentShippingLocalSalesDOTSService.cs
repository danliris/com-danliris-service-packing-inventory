using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDOTS
{
    public interface IGarmentShippingLocalSalesDOTSService
    {
        Task<int> Create(GarmentShippingLocalSalesDOTSViewModel viewModel);
        Task<GarmentShippingLocalSalesDOTSViewModel> ReadById(int id);
        ListResult<GarmentShippingLocalSalesDOTSViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingLocalSalesDOTSViewModel viewModel);
        Task<int> Delete(int id);
    }
}
