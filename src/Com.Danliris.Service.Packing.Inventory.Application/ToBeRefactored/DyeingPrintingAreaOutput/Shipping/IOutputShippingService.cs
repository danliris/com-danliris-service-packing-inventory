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
        Task<int> Create(OutputShippingViewModel viewModel);
        Task<OutputShippingViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<MemoryStream> GenerateExcel(int id);
        List<InputShippingProductionOrderViewModel> GetInputShippingProductionOrdersByDeliveryOrder(long deliveryOrderId);
        //List<OutputShippingProductionOrderViewModel> GetOutputShippingProductionOrdersByBon(int shippingInputId);
    }
}
