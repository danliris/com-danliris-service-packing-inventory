using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport
{
    public interface IGarmentShippingExportSalesDOService
    {
        Task<int> Create(GarmentShippingExportSalesDOViewModel viewModel);
        Task<GarmentShippingExportSalesDOViewModel> ReadById(int id);
        ListResult<GarmentShippingExportSalesDOViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingExportSalesDOViewModel viewModel);
        Task<int> Delete(int id);
    }
}
