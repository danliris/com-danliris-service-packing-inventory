using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesDO
{
    public interface IGarmentMDLocalSalesDOService
    {
        Task<int> Create(GarmentMDLocalSalesDOViewModel viewModel);
        ListResult<GarmentMDLocalSalesDOViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<GarmentMDLocalSalesDOViewModel> ReadById(int id);
        Task<int> Update(int id, GarmentMDLocalSalesDOViewModel viewModel);
        Task<int> Delete(int id);
    }
}
