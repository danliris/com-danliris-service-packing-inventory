using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct
{
    public interface IDyeingPrintingProductService
    {
        Task<ListResult<DyeingPrintingProductPackingViewModel>> GetDataProductPacking(int page, int size, string filter, string order, string keyword);
        Task<int> UpdatePrintingStatusProductPacking(int id, bool hasPrintingProduct);
    }
}
