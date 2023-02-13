using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport
{
    public interface IGarmentShippingExportSalesContractService
    {
        Task<int> Create(GarmentShippingExportSalesContractViewModel viewModel);
        Task<GarmentShippingExportSalesContractViewModel> ReadById(int id);
        ListResult<GarmentShippingExportSalesContractViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingExportSalesContractViewModel viewModel);
        Task<int> Delete(int id);
    }
}
