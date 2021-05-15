using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
  public  interface IStockOpnameWarehouseService
    {
        Task<int> Create(StockOpnameWarehouseViewModel viewModel);
        
        Task<StockOpnameWarehouseViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<IndexViewModel> Read(string keyword);
        Task<int> Delete(int bonId);
        Task<int> Update(int id, StockOpnameWarehouseViewModel viewModel);
    }
}
