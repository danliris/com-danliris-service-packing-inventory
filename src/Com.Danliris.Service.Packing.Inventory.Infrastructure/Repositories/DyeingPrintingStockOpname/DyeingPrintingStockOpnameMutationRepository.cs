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
    public class DyeingPrintingStockOpnameMutationRepository : IDyeingPrintingStockOpnameMutationRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DyeingPrintingStockOpnameMutationModel> _dbSet;

        public DyeingPrintingStockOpnameMutationRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DyeingPrintingStockOpnameMutationModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.Include(s => s.DyeingPrintingStockOpnameMutationItems).FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);
            foreach (var item in model.DyeingPrintingStockOpnameMutationItems)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();

        }

        public IQueryable<DyeingPrintingStockOpnameMutationModel> GetDbSet()
        {
            return _dbSet;
        }
        public Task<int> InsertAsync(DyeingPrintingStockOpnameMutationModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            foreach (var item in model.DyeingPrintingStockOpnameMutationItems)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }
        public IQueryable<DyeingPrintingStockOpnameMutationModel> ReadAll()
        {
            return _dbSet.Include(s => s.DyeingPrintingStockOpnameMutationItems).AsNoTracking().Where(entity => !entity.IsDeleted);
        }

        public IQueryable<DyeingPrintingStockOpnameMutationModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.Include(s => s.DyeingPrintingStockOpnameMutationItems).IgnoreQueryFilters().AsNoTracking();
        }

        public Task<DyeingPrintingStockOpnameMutationModel> ReadByIdAsync(int id)
        {
            return _dbSet.Include(s => s.DyeingPrintingStockOpnameMutationItems).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingStockOpnameMutationModel model)
        {
            var modelToUpdate = _dbSet.Include(s => s.DyeingPrintingStockOpnameMutationItems).FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetBonNo(model.BonNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);

            foreach (var item in modelToUpdate.DyeingPrintingStockOpnameMutationItems)
            {
                var localItem = model.DyeingPrintingStockOpnameMutationItems.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                }
                else
                {

                    //item.SetPackingCode(localItem.ProductSKUId, localItem.FabricSKUId, localItem.ProductSKUCode, localItem.ProductPackingId, localItem.FabricPackingId, localItem.ProductPackingCode, false, _identityProvider.Username, UserAgent);
                    //item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);
                    //item.SetBuyer(localItem.BuyerId, localItem.Buyer, _identityProvider.Username, UserAgent);
                    //item.SetCartNo(localItem.CartNo, _identityProvider.Username, UserAgent);
                    //item.SetColor(localItem.Color, _identityProvider.Username, UserAgent);
                    //item.SetConstruction(localItem.Construction, _identityProvider.Username, UserAgent);
                    //item.SetGrade(localItem.Grade, _identityProvider.Username, UserAgent);
                    //item.SetMotif(localItem.Motif, _identityProvider.Username, UserAgent);
                    //item.SetProductionOrder(localItem.ProductionOrderId, localItem.ProductionOrderNo, localItem.ProductionOrderType, localItem.ProductionOrderOrderQuantity, _identityProvider.Username, UserAgent);
                    //item.SetRemark(localItem.Remark, _identityProvider.Username, UserAgent);
                    //item.SetPackingInstruction(localItem.PackingInstruction, _identityProvider.Username, UserAgent);
                    //item.SetPackagingUnit(localItem.PackagingUnit, _identityProvider.Username, UserAgent);
                    //item.SetPackagingType(localItem.PackagingType, _identityProvider.Username, UserAgent);
                    //item.SetPackagingQty(localItem.PackagingQty, _identityProvider.Username, UserAgent);
                    //item.SetPackagingLength(localItem.PackagingLength, _identityProvider.Username, UserAgent);
                    //item.SetStatus(localItem.Status, _identityProvider.Username, UserAgent);
                    //item.SetUnit(localItem.Unit, _identityProvider.Username, UserAgent);
                    //item.SetUomUnit(localItem.UomUnit, _identityProvider.Username, UserAgent);
                    //item.SetMaterial(localItem.MaterialId, localItem.MaterialName, _identityProvider.Username, UserAgent);
                    //item.SetMaterialConstruction(localItem.MaterialConstructionId, localItem.MaterialConstructionName, _identityProvider.Username, UserAgent);

                    //item.SetMaterialWidth(localItem.MaterialWidth, _identityProvider.Username, UserAgent);
                    //item.SetDocumentNo(localItem.DocumentNo, _identityProvider.Username, UserAgent);
                    _dbContext.DyeingPrintingStockOpnameMutationItems.Update(item);
                }
            }

            foreach (var item in model.DyeingPrintingStockOpnameMutationItems.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                //modelToUpdate.DyeingPrintingStockOpnameProductionOrders.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }

    }
}
