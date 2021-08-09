using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaInputProductionOrderRepository : IRepository<DyeingPrintingAreaInputProductionOrderModel>
    {
        IQueryable<DyeingPrintingAreaInputProductionOrderModel> GetDbSet();
        IQueryable<DyeingPrintingAreaInputProductionOrderModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateFromFabricQualityControlAsync(int id, string grade, bool isChecked, double newBalance, double avalABalance, double avalBBalance, double avalConnectionBalance);
        Task<int> UpdateFromOutputAsync(int id, bool hasOutputDocument);
        Task<int> UpdateFromOutputAsync(int id, double balance);
        Task<int> UpdateFromOutputIMAsync(int id, double balance, double avalALength, double avalBLength, double avalConnectionLength);
        DyeingPrintingAreaInputProductionOrderModel GetInputProductionOrder(int id);
        Task<int> UpdateFromNextAreaInputAsync(int id, double balance, decimal qtyPacking);
        Task<int> UpdateBalanceAndRemainsAsync(int id, double balance);
        Task<int> UpdateBalanceAndRemainsWithFlagAsync(int id, double balance);
        Task<int> UpdateBalanceAndRemainsWithFlagAsync(int id, double balance, decimal qtyPacking);
        Task<int> UpdateFromOutputIMAsync(int id, double balance);
        Task<int> UpdateDateOutsync(int id, DateTimeOffset dateOut);
        Task<Tuple<int, List<PackingData>>> UpdatePackingFromOut(string destinationArea, string productionOrderNo, string grade, double balance);
        Task<int> RestorePacking(string destinationArea, List<PackingData> packingData);
        Task<int> UpdateFromNextAreaInputPackingAsync(List<PackingData> packingData);
        bool CheckIfHasInInput(string code);
    }
}