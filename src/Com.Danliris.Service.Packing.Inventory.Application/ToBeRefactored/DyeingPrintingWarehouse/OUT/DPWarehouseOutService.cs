using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingWarehouse;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.OUT.ViewModel;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using System.Threading.Tasks;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.OUT
{
    public class DPWarehouseOutService : IDPWarehouseOutService
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IDPWarehouseOutputRepository _dPWarehouseOutputRepository;
        private readonly IDPWarehouseSummaryRepository _dPWarehouseSummaryRepository;
        private readonly DbSet<DPWarehouseSummaryModel> _dbSetSummary;
        private readonly DbSet<DPWarehouseOutputModel> _dbSet;
        private readonly DbSet<DPWarehouseOutputItemModel> _dbSetItem;
        private readonly DbSet<DPWarehouseMovementModel> _dbSetMovement;
        private readonly IIdentityProvider _identityProvider;
        private const string UserAgent = "Repository";

        public DPWarehouseOutService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbSetSummary = dbContext.Set<DPWarehouseSummaryModel>();
            _dbSetMovement = dbContext.Set<DPWarehouseMovementModel>();
            _dbSet = dbContext.Set<DPWarehouseOutputModel>();
            _dbSetItem = dbContext.Set<DPWarehouseOutputItemModel>();
            _dPWarehouseSummaryRepository = serviceProvider.GetService<IDPWarehouseSummaryRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _dPWarehouseOutputRepository = serviceProvider.GetService<IDPWarehouseOutputRepository>();
            _dbContext = dbContext;
        }

        public List<OutputWarehouseItemListViewModel> ListOutputWarehouse(string packingCode, int trackId)
        {
            //List<OutputPreWarehouseItemListViewModel> queryResult;

            var queryResult = new List<OutputWarehouseItemListViewModel>();
            var query = _dPWarehouseSummaryRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc).
                                                                   Where(s => s.ProductPackingCode.Contains(packingCode) && s.TrackId == trackId
                                                                  );

            if (query != null)
            {
                queryResult = query.Select(p => new OutputWarehouseItemListViewModel()
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
                    //FinishWidth = p.FinishWidth,
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
                    Track = new Track
                    {
                        Id = p.TrackId,
                        Type = p.TrackType,
                        Name = p.TrackName,
                        Box = p.TrackBox
                    },
                    DPWarehouseSummaryId = p.Id


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
            var query = _dPWarehouseOutputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);

            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DPWarehouseOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DPWarehouseOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DPWarehouseOutputModel>.Order(query, OrderDictionary);
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

        public async Task<int> Create(DPWarehouseOutputCreateViewModel viewModel)
        {
            int result = 0;

            var model = _dPWarehouseOutputRepository.GetDbSet()
                //.Include(s => s.DyeingPrintingAreaInputProductionOrders)
                .FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                s.Date.AddHours(7).ToString("dd/MM/yyyy") == DateTime.Now.Date.AddHours(7).ToString("dd/MM/yyyy") &&
                s.Shift == "DAILY SHIFT");

            //var dateData = viewModel.Date;

            var ids = _dPWarehouseOutputRepository.GetDbSet().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI).Select(x => x.Id).ToList();

            if (model != null)
            {
                result = await UpdateExistingWarehouseOut(viewModel, model.Id, model.BonNo);
            }
            else
            {
                result = await OutputNewWarehouse(viewModel);
            }


            return result;


        }

        public async Task<int> OutputNewWarehouse(DPWarehouseOutputCreateViewModel viewModel)
        {
            int Created = 0;

            using (var transaction = this._dbContext.Database.BeginTransaction())
            {
                try
                {
                    int totalCurrentYearData = _dPWarehouseOutputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                             s.CreatedUtc.Year == DateTime.Now.Date.Year);

                    string bonNo = GenerateBonNo(totalCurrentYearData + 1, DateTime.Now.Date, viewModel.DestinationArea);

                    var model = new DPWarehouseOutputModel(
                        DateTime.Now.Date,
                        "GUDANG JADI",
                        "DAILY SHIFT",
                        bonNo,
                        viewModel.DestinationArea,
                        viewModel.DyeingPrintingWarehouseOutputItems.Select( s => new DPWarehouseOutputItemModel(

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
                                   viewModel.DeliveryOrderSalesId,
                                   viewModel.DeliveryOrderSalesNo,
                                   viewModel.DeliveryOrderSalesType,
                                   "",
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
                                   s.DPWarehouseSummaryId,
                                   s.Track.Id,
                                   s.Track.Type,
                                   s.Track.Name,
                                   s.Track.Box,
                                   viewModel.DestinationArea

                          )            
                        ).ToList()
                        
                        );

                    model.FlagForCreate(_identityProvider.Username, UserAgent);

                    _dbSet.Add(model);

                    foreach (var item in model.DPWarehouseOutputItems)
                    {
                        item.FlagForCreate(_identityProvider.Username, UserAgent);

                        var modelSummary = _dPWarehouseSummaryRepository.GetDbSet().FirstOrDefault(s => s.Id == item.DPWarehouseSummaryId);
                        modelSummary.BalanceRemains = modelSummary.BalanceRemains - item.Balance;
                        modelSummary.BalanceOut = modelSummary.BalanceOut + item.Balance;
                        modelSummary.PackagingQtyRemains = modelSummary.PackagingQtyRemains - item.PackagingQty;
                        modelSummary.PackagingQtyOut = modelSummary.PackagingQtyOut + item.PackagingQty;
                        EntityExtension.FlagForUpdate(modelSummary, _identityProvider.Username, UserAgent);

                    }
                    Created = await _dbContext.SaveChangesAsync();

                    await createMovement(model);
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

        public async Task<int> UpdateExistingWarehouseOut(DPWarehouseOutputCreateViewModel viewModel, int modelId, string bonNo)
        {
            int Created = 0;
            var listItem = new List<DPWarehouseOutputItemModel>();
            using (var transaction = this._dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in viewModel.DyeingPrintingWarehouseOutputItems)
                    {
                        var modelItem = new DPWarehouseOutputItemModel(

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
                                   viewModel.DeliveryOrderSalesId,
                                   viewModel.DeliveryOrderSalesNo,
                                   viewModel.DeliveryOrderSalesType,
                                   "",
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
                                   item.DPWarehouseSummaryId,
                                   item.Track.Id,
                                   item.Track.Type,
                                   item.Track.Name,
                                   item.Track.Box,
                                   viewModel.DestinationArea
                          );
                        modelItem.FlagForCreate(_identityProvider.Username, UserAgent);
                        listItem.Add(modelItem);
                        _dbSetItem.Add(modelItem);

                        var modelSummary = _dPWarehouseSummaryRepository.GetDbSet().FirstOrDefault(s => s.Id == item.DPWarehouseSummaryId);
                        modelSummary.BalanceRemains = modelSummary.BalanceRemains - (item.Sendquantity * item.PackagingLength);
                        modelSummary.BalanceOut = modelSummary.BalanceOut + (item.Sendquantity * item.PackagingLength);
                        modelSummary.PackagingQtyRemains = modelSummary.PackagingQtyRemains - (decimal)item.PackagingQty;
                        modelSummary.PackagingQtyOut = modelSummary.PackagingQtyOut + (decimal)item.PackagingQty;
                        EntityExtension.FlagForUpdate(modelSummary, _identityProvider.Username, UserAgent);
                    }

                    Created = await _dbContext.SaveChangesAsync();
                    await createMovementAv(listItem,  modelId, bonNo);


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

        private async Task<int> createMovement(DPWarehouseOutputModel model) 
        {
            int count = 0;
            foreach (var item in model.DPWarehouseOutputItems)
            {
                var modelMovement = new DPWarehouseMovementModel(
                            DateTime.Now,
                            DyeingPrintingArea.GUDANGJADI,
                            DyeingPrintingArea.OUT,
                            model.Id,
                            item.Id,
                            model.BonNo,
                            item.DPWarehouseSummaryId,
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
                            item.ProductPackingId,
                            item.ProductPackingCode,
                            item.Description,
                            item.TrackFromId,
                            item.TrackFromType,
                            item.TrackFromName,
                            item.TrackFromBox

                            );

                modelMovement.FlagForCreate(_identityProvider.Username, UserAgent);

                _dbSetMovement.Add(modelMovement);
            };

            count = await _dbContext.SaveChangesAsync();

            return count;
        }

        private async Task<int> createMovementAv(List<DPWarehouseOutputItemModel>modelItem, int modelId, string bonNo)
        {
            int count = 0;
            foreach (var item in modelItem)
            {
                var modelMovement = new DPWarehouseMovementModel(
                            DateTime.Now,
                            DyeingPrintingArea.GUDANGJADI,
                            DyeingPrintingArea.OUT,
                            modelId,
                            item.Id,
                            bonNo,
                            item.DPWarehouseSummaryId,
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
                            item.ProductPackingId,
                            item.ProductPackingCode,
                            item.Description,
                            item.TrackFromId,
                            item.TrackFromType,
                            item.TrackFromName,
                            item.TrackFromBox


                            );

                modelMovement.FlagForCreate(_identityProvider.Username, UserAgent);

                _dbSetMovement.Add(modelMovement);
            };

            count = await _dbContext.SaveChangesAsync();

            return count;


        }


        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            var bonNo = "";
            if (destinationArea == DyeingPrintingArea.SHIPPING)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.PACKING)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.TRANSIT)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

            return bonNo;
        }

    }
}
