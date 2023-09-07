using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.PreOutputWarehouse;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.List;

using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using System.IO;
using System.Data;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN
{
    public class DPWarehouseInService : IDPWarehouseInService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private readonly IDPWarehousePreInputRepository _dPWarehousePreInputRepository;
        private readonly IDPWarehouseInputRepository _dPWarehouseInputRepository;
        private readonly IDPWarehouseSummaryRepository _dPWarehouseSummaryRepository;
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DPWarehouseInputModel> _dbSet;
        private readonly DbSet<DPWarehouseInputItemModel> _dbSetItems;
        private readonly DbSet<DPWarehouseSummaryModel> _dbSetSummary;
        private readonly DbSet<DPWarehouseMovementModel> _dbSetMovement;
        private readonly DbSet<DyeingPrintingAreaInputProductionOrderModel> _dbSetInputOrder;
        private readonly DbSet<DyeingPrintingAreaInputModel> _dbSetInput;
        private readonly DbSet<DyeingPrintingAreaMovementModel> _dbSetMovementOrder;
        private readonly IIdentityProvider _identityProvider;
        private const string UserAgent = "Repository";

        public DPWarehouseInService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbSet = dbContext.Set<DPWarehouseInputModel>();
            _dbSetSummary = dbContext.Set<DPWarehouseSummaryModel>();
            _dbSetMovement = dbContext.Set<DPWarehouseMovementModel>();
            _dbSetItems = dbContext.Set<DPWarehouseInputItemModel>();
            _dbSetInput = dbContext.Set<DyeingPrintingAreaInputModel>();
            _dbSetMovementOrder = dbContext.Set<DyeingPrintingAreaMovementModel>();
            _dbSetInputOrder = dbContext.Set<DyeingPrintingAreaInputProductionOrderModel>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _dPWarehousePreInputRepository = serviceProvider.GetService<IDPWarehousePreInputRepository>();
            _dPWarehouseInputRepository = serviceProvider.GetService<IDPWarehouseInputRepository>();
            _dPWarehouseSummaryRepository = serviceProvider.GetService<IDPWarehouseSummaryRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _dbContext = dbContext;
        }

        public List<OutputPreWarehouseItemListViewModel> PreInputWarehouse(string packingCode)
        {
            //List<OutputPreWarehouseItemListViewModel> queryResult;

            var queryResult = new List<OutputPreWarehouseItemListViewModel>();
            var query = _dPWarehousePreInputRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc).
                                                                   Where(s => s.ProductPackingCode.Contains(packingCode)
                                                                  );

            if (query != null)
            {
                queryResult = query.Select(p => new OutputPreWarehouseItemListViewModel()
                {
                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = p.ProductionOrderId,
                        No = p.ProductionOrderNo,
                        Type = p.ProductionOrderType,
                        OrderQuantity = p.ProductionOrderOrderQuantity,
                        CreatedUtc = p.CreatedUtcOrderNo
                    },
                    MaterialWidth = p.MaterialWidth,
                    MaterialOrigin = p.MaterialOrigin,
                    FinishWidth = p.FinishWidth,
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = p.MaterialConstructionId,
                        Name = p.MaterialConstructionName
                    },
                    MaterialProduct = new Material()
                    {
                        Id = p.MaterialId,
                        Name = p.MaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = p.ProcessTypeId,
                        Name = p.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = p.YarnMaterialId,
                        Name = p.YarnMaterialName
                    },
                    CartNo = p.CartNo,
                    Buyer = p.Buyer,
                    BuyerId = p.BuyerId,
                    Construction = p.Construction,
                    Unit = p.Unit,
                    Color = p.Color,
                    Motif = p.Motif,
                    UomUnit = p.UomUnit,
                    Remark = p.Remark,
                    Grade = p.Grade,
                    Balance = p.BalanceRemains,
                    InputQuantity = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQtyRemains,
                    PackagingLength = p.PackagingLength,
                    InputPackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    Description = p.Description,


                    Qty = p.PackagingLength,
                    ProductSKUId = p.ProductSKUId,
                    FabricSKUId = p.FabricSKUId,
                    ProductSKUCode = p.ProductSKUCode,

                    ProductPackingId = p.ProductPackingId,
                    FabricPackingId = p.FabricPackingId,
                    ProductPackingCode = p.ProductPackingCode,

                    PreviousOutputPackagingQty = p.PackagingQty,

                }).ToList();


            }
            //else { 

            //}
            return queryResult;
        }


        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI &&
            //                                             s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument && d.Balance > 0));
            var query = _dPWarehouseInputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);

            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DPWarehouseInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DPWarehouseInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DPWarehouseInputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                Group = s.Group,
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }


        public async Task<int> Create(DPInputWarehouseCreateViewModel viewModel)
        {
            int result = 0;

            var model = _dPWarehouseInputRepository.GetDbSet()
                //.Include(s => s.DyeingPrintingAreaInputProductionOrders)
                .FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                s.Date.AddHours(7).ToString("dd/MM/yyyy") == DateTime.Now.Date.AddHours(7).ToString("dd/MM/yyyy") &&
                s.Shift == "DAILY SHIFT" &&
                s.Group == "");

            //var dateData = viewModel.Date;

            var ids = _dPWarehouseInputRepository.GetDbSet().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI).Select(x => x.Id).ToList();

            if (model != null)
            {
                result = await UpdateExistingWarehouse(viewModel, model.Id, model.BonNo);
            }
            else
            {
                result = await InsertNewWarehouse(viewModel);
            }


            return result;


        }
        public async Task<int> Reject(DPInputWarehouseCreateViewModel viewModel)
        {
            int Created = 0;
            DyeingPrintingAreaInputModel modelBon;

            DateTime date = DateTime.Now.Date;
            DateTime combined = new DateTime(date.Year, date.Month, date.Day, 14, 00, 00);

            if (DateTime.Now.AddHours(7) < combined) {
                modelBon = _dbSetInput.FirstOrDefault(s => s.Area == DyeingPrintingArea.PACKING && s.Shift == "PAGI" && s.CreatedUtc.Date == DateTime.Now.Date);
            }
            else {
                modelBon = _dbSetInput.FirstOrDefault(s => s.Area == DyeingPrintingArea.PACKING && s.Shift == "SIANG" && s.CreatedUtc.Date == DateTime.Now.Date);
            }
            using (var transaction = this._dbContext.Database.BeginTransaction())
            {
                try
                {
                    var listPreInput = new List<DPWarehousePreInputModel>();
                    var listInput = new List<DyeingPrintingAreaInputProductionOrderModel>();
                    
                    foreach (var item in viewModel.DyeingPrintingWarehouseInItems)
                    {

                        var dyeingPrintingAreaInputProductionOrderModel = new DyeingPrintingAreaInputProductionOrderModel(
                                    item.ProductionOrder.Id,
                                    item.ProductionOrder.No,
                                    item.CartNo,
                                    item.Buyer,
                                    item.Construction,
                                    item.Unit,
                                    item.Color,
                                    item.Motif,
                                    item.UomUnit,
                                    item.Sendquantity * item.PackagingLength,
                                    false,
                                    "",
                                    item.ProductionOrder.Type,
                                    item.ProductionOrder.OrderQuantity,
                                    item.Remark,
                                    item.Grade,
                                    item.Status,
                                    item.PackagingUnit,
                                    item.PackagingType,
                                    (decimal)item.Sendquantity,
                                    DyeingPrintingArea.PACKING,
                                    item.Sendquantity * item.PackagingLength,
                                    0,
                                    item.BuyerId,
                                    item.MaterialProduct.Id,
                                    item.MaterialProduct.Name,
                                    item.MaterialConstruction.Id,
                                    item.MaterialConstruction.Name,
                                    item.MaterialWidth,
                                    item.ProcessType.Id,
                                    item.ProcessType.Name,
                                    item.YarnMaterial.Id,
                                    item.YarnMaterial.Name,
                                    item.ProductSKUId,
                                    item.FabricSKUId,
                                    item.ProductSKUCode,
                                    false,
                                    item.ProductPackingId,
                                    item.FabricPackingId,
                                    item.ProductPackingCode,
                                    false,
                                    item.PackagingLength,
                                    item.Sendquantity * item.PackagingLength,
                                    (decimal) item.Sendquantity,
                                    item.FinishWidth, 
                                    item.MaterialOrigin,
                                    "REJECT GJ",
                                    item.Id,
                                    modelBon.Id

                        );

                        dyeingPrintingAreaInputProductionOrderModel.FlagForCreate(_identityProvider.Username, UserAgent);

                        listInput.Add(dyeingPrintingAreaInputProductionOrderModel);

                        _dbSetInputOrder.Add(dyeingPrintingAreaInputProductionOrderModel);


                        var modelPreInput = _dPWarehousePreInputRepository.GetDbSet().FirstOrDefault(s => s.Id == item.Id);

                        modelPreInput.BalanceReject = modelPreInput.BalanceReject + (item.Sendquantity * item.PackagingLength);
                        modelPreInput.BalanceRemains = modelPreInput.BalanceRemains - (item.Sendquantity * item.PackagingLength);
                        modelPreInput.PackagingQtyReject = modelPreInput.PackagingQtyReject + (decimal)item.Sendquantity;
                        modelPreInput.PackagingQtyRemains = modelPreInput.PackagingQtyRemains - (decimal)item.Sendquantity;

                        listPreInput.Add(modelPreInput);

                        EntityExtension.FlagForUpdate(modelPreInput, _identityProvider.Username, UserAgent);



                       
                    }

                    Created = await _dbContext.SaveChangesAsync();

                  
                    await createMovementReject(modelBon, listPreInput, listInput);


                    transaction.Commit();


                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            
            }
                return Created;
        }

        private async Task<int>createMovementReject(DyeingPrintingAreaInputModel modelBon, List<DPWarehousePreInputModel> preInput, List<DyeingPrintingAreaInputProductionOrderModel> InputOrder)
        {
            int Count = 0;
            foreach (var item in InputOrder)
            {
                var IdSum = preInput.FirstOrDefault(x => x.ProductPackingCode == item.ProductPackingCode && x.TrackId == 0);
                var dyeingPrintingAreaMovementModel = new DyeingPrintingAreaMovementModel(
                                        DateTime.Now,
                                        item.MaterialOrigin,
                                        DyeingPrintingArea.PACKING,
                                        DyeingPrintingArea.IN,
                                        modelBon.Id,
                                        modelBon.BonNo,
                                        item.ProductionOrderId,
                                        item.ProductionOrderNo,
                                        item.Buyer,
                                        item.Construction,
                                        item.Unit,
                                        item.Color,
                                        item.Motif,
                                        item.UomUnit,
                                        item.Balance,
                                        item.Id,
                                        item.ProductionOrderType,
                                        IdSum.Id,
                                        (decimal) item.PackagingQty
                            );

                dyeingPrintingAreaMovementModel.FlagForCreate(_identityProvider.Username, UserAgent);

                _dbSetMovementOrder.Add(dyeingPrintingAreaMovementModel);

            };

            Count = await _dbContext.SaveChangesAsync();
            return Count;
        }



        public async Task<int> InsertNewWarehouse(DPInputWarehouseCreateViewModel viewModel)
        {
            int Created = 0;



            using (var transaction = this._dbContext.Database.BeginTransaction())
            {
                try
                {
                    int totalCurrentYearData = _dPWarehouseInputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                             s.CreatedUtc.Year == DateTime.Now.Date.Year);

                    string bonNo = GenerateBonNo(totalCurrentYearData + 1, DateTime.Now.Date);

                    


                    var model = new DPWarehouseInputModel(
                                DateTime.Now.Date,
                                "GUDANG JADI",
                                "DAILY SHIFT",
                                bonNo,
                                "",
                                viewModel.DyeingPrintingWarehouseInItems.Select(s => new DPWarehouseInputItemModel(
                                   s.ProductionOrder.Id,
                                   s.ProductionOrder.No,
                                   s.MaterialProduct.Id,
                                   s.MaterialProduct.Name,
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
                                   s.Remark,
                                   s.Grade,
                                   s.Sendquantity * s.PackagingLength,
                                   s.PackingInstruction,
                                   s.ProductionOrder.Type,
                                   s.ProductionOrder.OrderQuantity,
                                   s.PackagingType,
                                   (decimal)s.Sendquantity,
                                   s.PackagingLength,
                                   s.PackagingUnit,
                                   s.Area,
                                   s.Description,
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
                                   s.FinishWidth,
                                   s.MaterialOrigin,
                                   s.ProductionOrder.CreatedUtc,
                                   s.Grade != "BS" ? 1360 : 0,
                                   s.Grade != "BS" ? "Jalur" : null,
                                   s.Grade != "BS" ? "FAST MOVE" : null




                                   )).ToList()
                        );

                    //model.Area.Trim();
                    
                    model.FlagForCreate(_identityProvider.Username, UserAgent);

                    _dbSet.Add(model);

                    var listSummary = new List<DPWarehouseSummaryModel>();

                    var IdSummary = new List<int>();

                    foreach (var item in model.DPWarehouseInputItems)
                    {
                        item.FlagForCreate(_identityProvider.Username, UserAgent);

                        #region save or update summary table

                        DPWarehouseSummaryModel modelSummary;

                        if (item.Grade != "BS")
                        {
                            modelSummary = _dPWarehouseSummaryRepository.GetDbSet().FirstOrDefault(s => s.ProductPackingCode.Contains(item.ProductPackingCode) && s.TrackId == 1360);
                        }
                        else {
                            modelSummary = _dPWarehouseSummaryRepository.GetDbSet().FirstOrDefault(s => s.ProductPackingCode.Contains(item.ProductPackingCode) && s.TrackId == 0);
                        }
                        
                        if (modelSummary == null)
                        {

                            modelSummary = new DPWarehouseSummaryModel(
                                        item.Balance,
                                        item.Balance,
                                        0,
                                        item.BuyerId,
                                        item.Buyer,
                                        "",
                                        item.Color,
                                        item.Grade,
                                        item.Construction,
                                        item.MaterialConstructionId,
                                        item.MaterialConstructionName,
                                        item.MaterialId,
                                        item.MaterialName,
                                        item.MaterialWidth,
                                        item.Motif,
                                        item.PackingInstruction,
                                        item.PackagingQty,
                                        item.PackagingQty,
                                        0,
                                        item.PackagingLength,
                                        item.PackagingType,
                                        item.PackagingUnit,
                                        item.ProductionOrderId,
                                        item.ProductionOrderNo,
                                        item.ProductionOrderType,
                                        item.ProductionOrderOrderQuantity,
                                        item.CreatedUtcOrderNo,
                                        item.ProcessTypeId,
                                        item.ProcessTypeName,
                                        item.YarnMaterialId,
                                        item.YarnMaterialName,
                                        item.Unit,
                                        item.UomUnit,
                                        item.Grade != "BS" ? 1360 : 0,
                                        item.Grade != "BS" ? "Jalur" : null,
                                        item.Grade != "BS" ? "FAST MOVE" : null,
                                        null,
                                        0,
                                        item.Description,
                                        item.ProductSKUId,
                                        item.FabricSKUId,
                                        item.ProductSKUCode,
                                        item.ProductPackingId,
                                        item.FabricPackingId,
                                        item.ProductPackingCode,
                                        item.MaterialOrigin,
                                        item.Remark,
                                        item.FinishWidth

                                );
                            modelSummary.FlagForCreate(_identityProvider.Username, UserAgent);

                            listSummary.Add(modelSummary);

                            _dbSetSummary.Add(modelSummary);
                        }
                        else
                        {
                            double balanceUpdate = modelSummary.Balance + item.Balance;
                            double balanceRemainsUpdate = modelSummary.BalanceRemains + item.Balance;
                            decimal packagingQtyUpdate = modelSummary.PackagingQty + item.PackagingQty;
                            decimal packagingQtyRemainsUpdate = modelSummary.PackagingQtyRemains + item.PackagingQty;
                            //modelSummary.FlagForUpdate(_identityProvider.Username, UserAgent);
                            //modelSummary.SetBalanceRemains(balanceUpdate, _identityProvider.Username, UserAgent);
                            //var modelSummaries = _dbSetSummary.FirstOrDefault(entity => entity.Id == modelSummary.Id);
                            //modelSummaries.BalanceRemains = balanceUpdate;
                            modelSummary.Balance = balanceUpdate;
                            modelSummary.BalanceRemains = balanceRemainsUpdate;
                            modelSummary.PackagingQty = packagingQtyUpdate;
                            modelSummary.PackagingQtyRemains = packagingQtyRemainsUpdate;
                            //EntityExtension.FlagForUpdate(modelSummaries, _identityProvider.Username, UserAgent);
                            EntityExtension.FlagForUpdate(modelSummary, _identityProvider.Username, UserAgent);
                            listSummary.Add(modelSummary);


                        }
                        #endregion

                        #region update dpWarehousePreInput
                        var modelPreInput = _dPWarehousePreInputRepository.GetDbSet().FirstOrDefault(s => s.ProductPackingCode.Contains(item.ProductPackingCode));

                        modelPreInput.BalanceReceipt = modelPreInput.BalanceReceipt + item.Balance;
                        modelPreInput.BalanceRemains = modelPreInput.BalanceRemains - item.Balance;
                        modelPreInput.PackagingQtyReceipt = modelPreInput.PackagingQtyReceipt + item.PackagingQty;
                        modelPreInput.PackagingQtyRemains = modelPreInput.PackagingQtyRemains - item.PackagingQty;

                        EntityExtension.FlagForUpdate(modelPreInput, _identityProvider.Username, UserAgent);

                        #endregion

                    }

                    Created = await _dbContext.SaveChangesAsync();

                    var modelItem = new DPWarehouseInputItemModel();
                    await createMovement(model, listSummary);


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
        public async Task<int> UpdateExistingWarehouse(DPInputWarehouseCreateViewModel viewModel, int modelId, string bonNo)
        {
            int Created = 0;

            var listSummary = new List<DPWarehouseSummaryModel>();
            var listItem = new List<DPWarehouseInputItemModel>();

            using (var transaction = this._dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in viewModel.DyeingPrintingWarehouseInItems)
                    {
                        var modelItem = new DPWarehouseInputItemModel(
                                               item.ProductionOrder.Id,
                                               item.ProductionOrder.No,
                                               item.MaterialProduct.Id,
                                               item.MaterialProduct.Name,
                                               item.MaterialConstruction.Id,
                                               item.MaterialConstruction.Name,
                                               item.MaterialWidth,
                                               item.BuyerId,
                                               item.Buyer,
                                               item.Construction,
                                               item.Unit,
                                               item.Color,
                                               item.Motif,
                                               item.UomUnit,
                                               item.Remark,
                                               item.Grade,
                                               item.Sendquantity * item.PackagingLength,
                                               item.PackingInstruction,
                                               item.ProductionOrder.Type,
                                               item.ProductionOrder.OrderQuantity,
                                               item.PackagingType,
                                               (decimal)item.Sendquantity,
                                               item.PackagingLength,
                                               item.PackagingUnit,
                                               item.Area,
                                               item.Description,
                                               modelId,
                                               item.ProductSKUId,
                                               item.FabricSKUId,
                                               item.ProductSKUCode,
                                               item.ProductPackingId,
                                               item.FabricPackingId,
                                               item.ProductPackingCode,
                                               item.ProcessType.Id,
                                               item.ProcessType.Name,
                                               item.YarnMaterial.Id,
                                               item.YarnMaterial.Name,
                                               item.FinishWidth,
                                               item.MaterialOrigin,
                                               item.ProductionOrder.CreatedUtc,
                                               item.Grade != "BS" ? 1360 : 0,
                                                item.Grade != "BS" ? "Jalur" : null,
                                                item.Grade != "BS" ? "FAST MOVE" : null

                                               );

                        modelItem.FlagForCreate(_identityProvider.Username, UserAgent);
                        listItem.Add(modelItem);
                        _dbSetItems.Add(modelItem);

                        #region save or update summary table

                        DPWarehouseSummaryModel modelSummary;

                        if (item.Grade != "BS")
                        {
                            modelSummary = _dPWarehouseSummaryRepository.GetDbSet().FirstOrDefault(s => s.ProductPackingCode.Contains(item.ProductPackingCode) && s.TrackId == 1360);
                        }
                        else
                        {
                            modelSummary = _dPWarehouseSummaryRepository.GetDbSet().FirstOrDefault(s => s.ProductPackingCode.Contains(item.ProductPackingCode) && s.TrackId == 0);
                        }
                        //var modelSummary = _dPWarehouseSummaryRepository.GetDbSet().FirstOrDefault(s => s.ProductPackingCode.Contains(item.ProductPackingCode) && s.TrackId == 0);
                        if (modelSummary == null)
                        {

                            modelSummary = new DPWarehouseSummaryModel(
                                        item.Sendquantity * item.PackagingLength,
                                        item.Sendquantity * item.PackagingLength,
                                        0,
                                        item.BuyerId,
                                        item.Buyer,
                                        "",
                                        item.Color,
                                        item.Grade,
                                        item.Construction,
                                        item.MaterialConstruction.Id,
                                        item.MaterialConstruction.Name,
                                        item.MaterialProduct.Id,
                                        item.MaterialProduct.Name,
                                        item.MaterialWidth,
                                        item.Motif,
                                        item.PackingInstruction,
                                        (decimal)item.Sendquantity,
                                        (decimal)item.Sendquantity,
                                        0,
                                        item.PackagingLength,
                                        item.PackagingType,
                                        item.PackagingUnit,
                                        item.ProductionOrder.Id,
                                        item.ProductionOrder.No,
                                        item.ProductionOrder.Type,
                                        item.ProductionOrder.OrderQuantity,
                                        item.ProductionOrder.CreatedUtc,
                                        item.ProcessType.Id,
                                        item.ProcessType.Name,
                                        item.YarnMaterial.Id,
                                        item.YarnMaterial.Name,
                                        item.Unit,
                                        item.UomUnit,
                                        item.Grade != "BS" ? 1360 : 0,
                                        item.Grade != "BS" ? "Jalur" : null,
                                        item.Grade != "BS" ? "FAST MOVE" : null,
                                        null,
                                        0,
                                        item.Description,
                                        item.ProductSKUId,
                                        item.FabricSKUId,
                                        item.ProductSKUCode,
                                        item.ProductPackingId,
                                        item.FabricPackingId,
                                        item.ProductPackingCode,
                                        item.MaterialOrigin,
                                        item.Remark,
                                        item.FinishWidth

                                );
                            modelSummary.FlagForCreate(_identityProvider.Username, UserAgent);

                            listSummary.Add(modelSummary);

                            _dbSetSummary.Add(modelSummary);
                        }
                        else
                        {
                            double balanceUpdate = modelSummary.Balance + (item.Sendquantity * item.PackagingLength);
                            double balanceRemainsUpdate = modelSummary.BalanceRemains + (item.Sendquantity * item.PackagingLength);
                            decimal packagingQtyUpdate = modelSummary.PackagingQty + (decimal) item.Sendquantity;
                            decimal packagingQtyRemainsUpdate = modelSummary.PackagingQtyRemains + (decimal) item.Sendquantity;
                            //modelSummary.FlagForUpdate(_identityProvider.Username, UserAgent);
                            //modelSummary.SetBalanceRemains(balanceUpdate, _identityProvider.Username, UserAgent);
                            //var modelSummaries = _dbSetSummary.FirstOrDefault(entity => entity.Id == modelSummary.Id);
                            //modelSummaries.BalanceRemains = balanceUpdate;
                            modelSummary.Balance = balanceUpdate;
                            modelSummary.BalanceRemains = balanceRemainsUpdate;
                            modelSummary.PackagingQty = packagingQtyUpdate;
                            modelSummary.PackagingQtyRemains = packagingQtyRemainsUpdate;
                            //EntityExtension.FlagForUpdate(modelSummaries, _identityProvider.Username, UserAgent);
                            EntityExtension.FlagForUpdate(modelSummary, _identityProvider.Username, UserAgent);
                            listSummary.Add(modelSummary);


                        }
                        #endregion

                        #region update dpWarehousePreInput
                        var modelPreInput = _dPWarehousePreInputRepository.GetDbSet().FirstOrDefault(s => s.ProductPackingCode.Contains(item.ProductPackingCode));



                        modelPreInput.BalanceReceipt = modelPreInput.BalanceReceipt + (item.Sendquantity * item.PackagingLength);
                        modelPreInput.BalanceRemains = modelPreInput.BalanceRemains - (item.Sendquantity * item.PackagingLength);
                        modelPreInput.PackagingQtyReceipt = modelPreInput.PackagingQtyReceipt + (decimal)item.Sendquantity;
                        modelPreInput.PackagingQtyRemains = modelPreInput.PackagingQtyRemains - (decimal)item.Sendquantity;

                        EntityExtension.FlagForUpdate(modelPreInput, _identityProvider.Username, UserAgent);

                        #endregion

                       
                    }

                    Created = await _dbContext.SaveChangesAsync();
                    await createMovementAv(listItem, listSummary, modelId, bonNo);


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
        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        private async Task<int> createMovement(DPWarehouseInputModel model, List<DPWarehouseSummaryModel> modelSum)
        {
            int count = 0;
            foreach (var item in model.DPWarehouseInputItems)
            {
                var IdSum = modelSum.FirstOrDefault(x => x.ProductPackingCode == item.ProductPackingCode && x.TrackId == item.TrackId);

                var modelMovement = new DPWarehouseMovementModel(
                            DateTime.Now,
                            DyeingPrintingArea.GUDANGJADI,
                            DyeingPrintingArea.IN,
                            model.Id,
                            item.Id,
                            model.BonNo,
                            IdSum.Id,
                            item.ProductionOrderId,
                            item.ProductionOrderNo,
                            item.Buyer,
                            item.Construction,
                            item.Unit,
                            item.Color,
                            item.Motif,
                            item.UomUnit,
                            item.Balance,
                            item.Grade,
                            item.ProductionOrderType,
                            item.Remark,
                            item.PackagingType,
                            item.PackagingQty,
                            item.PackagingUnit,
                            item.PackagingLength,
                            item.MaterialOrigin,
                            0,
                            "",
                            "",
                            item.TrackId,
                            item.TrackType,
                            item.TrackName,
                            item.TrackId,
                            item.TrackType,
                            item.TrackName,
                            item.ProductPackingId,
                            item.ProductPackingCode,
                            item.Description

                            );

                modelMovement.FlagForCreate(_identityProvider.Username, UserAgent);

                _dbSetMovement.Add(modelMovement);

                
                //int trackId = 0;
                //string trackType = "";
                //string trackName = "";
                //string trackBox = "";

                //if(item.Grade =="A")

                //EntityExtension.FlagForUpdate(item, _identityProvider.Username, UserAgent);




            }

            count = await _dbContext.SaveChangesAsync();
            return count;
        }

        private async Task<int> createMovementAv(List<DPWarehouseInputItemModel> modelItem, List<DPWarehouseSummaryModel> modelSum, int modelId, string modelBonNo)
        {
            int count = 0;
            
                

            foreach (var item in modelItem)
            {
                var IdSum = modelSum.FirstOrDefault(x => x.ProductPackingCode == item.ProductPackingCode && x.TrackId == item.TrackId);

                var modelMovement = new DPWarehouseMovementModel(
                            DateTime.Now,
                            DyeingPrintingArea.GUDANGJADI,
                            DyeingPrintingArea.IN,
                            modelId,
                            item.Id,
                            modelBonNo,
                            IdSum.Id,
                            item.ProductionOrderId,
                            item.ProductionOrderNo,
                            item.Buyer,
                            item.Construction,
                            item.Unit,
                            item.Color,
                            item.Motif,
                            item.UomUnit,
                            item.Balance,
                            item.Grade,
                            item.ProductionOrderType,
                            item.Remark,
                            item.PackagingType,
                            item.PackagingQty,
                            item.PackagingUnit,
                            item.PackagingLength,
                            item.MaterialOrigin,
                            0,
                            "",
                            "",
                            item.TrackId,
                            item.TrackType,
                            item.TrackName,
                            item.TrackId,
                            item.TrackType,
                            item.TrackName,
                            item.ProductPackingId,
                            item.ProductPackingCode,
                            item.Description
                            

                            );

                modelMovement.FlagForCreate(_identityProvider.Username, UserAgent);

                _dbSetMovement.Add(modelMovement);
            }
            count = await _dbContext.SaveChangesAsync();
            return count;
        }

        public async Task<DPInputWarehouseCreateViewModel> ReadById(int id)
        {
            var model = await _dPWarehouseInputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            DPInputWarehouseCreateViewModel vm = await MapToViewModel(model);

            return vm;
        }

        private async Task<DPInputWarehouseCreateViewModel> MapToViewModel(DPWarehouseInputModel model)
        {
            var vm = new DPInputWarehouseCreateViewModel();
            vm = new DPInputWarehouseCreateViewModel
            {

                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
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
                DyeingPrintingWarehouseInItems = model.DPWarehouseInputItems.Where(x => !x.IsDeleted).Select(s => new DPInputWarehouseItemCreateViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,

                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Balance = s.Balance,
                    Grade = s.Grade,
                    Motif = s.Motif,
                    PackagingQty = (double) s.PackagingQty,
                    PackagingLength = s.PackagingLength,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit,
                    
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        Type = s.ProductionOrderType,
                        OrderQuantity = s.ProductionOrderOrderQuantity
                    },
                    Remark = s.Remark,

                    ProcessType = new ProcessType()
                    { 
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
                    
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    
                    Description = s.Description

                }).ToList()
            };

            return vm;

        }

        public List<DPInputWarehouseMonitoringViewModel> GetMonitoring(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int offset)
        {

        
            IQueryable<DPWarehouseInputItemModel> inputItemsQuery;
            if (dateFrom == DateTimeOffset.MinValue && dateTo == DateTimeOffset.MinValue)
            {
                //stockOpnameMutationQuery = _stockOpnameMutationRepository.ReadAll();
                inputItemsQuery = _dbSetItems;
            }
            else
            {
                //stockOpnameMutationQuery = _stockOpnameMutationRepository.ReadAll().Where(s =>
                //                    s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date);
                inputItemsQuery = _dbSetItems.Where(s =>
                                        s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date);
            }




            if (productionOrderId != 0)
            {
                inputItemsQuery = inputItemsQuery.Where(s => s.ProductionOrderId == productionOrderId);
            }
            var query = (from b in inputItemsQuery
                         select new DPInputWarehouseMonitoringViewModel()
                         {
                             ProductionOrderId = b.ProductionOrderId,
                             ProductionOrderNo = b.ProductionOrderNo,
                             ProductPackingCode = b.ProductPackingCode,
                             ProcessTypeName = b.ProcessTypeName,
                             PackagingUnit = b.PackagingUnit,
                             Grade = b.Grade,
                             Construction = b.Construction,
                             Motif = b.Motif,
                             Color = b.Color,
                             Balance = b.Balance,
                             DateIn = b.CreatedUtc.AddHours(7),
                             PackagingQty = b.PackagingQty,
                             PackingLength = b.PackagingLength,
                             Description = b.Description.Trim(),
                             UomUnit = b.UomUnit
                         }).ToList();
            var result = query.GroupBy(s => new { s.ProductPackingCode, s.DateIn.Date, s.Description }).Select(d => new DPInputWarehouseMonitoringViewModel()
            {
                ProductionOrderId = d.First().ProductionOrderId,
                ProductionOrderNo = d.First().ProductionOrderNo,
                ProductPackingCode = d.First().ProductPackingCode,
                ProcessTypeName = d.First().ProcessTypeName,
                PackagingUnit = d.First().PackagingUnit,
                Grade = d.First().Grade,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Motif = d.First().Motif,
                UomUnit = d.First().UomUnit,
                Balance = d.Sum(a => a.Balance),
                DateIn = d.First().DateIn,
                PackagingQty = d.Sum(a => a.PackagingQty),
                PackingLength = d.First().PackingLength,
                Description = d.First().Description,
                
            }).OrderBy(o => o.ProductionOrderId).ToList();

            var totalPacking = result.Sum(x => x.PackagingQty);
            var totalInQty = result.Sum(x => x.Balance);
            result.Add(new DPInputWarehouseMonitoringViewModel()
            {
                ProductionOrderNo = "",
                ProductPackingCode = "",
                Construction = "",
                Color = "",
                Motif = "",
                Grade = "",
                
                PackagingUnit = "Total",
                PackagingQty = totalPacking,
                Balance = totalInQty,
                UomUnit = "MTR",
                Description = ""
            });

            return result;

        }
        public MemoryStream GenerateExcelMonitoring(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId,  int offset)
        {
            var data = GetMonitoring(dateFrom, dateTo, productionOrderId,  offset);
            DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Barcode", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY Packing", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Panjang Per Packing", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Total", DataType = typeof(double) });


            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "",  0, "",0, 0);
            }
            else
            {
                decimal packagingQty = 0;
                double total = 0;

                foreach (var item in data)
                {
                    var dateIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.AddHours(offset).Date.ToString("d");
                    // var sldbegin = item.SaldoBegin;
                    //saldoBegin =+ item.SaldoBegin;
                    dt.Rows.Add(item.ProductionOrderNo, dateIn, item.ProductPackingCode, item.Construction, item.Color, item.Motif,
                        item.Grade, item.PackagingQty, item.PackagingUnit,  item.PackingLength, item.Balance);

                    packagingQty += item.PackagingQty;
                    total += item.Balance;
                }

                //dt.Rows.Add("", "", "", "", "", "", "", packagingQty, "", 0, total);
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, string.Format("Laporan Stock {0}", "SO")) }, true);

        }

        public List<MonitoringPreInputWarehouseViewModel> GetMonitoringPreInput(int productionOrderId, string productPackingCode)
        {
            IQueryable<DPWarehousePreInputModel> preInput;

            if (productionOrderId != 0)
            {
                preInput = _dPWarehousePreInputRepository.ReadAll().Where(x => x.ProductionOrderId == productionOrderId && x.ProductPackingCode == (string.IsNullOrWhiteSpace(productPackingCode) ? x.ProductPackingCode : productPackingCode));
            }
            else {
                preInput = _dPWarehousePreInputRepository.ReadAll().Where(x => x.ProductPackingCode == (string.IsNullOrWhiteSpace(productPackingCode) ? x.ProductPackingCode : productPackingCode));
            }

                var Query = preInput.Where( s => s.BalanceRemains >0).Select( s => 
            
                            new MonitoringPreInputWarehouseViewModel() { 

                                ProductionOrderNo = s.ProductionOrderNo,
                                ProductPackingCode = s.ProductPackingCode,
                                Balance = s.Balance,
                                BalanceRemains = s.BalanceRemains,
                                BalanceReceipt = s.BalanceReceipt,
                                BalanceReject = s.BalanceReject,
                                PackagingQty = s.PackagingQty,
                                PackagingQtyRemains = s.PackagingQtyRemains,
                                PackagingQtyReceipt = s.PackagingQtyReceipt,
                                PackagingQtyReject = s.PackagingQtyReject,
                                PackagingLength = s.PackagingLength,
                                PackagingUnit = s.PackagingUnit,
                                Description = s.Description,
                                Grade = s.Grade,
                                UomUnit = s.UomUnit,
                                LastModifiedUtc = s.LastModifiedUtc
            
                            }).ToList();
            Query.OrderByDescending(s => s.LastModifiedUtc);

            return Query;
        }

        public MemoryStream GenerateExcelPreInput( int productionOrderId, string productPackingCode)
        {
            var data = GetMonitoringPreInput( productionOrderId, productPackingCode);
            DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Barcode", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Sisa Qty Pack", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Panjang Per Pack", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Sisa Qty", DataType = typeof(double) });

            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });


            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", 0, 0, "", 0, "", "");
            }
            else
            {
                decimal packagingQty = 0;
                double total = 0;

                foreach (var item in data)
                {
                    //var dateIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.AddHours(offset).Date.ToString("d");
                    // var sldbegin = item.SaldoBegin;
                    //saldoBegin =+ item.SaldoBegin;
                    dt.Rows.Add(item.ProductionOrderNo,  item.ProductPackingCode, item.Grade, item.BalanceRemains, item.PackagingLength,
                        item.PackagingUnit, item.PackagingQtyRemains, item.UomUnit, item.Description);

                    packagingQty += item.PackagingQtyRemains;
                    total += item.BalanceRemains;
                }

                dt.Rows.Add("", "", "", packagingQty, 0, "", total, "", "");
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, string.Format("Monitoring SPP Belum Diterima ")) }, true);

        }


    }




}
