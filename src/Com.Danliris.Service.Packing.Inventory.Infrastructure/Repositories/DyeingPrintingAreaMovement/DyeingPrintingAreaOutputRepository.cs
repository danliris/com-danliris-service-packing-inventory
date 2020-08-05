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
    public class DyeingPrintingAreaOutputRepository : IDyeingPrintingAreaOutputRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DyeingPrintingAreaOutputModel> _dbSet;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";
        private const string PENJUALAN = "PENJUALAN";
        private const string BUYER = "BUYER";
        private const string PRODUKSI = "PRODUKSI";

        public DyeingPrintingAreaOutputRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DyeingPrintingAreaOutputModel>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }


        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.Include(s => s.DyeingPrintingAreaOutputProductionOrders).FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAdjustment(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteIMArea(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);

                if (model.DestinationArea == PRODUKSI)
                {
                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }
                else
                {
                    result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }

            }

            //model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteShippingArea(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);

                if (model.DestinationArea != BUYER)
                {
                    if (model.DestinationArea == INSPECTIONMATERIAL)
                    {

                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                    }
                    else
                    {

                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }

                }
                else
                {
                    result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, false);
                    var prevData = await _outputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                    if (prevData != null)
                    {
                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(prevData.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                    }
                }

            }

            //model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteTransitArea(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);

                if (model.DestinationArea == INSPECTIONMATERIAL)
                {

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }
                else
                {

                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }

            }

            //model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeletePackingArea(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);

                if (model.DestinationArea == INSPECTIONMATERIAL)
                {

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }
                else
                {

                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }

            }

            //model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteWarehouseArea(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);

                if (model.DestinationArea == INSPECTIONMATERIAL)
                {

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                }
                else
                {

                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                }
            }

            //model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);

            result += await _dbContext.SaveChangesAsync();
            return result;
        }

        public IQueryable<DyeingPrintingAreaOutputModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(DyeingPrintingAreaOutputModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaOutputModel> ReadAll()
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaOutputProductionOrders).AsNoTracking();
        }

        public IQueryable<DyeingPrintingAreaOutputModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaOutputProductionOrders).IgnoreQueryFilters().AsNoTracking();
        }

        public Task<DyeingPrintingAreaOutputModel> ReadByIdAsync(int id)
        {
            return _dbSet.Include(s => s.DyeingPrintingAreaOutputProductionOrders).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingAreaOutputModel model)
        {
            var modelToUpdate = _dbSet.Include(s => s.DyeingPrintingAreaOutputProductionOrders).FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetArea(model.Area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBonNo(model.BonNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDestinationArea(model.DestinationArea, _identityProvider.Username, UserAgent);
            modelToUpdate.SetHasNextAreaDocument(model.HasNextAreaDocument, _identityProvider.Username, UserAgent);
            modelToUpdate.SetGroup(model.Group, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDeliveryOrderSales(model.DeliveryOrderSalesId, model.DeliveryOrderSalesNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetHasSalesInvoice(model.HasSalesInvoice, _identityProvider.Username, UserAgent);
            foreach (var item in modelToUpdate.DyeingPrintingAreaOutputProductionOrders)
            {
                var localItem = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                }
                else
                {
                    item.SetArea(localItem.Area, _identityProvider.Username, UserAgent);
                    item.SetDestinationArea(localItem.DestinationArea, _identityProvider.Username, UserAgent);
                    item.SetHasNextAreaDocument(localItem.HasNextAreaDocument, _identityProvider.Username, UserAgent);
                    item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);
                    item.SetBuyer(localItem.BuyerId, localItem.Buyer, _identityProvider.Username, UserAgent);
                    item.SetCartNo(localItem.CartNo, _identityProvider.Username, UserAgent);
                    item.SetColor(localItem.Color, _identityProvider.Username, UserAgent);
                    item.SetConstruction(localItem.Construction, _identityProvider.Username, UserAgent);
                    item.SetGrade(localItem.Grade, _identityProvider.Username, UserAgent);
                    item.SetMotif(localItem.Motif, _identityProvider.Username, UserAgent);
                    item.SetProductionOrder(localItem.ProductionOrderId, localItem.ProductionOrderNo, localItem.ProductionOrderType, localItem.ProductionOrderOrderQuantity, _identityProvider.Username, UserAgent);
                    item.SetRemark(localItem.Remark, _identityProvider.Username, UserAgent);
                    item.SetPackingInstruction(localItem.PackingInstruction, _identityProvider.Username, UserAgent);
                    item.SetStatus(localItem.Status, _identityProvider.Username, UserAgent);
                    item.SetUnit(localItem.Unit, _identityProvider.Username, UserAgent);
                    item.SetUomUnit(localItem.UomUnit, _identityProvider.Username, UserAgent);
                    item.SetDeliveryOrderSales(localItem.DeliveryOrderSalesId, localItem.DeliveryOrderSalesNo, _identityProvider.Username, UserAgent);
                    item.SetAvalType(localItem.AvalType, _identityProvider.Username, UserAgent);
                    //item.SetAvalALength(localItem.AvalALength, _identityProvider.Username, UserAgent);
                    //item.SetAvalBLength(localItem.AvalBLength, _identityProvider.Username, UserAgent);
                    //item.SetAvalConnectionLength(localItem.AvalConnectionLength, _identityProvider.Username, UserAgent);
                    item.SetHasSalesInvoice(localItem.HasSalesInvoice, _identityProvider.Username, UserAgent);
                    item.SetShippingGrade(localItem.ShippingGrade, _identityProvider.Username, UserAgent);
                    item.SetShippingRemark(localItem.ShippingRemark, _identityProvider.Username, UserAgent);
                    item.SetWeight(localItem.Weight, _identityProvider.Username, UserAgent);
                    item.SetMaterial(localItem.MaterialId, localItem.MaterialName, _identityProvider.Username, UserAgent);
                    item.SetMaterialConstruction(localItem.MaterialConstructionId, localItem.MaterialConstructionName, _identityProvider.Username, UserAgent);
                    item.SetMaterialWidth(localItem.MaterialWidth, _identityProvider.Username, UserAgent);
                    item.SetAdjDocumentNo(localItem.AdjDocumentNo, _identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                modelToUpdate.DyeingPrintingAreaOutputProductionOrders.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromInputAsync(int id, bool hasNextAreaDocument)
        {
            var modelToUpdate = _dbSet.Include(s => s.DyeingPrintingAreaOutputProductionOrders).FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);

            foreach (var item in modelToUpdate.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateFromInputAsync(int id, bool hasNextAreaDocument, List<int> idSpp)
        {
            var resultUpdate = 0;
            var modelToUpdate = _dbSet.Include(s => s.DyeingPrintingAreaOutputProductionOrders).FirstOrDefault(s => s.Id == id);
            var sppUpdated = modelToUpdate.DyeingPrintingAreaOutputProductionOrders.Where(t => idSpp.Contains(t.Id)).ToList();
            if (modelToUpdate.DyeingPrintingAreaOutputProductionOrders.Count() == sppUpdated.Count())
                modelToUpdate.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);

            //modelToUpdate.DyeingPrintingAreaOutputProductionOrders = sppUpdated;
            foreach (var item in sppUpdated)
            {
                item.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);
                resultUpdate = await _outputProductionOrderRepository.UpdateAsync(item.Id, item);
            }

            return resultUpdate;
        }

        public Task<int> UpdateFromInputNextAreaFlagParentOnlyAsync(int id, bool hasNextAreaDocument)
        {
            var modelToUpdate = _dbSet.Where(s => s.Id == id).FirstOrDefault();
            modelToUpdate.SetHasNextAreaDocument(hasNextAreaDocument, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateHasSalesInvoice(int id, bool hasSalesInvoice)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetHasSalesInvoice(hasSalesInvoice, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateIMArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                var localItem = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                    if (model.DestinationArea == PRODUKSI)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }

                }
                else
                {
                    var diffBalance = item.Balance - localItem.Balance;
                    if (model.DestinationArea == PRODUKSI)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                    }
                    item.SetGrade(localItem.Grade, _identityProvider.Username, UserAgent);
                    item.SetRemark(localItem.Remark, _identityProvider.Username, UserAgent);
                    item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);
                    item.SetAvalType(localItem.AvalType, _identityProvider.Username, UserAgent);
                    item.SetMachine(localItem.Machine, _identityProvider.Username, UserAgent);

                    item.SetProductSKUId(localItem.ProductSKUId, _identityProvider.Username, UserAgent);
                    item.SetFabricSKUId(localItem.FabricSKUId, _identityProvider.Username, UserAgent);
                    item.SetProductSKUCode(localItem.ProductSKUCode, _identityProvider.Username, UserAgent);
                    item.SetHasPrintingProductSKU(localItem.HasPrintingProductSKU, _identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);

                dbModel.DyeingPrintingAreaOutputProductionOrders.Add(item);

                if (model.DestinationArea == PRODUKSI)
                {
                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
                else
                {
                    result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
            }

            result += await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> UpdatePackingArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                var localItem = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                    if (model.DestinationArea == INSPECTIONMATERIAL)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }

                }
                else
                {
                    var diffBalance = item.Balance - localItem.Balance;

                    if (model.DestinationArea == INSPECTIONMATERIAL)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                    }
                    item.SetPackagingType(localItem.PackagingType, _identityProvider.Username, UserAgent);
                    item.SetPackagingQty(localItem.PackagingQty, _identityProvider.Username, UserAgent);
                    item.SetPackagingUnit(localItem.PackagingUnit, _identityProvider.Username, UserAgent);
                    item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);
                    item.SetDescription(localItem.Description, _identityProvider.Username, UserAgent);

                    item.SetProductPackingId(localItem.ProductPackingId, _identityProvider.Username, UserAgent);
                    item.SetFabricPackingId(localItem.FabricPackingId, _identityProvider.Username, UserAgent);
                    item.SetProductPackingCode(localItem.ProductPackingCode, _identityProvider.Username, UserAgent);
                    item.SetHasPrintingProductPacking(localItem.HasPrintingProductPacking, _identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);

                dbModel.DyeingPrintingAreaOutputProductionOrders.Add(item);

                if (model.DestinationArea == INSPECTIONMATERIAL)
                {
                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
                else
                {
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
            }

            result += await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> UpdateTransitArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                var localItem = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);

                    if (model.DestinationArea == INSPECTIONMATERIAL)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }

                }
                else
                {
                    var diffBalance = item.Balance - localItem.Balance;

                    if (model.DestinationArea == INSPECTIONMATERIAL)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                    }
                    item.SetGrade(localItem.Grade, _identityProvider.Username, UserAgent);
                    item.SetRemark(localItem.Remark, _identityProvider.Username, UserAgent);
                    item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);

                }
            }

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);

                dbModel.DyeingPrintingAreaOutputProductionOrders.Add(item);

                if (model.DestinationArea == INSPECTIONMATERIAL)
                {
                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
                else
                {
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
            }

            result += await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> UpdateShippingArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                var localItem = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);

                    if (model.DestinationArea != BUYER)
                    {
                        if (model.DestinationArea == INSPECTIONMATERIAL)
                        {
                            result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                        }
                        else
                        {
                            result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                        }
                    }
                    else
                    {
                        result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, false);
                        var prevData = await _outputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                        if (prevData != null)
                        {
                            result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(prevData.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                        }
                    }

                }
                else
                {
                    if (model.DestinationArea != BUYER)
                    {
                        var diffBalance = item.Balance - localItem.Balance;
                        var diffQtyPackking = item.PackagingQty - localItem.PackagingQty;

                        if (model.DestinationArea == INSPECTIONMATERIAL)
                        {
                            result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1, diffQtyPackking * -1);
                        }
                        else
                        {
                            result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                        }
                    }
                    item.SetPackagingQty(localItem.PackagingQty, _identityProvider.Username, UserAgent);
                    item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);
                    item.SetShippingGrade(localItem.ShippingGrade, _identityProvider.Username, UserAgent);
                    item.SetShippingRemark(localItem.ShippingRemark, _identityProvider.Username, UserAgent);
                    item.SetWeight(localItem.Weight, _identityProvider.Username, UserAgent);
                    item.SetDeliveryNote(localItem.DeliveryNote, _identityProvider.Username, UserAgent);
                }
            }

            result += await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> UpdateAdjustmentData(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var localItem = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
                else
                {
                    var diffBalance = item.Balance - localItem.Balance;
                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance);
                    
                    item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);
                    item.SetAdjDocumentNo(localItem.AdjDocumentNo, _identityProvider.Username, UserAgent);
                    item.SetBuyer(localItem.BuyerId, localItem.Buyer, _identityProvider.Username, UserAgent);
                    item.SetCartNo(localItem.CartNo, _identityProvider.Username, UserAgent);
                    item.SetColor(localItem.Color, _identityProvider.Username, UserAgent);
                    item.SetConstruction(localItem.Construction, _identityProvider.Username, UserAgent);
                    item.SetMotif(localItem.Motif, _identityProvider.Username, UserAgent);
                    item.SetProductionOrder(localItem.ProductionOrderId, localItem.ProductionOrderNo, localItem.ProductionOrderType, localItem.ProductionOrderOrderQuantity, _identityProvider.Username, UserAgent);
                    item.SetRemark(localItem.Remark, _identityProvider.Username, UserAgent);
                    item.SetUnit(localItem.Unit, _identityProvider.Username, UserAgent);
                    item.SetUomUnit(localItem.UomUnit, _identityProvider.Username, UserAgent);
                    item.SetMaterial(localItem.MaterialId, localItem.MaterialName, _identityProvider.Username, UserAgent);
                    item.SetMaterialConstruction(localItem.MaterialConstructionId, localItem.MaterialConstructionName, _identityProvider.Username, UserAgent);
                    item.SetMaterialWidth(localItem.MaterialWidth, _identityProvider.Username, UserAgent);
                    item.SetGrade(localItem.Grade, _identityProvider.Username, UserAgent);
                    item.SetPackagingQty(localItem.PackagingQty, _identityProvider.Username, UserAgent);
                    item.SetPackagingUnit(localItem.PackagingUnit, _identityProvider.Username, UserAgent);

                }
            }

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                dbModel.DyeingPrintingAreaOutputProductionOrders.Add(item);
                result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                

            }
            return result;

        }

        public async Task<int> UpdateWarehouseArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel)
        {
            int result = 0;
            dbModel.SetDate(model.Date, _identityProvider.Username, UserAgent);
            dbModel.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            dbModel.SetGroup(model.Group, _identityProvider.Username, UserAgent);

            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument))
            {
                var localItem = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);

                if (localItem == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);
                    if (model.DestinationArea == INSPECTIONMATERIAL)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }

                }
                else
                {
                    var diffBalance = item.Balance - localItem.Balance;

                    if (model.DestinationArea == INSPECTIONMATERIAL)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, diffBalance * -1);
                    }
                    item.SetDeliveryOrderSales(localItem.DeliveryOrderSalesId, localItem.DeliveryOrderSalesNo, _identityProvider.Username, UserAgent);
                    item.SetPackagingQty(localItem.PackagingQty, _identityProvider.Username, UserAgent);
                    item.SetPackagingUnit(localItem.PackagingUnit, _identityProvider.Username, UserAgent);
                    item.SetBalance(localItem.Balance, _identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);

                dbModel.DyeingPrintingAreaOutputProductionOrders.Add(item);

                if (model.DestinationArea == INSPECTIONMATERIAL)
                {
                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
                else
                {
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                }
            }

            result += await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
