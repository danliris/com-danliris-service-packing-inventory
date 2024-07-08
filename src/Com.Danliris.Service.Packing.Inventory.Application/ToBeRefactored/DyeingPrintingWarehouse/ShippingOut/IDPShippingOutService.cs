using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingOut.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingOut
{
    public interface IDPShippingOutService
    {
        List<InputShippingItemViewModel> GetInputByDeliveryOrder(long deliveryOrderId);
        Task<int> Create(OutputShippingViewModel viewModel);
        Task<int> Update(int Id, OutputShippingViewModel viewModel);
        ListResult<IndexOutViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<OutputShippingViewModel> ReadById(int id);
    }
}
