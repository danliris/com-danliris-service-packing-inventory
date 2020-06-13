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

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaInputRepository : IDyeingPrintingAreaInputRepository
    {
        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";

        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DyeingPrintingAreaInputModel> _dbSet;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputSPPRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _SPPRepository;

        public DyeingPrintingAreaInputRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DyeingPrintingAreaInputModel>();
            _outputSPPRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _SPPRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.Include(s => s.DyeingPrintingAreaInputProductionOrders).FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAvalTransformationArea(DyeingPrintingAreaInputModel model)
        {
            int result = 0;
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
                result += await _SPPRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaOutputProductionOrderId, false);
            }

            _dbSet.Update(model);

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public Task<int> DeleteIMArea(DyeingPrintingAreaInputModel model)
        {
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }
            
            //model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteShippingArea(DyeingPrintingAreaInputModel model)
        {
            int result = 0;
            result += await _outputSPPRepository.UpdateFromInputAsync(model.DyeingPrintingAreaInputProductionOrders.Select(s => s.DyeingPrintingAreaOutputProductionOrderId), false);
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
                var previousOutputData = await _outputSPPRepository.ReadByIdAsync(item.DyeingPrintingAreaOutputProductionOrderId);
                result += await _SPPRepository.UpdateFromNextAreaInputAsync(previousOutputData.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
            }

            _dbSet.Update(model);

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteTransitArea(DyeingPrintingAreaInputModel model)
        {
            int result = 0;
            result += await _outputSPPRepository.UpdateFromInputAsync(model.DyeingPrintingAreaInputProductionOrders.Select(s => s.DyeingPrintingAreaOutputProductionOrderId), false);
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
                var previousOutputData = await _outputSPPRepository.ReadByIdAsync(item.DyeingPrintingAreaOutputProductionOrderId);
                result += await _SPPRepository.UpdateFromNextAreaInputAsync(previousOutputData.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
            }

            //model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public IQueryable<DyeingPrintingAreaInputModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(DyeingPrintingAreaInputModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaInputModel> ReadAll()
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaInputProductionOrders).AsNoTracking();
        }

        public IQueryable<DyeingPrintingAreaInputModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaInputProductionOrders).IgnoreQueryFilters().AsNoTracking();
        }

        public Task<DyeingPrintingAreaInputModel> ReadByIdAsync(int id)
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaInputProductionOrders).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingAreaInputModel model)
        {
            var modelToUpdate = _dbSet.Include(s => s.DyeingPrintingAreaInputProductionOrders).FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetArea(model.Area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBonNo(model.BonNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            modelToUpdate.SetGroup(model.Group, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTotalAvalQuantity(model.TotalAvalQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTotalAvalWeight(model.TotalAvalWeight, _identityProvider.Username, UserAgent);

            foreach (var item in modelToUpdate.DyeingPrintingAreaInputProductionOrders)
            {
                var localItem = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                }
                else
                {
                    item.SetArea(localItem.Area, _identityProvider.Username, UserAgent);
                    item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);
                    item.SetBuyer(localItem.BuyerId, localItem.Buyer, _identityProvider.Username, UserAgent);
                    item.SetCartNo(localItem.CartNo, _identityProvider.Username, UserAgent);
                    item.SetColor(localItem.Color, _identityProvider.Username, UserAgent);
                    item.SetConstruction(localItem.Construction, _identityProvider.Username, UserAgent);
                    item.SetGrade(localItem.Grade, _identityProvider.Username, UserAgent);
                    item.SetHasOutputDocument(localItem.HasOutputDocument, _identityProvider.Username, UserAgent);
                    item.SetMotif(localItem.Motif, _identityProvider.Username, UserAgent);
                    item.SetProductionOrder(localItem.ProductionOrderId, localItem.ProductionOrderNo, localItem.ProductionOrderType, localItem.ProductionOrderOrderQuantity, _identityProvider.Username, UserAgent);
                    item.SetRemark(localItem.Remark, _identityProvider.Username, UserAgent);
                    item.SetStatus(localItem.Status, _identityProvider.Username, UserAgent);
                    item.SetUnit(localItem.Unit, _identityProvider.Username, UserAgent);
                    item.SetUomUnit(localItem.UomUnit, _identityProvider.Username, UserAgent);
                    item.SetIsChecked(localItem.IsChecked, _identityProvider.Username, UserAgent);
                    item.SetPackingInstruction(localItem.PackingInstruction, _identityProvider.Username, UserAgent);
                    item.SetDeliveryOrderSales(localItem.DeliveryOrderSalesId, localItem.DeliveryOrderSalesNo, _identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                modelToUpdate.DyeingPrintingAreaInputProductionOrders.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAvalTransformationArea(int id, DyeingPrintingAreaInputModel model, DyeingPrintingAreaInputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);
            dbModel.SetIsTransformedAval(model.IsTransformedAval, _identityProvider.Username, UserAgent);
            dbModel.SetTotalAvalQuantity(model.TotalAvalQuantity, _identityProvider.Username, UserAgent);
            dbModel.SetTotalAvalWeight(model.TotalAvalWeight, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument))
            {
                var localItem = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                    result += await _SPPRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaOutputProductionOrderId, false);
                }
                else
                {
                    item.SetAvalQuantity(localItem.AvalQuantity, _identityProvider.Username, UserAgent);
                    item.SetAvalQuantityKG(localItem.AvalQuantityKg, _identityProvider.Username, UserAgent);
                }
            }

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public Task<int> UpdateIMArea(int id, DyeingPrintingAreaInputModel model, DyeingPrintingAreaInputModel modelToUpdate)
        {
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            modelToUpdate.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in modelToUpdate.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument))
            {
                var localItem = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);

                }
                else
                {
                    item.SetCartNo(localItem.CartNo, _identityProvider.Username, UserAgent);
                    var diffBalance = localItem.Balance - item.Balance;

                    var newBalanceRemains = item.BalanceRemains + diffBalance;
                    var newBalance = item.Balance + diffBalance;
                    if (newBalanceRemains <= 0)
                    {
                        item.SetHasOutputDocument(true, _identityProvider.Username, UserAgent);
                    }
                    else
                    {
                        item.SetHasOutputDocument(false, _identityProvider.Username, UserAgent);
                    }
                    item.SetBalanceRemains(newBalanceRemains, _identityProvider.Username, UserAgent);
                    item.SetBalance(newBalance, _identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                modelToUpdate.DyeingPrintingAreaInputProductionOrders.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateShippingArea(int id, DyeingPrintingAreaInputModel model, DyeingPrintingAreaInputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument))
            {
                var localItem = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                    result += await _outputSPPRepository.UpdateFromInputNextAreaFlagAsync(item.DyeingPrintingAreaOutputProductionOrderId, false);
                    var previousOutputData = await _outputSPPRepository.ReadByIdAsync(item.DyeingPrintingAreaOutputProductionOrderId);
                    result += await _SPPRepository.UpdateFromNextAreaInputAsync(previousOutputData.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }
            }

            //foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(s => s.Id == 0))
            //{
            //    item.FlagForCreate(_identityProvider.Username, UserAgent);
            //    dbModel.DyeingPrintingAreaInputProductionOrders.Add(item);
            //    result += await _outputSPPRepository.UpdateFromInputNextAreaFlagAsync(item.DyeingPrintingAreaOutputProductionOrderId, true);
            //    var previousOutputData = await _outputSPPRepository.ReadByIdAsync(item.DyeingPrintingAreaOutputProductionOrderId);
            //    result += await _SPPRepository.UpdateFromNextAreaInputAsync(previousOutputData.DyeingPrintingAreaInputProductionOrderId, item.Balance);
            //}

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateTransitArea(int id, DyeingPrintingAreaInputModel model, DyeingPrintingAreaInputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument))
            {
                var localItem = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                    result += await _outputSPPRepository.UpdateFromInputNextAreaFlagAsync(item.DyeingPrintingAreaOutputProductionOrderId, false);
                    var previousOutputData = await _outputSPPRepository.ReadByIdAsync(item.DyeingPrintingAreaOutputProductionOrderId);
                    result += await _SPPRepository.UpdateFromNextAreaInputAsync(previousOutputData.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }
            }

            //foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(s => s.Id == 0))
            //{
            //    item.FlagForCreate(_identityProvider.Username, UserAgent);
            //    dbModel.DyeingPrintingAreaInputProductionOrders.Add(item);
            //    result += await _outputSPPRepository.UpdateFromInputNextAreaFlagAsync(item.DyeingPrintingAreaOutputProductionOrderId, true);
            //    var previousOutputData = await _outputSPPRepository.ReadByIdAsync(item.DyeingPrintingAreaOutputProductionOrderId);
            //    result += await _SPPRepository.UpdateFromNextAreaInputAsync(previousOutputData.DyeingPrintingAreaInputProductionOrderId, item.Balance);
            //}

            result += await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
