using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesContract
{
    public interface IGarmentMDLocalSalesContractService
    {
        Task<int> Create(GarmentMDLocalSalesContractViewModel viewModel);
        ListResult<GarmentMDLocalSalesContractViewModel> Read(int page, int size, string filter, string order, string keyword);
    }
}
