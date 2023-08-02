using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN
{
    public interface IDPShippingInService
    {
        List<PreInputShippingViewModel> GetOutputPreShippingProductionOrders(long deliveriOrderSalesId);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Create(InputShippingViewModel viewModel);
        Task<InputShippingViewModel> ReadById(int id);
        Task<InputShippingViewModel> ReadByIdBon(int id);
        MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, string type, int offSet);
        ListResult<PreInputShippingViewModel> GetDOLoader(int page, int size, string filter, string order, string keyword);
    }
}