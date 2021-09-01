using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct
{
    public interface IDyeingPrintingProductService
    {
        ListResult<DyeingPrintingProductPackingViewModel> GetDataProductPacking(int page, int size, string filter, string order, string keyword, bool isStockOpname);
        DyeingPrintingProductPackingViewModel GetDataProductByPackingCode(string packingCode);
        Task<int> UpdatePrintingStatusProductPacking(int id, bool hasPrintingProduct);
    }
}
