using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesContract
{
    public interface IGarmentMDLocalSalesContractService
    {
        Task<int> Create(GarmentMDLocalSalesContractViewModel viewModel);
    }
}
