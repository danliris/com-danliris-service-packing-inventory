using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaInputProductionOrderRepository : IDyeingPrintingAreaInputProductionOrderRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DyeingPrintingAreaInputProductionOrderModel> _dbSet;

        public DyeingPrintingAreaInputProductionOrderRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DyeingPrintingAreaInputProductionOrderModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaInputProductionOrderModel> GetDbSet()
        {
            return _dbSet;
        }

        public DyeingPrintingAreaInputProductionOrderModel GetInputProductionOrder(int id)
        {
            return _dbSet.FirstOrDefault(o => o.Id == id);
        }

        public Task<int> InsertAsync(DyeingPrintingAreaInputProductionOrderModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaInputProductionOrderModel> ReadAll()
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaInput).AsNoTracking();
        }

        public IQueryable<DyeingPrintingAreaInputProductionOrderModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaInput).IgnoreQueryFilters().AsNoTracking();
        }

        public Task<DyeingPrintingAreaInputProductionOrderModel> ReadByIdAsync(int id)
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaInput).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingAreaInputProductionOrderModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetArea(model.Area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBalance(model.Balance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyer(model.BuyerId, model.Buyer, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCartNo(model.CartNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetColor(model.Color, _identityProvider.Username, UserAgent);
            modelToUpdate.SetConstruction(model.Construction, _identityProvider.Username, UserAgent);
            modelToUpdate.SetGrade(model.Grade, _identityProvider.Username, UserAgent);
            modelToUpdate.SetHasOutputDocument(model.HasOutputDocument, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMotif(model.Motif, _identityProvider.Username, UserAgent);
            modelToUpdate.SetProductionOrder(model.ProductionOrderId, model.ProductionOrderNo, model.ProductionOrderType, model.ProductionOrderOrderQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetStatus(model.Status, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnit(model.Unit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsChecked(model.IsChecked, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackingInstruction(model.PackingInstruction, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDeliveryOrderSales(model.DeliveryOrderSalesId, model.DeliveryOrderSalesNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBalanceRemains(model.BalanceRemains, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterial(model.MaterialId, model.MaterialName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterialConstruction(model.MaterialConstructionId, model.MaterialConstructionName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterialWidth(model.MaterialWidth, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMachine(model.Machine, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromFabricQualityControlAsync(int id, string grade, bool isChecked, double newBalance, double avalABalance, double avalBBalance, double avalConnectionBalance)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);
            modelToUpdate.SetGrade(grade, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsChecked(isChecked, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInitLength(newBalance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAvalALength(avalABalance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAvalBLength(avalBBalance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAvalConnectionLength(avalConnectionBalance, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromOutputAsync(int id, bool hasOutputDocument)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);
            modelToUpdate.SetHasOutputDocument(hasOutputDocument, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromOutputAsync(int id, double balance)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalance = modelToUpdate.BalanceRemains - balance;
                modelToUpdate.SetBalanceRemains(newBalance, _identityProvider.Username, UserAgent);
                if (newBalance <= 0)
                {
                    modelToUpdate.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
                }
                else
                {
                    modelToUpdate.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromOutputIMAsync(int id, double balance)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalance = modelToUpdate.BalanceRemains - balance;
                modelToUpdate.SetBalanceRemains(newBalance, _identityProvider.Username, UserAgent);
                if (newBalance == 0)
                {
                    modelToUpdate.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
                }
                else
                {
                    modelToUpdate.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateBalanceAndRemainsAsync(int id, double balance)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalanceRemains = modelToUpdate.BalanceRemains - balance;
                var newBalance = modelToUpdate.Balance - balance;
                modelToUpdate.SetBalanceRemains(newBalanceRemains, _identityProvider.Username, UserAgent);
                modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);
                if (newBalanceRemains == 0)
                {
                    modelToUpdate.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
                }
                else
                {
                    modelToUpdate.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateBalanceAndRemainsWithFlagAsync(int id, double balance)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalanceRemains = modelToUpdate.BalanceRemains - balance;
                var newBalance = modelToUpdate.Balance - balance;
                modelToUpdate.SetBalanceRemains(newBalanceRemains, _identityProvider.Username, UserAgent);
                modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);
                if (newBalanceRemains <= 0)
                {
                    modelToUpdate.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
                }
                else
                {
                    modelToUpdate.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateBalanceAndRemainsWithFlagAsync(int id, double balance, decimal qtyPacking)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalanceRemains = modelToUpdate.BalanceRemains - balance;
                var newBalance = modelToUpdate.Balance - balance;
                modelToUpdate.SetBalanceRemains(newBalanceRemains, _identityProvider.Username, UserAgent);
                modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);

                var newQtyPacking = modelToUpdate.PackagingQty - qtyPacking;
                modelToUpdate.SetPackagingQty(newQtyPacking, _identityProvider.Username, UserAgent);

                if (newBalanceRemains <= 0)
                {
                    modelToUpdate.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
                }
                else
                {
                    modelToUpdate.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromNextAreaInputAsync(int id, double balance, decimal qtyPacking)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);
            var newBalance = modelToUpdate.Balance - balance;
            modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);

            if (modelToUpdate.Area == DyeingPrintingArea.GUDANGJADI || modelToUpdate.Area == DyeingPrintingArea.SHIPPING || modelToUpdate.Area == DyeingPrintingArea.TRANSIT)
            {
                var newQtyPacking = modelToUpdate.PackagingQty - qtyPacking;
                modelToUpdate.SetPackagingQty(newQtyPacking, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromOutputIMAsync(int id, double balance, double avalALength, double avalBLength, double avalConnectionLength)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);
            var newInitLength = modelToUpdate.InitLength - balance;
            var newAvalALength = modelToUpdate.AvalALength - avalALength;
            var newAvalBLength = modelToUpdate.AvalBLength - avalBLength;
            var newAvalConnectionLength = modelToUpdate.AvalConnectionLength - avalConnectionLength;
            var newBalance = modelToUpdate.Balance - balance;
            modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAvalALength(newAvalALength, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAvalBLength(newAvalBLength, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAvalConnectionLength(newAvalConnectionLength, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInitLength(newInitLength, _identityProvider.Username, UserAgent);
            if (newBalance <= 0)
            {
                modelToUpdate.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
            }
            else
            {
                modelToUpdate.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
            }
            return _dbContext.SaveChangesAsync();
        }

        public async Task<Tuple<int, List<PackingData>>> UpdatePackingFromOut(string destinationArea, string productionOrderNo, string grade, double balance)
        {
            var data = _dbSet.Where(s => s.Area == DyeingPrintingArea.PACKING && s.ProductionOrderNo == productionOrderNo && s.Grade == grade &&
                s.BalanceRemains > 0).Include(s => s.DyeingPrintingAreaInput).OrderBy(s => s.DyeingPrintingAreaInput.Date).ToList();

            int index = 0;
            var packingData = new List<PackingData>();
            while (balance > 0)
            {
                var item = data.ElementAtOrDefault(index);

                if (item != null)
                {
                    var previousPacking = new PackingData()
                    {
                        Id = item.Id
                    };

                    double diffBalance = 0;
                    var tempBalanceRemains = item.BalanceRemains - balance;
                    var tempBalance = item.Balance - balance;

                    if (tempBalanceRemains < 0)
                    {
                        balance = tempBalanceRemains * -1;
                        diffBalance = item.BalanceRemains;
                        item.SetBalanceRemains(0, _identityProvider.Username, UserAgent);
                        if (destinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {
                            item.SetBalance(0, _identityProvider.Username, UserAgent);
                        }
                        item.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
                    }
                    else
                    {
                        diffBalance = balance;
                        item.SetBalanceRemains(tempBalanceRemains, _identityProvider.Username, UserAgent);
                        if (destinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {
                            item.SetBalance(tempBalance, _identityProvider.Username, UserAgent);
                        }
                        if (tempBalanceRemains == 0)
                        {
                            item.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
                        }
                        else
                        {
                            item.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
                        }
                        balance = 0;

                    }

                    previousPacking.Balance = diffBalance;

                    packingData.Add(previousPacking);

                    index++;
                }
                else
                {
                    break;
                }
            }

            int result = await _dbContext.SaveChangesAsync();

            return new Tuple<int, List<PackingData>>(result, packingData);
        }

        public Task<int> RestorePacking(string destinationArea, List<PackingData> packingData)
        {
            foreach (var item in packingData)
            {
                var model = _dbSet.FirstOrDefault(s => s.Id == item.Id);
                if (model != null)
                {
                    var newBalanceRemains = model.BalanceRemains + item.Balance;
                    var newBalance = model.Balance + item.Balance;


                    model.SetBalanceRemains(newBalanceRemains, _identityProvider.Username, UserAgent);
                    if (destinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                    {
                        model.SetBalance(newBalance, _identityProvider.Username, UserAgent);
                    }
                    model.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
                }

            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromNextAreaInputPackingAsync(List<PackingData> packingData)
        {
                foreach (var item in packingData)
            {
                var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == item.Id);
                if (modelToUpdate != null)
                {
                    var newBalance = modelToUpdate.Balance - item.Balance;
                    modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateDateOutsync(int id, DateTimeOffset dateOut)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                modelToUpdate.SetDateOut(dateOut, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
