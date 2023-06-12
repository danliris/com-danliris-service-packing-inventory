using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname
{
    public interface IDyeingPrintingStockOpnameSummaryRepository : IRepository<DyeingPrintingStockOpnameSummaryModel>
    {
        IQueryable<DyeingPrintingStockOpnameSummaryModel> GetDbSet();
        IQueryable<DyeingPrintingStockOpnameSummaryModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateBalance(int id, double balance);
        Task<int> UpdateBalanceRemainsIn(int id, double balanceRemains);
        Task<int> UpdatePackingQty(int id, decimal packagingQtyOut);
        Task<int> UpdatePackingQtyRemainsIn(int id, decimal packagingQtyRemains);
        Task<int> UpdatePackingQtyOut(int id, decimal packagingQtyOut);

        Task<int> UpdatePackingQtyRemainsOut(int id, decimal packagingQtyRemains);
        Task<int> UpdateBalanceRemainsOut(int id, double balanceRemains);
        Task<int> UpdateBalanceOut(int id, double balanceOut);

        Task<int> UpdateSplitQuantity(int id, double splitQuantity);

    }
}
