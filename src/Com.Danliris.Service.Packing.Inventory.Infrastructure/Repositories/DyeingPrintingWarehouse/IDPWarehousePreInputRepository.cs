using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingWarehouse
{
    public interface IDPWarehousePreInputRepository : IRepository<DPWarehousePreInputModel>
    {
        IQueryable<DPWarehousePreInputModel> GetDbSet();
        IQueryable<DPWarehousePreInputModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateBalance(int id, double balance);
        Task<int> UpdateBalanceRemainsIn(int id, double balanceRemains);
    }
}
