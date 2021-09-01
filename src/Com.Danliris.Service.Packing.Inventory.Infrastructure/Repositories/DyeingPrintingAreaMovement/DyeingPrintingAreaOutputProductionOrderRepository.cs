﻿using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaOutputProductionOrderRepository : IDyeingPrintingAreaOutputProductionOrderRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DyeingPrintingAreaOutputProductionOrderModel> _dbSet;

        public DyeingPrintingAreaOutputProductionOrderRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DyeingPrintingAreaOutputProductionOrderModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);

            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaOutputProductionOrderModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(DyeingPrintingAreaOutputProductionOrderModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaOutputProductionOrderModel> ReadAll()
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaOutput).AsNoTracking();
        }

        public IQueryable<DyeingPrintingAreaOutputProductionOrderModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaOutput).IgnoreQueryFilters().AsNoTracking();
        }

        public Task<DyeingPrintingAreaOutputProductionOrderModel> ReadByIdAsync(int id)
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaOutput).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingAreaOutputProductionOrderModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetArea(model.Area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDestinationArea(model.DestinationArea, _identityProvider.Username, UserAgent);
            modelToUpdate.SetHasNextAreaDocument(model.HasNextAreaDocument, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNextAreaInputStatus(model.NextAreaInputStatus, _identityProvider.Username, UserAgent);
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
            modelToUpdate.SetDeliveryOrderSales(model.DeliveryOrderSalesId, model.DeliveryOrderSalesNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingType(model.PackagingType, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingQty(model.PackagingQty, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackagingUnit(model.PackagingUnit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAvalType(model.AvalType, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetAvalALength(model.AvalALength, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetAvalBLength(model.AvalBLength, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetAvalConnectionLength(model.AvalConnectionLength, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDescription(model.Description, _identityProvider.Username, UserAgent);
            modelToUpdate.SetHasSalesInvoice(model.HasSalesInvoice, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShippingGrade(model.ShippingGrade, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShippingRemark(model.ShippingRemark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetWeight(model.Weight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterial(model.MaterialId, model.MaterialName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterialConstruction(model.MaterialConstructionId, model.MaterialConstructionName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMachine(model.Machine, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMaterialWidth(model.MaterialWidth, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFinishWidth(model.FinishWidth, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromInputNextAreaFlagAsync(int id, bool hasNextAreaDocument)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);

            if (modelToUpdate != null)
            {
                modelToUpdate.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromInputNextAreaFlagAsync(int id, bool hasNextAreaDocument, string nextAreaInputStatus)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);

            if (modelToUpdate != null)
            {
                modelToUpdate.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);
                modelToUpdate.SetNextAreaInputStatus(nextAreaInputStatus, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromInputAsync(IEnumerable<int> ids, bool hasNextAreaDocument)
        {
            var modelToUpdate = _dbSet.Where(s => ids.Contains(s.Id));

            foreach (var item in modelToUpdate)
            {
                item.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromInputAsync(IEnumerable<int> ids, bool hasNextAreaDocument, string nextAreaInputStatus)
        {
            var modelToUpdate = _dbSet.Where(s => ids.Contains(s.Id));

            foreach (var item in modelToUpdate)
            {
                item.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);
                item.SetNextAreaInputStatus(nextAreaInputStatus, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateHasSalesInvoice(int id, bool hasSalesInvoice)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetHasSalesInvoice(hasSalesInvoice, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateHasPrintingProductPacking(int id, bool hasPrintingProductPacking)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetHasPrintingProductPacking(hasPrintingProductPacking, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateOutputBalancePackingQtyFromInput(int Id, decimal inputPackingQty) {
            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == Id);
            if (modelToUpdate != null) {
                var newQtyPacking = modelToUpdate.PackagingQty - inputPackingQty;
                modelToUpdate.SetPackagingQty(newQtyPacking, _identityProvider.Username, UserAgent);

                var newBalance = modelToUpdate.PackagingLength * (double) newQtyPacking;
                modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);
            }
            return _dbContext.SaveChangesAsync();
        }

    }
}