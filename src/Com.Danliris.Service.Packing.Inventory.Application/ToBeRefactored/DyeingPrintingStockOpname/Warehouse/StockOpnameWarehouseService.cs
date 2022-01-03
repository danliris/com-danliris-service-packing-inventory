using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct;
using OfficeOpenXml;
using System.IO;
using System.Globalization;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{


    public class StockOpnameWarehouseService : IStockOpnameWarehouseService
    {
        private const string UserAgent = "packing-inventory-service";
        private readonly IDyeingPrintingStockOpnameRepository _stockOpnameRepository;
        private readonly IDyeingPrintingStockOpnameProductionOrderRepository _stockOpnameProductionOrderRepository;
        private readonly IFabricPackingSKUService _fabricPackingSKUService;
        private readonly IProductPackingService _productPackingService;
        private readonly IIdentityProvider _identityProvider;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private readonly IServiceProvider _serviceProvider;

        public StockOpnameWarehouseService(IServiceProvider serviceProvider)
        {
            _stockOpnameRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameRepository>();
            _stockOpnameProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameProductionOrderRepository>();
            _fabricPackingSKUService = serviceProvider.GetService<IFabricPackingSKUService>();
            _productPackingService = serviceProvider.GetService<IProductPackingService>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _serviceProvider = serviceProvider;
        }

        public async Task<int> Create(StockOpnameWarehouseViewModel viewModel)
        {
            int result = 0;

            if (viewModel.Type == DyeingPrintingArea.STOCK_OPNAME)
            {
                result = await CreateStockOpname(viewModel);
            }

            return result;
        }

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            var bonNo = "";
            if (destinationArea == DyeingPrintingArea.GUDANGJADI)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }


            return bonNo;
        }


        private async Task<int> CreateStockOpname(StockOpnameWarehouseViewModel viewModel)
        {
            int result = 0;
            var model = _stockOpnameRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                        s.Date.AddHours(7).ToString("dd/MM/YYYY").Equals(viewModel.Date.AddHours(7).ToString("dd/MM/YYYY")) &&
                                                                                        s.Type == DyeingPrintingArea.STOCK_OPNAME
                                                                                        && !s.IsStockOpname
                                                                                        && !s.IsDeleted);
            //viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.Where(s => s.IsSave).ToList();
            viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.ToList();
            if (model == null)
            {
                int totalCurrentYearData = _stockOpnameRepository.ReadAllIgnoreQueryFilter()
                                                            .Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                        s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.STOCK_OPNAME);



                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.Area);

                model = new DyeingPrintingStockOpnameModel(DyeingPrintingArea.GUDANGJADI, bonNo, viewModel.Date, DyeingPrintingArea.STOCK_OPNAME,
                                                          viewModel.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingStockOpnameProductionOrderModel(
                                                                s.Balance,
                                                                s.BuyerId,
                                                                s.Buyer,
                                                                s.Color,
                                                                s.Construction,
                                                                s.DocumentNo,
                                                                s.Grade,
                                                                s.MaterialConstruction.Id,
                                                                s.MaterialConstruction.Name,
                                                                s.Material.Id,
                                                                s.Material.Name,
                                                                s.MaterialWidth,
                                                                s.Motif,
                                                                s.PackingInstruction,
                                                                s.PackagingQty,
                                                                s.Quantity,
                                                                s.PackagingType,
                                                                s.PackagingUnit,
                                                                s.ProductionOrder.Id,
                                                                s.ProductionOrder.No,
                                                                s.ProductionOrder.Type,
                                                                s.ProductionOrder.OrderQuantity,
                                                                s.ProcessType.Id,
                                                                s.ProcessType.Name,
                                                                s.YarnMaterial.Id,
                                                                s.YarnMaterial.Name,
                                                                s.Remark,
                                                                s.Status,
                                                                s.Unit,
                                                                s.UomUnit,
                                                                false,
                                                                null
                                                                )).ToList(), false);


                result = await _stockOpnameRepository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingStockOpnameProductionOrders)
                {
                    var skuData = _fabricPackingSKUService.AutoCreateSKU(new FabricSKUAutoCreateFormDto()
                    {
                        Grade = item.Grade,
                        ProcessType = item.ProcessTypeName,
                        ProductionOrderNo = item.ProductionOrderNo,
                        UOM = item.UomUnit,
                        materialId = item.MaterialId,
                        materialName = item.MaterialName,
                        materialConstructionId = item.MaterialConstructionId,
                        materialConstructionName = item.MaterialConstructionName,
                        yarnMaterialId = item.YarnMaterialId,
                        yarnMaterialName = item.YarnMaterialName,
                        uomUnit = item.UomUnit,
                        motif = item.Motif,
                        color = item.Color,
                        Width = item.MaterialWidth
                    });

                    var packingData = _fabricPackingSKUService.AutoCreatePacking(new FabricPackingAutoCreateFormDto()
                    {
                        FabricSKUId = skuData.FabricSKUId,
                        PackingType = item.PackagingUnit,
                        Quantity = (int)item.PackagingQty,
                        Length = item.PackagingLength
                    });

                    var packingCodes = string.Join(',', packingData.ProductPackingCodes);
                    item.SetPackingCode(skuData.ProductSKUId, skuData.FabricSKUId, skuData.ProductSKUCode, packingData.ProductPackingId, packingData.FabricPackingId, packingCodes, false, _identityProvider.Username, UserAgent);
                }

                await _stockOpnameRepository.UpdateAsync(model.Id, model);
                //viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders

            }
            else
            {
                foreach (var item in viewModel.WarehousesProductionOrders)
                {



                    var modelItem = new DyeingPrintingStockOpnameProductionOrderModel(
                                                                item.Balance,
                                                                item.BuyerId,
                                                                item.Buyer,
                                                                item.Color,
                                                                item.Construction,
                                                                item.DocumentNo,
                                                                item.Grade,
                                                                item.MaterialConstruction.Id,
                                                                item.MaterialConstruction.Name,
                                                                item.Material.Id,
                                                                item.Material.Name,
                                                                item.MaterialWidth,
                                                                item.Motif,
                                                                item.PackingInstruction,
                                                                item.PackagingQty,
                                                                item.Quantity,
                                                                item.PackagingType,
                                                                item.PackagingUnit,
                                                                item.ProductionOrder.Id,
                                                                item.ProductionOrder.No,
                                                                item.ProductionOrder.Type,
                                                                item.ProductionOrder.OrderQuantity,
                                                                item.ProcessType.Id,
                                                                item.ProcessType.Name,
                                                                item.YarnMaterial.Id,
                                                                item.YarnMaterial.Name,
                                                                item.Remark,
                                                                item.Status,
                                                                item.Unit,
                                                                item.UomUnit,
                                                                false,
                                                                null
                                                                );

                    modelItem.DyeingPrintingStockOpnameId = model.Id;

                    var skuData = _fabricPackingSKUService.AutoCreateSKU(new FabricSKUAutoCreateFormDto()
                    {
                        Grade = item.Grade,
                        ProcessType = item.ProcessType.Name,
                        ProductionOrderNo = item.ProductionOrder.No,
                        UOM = item.UomUnit,
                        materialId = item.Material.Id,
                        materialName = item.Material.Name,
                        materialConstructionId = item.MaterialConstruction.Id,
                        materialConstructionName = item.MaterialConstruction.Name,
                        yarnMaterialId = item.YarnMaterial.Id,
                        yarnMaterialName = item.YarnMaterial.Name,
                        uomUnit = item.UomUnit,
                        motif = item.Motif,
                        color = item.Color,
                        Width = item.MaterialWidth
                    });

                    var packingData = _fabricPackingSKUService.AutoCreatePacking(new FabricPackingAutoCreateFormDto()
                    {
                        FabricSKUId = skuData.FabricSKUId,
                        PackingType = modelItem.PackagingUnit,
                        Quantity = (int)modelItem.PackagingQty,
                        Length = modelItem.PackagingLength
                    });

                    var packingCodes = string.Join(',', packingData.ProductPackingCodes);
                    modelItem.SetPackingCode(skuData.ProductSKUId, skuData.FabricSKUId, skuData.ProductSKUCode, packingData.ProductPackingId, packingData.FabricPackingId, packingCodes, false, _identityProvider.Username, UserAgent);
                    result += await _stockOpnameProductionOrderRepository.InsertAsync(modelItem);

                }

            }

            return result;
        }

        public async Task<int> Delete(int bonId)
        {
            var model = await _stockOpnameRepository.ReadByIdAsync(bonId);
            var result = 0;
            if (model != null)
            {
                result += await _stockOpnameRepository.DeleteAsync(bonId);
            }
            return result;
        }




        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword, bool isStockOpname)
        {
            var validIds = _stockOpnameProductionOrderRepository.ReadAll().Where(entity => entity.IsStockOpname == isStockOpname).Select(entity => entity.DyeingPrintingStockOpnameId).ToList();
            var query = _stockOpnameRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && validIds.Contains(s.Id));

            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingStockOpnameModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingStockOpnameModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingStockOpnameModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Id = s.Id,
                Area = s.Area,
                Type = DyeingPrintingArea.STOCK_OPNAME,
                BonNo = s.BonNo,
                Date = s.Date,

            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<IndexViewModel> Read(string keyword)
        {
            var query = _stockOpnameRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingStockOpnameModel>.Search(query, SearchAttributes, keyword);

            var data = query.Select(s => new IndexViewModel()
            {
                Id = s.Id,
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,

            });

            return new ListResult<IndexViewModel>(data.ToList(), 0, data.Count(), query.Count());
        }


        public async Task<StockOpnameWarehouseViewModel> ReadById(int id)
        {
            var model = await _stockOpnameRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            StockOpnameWarehouseViewModel vm = await MapToViewModel(model);

            return vm;
        }

        public async Task<int> Update(int id, StockOpnameWarehouseViewModel viewModel)
        {
            int result = 0;
            result = await this.UpdateStockOpname(id, viewModel);

            return result;

        }

        private async Task<int> UpdateStockOpname(int id, StockOpnameWarehouseViewModel viewModel)
        {
            string type;
            int result = 0;
            var dbModel = await _stockOpnameRepository.ReadByIdAsync(id);

            var model = new DyeingPrintingStockOpnameModel(
                viewModel.Area,
                viewModel.BonNo,
                viewModel.Date,
                viewModel.Type,
                viewModel.WarehousesProductionOrders.Select(s => new DyeingPrintingStockOpnameProductionOrderModel(
                s.Balance,
                s.BuyerId,
                s.Buyer,
                s.Color,
                s.Construction,
                s.DocumentNo,
                s.Grade,
                s.MaterialConstruction.Id,
                s.MaterialConstruction.Name,
                s.Material.Id,
                s.Material.Name,
                s.MaterialWidth,
                s.Motif,
                s.PackingInstruction,
                s.PackagingQty,
                s.Quantity,
                s.PackagingType,
                s.PackagingUnit,
                s.ProductionOrder.Id,
                s.ProductionOrder.No,
                s.ProductionOrder.Type,
                s.ProductionOrder.OrderQuantity,
                s.ProcessType.Id,
                s.ProcessType.Name,
                s.YarnMaterial.Id,
                s.YarnMaterial.Name,
                s.Remark,
                s.Status,
                s.Unit,
                s.UomUnit,
                false,
                null)
                {
                    Id = s.Id
                }).ToList(), false);


            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            Dictionary<int, decimal> dictQtyPacking = new Dictionary<int, decimal>();
            foreach (var item in dbModel.DyeingPrintingStockOpnameProductionOrders)
            {
                var lclModel = model.DyeingPrintingStockOpnameProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {


                    if (string.IsNullOrWhiteSpace(item.ProductPackingCode))
                    {
                        var skuData = _fabricPackingSKUService.AutoCreateSKU(new FabricSKUAutoCreateFormDto()
                        {
                            Grade = item.Grade,
                            ProcessType = item.ProcessTypeName,
                            ProductionOrderNo = item.ProductionOrderNo,
                            UOM = item.UomUnit,
                            materialId = item.MaterialId,
                            materialName = item.MaterialName,
                            materialConstructionId = item.MaterialConstructionId,
                            materialConstructionName = item.MaterialConstructionName,
                            yarnMaterialId = item.YarnMaterialId,
                            yarnMaterialName = item.YarnMaterialName,
                            uomUnit = item.UomUnit,
                            motif = item.Motif,
                            color = item.Color,
                            Width = item.MaterialWidth
                        });

                        var packingData = _fabricPackingSKUService.AutoCreatePacking(new FabricPackingAutoCreateFormDto()
                        {
                            FabricSKUId = skuData.FabricSKUId,
                            PackingType = lclModel.PackagingUnit,
                            Quantity = (int)lclModel.PackagingQty,
                            Length = lclModel.PackagingLength
                        });

                        var packingCodes = string.Join(',', packingData.ProductPackingCodes);
                        lclModel.SetPackingCode(skuData.ProductSKUId, skuData.FabricSKUId, skuData.ProductSKUCode, packingData.ProductPackingId, packingData.FabricPackingId, packingCodes, false, _identityProvider.Username, UserAgent);
                    }
                    else
                    {
                        lclModel.SetPackingCode(item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, false, _identityProvider.Username, UserAgent);
                    }

                    var diffBalance = lclModel.Balance - lclModel.Balance;
                    var diffQtyPacking = lclModel.PackagingQty - lclModel.PackagingQty;
                    dictBalance.Add(lclModel.Id, diffBalance);
                    dictQtyPacking.Add(lclModel.Id, diffQtyPacking);
                }


            }

            var deletedData = dbModel.DyeingPrintingStockOpnameProductionOrders.Where(s => !model.DyeingPrintingStockOpnameProductionOrders.Any(d => d.Id == s.Id)).ToList();

            // result = await _outputRepository.UpdateAdjustmentData(id, model, dbModel);
            if (dbModel != null)
            {
                result = await _stockOpnameRepository.UpdateAsync(id, model);
            }


            return result;
        }

        private async Task<StockOpnameWarehouseViewModel> MapToViewModel(DyeingPrintingStockOpnameModel model)
        {
            var vm = new StockOpnameWarehouseViewModel();
            vm = new StockOpnameWarehouseViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                BonNo = model.BonNo,
                Type = DyeingPrintingArea.STOCK_OPNAME,
                Bon = new IndexViewModel
                {
                    Area = model.Area,
                    BonNo = model.BonNo,
                    Date = model.Date,
                    Id = model.Id
                },
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
                WarehousesProductionOrders = model.DyeingPrintingStockOpnameProductionOrders.Select(s => new StockOpnameWarehouseProductionOrderViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Balance = s.Balance,
                    Buyer = s.Buyer,
                    BuyerId = s.BuyerId,
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Grade = s.Grade,
                    GradeProduct = new GradeProduct()
                    {
                        Type = s.Grade
                    },
                    PackingInstruction = s.PackingInstruction,
                    Remark = s.Remark,
                    Status = s.Status,
                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    Motif = s.Motif,

                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType
                    },
                    Unit = s.Unit,
                    MaterialWidth = s.MaterialWidth,
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
                    YarnMaterial = new YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },
                    ProcessType = new ProcessType()
                    {
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
                    UomUnit = s.UomUnit,
                    Uom = new UnitOfMeasurement()
                    {
                        Unit = s.PackagingUnit
                    },
                    PackagingQty = s.PackagingQty,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit,
                    ProductionOrderNo = s.ProductionOrderNo,
                    QtyOrder = s.ProductionOrderOrderQuantity,
                    DocumentNo = s.DocumentNo,
                    Quantity = s.PackagingLength
                }).ToList()
            };
            //foreach (var item in vm.WarehousesProductionOrders)
            //{
            //    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
            //    if (inputData != null)
            //    {
            //        item.BalanceRemains = inputData.BalanceRemains + item.Balance;
            //    }
            //}


            return vm;
        }

        public async Task<int> Create(StockOpnameBarcodeFormDto form)
        {
            var packingCodes = form.Data.Distinct().ToList();

            var existingCodes = new List<string>();

            foreach (var packingCode in packingCodes)
            {
                if (_stockOpnameProductionOrderRepository.ReadAll().Any(element => element.IsStockOpname && element.ProductPackingCode.Contains(packingCode)))
                    existingCodes.Add(packingCode);
            }

            if (existingCodes.Count > 0)
            {
                var codes = string.Join(',', existingCodes);
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Kode packing sudah tersimpan: " + codes, new List<string> { "Message" }),
                    new ValidationResult(codes, new List<string> { "PackingCodes" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }



            var stockOpnameForms = new List<DyeingPrintingProductPackingViewModel>();
            foreach (var packingCode in packingCodes)
            {
                var packing = _outputProductionOrderRepository.ReadAll().Where(entity => entity.ProductPackingCode.Contains(packingCode)).Select(s => new DyeingPrintingProductPackingViewModel()
                {
                    Color = s.Color,
                    FabricPackingId = s.FabricPackingId,
                    FabricSKUId = s.FabricSKUId,
                    ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType
                    },
                    HasPrintingProductPacking = s.HasPrintingProductPacking,
                    HasPrintingProductSKU = s.HasPrintingProductSKU,
                    Id = s.Id,
                    Material = new Application.CommonViewModelObjectProperties.Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
                    {
                        Name = s.MaterialConstructionName,
                        Id = s.MaterialConstructionId
                    },
                    MaterialWidth = s.MaterialWidth,
                    Motif = s.Motif,
                    ProductPackingCodes = s.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    ProductPackingId = s.ProductPackingId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductSKUId = s.ProductSKUId,
                    UomUnit = s.UomUnit,
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },
                    Quantity = s.PackagingQty,
                    ProductPackingLength = s.PackagingLength,
                    ProductPackingType = s.PackagingUnit,
                    Grade = s.Grade,
                    Unit = s.Unit,
                    Buyer = s.Buyer,
                    ProcessType = new ProcessType()
                    {
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
                    BuyerId = s.BuyerId,
                    PackingInstruction = s.PackingInstruction,
                    Construction = s.Construction
                }).FirstOrDefault();

                if (packing == null)
                {
                    packing = _stockOpnameProductionOrderRepository.ReadAll().Where(entity => entity.ProductPackingCode.Contains(packingCode)).Select(s => new DyeingPrintingProductPackingViewModel()
                    {
                        Color = s.Color,
                        FabricPackingId = s.FabricPackingId,
                        FabricSKUId = s.FabricSKUId,
                        ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            OrderQuantity = s.ProductionOrderOrderQuantity,
                            Type = s.ProductionOrderType
                        },
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        Id = s.Id,
                        Material = new Application.CommonViewModelObjectProperties.Material()
                        {
                            Id = s.MaterialId,
                            Name = s.MaterialName
                        },
                        MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
                        {
                            Name = s.MaterialConstructionName,
                            Id = s.MaterialConstructionId
                        },
                        MaterialWidth = s.MaterialWidth,
                        Motif = s.Motif,
                        ProductPackingCodes = s.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
                        ProductPackingId = s.ProductPackingId,
                        ProductSKUCode = s.ProductSKUCode,
                        ProductSKUId = s.ProductSKUId,
                        UomUnit = s.UomUnit,
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.YarnMaterialId,
                            Name = s.YarnMaterialName
                        },
                        Quantity = s.PackagingQty,
                        ProductPackingLength = s.PackagingLength,
                        ProductPackingType = s.PackagingUnit,
                        Grade = s.Grade,
                        Unit = s.Unit,
                        Buyer = s.Buyer,
                        ProcessType = new ProcessType()
                        {
                            Id = s.ProcessTypeId,
                            Name = s.ProcessTypeName
                        },
                        DocumentNo = s.DocumentNo,
                        BuyerId = s.BuyerId,
                        PackingInstruction = s.PackingInstruction,
                        Construction = s.Construction
                    }).FirstOrDefault();
                }

                if (packing != null)
                    stockOpnameForms.Add(packing);
            }

            stockOpnameForms = stockOpnameForms.Distinct().ToList();
            //var 
            var forms = stockOpnameForms
                .GroupBy(element => new
                {
                    element.ProductionOrder.No,
                    element.ProductSKUCode,
                    element.PackagingLength
                })
                .Select(element => new
                {
                    OrderNo = element.Key.No,
                    SKUCode = element.Key.ProductSKUCode,
                    PackingLength = element.Key.PackagingLength,
                    Id = element.FirstOrDefault().Id
                })
                .ToList();

            var result = 0;

            if (forms.Count > 0)
            {
                var items = new List<StockOpnameWarehouseProductionOrderViewModel>();

                foreach (var itemForm in forms)
                {
                    var stockOpnameForm = stockOpnameForms.FirstOrDefault(element => element.Id == itemForm.Id);
                    var scannedPackingCodes = packingCodes.Where(element => stockOpnameForm.ProductPackingCodes.Contains(element)).ToList();
                    var scannedQuantity = stockOpnameForm.ProductPackingCodes.Where(element => packingCodes.Contains(element)).Count();
                    var productIds = await _productPackingService.GetByCode(string.Join(',', scannedPackingCodes));
                    var item = new StockOpnameWarehouseProductionOrderViewModel()
                    {
                        Balance = stockOpnameForm.ProductPackingLength * scannedQuantity,
                        BuyerId = stockOpnameForm.BuyerId,
                        DocumentNo = stockOpnameForm.DocumentNo,
                        MaterialWidth = stockOpnameForm.MaterialWidth,
                        PackingInstruction = stockOpnameForm.PackingInstruction,
                        ProductionOrder = new ProductionOrder()
                        {
                            Code = stockOpnameForm.ProductionOrder.Code,
                            Id = stockOpnameForm.ProductionOrder.Id,
                            No = stockOpnameForm.ProductionOrder.No,
                            OrderQuantity = stockOpnameForm.ProductionOrder.OrderQuantity,
                            Type = stockOpnameForm.ProductionOrder.Type
                        },
                        Construction = stockOpnameForm.Construction,
                        Unit = stockOpnameForm.Unit,
                        Buyer = stockOpnameForm.Buyer,
                        Color = stockOpnameForm.Color,
                        Motif = stockOpnameForm.Motif,
                        Grade = stockOpnameForm.Grade,
                        PackagingQty = scannedQuantity,
                        PackagingUnit = stockOpnameForm.ProductPackingType,
                        Uom = new UnitOfMeasurement()
                        {
                            Unit = stockOpnameForm.UomUnit
                        },
                        UomUnit = stockOpnameForm.UomUnit,
                        PackagingLength = stockOpnameForm.ProductPackingLength,
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Code = stockOpnameForm.MaterialConstruction.Code,
                            Id = stockOpnameForm.MaterialConstruction.Id,
                            Name = stockOpnameForm.MaterialConstruction.Name
                        },
                        ProcessType = new ProcessType()
                        {
                            Id = stockOpnameForm.ProcessType.Id,
                            Name = stockOpnameForm.ProcessType.Name
                        },
                        Material = new Material()
                        {
                            Code = stockOpnameForm.Material.Code,
                            Name = stockOpnameForm.Material.Name,
                            Id = stockOpnameForm.Material.Id
                        },
                        YarnMaterial = new YarnMaterial()
                        {
                            Id = stockOpnameForm.YarnMaterial.Id,
                            Name = stockOpnameForm.YarnMaterial.Name
                        },
                        IsStockOpname = true,
                        PackingCodes = string.Join(',', scannedPackingCodes),
                        ProductSKUId = stockOpnameForm.ProductSKUId,
                        FabricSKUId = stockOpnameForm.FabricSKUId,
                        ProductSKUCode = stockOpnameForm.ProductSKUCode,
                        ProductPackingId = productIds == null ? stockOpnameForm.ProductPackingId : productIds.Id,
                        FabricPackingId = stockOpnameForm.FabricPackingId,
                        HasPrintingProductSKU = stockOpnameForm.HasPrintingProductSKU

                    };

                    if (items.Where(s => s.PackingCodes == item.PackingCodes).Count() == 0)
                    {
                        items.Add(item);
                    }
                }

                var createForm = new StockOpnameWarehouseViewModel()
                {
                    Type = "STOCK OPNAME",
                    Date = DateTimeOffset.UtcNow,
                    WarehousesProductionOrders = items,
                    IsStockOpname = true
                };

                result = await CreateStockOpnameV2(createForm);

            }

            //if (stockOpnameForms.Count > 0)
            //{
            //    foreach (var stockOpnameForm in stockOpnameForms)
            //    {
            //        var scannedPackingCodes = packingCodes.Where(element => stockOpnameForm.ProductPackingCodes.Contains(element)).ToList();
            //        var scannedQuantity = stockOpnameForm.ProductPackingCodes.Where(element => packingCodes.Contains(element)).Count();
            //        var item = new StockOpnameWarehouseProductionOrderViewModel()
            //        {
            //            Balance = stockOpnameForm.Balance,
            //            BuyerId = stockOpnameForm.BuyerId,
            //            DocumentNo = stockOpnameForm.DocumentNo,
            //            MaterialWidth = stockOpnameForm.MaterialWidth,
            //            PackingInstruction = stockOpnameForm.PackingInstruction,
            //            ProductionOrder = new ProductionOrder()
            //            {
            //                Code = stockOpnameForm.ProductionOrder.Code,
            //                Id = stockOpnameForm.ProductionOrder.Id,
            //                No = stockOpnameForm.ProductionOrder.No,
            //                OrderQuantity = stockOpnameForm.ProductionOrder.OrderQuantity,
            //                Type = stockOpnameForm.ProductionOrder.Type
            //            },
            //            Construction = stockOpnameForm.Construction,
            //            Unit = stockOpnameForm.Unit,
            //            Buyer = stockOpnameForm.Buyer,
            //            Color = stockOpnameForm.Color,
            //            Motif = stockOpnameForm.Motif,
            //            Grade = stockOpnameForm.Grade,
            //            PackagingQty = scannedQuantity,
            //            PackagingUnit = stockOpnameForm.ProductPackingType,
            //            Uom = new UnitOfMeasurement()
            //            {
            //                Unit = stockOpnameForm.UomUnit
            //            },
            //            UomUnit = stockOpnameForm.UomUnit,
            //            PackagingLength = stockOpnameForm.PackagingLength,
            //            MaterialConstruction = new MaterialConstruction()
            //            {
            //                Code = stockOpnameForm.MaterialConstruction.Code,
            //                Id = stockOpnameForm.MaterialConstruction.Id,
            //                Name = stockOpnameForm.MaterialConstruction.Name
            //            },
            //            ProcessType = new ProcessType()
            //            {
            //                Id = stockOpnameForm.ProcessType.Id,
            //                Name = stockOpnameForm.ProcessType.Name
            //            },
            //            Material = new Material()
            //            {
            //                Code = stockOpnameForm.Material.Code,
            //                Name = stockOpnameForm.Material.Name,
            //                Id = stockOpnameForm.Material.Id
            //            },
            //            YarnMaterial = new YarnMaterial()
            //            {
            //                Id = stockOpnameForm.YarnMaterial.Id,
            //                Name = stockOpnameForm.YarnMaterial.Name
            //            },
            //            IsStockOpname = true,
            //            PackingCodes = string.Join(',', scannedPackingCodes),
            //            ProductSKUId = stockOpnameForm.ProductSKUId,
            //            FabricSKUId = stockOpnameForm.FabricSKUId,
            //            ProductSKUCode = stockOpnameForm.ProductSKUCode,
            //            ProductPackingId = stockOpnameForm.ProductPackingId,
            //            FabricPackingId = stockOpnameForm.FabricPackingId,
            //            ProductPackingCodes = stockOpnameForm.ProductPackingCodes,
            //            HasPrintingProductSKU = stockOpnameForm.HasPrintingProductSKU

            //        };

            //        items.Add(item);
            //    }

            //    var createForm = new StockOpnameWarehouseViewModel()
            //    {
            //        Type = "STOCK OPNAME",
            //        Date = DateTimeOffset.UtcNow,
            //        WarehousesProductionOrders = items,
            //        IsStockOpname = true
            //    };

            //    //result = await CreateStockOpnameV2(createForm);
            //}

            return result;
        }

        private async Task<int> CreateStockOpnameV2(StockOpnameWarehouseViewModel viewModel)
        {
            int result = 0;
            var model = _stockOpnameRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                        s.Date.AddHours(7).ToString("dd/MM/YYYY").Equals(viewModel.Date.AddHours(7).ToString("dd/MM/YYYY")) &&
                                                                                        s.Type == DyeingPrintingArea.STOCK_OPNAME
                                                                                        && s.IsStockOpname
                                                                                        && !s.IsDeleted);
            //viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.Where(s => s.IsSave).ToList();
            viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.ToList();
            if (model == null)
            {
                int totalCurrentYearData = _stockOpnameRepository.ReadAllIgnoreQueryFilter()
                                                            .Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                        s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.STOCK_OPNAME);



                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.Area);

                model = new DyeingPrintingStockOpnameModel(DyeingPrintingArea.GUDANGJADI, bonNo, viewModel.Date, DyeingPrintingArea.STOCK_OPNAME,
                                                          viewModel.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingStockOpnameProductionOrderModel(
                                                                s.Balance,
                                                                s.BuyerId,
                                                                s.Buyer,
                                                                s.Color,
                                                                s.Construction,
                                                                s.DocumentNo,
                                                                s.Grade,
                                                                s.MaterialConstruction.Id,
                                                                s.MaterialConstruction.Name,
                                                                s.Material.Id,
                                                                s.Material.Name,
                                                                s.MaterialWidth,
                                                                s.Motif,
                                                                s.PackingInstruction,
                                                                s.PackagingQty,
                                                                s.PackagingLength,
                                                                s.PackagingType,
                                                                s.PackagingUnit,
                                                                s.ProductionOrder.Id,
                                                                s.ProductionOrder.No,
                                                                s.ProductionOrder.Type,
                                                                s.ProductionOrder.OrderQuantity,
                                                                s.ProcessType.Id,
                                                                s.ProcessType.Name,
                                                                s.YarnMaterial.Id,
                                                                s.YarnMaterial.Name,
                                                                s.Remark,
                                                                s.Status,
                                                                s.Unit,
                                                                s.UomUnit,
                                                                true,
                                                                s.PackingCodes,
                                                                s.ProductSKUId,
                                                                s.FabricSKUId,
                                                                s.ProductSKUCode,
                                                                s.ProductPackingId,
                                                                s.FabricPackingId,
                                                                false
                                                                )).ToList(), true);


                result = await _stockOpnameRepository.InsertAsync(model);

                await _stockOpnameRepository.UpdateAsync(model.Id, model);

            }
            else
            {
                foreach (var item in viewModel.WarehousesProductionOrders)
                {



                    var modelItem = new DyeingPrintingStockOpnameProductionOrderModel(
                                                                item.Balance,
                                                                item.BuyerId,
                                                                item.Buyer,
                                                                item.Color,
                                                                item.Construction,
                                                                item.DocumentNo,
                                                                item.Grade,
                                                                item.MaterialConstruction.Id,
                                                                item.MaterialConstruction.Name,
                                                                item.Material.Id,
                                                                item.Material.Name,
                                                                item.MaterialWidth,
                                                                item.Motif,
                                                                item.PackingInstruction,
                                                                item.PackagingQty,
                                                                item.PackagingLength,
                                                                item.PackagingType,
                                                                item.PackagingUnit,
                                                                item.ProductionOrder.Id,
                                                                item.ProductionOrder.No,
                                                                item.ProductionOrder.Type,
                                                                item.ProductionOrder.OrderQuantity,
                                                                item.ProcessType.Id,
                                                                item.ProcessType.Name,
                                                                item.YarnMaterial.Id,
                                                                item.YarnMaterial.Name,
                                                                item.Remark,
                                                                item.Status,
                                                                item.Unit,
                                                                item.UomUnit,
                                                                true,
                                                                item.PackingCodes,
                                                                item.ProductSKUId,
                                                                item.FabricSKUId,
                                                                item.ProductSKUCode,
                                                                item.ProductPackingId,
                                                                item.FabricPackingId,
                                                                false
                                                                );

                    modelItem.DyeingPrintingStockOpnameId = model.Id;

                    result += await _stockOpnameProductionOrderRepository.InsertAsync(modelItem);

                }

            }

            return result;
        }

        public async Task<MemoryStream> GenerateExcelDocumentAsync(int id, int offSet)
        {
            DyeingPrintingStockOpnameModel model = await _stockOpnameRepository.ReadByIdAsync(id);
            var query = model.DyeingPrintingStockOpnameProductionOrders;

            var indexNumber = 1;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO.", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY ORDER", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "JENIS ORDER", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MATERIAL", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "UNIT", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "WARNA", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MOTIF", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "GRADE", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY PACKAGING", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SATUAN PACK", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY SATUAN", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY TOTAL", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO DOKUMEN", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", 0, "", "", "", "", "", "", "", 0, "", "", 0, 0, "");
            }
            else
            {
                foreach (var item in query)
                {
                    //var dataIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                    //var dataOut = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateOut.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");

                    dt.Rows.Add(indexNumber,
                                item.ProductionOrderNo,
                                item.ProductionOrderOrderQuantity,
                                item.ProductionOrderType,
                                item.Construction,
                                item.Unit,
                                item.Buyer,
                                item.Color,
                                item.Motif,
                                item.Grade,
                                item.PackagingQty,
                                item.PackagingUnit,
                                item.UomUnit,
                                item.PackagingLength,
                                item.Balance,
                                item.DocumentNo
                                );
                    indexNumber++;
                }
            }

            ExcelPackage package = new ExcelPackage();
            #region Header
            var sheet = package.Workbook.Worksheets.Add("Bon Keluar Aval");

            sheet.Cells[1, 1].Value = "TANGGAL";
            sheet.Cells[1, 2].Value = model.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[2, 1].Value = "NO. BON";
            sheet.Cells[2, 2].Value = model.BonNo;
            sheet.Cells[2, 2, 2, 3].Merge = true;

            sheet.Cells[4, 1].Value = "NO.";
            sheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 1, 5, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 1, 5, 1].Merge = true;

            sheet.Cells[4, 2].Value = "NO. SPP";
            sheet.Cells[4, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 2, 5, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 2, 5, 2].Merge = true;

            sheet.Cells[4, 3].Value = "QTY ORDER";
            sheet.Cells[4, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 3, 5, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 3, 5, 3].Merge = true;

            sheet.Cells[4, 4].Value = "JENIS ORDER";
            sheet.Cells[4, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 4, 5, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 4, 5, 4].Merge = true;

            sheet.Cells[4, 5].Value = "MATERIAL";
            sheet.Cells[4, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 5, 5, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 5, 5, 5].Merge = true;

            sheet.Cells[4, 6].Value = "UNIT";
            sheet.Cells[4, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 6, 5, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 6, 5, 6].Merge = true;

            sheet.Cells[4, 7].Value = "BUYER";
            sheet.Cells[4, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 7, 5, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 7, 5, 7].Merge = true;

            sheet.Cells[4, 8].Value = "WARNA";
            sheet.Cells[4, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 8, 5, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 8, 5, 8].Merge = true;

            sheet.Cells[4, 9].Value = "MOTIF";
            sheet.Cells[4, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 9, 5, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 9, 5, 9].Merge = true;

            sheet.Cells[4, 10].Value = "GRADE";
            sheet.Cells[4, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 10, 5, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 10, 5, 10].Merge = true;

            sheet.Cells[4, 11].Value = "QTY PACKAGING";
            sheet.Cells[4, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 11, 5, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 11, 5, 11].Merge = true;

            sheet.Cells[4, 12].Value = "SATUAN PACK";
            sheet.Cells[4, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 12, 5, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 12, 5, 12].Merge = true;

            sheet.Cells[4, 13].Value = "SATUAN";
            sheet.Cells[4, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 13].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 13, 5, 13].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 13, 5, 13].Merge = true;

            sheet.Cells[4, 14].Value = "QTY SATUAN";
            sheet.Cells[4, 14].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 14].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 14, 5, 14].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 14, 5, 14].Merge = true;

            sheet.Cells[4, 15].Value = "QTY TOTAL";
            sheet.Cells[4, 15].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 15].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 15, 5, 15].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 15, 5, 15].Merge = true;

            sheet.Cells[4, 16].Value = "NO DOKUMEN";
            sheet.Cells[4, 16].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[4, 16].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[4, 16, 5, 16].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[4, 16, 5, 16].Merge = true;
            #endregion

            int tableRowStart = 6;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

    }
}