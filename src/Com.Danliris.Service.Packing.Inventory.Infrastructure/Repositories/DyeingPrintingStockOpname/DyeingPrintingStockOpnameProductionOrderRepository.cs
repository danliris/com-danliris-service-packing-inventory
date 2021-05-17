using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname
{
   public class DyeingPrintingStockOpnameProductionOrderRepository: IDyeingPrintingStockOpnameProductionOrderRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DyeingPrintingStockOpnameProductionOrderModel> _dbSet;

        public DyeingPrintingStockOpnameProductionOrderRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DyeingPrintingStockOpnameProductionOrderModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);

            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingStockOpnameProductionOrderModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(DyeingPrintingStockOpnameProductionOrderModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingStockOpnameProductionOrderModel> ReadAll()
        {
            return _dbSet.Include(s => s.DyeingPrintingStockOpname).AsNoTracking();
           
        }

        public IQueryable<DyeingPrintingStockOpnameProductionOrderModel> ReadAllIgnoreQueryFilter()
        {
              return _dbSet.Include(s => s.DyeingPrintingStockOpname).IgnoreQueryFilters().AsNoTracking();
          
        }

        public Task<DyeingPrintingStockOpnameProductionOrderModel> ReadByIdAsync(int id)
        {
            return _dbSet.Include(s => s.DyeingPrintingStockOpname).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingStockOpnameProductionOrderModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            
            modelToUpdate.SetBalance(model.Balance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyer(model.BuyerId, model.Buyer, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCartNo(model.CartNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetColor(model.Color, _identityProvider.Username, UserAgent);
            modelToUpdate.SetConstruction(model.Construction, _identityProvider.Username, UserAgent);
            modelToUpdate.SetGrade(model.Grade, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMotif(model.Motif, _identityProvider.Username, UserAgent);
            modelToUpdate.SetProductionOrder(model.ProductionOrderId, model.ProductionOrderNo, model.ProductionOrderType, model.ProductionOrderOrderQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackingInstruction(model.PackingInstruction, _identityProvider.Username, UserAgent);
            modelToUpdate.SetStatus(model.Status, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnit(model.Unit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingType(model.PackagingType, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingQty(model.PackagingQty, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingUnit(model.PackagingUnit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingLength(model.PackagingLength, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterial(model.MaterialId, model.MaterialName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterialConstruction(model.MaterialConstructionId, model.MaterialConstructionName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterialWidth(model.MaterialWidth, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }

        
    }
}
