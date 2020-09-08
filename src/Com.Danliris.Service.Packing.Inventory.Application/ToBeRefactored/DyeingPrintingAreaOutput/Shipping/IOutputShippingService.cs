using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public interface IOutputShippingService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, OutputShippingViewModel viewModel);
        Task<int> Create(OutputShippingViewModel viewModel);
        Task<OutputShippingViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<IndexViewModel> ReadForSales(int page, int size, string filter, string order, string keyword);
        List<InputShippingProductionOrderViewModel> GetInputShippingProductionOrdersByDeliveryOrder(long deliveryOrderId);
        Task<int> UpdateHasSalesInvoice(int id, OutputShippingUpdateSalesInvoiceViewModel salesInvoice);
        //List<OutputShippingProductionOrderViewModel> GetOutputShippingProductionOrdersByBon(int shippingInputId);
        MemoryStream GenerateExcel(OutputShippingViewModel viewModel);
        MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
        ListResult<AdjShippingProductionOrderViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword);
        ListResult<InputShippingProductionOrderViewModel> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword);
        List<InputShippingProductionOrderViewModel> GetInputShippingProductionOrdersByProductionOrder(long productionOrderId);
    }
}
