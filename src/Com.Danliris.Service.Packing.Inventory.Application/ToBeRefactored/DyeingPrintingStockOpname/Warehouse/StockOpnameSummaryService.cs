using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameSummaryService : IStockOpnameSummaryService
    {

        private const string UserAgent = "packing-inventory-service";
        private readonly IDyeingPrintingStockOpnameRepository _stockOpnameRepository;
        private readonly IDyeingPrintingStockOpnameProductionOrderRepository _stockOpnameProductionOrderRepository;
        private readonly IDyeingPrintingStockOpnameMutationItemRepository _stockOpnameMutationItemsRepository;
        private readonly IDyeingPrintingStockOpnameSummaryRepository _stockOpnameSummaryRepository;
        private readonly IFabricPackingSKUService _fabricPackingSKUService;
        private readonly IProductPackingService _productPackingService;
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;

        public StockOpnameSummaryService(IServiceProvider serviceProvider)
        {
            _stockOpnameRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameRepository>();
            _stockOpnameProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameProductionOrderRepository>();
            _stockOpnameMutationItemsRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameMutationItemRepository>();
            _stockOpnameSummaryRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameSummaryRepository>();
            _fabricPackingSKUService = serviceProvider.GetService<IFabricPackingSKUService>();
            _productPackingService = serviceProvider.GetService<IProductPackingService>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }

        public List<UpdateTrackViewModel> GetDataUpdateTrack(int productionOrderId, string barcode, int trackId)
        {
            IQueryable<DyeingPrintingStockOpnameSummaryModel> stockOpnameSummaryQuery;

            stockOpnameSummaryQuery = _stockOpnameSummaryRepository.ReadAll();

            if (productionOrderId != 0)
            {
                stockOpnameSummaryQuery = stockOpnameSummaryQuery.Where(model => model.ProductionOrderId == productionOrderId);
            }

            if (!string.IsNullOrEmpty(barcode))
            {
                stockOpnameSummaryQuery = stockOpnameSummaryQuery.Where(model => model.ProductPackingCode.Contains(barcode));
            }

            if (trackId != 0)
            {
                stockOpnameSummaryQuery = stockOpnameSummaryQuery.Where(model => model.TrackId == trackId);
            }

            var result = stockOpnameSummaryQuery.Select(b => new UpdateTrackViewModel()
            {
                Id = b.Id, 
                ProductionOrderId = b.ProductionOrderId,
                ProductionOrderNo = b.ProductionOrderNo,
                ProductPackingCode = b.ProductPackingCode,
                ProcessTypeName = b.ProcessTypeName,
                PackagingUnit = b.PackagingUnit,

                Grade = b.Grade,
                Color = b.Color,
                Construction = b.Construction,
                Motif = b.Motif,
                TrackId = b.TrackId,
                TrackName = b.TrackName,
                TrackBox = b.TrackBox,
                Track = b.TrackType + " - " + b.TrackName + " - " + b.TrackBox,
                PackagingQty = b.PackagingQtyRemains,
                PackagingLength = b.PackagingLength,
                Balance = b.BalanceRemains,

            }).ToList();

            return result;

        }

        public async Task<StockOpnameWarehouseSummaryViewModel> ReadById(int id)
        {
            var model = await _stockOpnameSummaryRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            StockOpnameWarehouseSummaryViewModel vm = await MapToViewModel(model);

            return vm;
        }

        private async Task<StockOpnameWarehouseSummaryViewModel> MapToViewModel(DyeingPrintingStockOpnameSummaryModel model)
        {

            var vm = new StockOpnameWarehouseSummaryViewModel();
            vm = new StockOpnameWarehouseSummaryViewModel()
            {
                Active = model.Active,
                LastModifiedUtc = model.LastModifiedUtc,
                Balance = model.Balance,
                BalanceRemains = model.BalanceRemains,
                BalanceEnd = model.BalanceEnd,
                Buyer = model.Buyer,
                BuyerId = model.BuyerId,
                Color = model.Color,
                Construction = model.Construction,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                Grade = model.Grade,
                GradeProduct = new GradeProduct()
                {
                    Type = model.Grade
                },
                PackingInstruction = model.PackingInstruction,
                Remark = model.Remark,
                Status = model.Status,
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                Motif = model.Motif,

                ProductionOrder = new ProductionOrder()
                {
                    Id = model.ProductionOrderId,
                    No = model.ProductionOrderNo,
                    OrderQuantity = model.ProductionOrderOrderQuantity,
                    Type = model.ProductionOrderType
                },
                Unit = model.Unit,
                MaterialWidth = model.MaterialWidth,
                Material = new Material()
                {
                    Id = model.MaterialId,
                    Name = model.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Name = model.MaterialConstructionName,
                    Id = model.MaterialConstructionId
                },
                YarnMaterial = new YarnMaterial()
                {
                    Id = model.YarnMaterialId,
                    Name = model.YarnMaterialName
                },
                ProcessType = new ProcessType()
                {
                    Id = model.ProcessTypeId,
                    Name = model.ProcessTypeName
                },
                UomUnit = model.UomUnit,
                Uom = new UnitOfMeasurement()
                {
                    Unit = model.PackagingUnit
                },
                PackagingQty = model.PackagingQty,
                PackagingQtyRemains = model.PackagingQtyRemains,
                PackagingQtyEnd = model.PackagingQtyEnd,
                PackagingType = model.PackagingType,
                PackagingUnit = model.PackagingUnit,
                ProductionOrderNo = model.ProductionOrderNo,
                QtyOrder = model.ProductionOrderOrderQuantity,
                
                Track = new Track()
                {
                    Id = model.TrackId,
                    Name = model.TrackName,
                    Type = model.TrackType,
                    Box = model.TrackBox
                },
                ProductPackingCode = model.ProductPackingCode,
                ProductPackingId = model.ProductPackingId,
                ProductSKUId = model.ProductSKUId,
                ProductSKUCode = model.ProductSKUCode,
                TrackName = model.TrackType + " - "+ model.TrackName +" - "+ model.TrackBox,
                PackagingLength = model.PackagingLength
            };

            return vm;
        }


        public async Task<int> Update(int id, StockOpnameTrackViewModel viewModel)
        {
            int result = 0;
            result = await this.UpdateTrack(id, viewModel);

            return result;

        }
        private async Task<int> UpdateTrack(int id, StockOpnameTrackViewModel viewModel)
        {
            
            int result = 0;
            DyeingPrintingStockOpnameSummaryModel dataUpdate;
            var dbModel = await _stockOpnameSummaryRepository.ReadByIdAsync(id);
            for (int i = 0; i < viewModel.Items.Count(); i++)
            {
                
                var modelSummary = _stockOpnameSummaryRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.ProductPackingCode.Contains(viewModel.ProductPackingCode) && s.TrackId == viewModel.Items[i].Track.Id && s.PackagingLength == viewModel.PackagingLength);

                //var balance = modelSummary.Balance - viewModel.Items[i].Balance;
                //var balanceRemains = modelSummary.BalanceRemains - viewModel.Items[i].Balance;

                if (i == 0)
                {
                    dataUpdate = new DyeingPrintingStockOpnameSummaryModel(
                                                            dbModel.Balance,
                                                            viewModel.Items[i].Balance,
                                                            dbModel.BuyerId,
                                                            dbModel.Buyer,
                                                            dbModel.Color,
                                                            dbModel.Construction,
                                                            dbModel.Grade,
                                                            dbModel.MaterialConstructionId,
                                                            dbModel.MaterialConstructionName,
                                                            dbModel.MaterialId,
                                                            dbModel.MaterialName,
                                                            dbModel.MaterialWidth,
                                                            dbModel.Motif,
                                                            dbModel.PackingInstruction,
                                                            dbModel.PackagingQty,
                                                            (decimal)viewModel.Items[i].PackagingQtySplit,
                                                            dbModel.PackagingLength,
                                                            dbModel.PackagingType,
                                                            dbModel.PackagingUnit,
                                                            dbModel.ProductionOrderId,
                                                            dbModel.ProductionOrderNo,
                                                            dbModel.ProductionOrderType,
                                                            dbModel.ProductionOrderOrderQuantity,
                                                            dbModel.ProcessTypeId,
                                                            dbModel.ProcessTypeName,
                                                            dbModel.YarnMaterialId,
                                                            dbModel.YarnMaterialName,
                                                            dbModel.Remark,
                                                            dbModel.Status,
                                                            dbModel.Unit,
                                                            dbModel.UomUnit,
                                                            dbModel.FabricSKUId,
                                                            dbModel.ProductSKUId,
                                                            dbModel.ProductSKUCode,
                                                            dbModel.ProductPackingId,
                                                            dbModel.ProductPackingCode,
                                                            viewModel.Items[i].Track.Id,
                                                            viewModel.Items[i].Track.Type,
                                                            viewModel.Items[i].Track.Name,
                                                            viewModel.Items[i].Track.Box,
                                                            dbModel.CreatedUtcOrderNo,
                                                            dbModel.SplitQuantity + viewModel.Items[i].PackingQtyDiff

                                                            );

                    result += await _stockOpnameSummaryRepository.UpdateAsync(id, dataUpdate);


                    //result += await _stockOpnameSummaryRepository.UpdateBalanceRemainsOut(id, viewModel.Items[i].Balance);
                    //result += await _stockOpnameSummaryRepository.UpdatePackingQtyRemainsOut(id, (decimal)viewModel.Items[i].PackagingQtySplit);
                    //result += await _stockOpnameSummaryRepository.UpdateSplitQuantity(id, viewModel.Items[i].PackagingQtySplit);
                    //if(modelSummary == null)
                    //{

                    //    modelSummary = new DyeingPrintingStockOpnameSummaryModel(
                    //                                            viewModel.Items[i].Balance,
                    //                                            viewModel.Items[i].Balance ,
                    //                                            modelSummary.BuyerId,
                    //                                            modelSummary.Buyer,
                    //                                            modelSummary.Color,
                    //                                            modelSummary.Construction,
                    //                                            modelSummary.Grade,
                    //                                            modelSummary.MaterialConstructionId,
                    //                                            modelSummary.MaterialConstructionName,
                    //                                            modelSummary.MaterialId,
                    //                                            modelSummary.MaterialName,
                    //                                            modelSummary.MaterialWidth,
                    //                                            modelSummary.Motif,
                    //                                            modelSummary.PackingInstruction,
                    //                                            (decimal)viewModel.Items[i].PackagingQtySplit,
                    //                                            (decimal)viewModel.Items[i].PackagingQtySplit,
                    //                                            modelSummary.PackagingLength,
                    //                                            modelSummary.PackagingType,
                    //                                            modelSummary.PackagingUnit,
                    //                                            modelSummary.ProductionOrderId,
                    //                                            modelSummary.ProductionOrderNo,
                    //                                            modelSummary.ProductionOrderType,
                    //                                            modelSummary.ProductionOrderOrderQuantity,
                    //                                            modelSummary.ProcessTypeId,
                    //                                            modelSummary.ProcessTypeName,
                    //                                            modelSummary.YarnMaterialId,
                    //                                            modelSummary.YarnMaterialName,
                    //                                            modelSummary.Remark,
                    //                                            modelSummary.Status,
                    //                                            modelSummary.Unit,
                    //                                            modelSummary.UomUnit,
                    //                                            modelSummary.FabricSKUId,
                    //                                            modelSummary.ProductSKUId,
                    //                                            modelSummary.ProductSKUCode,
                    //                                            modelSummary.ProductPackingId,
                    //                                            modelSummary.ProductPackingCode,
                    //                                            viewModel.Items[i].Track.Id,
                    //                                            viewModel.Items[i].Track.Type,
                    //                                            viewModel.Items[i].Track.Name,
                    //                                            viewModel.Items[i].Track.Box,
                    //                                            modelSummary.CreatedUtcOrderNo

                    //                                            );

                    //    result += await _stockOpnameSummaryRepository.InsertAsync(modelSummary);

                    //}

                }else {

                    if (modelSummary != null)
                    {
                        dataUpdate = new DyeingPrintingStockOpnameSummaryModel(
                                                            modelSummary.Balance + viewModel.Items[i].Balance,
                                                            modelSummary.BalanceRemains + viewModel.Items[i].Balance,
                                                            modelSummary.BuyerId,
                                                            modelSummary.Buyer,
                                                            modelSummary.Color,
                                                            modelSummary.Construction,
                                                            modelSummary.Grade,
                                                            modelSummary.MaterialConstructionId,
                                                            modelSummary.MaterialConstructionName,
                                                            modelSummary.MaterialId,
                                                            modelSummary.MaterialName,
                                                            modelSummary.MaterialWidth,
                                                            modelSummary.Motif,
                                                            modelSummary.PackingInstruction,
                                                            modelSummary.PackagingQty + (decimal)viewModel.Items[i].PackagingQtySplit,
                                                            modelSummary.PackagingQtyRemains + (decimal)viewModel.Items[i].PackagingQtySplit,
                                                            modelSummary.PackagingLength,
                                                            modelSummary.PackagingType,
                                                            modelSummary.PackagingUnit,
                                                            modelSummary.ProductionOrderId,
                                                            modelSummary.ProductionOrderNo,
                                                            modelSummary.ProductionOrderType,
                                                            modelSummary.ProductionOrderOrderQuantity,
                                                            modelSummary.ProcessTypeId,
                                                            modelSummary.ProcessTypeName,
                                                            modelSummary.YarnMaterialId,
                                                            modelSummary.YarnMaterialName,
                                                            modelSummary.Remark,
                                                            modelSummary.Status,
                                                            modelSummary.Unit,
                                                            modelSummary.UomUnit,
                                                            modelSummary.FabricSKUId,
                                                            modelSummary.ProductSKUId,
                                                            modelSummary.ProductSKUCode,
                                                            modelSummary.ProductPackingId,
                                                            modelSummary.ProductPackingCode,
                                                            viewModel.Items[i].Track.Id,
                                                            viewModel.Items[i].Track.Type,
                                                            viewModel.Items[i].Track.Name,
                                                            viewModel.Items[i].Track.Box,
                                                            dbModel.CreatedUtcOrderNo,
                                                            modelSummary.SplitQuantity 

                                                            );

                        result += await _stockOpnameSummaryRepository.UpdateAsync(modelSummary.Id, dataUpdate);
                    }
                    else {
                        dataUpdate = new DyeingPrintingStockOpnameSummaryModel(
                                                            viewModel.Items[i].Balance,
                                                            viewModel.Items[i].Balance,
                                                            dbModel.BuyerId,
                                                            dbModel.Buyer,
                                                            dbModel.Color,
                                                            dbModel.Construction,
                                                            dbModel.Grade,
                                                            dbModel.MaterialConstructionId,
                                                            dbModel.MaterialConstructionName,
                                                            dbModel.MaterialId,
                                                            dbModel.MaterialName,
                                                            dbModel.MaterialWidth,
                                                            dbModel.Motif,
                                                            dbModel.PackingInstruction,
                                                            (decimal)viewModel.Items[i].PackagingQtySplit,
                                                            (decimal)viewModel.Items[i].PackagingQtySplit,
                                                            dbModel.PackagingLength,
                                                            dbModel.PackagingType,
                                                            dbModel.PackagingUnit,
                                                            dbModel.ProductionOrderId,
                                                            dbModel.ProductionOrderNo,
                                                            dbModel.ProductionOrderType,
                                                            dbModel.ProductionOrderOrderQuantity,
                                                            dbModel.ProcessTypeId,
                                                            dbModel.ProcessTypeName,
                                                            dbModel.YarnMaterialId,
                                                            dbModel.YarnMaterialName,
                                                            dbModel.Remark,
                                                            dbModel.Status,
                                                            dbModel.Unit,
                                                            dbModel.UomUnit,
                                                            dbModel.FabricSKUId,
                                                            dbModel.ProductSKUId,
                                                            dbModel.ProductSKUCode,
                                                            dbModel.ProductPackingId,
                                                            dbModel.ProductPackingCode,
                                                            viewModel.Items[i].Track.Id,
                                                            viewModel.Items[i].Track.Type,
                                                            viewModel.Items[i].Track.Name,
                                                            viewModel.Items[i].Track.Box,
                                                            dbModel.CreatedUtcOrderNo

                                                            );

                        result += await _stockOpnameSummaryRepository.InsertAsync(dataUpdate);

                    }
                        

                }



            }


                return result;
               
        }
    }
}
