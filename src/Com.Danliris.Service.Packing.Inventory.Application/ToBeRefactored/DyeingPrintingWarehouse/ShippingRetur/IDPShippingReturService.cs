using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingRetur.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingRetur
{
    public interface IDPShippingReturService
    {
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Create(ShippingReturViewModel viewModel);
        ListResult<barcodeViewModel> GetCodeLoader(int page, int size, string filter, string order, string keyword);
        Task<ShippingReturViewModel> ReadById(int id);
        //int CheckBarcode(string barcode);
    }
}
