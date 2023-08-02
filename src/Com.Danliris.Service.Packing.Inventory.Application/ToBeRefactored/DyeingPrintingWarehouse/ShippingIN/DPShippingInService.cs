using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingWarehouse;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse.DPShipping;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System.IO;
using System.Data;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN
{
    public class DPShippingInService : IDPShippingInService
    {
        private readonly IDPWarehouseOutputRepository _dPWarehouseOutputRepository;
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DPWarehouseOutputItemModel> _dbSetOutputWarhouseItem;
        private readonly DbSet<DPShippingInputModel> _dbSet;
        private readonly DbSet<DPShippingInputItemModel> _dbSetItem;
        private readonly DbSet<DPShippingMovementModel> _dbSetShippingMovement;
        private readonly IIdentityProvider _identityProvider;
        private const string UserAgent = "Repository";
        public DPShippingInService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dPWarehouseOutputRepository = serviceProvider.GetService<IDPWarehouseOutputRepository>();
            _dbSetOutputWarhouseItem = dbContext.Set<DPWarehouseOutputItemModel>();
            _dbSet = dbContext.Set<DPShippingInputModel>();
            _dbSetItem = dbContext.Set<DPShippingInputItemModel>();
            _dbSetShippingMovement = dbContext.Set<DPShippingMovementModel>();
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public List<PreInputShippingViewModel> GetOutputPreShippingProductionOrders(long deliveriOrderSalesId)
        {
            IQueryable<DPWarehouseOutputItemModel> outputWarehouseItem;

            if (deliveriOrderSalesId == 0)
            {
                outputWarehouseItem = _dbSetOutputWarhouseItem.AsNoTracking().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.DestinationArea == DyeingPrintingArea.SHIPPING && !s.HasNextAreaDocument).Take(100);
            }
            else {
                outputWarehouseItem = _dbSetOutputWarhouseItem.AsNoTracking().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.DestinationArea == DyeingPrintingArea.SHIPPING && !s.HasNextAreaDocument && s.DeliveryOrderSalesId == deliveriOrderSalesId).Take(100);
            }

            var data = outputWarehouseItem.Select(s => new PreInputShippingViewModel()
            {
                Id = s.Id,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo,
                    Name = s.DestinationBuyerName
                },
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = s.ProductionOrderOrderQuantity,
                    Type = s.ProductionOrderType
                },
                MaterialWidth = s.MaterialWidth,
                MaterialOrigin = s.MaterialOrigin,
                FinishWidth = s.FinishWidth,
                Material = new Material()
                {
                    Id = s.MaterialId,
                    Name = s.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Name = s.MaterialConstructionName,
                    Id = s.MaterialConstructionId
                },
                ProcessType = new CommonViewModelObjectProperties.ProcessType()
                {
                    Id = s.ProcessTypeId,
                    Name = s.ProcessTypeName
                },
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = s.YarnMaterialId,
                    Name = s.YarnMaterialName
                },
                ProductTextile = new ProductTextile()
                {
                    Id = s.ProductTextileId,
                    Code = s.ProductTextileCode,
                    Name = s.ProductTextileName
                },
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Construction = s.Construction,
                Color = s.Color,
                Motif = s.Motif,
                Unit = s.Unit,
                Area = s.Area,
                PackingType = s.PackagingType,
                Remark = s.Remark,
                Grade = s.Grade,
                PackagingQty = s.PackagingQty,
                Balance = s.Balance,
                PackingLength = s.PackagingLength,
                UomUnit = s.UomUnit,
                DPWarhouseInputItemId = s.DPWarehouseOutputId,
                ProductSKUId = s.ProductSKUId,
                FabricSKUId = s.FabricSKUId,
                ProductSKUCode = s.ProductSKUCode,
                ProductPackingId = s.ProductPackingId,
                FabricPackingId = s.FabricPackingId,
                ProductPackingCode = s.ProductPackingCode,
                DeliveryOrderSalesType = s.DeliveryOrderSalesType,
                PackagingUnit = s.PackagingUnit,
                PackingInstruction = s.PackingInstruction,
                DestinationBuyerName = s.DestinationBuyerName
                



            });

            return data.ToList();
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _dbSet.AsNoTracking().Where(x => x.ShippingType == DyeingPrintingArea.GUDANGJADI && !x.IsDeleted);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };
            query = QueryHelper<DPShippingInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DPShippingInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DPShippingInputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
               
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                ShippingType = s.ShippingType,
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<int> Create(InputShippingViewModel viewModel)
        {
            int Created = 0;
            using (var transaction = this._dbContext.Database.BeginTransaction()) {

                try
                {
                    var model = _dbSet.AsNoTracking().FirstOrDefault(s => s.Date.Date == viewModel.Date.Date && s.ShippingType == "GUDANG JADI");
                    var listItem = new List<DPShippingInputItemModel>();

                    if (model == null)
                    {
                        int totalCurrentYearData = _dbSet.AsNoTracking().Count(s => s.CreatedUtc.Year == viewModel.Date.Year);
                        string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);

                        model = new DPShippingInputModel(
                                viewModel.Date, 
                                DyeingPrintingArea.GUDANGJADI,
                                "DAILY SHIFT",
                                bonNo,
                                viewModel.ShippingProductionOrders.Select( s => new DPShippingInputItemModel(
                                    (int)s.ProductionOrder.Id,
                                    s.ProductionOrder.No,
                                    s.Material.Id,
                                    s.Material.Name,
                                    s.MaterialConstruction.Id,
                                    s.MaterialConstruction.Name,
                                    s.MaterialWidth,
                                    s.BuyerId,
                                    s.Buyer,
                                    s.Construction,
                                    s.Unit,
                                    s.Color,
                                    s.Motif,
                                    s.UomUnit,
                                    s.Balance,
                                    s.Balance,
                                    s.PackingInstruction,
                                    s.ProductionOrder.Type,
                                    s.ProductionOrder.OrderQuantity.ToString(),
                                    s.CreatedUtcOrderNo,
                                    s.Remark,
                                    s.Grade,
                                    s.Description,
                                    (int)s.DeliveryOrder.Id,
                                    s.DeliveryOrder.No,
                                    s.PackagingUnit,
                                    s.PackingType,
                                    s.PackagingQty,
                                    s.PackingLength,
                                    DyeingPrintingArea.GUDANGJADI,
                                    s.Id,
                                    s.ProductSKUId,
                                    s.FabricSKUId,
                                    s.ProductSKUCode,
                                    s.ProductPackingId,
                                    s.FabricPackingId,
                                    s.ProductPackingCode,
                                    s.ProcessType.Id,
                                    s.ProcessType.Name,
                                    s.YarnMaterial.Id,
                                    s.YarnMaterial.Name,
                                    (double)s.PackagingQty,
                                    s.Balance,
                                    s.FinishWidth,
                                    s.DestinationBuyerName,
                                    s.MaterialOrigin,
                                    s.DeliveryOrderSalesType


                                    )).ToList());

                        model.FlagForCreate(_identityProvider.Username, UserAgent);

                        _dbSet.Add(model);

                        foreach (var item in model.DPShippingInputItems)
                        {
                            item.FlagForCreate(_identityProvider.Username, UserAgent);
                        }

                        foreach (var s in viewModel.ShippingProductionOrders)
                        {
                            var warehouseItem = _dbSetOutputWarhouseItem.FirstOrDefault(x => x.Id == s.Id);

                            warehouseItem.HasNextAreaDocument = true;
                            warehouseItem.NextAreaInputStatus = "DITERIMA";
                            EntityExtension.FlagForUpdate(warehouseItem, _identityProvider.Username, UserAgent);

                        }

                        Created = await _dbContext.SaveChangesAsync();

                        await createMovement(model.DPShippingInputItems.ToList());
                    }
                    else {

                        
                        foreach (var s in viewModel.ShippingProductionOrders)
                        {
                            var modelItem = new DPShippingInputItemModel(
                                    (int)s.ProductionOrder.Id,
                                    s.ProductionOrder.No,
                                    s.Material.Id,
                                    s.Material.Name,
                                    s.MaterialConstruction.Id,
                                    s.MaterialConstruction.Name,
                                    s.MaterialWidth,
                                    s.BuyerId,
                                    s.Buyer,
                                    s.Construction,
                                    s.Unit,
                                    s.Color,
                                    s.Motif,
                                    s.UomUnit,
                                    s.Balance,
                                    s.Balance,
                                    s.PackingInstruction,
                                    s.ProductionOrder.Type,
                                    s.ProductionOrder.OrderQuantity.ToString(),
                                    s.CreatedUtcOrderNo,
                                    s.Remark,
                                    s.Grade,
                                    s.Description,
                                    (int)s.DeliveryOrder.Id,
                                    s.DeliveryOrder.No,
                                    s.PackagingUnit,
                                    s.PackingType,
                                    s.PackagingQty,
                                    s.PackingLength,
                                    DyeingPrintingArea.GUDANGJADI,
                                    model.Id,
                                    s.ProductSKUId,
                                    s.FabricSKUId,
                                    s.ProductSKUCode,
                                    s.ProductPackingId,
                                    s.FabricPackingId,
                                    s.ProductPackingCode,
                                    s.ProcessType.Id,
                                    s.ProcessType.Name,
                                    s.YarnMaterial.Id,
                                    s.YarnMaterial.Name,
                                    (double)s.PackagingQty,
                                    s.Balance,
                                    s.FinishWidth,
                                    s.DestinationBuyerName,
                                    s.MaterialOrigin,
                                    s.DeliveryOrderSalesType
                                );

                            modelItem.FlagForCreate(_identityProvider.Username, UserAgent);
                            listItem.Add(modelItem);
                            _dbSetItem.Add(modelItem);

                            var warehouseItem = _dbSetOutputWarhouseItem.FirstOrDefault(x => x.Id == s.Id);

                            warehouseItem.HasNextAreaDocument = true;
                            warehouseItem.NextAreaInputStatus = "DITERIMA";
                            EntityExtension.FlagForUpdate(warehouseItem, _identityProvider.Username, UserAgent);
                        }
                        Created = await _dbContext.SaveChangesAsync();
                        await createMovement(listItem);

                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

               

            

            return Created;
        }

        private async Task<int> createMovement(List<DPShippingInputItemModel> modelItem)
        {
            int count = 0;
           
            foreach (var item in modelItem)
            {
                var movement = new DPShippingMovementModel(
                        item.CreatedUtc.Date,
                        DyeingPrintingArea.SHIPPING,
                        DyeingPrintingArea.IN,
                        item.Id,
                        0,
                        item.DPShippingInputId,
                        item.ProductionOrderId,
                        item.ProductionOrderNo,
                        item.BuyerId,
                        item.BuyerName,
                        item.Construction,
                        item.Unit,
                        item.Color,
                        item.Motif,
                        item.UomUnit,
                        item.Balance,
                        item.Grade,
                        item.ProductionOrderType,
                        item.Remark,
                        item.Description,
                        item.PackagingType,
                        item.PackagingLength,
                        item.PackagingQty, 
                        item.PackagingUnit,
                        item.MaterialOrigin,
                        item.ProductPackingCode,
                        item.ProductPackingId,
                        0,
                        "",
                        ""
                    );
                movement.FlagForCreate(_identityProvider.Username, UserAgent);

                _dbSetShippingMovement.Add(movement);

            }

            count = await _dbContext.SaveChangesAsync();
            return count;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            

                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            

        }

        public async Task<InputShippingViewModel> ReadById(int id)
        {
            var model = await _dbSet.Include(s => s.DPShippingInputItems).FirstOrDefaultAsync(s => s.Id == id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        private InputShippingViewModel MapToViewModel(DPShippingInputModel model)
        {
            var vm = new InputShippingViewModel()
            {
                Active = model.Active,
                Id = model.Id,
              
                BonNo = model.BonNo,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
               
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Shift = model.Shift,
                ShippingType = model.ShippingType,
                ShippingProductionOrders = model.DPShippingInputItems.Select( s => new InputShippingItemViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Buyer = s.BuyerName,
                    BuyerId = s.BuyerId,
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Remark = s.Remark,
                    PackingInstruction = s.PackingInstruction,
                    
                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    PackagingUnit = s.PackagingUnit,
                    Motif = s.Motif,
                    Grade = s.Grade,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = s.DeliveryOrderSalesId,
                        No = s.DeliveryOrderSalesNo
                    },
                    PackagingQty = s.PackagingQty,
                    PackingType = s.PackagingType,
                    Balance = s.Balance,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = Convert.ToDouble(s.ProductionOrderOrderQuantity),
                        Type = s.ProductionOrderType
                    },
                    DeliveryOrderRetur = new DeliveryOrderRetur()
                    {
                        Id = s.DeliveryOrderReturId,
                        No = s.DeliveryOrderReturNo
                    },
                    MaterialWidth = s.MaterialWidth,
                    MaterialOrigin = s.MaterialOrigin,
                    FinishWidth = s.FinishWidth,
                    Material = new Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = s.MaterialConstructionName,
                        Id = s.MaterialConstructionId
                    },
                    PackingLength = s.PackagingLength,
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },
                    
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                  
                }
                    
                    ).ToList()
            };

            return vm;
        }
        public async Task<InputShippingViewModel> ReadByIdBon(int id)
        {
            var model = await _dbSet.Include(s => s.DPShippingInputItems).FirstOrDefaultAsync(s => s.Id == id);
            if (model == null)
                return null;

            var vm = MapToViewModelBon(model);

            return vm;
        }
        private InputShippingViewModel MapToViewModelBon(DPShippingInputModel model)
        {
            var vm = new InputShippingViewModel()
            {
                Active = model.Active,
                Id = model.Id,

                BonNo = model.BonNo,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,

                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Shift = model.Shift,
                ShippingType = model.ShippingType,
                ShippingProductionOrders = model.DPShippingInputItems.GroupBy(r => new { r.ProductionOrderNo, r.Grade }).Select(s => new InputShippingItemViewModel()
                {
                    Active = s.First().Active,
                    LastModifiedUtc = s.First().LastModifiedUtc,
                    Buyer = s.First().BuyerName,
                    BuyerId = s.First().BuyerId,
                    Color = s.First().Color,
                    Construction = s.First().Construction,
                    CreatedAgent = s.First().CreatedAgent,
                    CreatedBy = s.First().CreatedBy,
                    CreatedUtc = s.First().CreatedUtc,
                    DeletedAgent = s.First().DeletedAgent,
                    DeletedBy = s.First().DeletedBy,
                    DeletedUtc = s.First().DeletedUtc,
                    Remark = s.First().Remark,
                    PackingInstruction = s.First().PackingInstruction,

                    Id = s.First().Id,
                    IsDeleted = s.First().IsDeleted,
                    LastModifiedAgent = s.First().LastModifiedAgent,
                    LastModifiedBy = s.First().LastModifiedBy,
                    PackagingUnit = s.First().PackagingUnit,
                    Motif = s.First().Motif,
                    Grade = s.Key.Grade,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = s.First().DeliveryOrderSalesId,
                        No = s.First().DeliveryOrderSalesNo
                    },
                    PackagingQty = s.First().PackagingQty,
                    PackingType = s.First().PackagingType,
                    Balance = s.First().Balance,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.First().ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        OrderQuantity = Convert.ToDouble(s.First().ProductionOrderOrderQuantity),
                        Type = s.First().ProductionOrderType
                    },
                    DeliveryOrderRetur = new DeliveryOrderRetur()
                    {
                        Id = s.First().DeliveryOrderReturId,
                        No = s.First().DeliveryOrderReturNo
                    },
                    MaterialWidth = s.First().MaterialWidth,
                    MaterialOrigin = s.First().MaterialOrigin,
                    FinishWidth = s.First().FinishWidth,
                    Material = new Material()
                    {
                        Id = s.First().MaterialId,
                        Name = s.First().MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = s.First().MaterialConstructionName,
                        Id = s.First().MaterialConstructionId
                    },
                    PackingLength = s.First().PackagingLength,
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = s.First().ProcessTypeId,
                        Name = s.First().ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.First().YarnMaterialId,
                        Name = s.First().YarnMaterialName
                    },

                    Unit = s.First().Unit,
                    UomUnit = s.First().UomUnit,
                    ProductSKUId = s.First().ProductSKUId,
                    FabricSKUId = s.First().FabricSKUId,
                    ProductSKUCode = s.First().ProductSKUCode,
                    ProductPackingId = s.First().ProductPackingId,
                    FabricPackingId = s.First().FabricPackingId,
                    ProductPackingCode = s.First().ProductPackingCode,

                }

                    ).ToList()
            };

            return vm;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, string type, int offSet)
        {
            //var query = _repository.ReadAll()
            //    .Where(s => s.Area == SHIPPING && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _dbSet.AsNoTracking()
               .Where(s => s.ShippingType == DyeingPrintingArea.GUDANGJADI);

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }

            query = query.OrderBy(x => x.BonNo);

            var modelAll = query.Select(s => new
            {

                SppList = s.DPShippingInputItems.Select(d => new
                {
                    s.BonNo,
                    d.DeliveryOrderSalesNo,
                    d.ProductionOrderId,
                    d.ProductionOrderNo,
                    s.Date,
                    d.ProductionOrderOrderQuantity,
                    d.Construction,
                    d.MaterialOrigin,
                    d.Unit,
                    d.BuyerName,
                    d.Color,
                    d.Motif,
                    d.Grade,
                    d.Remark,
                    d.InputPackagingQty,
                    d.PackagingUnit,
                    d.InputQuantity,
                    d.UomUnit,
                    d.ProductPackingCode
                })
            });

            if (type == "BON")
            {
                modelAll = modelAll.Select(s => new
                {

                    SppList = s.SppList.GroupBy(r => new { r.ProductionOrderId, r.Grade }).Select(d => new
                    {
                        d.First().BonNo,
                        d.First().DeliveryOrderSalesNo,
                        d.First().ProductionOrderId,
                        d.First().ProductionOrderNo,
                        d.First().Date,
                        d.First().ProductionOrderOrderQuantity,
                        d.First().Construction,
                        d.First().MaterialOrigin,
                        d.First().Unit,
                        d.First().BuyerName,
                        d.First().Color,
                        d.First().Motif,
                        d.First().Grade,
                        d.First().Remark,
                        InputPackagingQty = d.Sum(x => x.InputPackagingQty),
                        d.First().PackagingUnit,
                        InputQuantity = d.Sum(x => x.InputQuantity),
                        d.First().UomUnit,
                        d.First().ProductPackingCode


                    })
                });
            }
            else
            {
                modelAll = modelAll.Select(s => new
                {

                    SppList = s.SppList.Select(d => new
                    {
                        d.BonNo,
                        d.DeliveryOrderSalesNo,
                        d.ProductionOrderId,
                        d.ProductionOrderNo,
                        d.Date,
                        d.ProductionOrderOrderQuantity,
                        d.Construction,
                        d.MaterialOrigin,
                        d.Unit,
                        d.BuyerName,
                        d.Color,
                        d.Motif,
                        d.Grade,
                        d.Remark,
                        d.InputPackagingQty,
                        d.PackagingUnit,
                        d.InputQuantity,
                        d.UomUnit,
                        d.ProductPackingCode


                    })
                });
            }



            var query1 = modelAll;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Delivery Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Barcode", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            //dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Asal Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Pack", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Pack", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });

            if (query1.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "",  "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query1)
                {
                    //foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(d => !d.HasOutputDocument).OrderBy(s => s.ProductionOrderNo))
                    foreach (var item in model.SppList.OrderBy(s => s.ProductionOrderNo))
                    {
                        var dateIn = item.Date.Equals(DateTimeOffset.MinValue) ? "" : item.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");

                        dt.Rows.Add(item.BonNo,
                                     item.DeliveryOrderSalesNo,
                                     item.ProductionOrderNo,
                                     item.ProductPackingCode,
                                     dateIn,
                                     item.ProductionOrderOrderQuantity,
                                     item.Construction,
                                     item.MaterialOrigin,
                                     item.Unit,
                                     item.BuyerName,
                                     item.Color,
                                     item.Motif,
                                     item.Grade,
                                     item.Remark,
                                     item.InputPackagingQty.ToString("N2", CultureInfo.InvariantCulture),
                                     item.PackagingUnit,
                                     item.InputQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                     item.UomUnit); ;
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Shipping") }, true);
        }

        public ListResult<PreInputShippingViewModel> GetDOLoader(int page, int size, string filter, string order, string keyword)
        {
            var query = _dbSetOutputWarhouseItem.AsNoTracking().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.DestinationArea == DyeingPrintingArea.SHIPPING && !s.HasNextAreaDocument);
            List<string> SearchAttributes = new List<string>()
            {
                "DeliveryOrderSalesNo"
            };

            query = QueryHelper<DPWarehouseOutputItemModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DPWarehouseOutputItemModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DPWarehouseOutputItemModel>.Order(query, OrderDictionary);
            var data = query
                .GroupBy(d => d.DeliveryOrderSalesId)
                .Select(s => s.First())
                .Skip((page - 1) * size).Take(size)
                .OrderBy(s => s.ProductionOrderNo)
                .Select(s => new PreInputShippingViewModel()
                {
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = s.DeliveryOrderSalesId,
                        No = s.DeliveryOrderSalesNo

                    }
                });

            return new ListResult<PreInputShippingViewModel>(data.ToList(), page, size, query.Count());
        }
    }
}
