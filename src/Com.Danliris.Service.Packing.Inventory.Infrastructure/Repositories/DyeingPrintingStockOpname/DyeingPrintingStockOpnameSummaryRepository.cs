using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname
{
    public class DyeingPrintingStockOpnameSummaryRepository : IDyeingPrintingStockOpnameSummaryRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DyeingPrintingStockOpnameSummaryModel> _dbSet;

        public DyeingPrintingStockOpnameSummaryRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DyeingPrintingStockOpnameSummaryModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<DyeingPrintingStockOpnameSummaryModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingStockOpnameSummaryModel> ReadAll()
        {
            return _dbSet.AsNoTracking();

        }

        public IQueryable<DyeingPrintingStockOpnameSummaryModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.IgnoreQueryFilters().AsNoTracking();

        }

        public Task<DyeingPrintingStockOpnameSummaryModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingStockOpnameSummaryModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);


            modelToUpdate.SetBalance(model.Balance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBalanceRemains(model.BalanceRemains, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingQty(model.PackagingQty, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingQtyRemains(model.PackagingQtyRemains, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSplitQuantity(model.SplitQuantity, _identityProvider.Username, UserAgent);
            

            //modelToUpdate.SetBalance(model.Balance, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetBuyer(model.BuyerId, model.Buyer, _identityProvider.Username, UserAgent);

            //modelToUpdate.SetCartNo(model.CartNo, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetColor(model.Color, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetConstruction(model.Construction, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetGrade(model.Grade, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetMotif(model.Motif, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetProductionOrder(model.ProductionOrderId, model.ProductionOrderNo, model.ProductionOrderType, model.ProductionOrderOrderQuantity, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetPackingInstruction(model.PackingInstruction, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetStatus(model.Status, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetUnit(model.Unit, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetPackagingType(model.PackagingType, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetPackagingQty(model.PackagingQty, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetPackagingUnit(model.PackagingUnit, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetPackagingLength(model.PackagingLength, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetMaterial(model.MaterialId, model.MaterialName, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetMaterialConstruction(model.MaterialConstructionId, model.MaterialConstructionName, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetMaterialWidth(model.MaterialWidth, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }


        public Task<int> InsertAsync(DyeingPrintingStockOpnameSummaryModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }
        public Task<int> UpdateBalance(int id, double balance)
        {
            


            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalance = modelToUpdate.Balance + balance;
                modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);
               
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateBalanceOut(int id, double balanceOut)
        {



            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalanceOut = modelToUpdate.BalanceEnd + balanceOut;
                modelToUpdate.SetBalanceEnd(newBalanceOut, _identityProvider.Username, UserAgent);

            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateBalanceRemainsIn(int id, double balanceRemains)
        {



            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalanceRemains = modelToUpdate.BalanceRemains + balanceRemains;
                modelToUpdate.SetBalanceRemains(newBalanceRemains, _identityProvider.Username, UserAgent);

            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateBalanceRemainsOut(int id, double balanceRemains)
        {



            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalanceRemains = modelToUpdate.BalanceRemains - balanceRemains;
                modelToUpdate.SetBalanceRemains(newBalanceRemains, _identityProvider.Username, UserAgent);

            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdatePackingQty(int id, decimal packagingQty)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newPackagingQty = modelToUpdate.PackagingQty + packagingQty;
                modelToUpdate.SetPackagingQty(newPackagingQty, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdatePackingQtyOut(int id, decimal packagingQtyOut)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newPackagingQtyEnd = modelToUpdate.PackagingQtyEnd + packagingQtyOut;
                modelToUpdate.SetPackagingQtyEnd(newPackagingQtyEnd, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdatePackingQtyRemainsIn(int id, decimal packagingQtyRemains)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newPackagingQtyRemains = modelToUpdate.PackagingQtyRemains + packagingQtyRemains;
                modelToUpdate.SetPackagingQtyRemains(newPackagingQtyRemains, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdatePackingQtyRemainsOut(int id, decimal packagingQtyRemains)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newPackagingQtyRemains = modelToUpdate.PackagingQtyRemains - packagingQtyRemains;
                modelToUpdate.SetPackagingQtyRemains(newPackagingQtyRemains, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }


        public Task<int> UpdateSplitQuantity(int id, double splitQuantity)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newsplitQuantity = modelToUpdate.SplitQuantity + splitQuantity;
                modelToUpdate.SetSplitQuantity(newsplitQuantity, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

    }
}
