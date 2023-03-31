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
using OfficeOpenXml.Style;
using System.Collections.ObjectModel;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{


    public class StockOpnameWarehouseService : IStockOpnameWarehouseService
    {
        private const string UserAgent = "packing-inventory-service";
        private readonly IDyeingPrintingStockOpnameRepository _stockOpnameRepository;
        private readonly IDyeingPrintingStockOpnameProductionOrderRepository _stockOpnameProductionOrderRepository;
        private readonly IDyeingPrintingStockOpnameMutationItemRepository _stockOpnameMutationItemsRepository;
        private readonly IFabricPackingSKUService _fabricPackingSKUService;
        private readonly IProductPackingService _productPackingService;
        private readonly IIdentityProvider _identityProvider;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private readonly IServiceProvider _serviceProvider;
        public List<BarcodeInfoViewModel> _barcodes;
        //public ObservableCollection<BarcodeInfo> BarcodeList { get; set; }

        public StockOpnameWarehouseService(IServiceProvider serviceProvider)
        {
            _stockOpnameRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameRepository>();
            _stockOpnameProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameProductionOrderRepository>();
            _stockOpnameMutationItemsRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameMutationItemRepository>();
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
                                                                null,
                                                                s.TrackId,
                                                                s.TrackType,
                                                                s.TrackName,
                                                                s.TrackBox

                                                                )).ToList(), false);


                result = await _stockOpnameRepository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingStockOpnameProductionOrders)
                {
                    var skuData = _fabricPackingSKUService.AutoCreateSKUSO(new FabricSKUAutoCreateFormDto()
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

                    var packingData = _fabricPackingSKUService.AutoCreatePackingSO(new FabricPackingAutoCreateFormDto()
                    {
                        FabricSKUId = skuData.FabricSKUId,
                        PackingType = item.PackagingUnit,
                        Quantity = (int)item.PackagingQty,
                        Length = item.PackagingLength
                    });

                    //var packingCodes = string.Join(',', packingData.ProductPackingCodes);
                    var packingCodes = packingData.ProductPackingCode;
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
                                                                null,
                                                                item.TrackId,
                                                                item.TrackType,
                                                                item.TrackName,
                                                                item.TrackBox
                                                                );

                    modelItem.DyeingPrintingStockOpnameId = model.Id;

                    var skuData = _fabricPackingSKUService.AutoCreateSKUSO(new FabricSKUAutoCreateFormDto()
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

                    var packingData = _fabricPackingSKUService.AutoCreatePackingSO(new FabricPackingAutoCreateFormDto()
                    {
                        FabricSKUId = skuData.FabricSKUId,
                        PackingType = modelItem.PackagingUnit,
                        Quantity = (int)modelItem.PackagingQty,
                        Length = modelItem.PackagingLength
                    });

                    //var packingCodes = string.Join(',', packingData.ProductPackingCodes);
                    var packingCodes = packingData.ProductPackingCode;
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
                null,
                s.TrackId,
                s.TrackType,
                s.TrackName,
                s.TrackBox )
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
                WarehousesProductionOrders = model.DyeingPrintingStockOpnameProductionOrders.Where( x => !x.IsDeleted).Select(s => new StockOpnameWarehouseProductionOrderViewModel()
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
                    Quantity = s.PackagingLength,
                    Track = new Track() 
                    { 
                        Id = s.TrackId,
                        Name = s.TrackName,
                        Type = s.TrackType,
                        Box = s.TrackBox
                    }
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

        //Tanggal 23/03/2023 By Gama
        //public async Task<int> Create(StockOpnameBarcodeFormDto form)
        //{
        //    var packingCodes = form.Data.Distinct().ToList();

        //    var existingCodes = new List<string>();

        //    foreach (var packingCode in packingCodes)
        //    {
        //        if (_stockOpnameProductionOrderRepository.ReadAll().Any(element => element.IsStockOpname && element.ProductPackingCode.Contains(packingCode)))
        //            existingCodes.Add(packingCode);
        //    }

        //    if (existingCodes.Count > 0)
        //    {
        //        var codes = string.Join(',', existingCodes);
        //        var errorResult = new List<ValidationResult>()
        //        {
        //            new ValidationResult("Kode packing sudah tersimpan: " + codes, new List<string> { "Message" }),
        //            new ValidationResult(codes, new List<string> { "PackingCodes" })
        //        };
        //        var validationContext = new ValidationContext(form, _serviceProvider, null);
        //        throw new ServiceValidationException(validationContext, errorResult);
        //    }

        //    var packingCodeAndIds = _outputProductionOrderRepository.ReadAll().Where(entity => !string.IsNullOrWhiteSpace(entity.ProductPackingCode)).Select(entity => new { entity.Id, entity.ProductPackingCode }).ToList();



        //    var stockOpnameForms = new List<DyeingPrintingProductPackingViewModel>();
        //    foreach (var packingCode in packingCodes)
        //    {
        //        var output = packingCodeAndIds.Where(entity => entity.ProductPackingCode.Contains(packingCode)).FirstOrDefault();
        //        var packing = (DyeingPrintingProductPackingViewModel)null;
        //        if (output != null)
        //        {
        //            packing = _outputProductionOrderRepository.ReadAll().Where(entity => output.Id == entity.Id).Select(s => new DyeingPrintingProductPackingViewModel()
        //            {
        //                Color = s.Color,
        //                FabricPackingId = s.FabricPackingId,
        //                FabricSKUId = s.FabricSKUId,
        //                ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
        //                {
        //                    Id = s.ProductionOrderId,
        //                    No = s.ProductionOrderNo,
        //                    OrderQuantity = s.ProductionOrderOrderQuantity,
        //                    Type = s.ProductionOrderType
        //                },
        //                HasPrintingProductPacking = s.HasPrintingProductPacking,
        //                HasPrintingProductSKU = s.HasPrintingProductSKU,
        //                Id = s.Id,
        //                Material = new Application.CommonViewModelObjectProperties.Material()
        //                {
        //                    Id = s.MaterialId,
        //                    Name = s.MaterialName
        //                },
        //                MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
        //                {
        //                    Name = s.MaterialConstructionName,
        //                    Id = s.MaterialConstructionId
        //                },
        //                MaterialWidth = s.MaterialWidth,
        //                Motif = s.Motif,
        //                ProductPackingCodes = s.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
        //                PackagingLength = s.PackagingLength,
        //                ProductPackingId = s.ProductPackingId,
        //                ProductSKUCode = s.ProductSKUCode,
        //                ProductSKUId = s.ProductSKUId,
        //                UomUnit = s.UomUnit,
        //                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
        //                {
        //                    Id = s.YarnMaterialId,
        //                    Name = s.YarnMaterialName
        //                },
        //                Quantity = s.PackagingQty,
        //                ProductPackingLength = s.PackagingLength,
        //                ProductPackingType = s.PackagingUnit,
        //                Grade = s.Grade,
        //                Unit = s.Unit,
        //                Buyer = s.Buyer,
        //                ProcessType = new ProcessType()
        //                {
        //                    Id = s.ProcessTypeId,
        //                    Name = s.ProcessTypeName
        //                },
        //                BuyerId = s.BuyerId,
        //                PackingInstruction = s.PackingInstruction,
        //                Construction = s.Construction
        //            }).FirstOrDefault();
        //        }

        //        if (packing == null)
        //        {
        //            packing = _stockOpnameProductionOrderRepository.ReadAll().Where(entity => entity.ProductPackingCode.Contains(packingCode)).Select(s => new DyeingPrintingProductPackingViewModel()
        //            {
        //                Color = s.Color,
        //                FabricPackingId = s.FabricPackingId,
        //                FabricSKUId = s.FabricSKUId,
        //                ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
        //                {
        //                    Id = s.ProductionOrderId,
        //                    No = s.ProductionOrderNo,
        //                    OrderQuantity = s.ProductionOrderOrderQuantity,
        //                    Type = s.ProductionOrderType
        //                },
        //                HasPrintingProductPacking = s.HasPrintingProductPacking,
        //                HasPrintingProductSKU = s.HasPrintingProductSKU,
        //                Id = s.Id,
        //                Material = new Application.CommonViewModelObjectProperties.Material()
        //                {
        //                    Id = s.MaterialId,
        //                    Name = s.MaterialName
        //                },
        //                MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
        //                {
        //                    Name = s.MaterialConstructionName,
        //                    Id = s.MaterialConstructionId
        //                },
        //                MaterialWidth = s.MaterialWidth,
        //                Motif = s.Motif,
        //                ProductPackingCodes = s.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
        //                ProductPackingId = s.ProductPackingId,
        //                ProductSKUCode = s.ProductSKUCode,
        //                ProductSKUId = s.ProductSKUId,
        //                UomUnit = s.UomUnit,
        //                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
        //                {
        //                    Id = s.YarnMaterialId,
        //                    Name = s.YarnMaterialName
        //                },
        //                Quantity = s.PackagingQty,
        //                ProductPackingLength = s.PackagingLength,
        //                PackagingLength = s.PackagingLength,
        //                ProductPackingType = s.PackagingUnit,
        //                Grade = s.Grade,
        //                Unit = s.Unit,
        //                Buyer = s.Buyer,
        //                ProcessType = new ProcessType()
        //                {
        //                    Id = s.ProcessTypeId,
        //                    Name = s.ProcessTypeName
        //                },
        //                DocumentNo = s.DocumentNo,
        //                BuyerId = s.BuyerId,
        //                PackingInstruction = s.PackingInstruction,
        //                Construction = s.Construction
        //            }).FirstOrDefault();
        //        }

        //        if (packing != null)
        //            stockOpnameForms.Add(packing);
        //    }

        //    stockOpnameForms = stockOpnameForms.Distinct().ToList();
        //    //var 
        //    var forms = stockOpnameForms
        //        .GroupBy(element => new
        //        {
        //            element.ProductionOrder.No,
        //            element.ProductSKUCode,
        //            element.PackagingLength
        //        })
        //        .Select(element => new
        //        {
        //            OrderNo = element.Key.No,
        //            SKUCode = element.Key.ProductSKUCode,
        //            PackingLength = element.Key.PackagingLength,
        //            Id = element.FirstOrDefault().Id
        //        })
        //        .ToList();

        //    var result = 0;

        //    if (forms.Count > 0)
        //    {
        //        var items = new List<StockOpnameWarehouseProductionOrderViewModel>();

        //        foreach (var itemForm in forms)
        //        {
        //            var stockOpnameForm = stockOpnameForms.FirstOrDefault(element => element.Id == itemForm.Id);
        //            var scannedPackingCodes = packingCodes.Where(element => stockOpnameForm.ProductPackingCodes.Contains(element)).ToList();
        //            var scannedQuantity = stockOpnameForm.ProductPackingCodes.Where(element => packingCodes.Contains(element)).Count();
        //            var productIds = await _productPackingService.GetByCode(string.Join(',', scannedPackingCodes));
        //            var item = new StockOpnameWarehouseProductionOrderViewModel()
        //            {
        //                Balance = stockOpnameForm.ProductPackingLength * scannedQuantity,
        //                BuyerId = stockOpnameForm.BuyerId,
        //                DocumentNo = stockOpnameForm.DocumentNo,
        //                MaterialWidth = stockOpnameForm.MaterialWidth,
        //                PackingInstruction = stockOpnameForm.PackingInstruction,
        //                ProductionOrder = new ProductionOrder()
        //                {
        //                    Code = stockOpnameForm.ProductionOrder.Code,
        //                    Id = stockOpnameForm.ProductionOrder.Id,
        //                    No = stockOpnameForm.ProductionOrder.No,
        //                    OrderQuantity = stockOpnameForm.ProductionOrder.OrderQuantity,
        //                    Type = stockOpnameForm.ProductionOrder.Type
        //                },
        //                Construction = stockOpnameForm.Construction,
        //                Unit = stockOpnameForm.Unit,
        //                Buyer = stockOpnameForm.Buyer,
        //                Color = stockOpnameForm.Color,
        //                Motif = stockOpnameForm.Motif,
        //                Grade = stockOpnameForm.Grade,
        //                PackagingQty = scannedQuantity,
        //                PackagingUnit = stockOpnameForm.ProductPackingType,
        //                Uom = new UnitOfMeasurement()
        //                {
        //                    Unit = stockOpnameForm.UomUnit
        //                },
        //                UomUnit = stockOpnameForm.UomUnit,
        //                PackagingLength = stockOpnameForm.ProductPackingLength,
        //                MaterialConstruction = new MaterialConstruction()
        //                {
        //                    Code = stockOpnameForm.MaterialConstruction.Code,
        //                    Id = stockOpnameForm.MaterialConstruction.Id,
        //                    Name = stockOpnameForm.MaterialConstruction.Name
        //                },
        //                ProcessType = new ProcessType()
        //                {
        //                    Id = stockOpnameForm.ProcessType.Id,
        //                    Name = stockOpnameForm.ProcessType.Name
        //                },
        //                Material = new Material()
        //                {
        //                    Code = stockOpnameForm.Material.Code,
        //                    Name = stockOpnameForm.Material.Name,
        //                    Id = stockOpnameForm.Material.Id
        //                },
        //                YarnMaterial = new YarnMaterial()
        //                {
        //                    Id = stockOpnameForm.YarnMaterial.Id,
        //                    Name = stockOpnameForm.YarnMaterial.Name
        //                },
        //                IsStockOpname = true,
        //                PackingCodes = string.Join(',', scannedPackingCodes),
        //                ProductSKUId = stockOpnameForm.ProductSKUId,
        //                FabricSKUId = stockOpnameForm.FabricSKUId,
        //                ProductSKUCode = stockOpnameForm.ProductSKUCode,
        //                ProductPackingId = productIds == null ? stockOpnameForm.ProductPackingId : productIds.Id,
        //                FabricPackingId = stockOpnameForm.FabricPackingId,
        //                HasPrintingProductSKU = stockOpnameForm.HasPrintingProductSKU

        //            };

        //            if (items.Where(s => s.PackingCodes == item.PackingCodes).Count() == 0)
        //            {
        //                items.Add(item);
        //            }
        //        }

        //        var createForm = new StockOpnameWarehouseViewModel()
        //        {
        //            Type = "STOCK OPNAME",
        //            Date = DateTimeOffset.UtcNow,
        //            WarehousesProductionOrders = items,
        //            IsStockOpname = true
        //        };

        //        result = await CreateStockOpnameV2(createForm);

        //    }

        //    //if (stockOpnameForms.Count > 0)
        //    //{
        //    //    foreach (var stockOpnameForm in stockOpnameForms)
        //    //    {
        //    //        var scannedPackingCodes = packingCodes.Where(element => stockOpnameForm.ProductPackingCodes.Contains(element)).ToList();
        //    //        var scannedQuantity = stockOpnameForm.ProductPackingCodes.Where(element => packingCodes.Contains(element)).Count();
        //    //        var item = new StockOpnameWarehouseProductionOrderViewModel()
        //    //        {
        //    //            Balance = stockOpnameForm.Balance,
        //    //            BuyerId = stockOpnameForm.BuyerId,
        //    //            DocumentNo = stockOpnameForm.DocumentNo,
        //    //            MaterialWidth = stockOpnameForm.MaterialWidth,
        //    //            PackingInstruction = stockOpnameForm.PackingInstruction,
        //    //            ProductionOrder = new ProductionOrder()
        //    //            {
        //    //                Code = stockOpnameForm.ProductionOrder.Code,
        //    //                Id = stockOpnameForm.ProductionOrder.Id,
        //    //                No = stockOpnameForm.ProductionOrder.No,
        //    //                OrderQuantity = stockOpnameForm.ProductionOrder.OrderQuantity,
        //    //                Type = stockOpnameForm.ProductionOrder.Type
        //    //            },
        //    //            Construction = stockOpnameForm.Construction,
        //    //            Unit = stockOpnameForm.Unit,
        //    //            Buyer = stockOpnameForm.Buyer,
        //    //            Color = stockOpnameForm.Color,
        //    //            Motif = stockOpnameForm.Motif,
        //    //            Grade = stockOpnameForm.Grade,
        //    //            PackagingQty = scannedQuantity,
        //    //            PackagingUnit = stockOpnameForm.ProductPackingType,
        //    //            Uom = new UnitOfMeasurement()
        //    //            {
        //    //                Unit = stockOpnameForm.UomUnit
        //    //            },
        //    //            UomUnit = stockOpnameForm.UomUnit,
        //    //            PackagingLength = stockOpnameForm.PackagingLength,
        //    //            MaterialConstruction = new MaterialConstruction()
        //    //            {
        //    //                Code = stockOpnameForm.MaterialConstruction.Code,
        //    //                Id = stockOpnameForm.MaterialConstruction.Id,
        //    //                Name = stockOpnameForm.MaterialConstruction.Name
        //    //            },
        //    //            ProcessType = new ProcessType()
        //    //            {
        //    //                Id = stockOpnameForm.ProcessType.Id,
        //    //                Name = stockOpnameForm.ProcessType.Name
        //    //            },
        //    //            Material = new Material()
        //    //            {
        //    //                Code = stockOpnameForm.Material.Code,
        //    //                Name = stockOpnameForm.Material.Name,
        //    //                Id = stockOpnameForm.Material.Id
        //    //            },
        //    //            YarnMaterial = new YarnMaterial()
        //    //            {
        //    //                Id = stockOpnameForm.YarnMaterial.Id,
        //    //                Name = stockOpnameForm.YarnMaterial.Name
        //    //            },
        //    //            IsStockOpname = true,
        //    //            PackingCodes = string.Join(',', scannedPackingCodes),
        //    //            ProductSKUId = stockOpnameForm.ProductSKUId,
        //    //            FabricSKUId = stockOpnameForm.FabricSKUId,
        //    //            ProductSKUCode = stockOpnameForm.ProductSKUCode,
        //    //            ProductPackingId = stockOpnameForm.ProductPackingId,
        //    //            FabricPackingId = stockOpnameForm.FabricPackingId,
        //    //            ProductPackingCodes = stockOpnameForm.ProductPackingCodes,
        //    //            HasPrintingProductSKU = stockOpnameForm.HasPrintingProductSKU

        //    //        };

        //    //        items.Add(item);
        //    //    }

        //    //    var createForm = new StockOpnameWarehouseViewModel()
        //    //    {
        //    //        Type = "STOCK OPNAME",
        //    //        Date = DateTimeOffset.UtcNow,
        //    //        WarehousesProductionOrders = items,
        //    //        IsStockOpname = true
        //    //    };

        //    //    //result = await CreateStockOpnameV2(createForm);
        //    //}

        //    return result;
        //}

        //Tanggal 23-03-2023 By Gama
        //private async Task<int> CreateStockOpnameV2(StockOpnameWarehouseViewModel viewModel)
        //{
        //    int result = 0;
        //    var model = _stockOpnameRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
        //                                                                                s.Date.AddHours(7).ToString("dd/MM/YYYY").Equals(viewModel.Date.AddHours(7).ToString("dd/MM/YYYY")) &&
        //                                                                                s.Type == DyeingPrintingArea.STOCK_OPNAME
        //                                                                                && s.IsStockOpname
        //                                                                                && !s.IsDeleted);
        //    //viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.Where(s => s.IsSave).ToList();
        //    viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.ToList();
        //    if (model == null)
        //    {
        //        int totalCurrentYearData = _stockOpnameRepository.ReadAllIgnoreQueryFilter()
        //                                                    .Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
        //                                                                s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.STOCK_OPNAME);



        //        string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.Area);

        //        model = new DyeingPrintingStockOpnameModel(DyeingPrintingArea.GUDANGJADI, bonNo, viewModel.Date, DyeingPrintingArea.STOCK_OPNAME,
        //                                                  viewModel.WarehousesProductionOrders.Select(s =>
        //                                                    new DyeingPrintingStockOpnameProductionOrderModel(
        //                                                        s.Balance,
        //                                                        s.BuyerId,
        //                                                        s.Buyer,
        //                                                        s.Color,
        //                                                        s.Construction,
        //                                                        s.DocumentNo,
        //                                                        s.Grade,
        //                                                        s.MaterialConstruction.Id,
        //                                                        s.MaterialConstruction.Name,
        //                                                        s.Material.Id,
        //                                                        s.Material.Name,
        //                                                        s.MaterialWidth,
        //                                                        s.Motif,
        //                                                        s.PackingInstruction,
        //                                                        s.PackagingQty,
        //                                                        s.PackagingLength,
        //                                                        s.PackagingType,
        //                                                        s.PackagingUnit,
        //                                                        s.ProductionOrder.Id,
        //                                                        s.ProductionOrder.No,
        //                                                        s.ProductionOrder.Type,
        //                                                        s.ProductionOrder.OrderQuantity,
        //                                                        s.ProcessType.Id,
        //                                                        s.ProcessType.Name,
        //                                                        s.YarnMaterial.Id,
        //                                                        s.YarnMaterial.Name,
        //                                                        s.Remark,
        //                                                        s.Status,
        //                                                        s.Unit,
        //                                                        s.UomUnit,
        //                                                        true,
        //                                                        s.PackingCodes,
        //                                                        s.ProductSKUId,
        //                                                        s.FabricSKUId,
        //                                                        s.ProductSKUCode,
        //                                                        s.ProductPackingId,
        //                                                        s.FabricPackingId,
        //                                                        false
        //                                                        )).ToList(), true);


        //        result = await _stockOpnameRepository.InsertAsync(model);

        //        await _stockOpnameRepository.UpdateAsync(model.Id, model);

        //    }
        //    else
        //    {
        //        foreach (var item in viewModel.WarehousesProductionOrders)
        //        {



        //            var modelItem = new DyeingPrintingStockOpnameProductionOrderModel(
        //                                                        item.Balance,
        //                                                        item.BuyerId,
        //                                                        item.Buyer,
        //                                                        item.Color,
        //                                                        item.Construction,
        //                                                        item.DocumentNo,
        //                                                        item.Grade,
        //                                                        item.MaterialConstruction.Id,
        //                                                        item.MaterialConstruction.Name,
        //                                                        item.Material.Id,
        //                                                        item.Material.Name,
        //                                                        item.MaterialWidth,
        //                                                        item.Motif,
        //                                                        item.PackingInstruction,
        //                                                        item.PackagingQty,
        //                                                        item.PackagingLength,
        //                                                        item.PackagingType,
        //                                                        item.PackagingUnit,
        //                                                        item.ProductionOrder.Id,
        //                                                        item.ProductionOrder.No,
        //                                                        item.ProductionOrder.Type,
        //                                                        item.ProductionOrder.OrderQuantity,
        //                                                        item.ProcessType.Id,
        //                                                        item.ProcessType.Name,
        //                                                        item.YarnMaterial.Id,
        //                                                        item.YarnMaterial.Name,
        //                                                        item.Remark,
        //                                                        item.Status,
        //                                                        item.Unit,
        //                                                        item.UomUnit,
        //                                                        true,
        //                                                        item.PackingCodes,
        //                                                        item.ProductSKUId,
        //                                                        item.FabricSKUId,
        //                                                        item.ProductSKUCode,
        //                                                        item.ProductPackingId,
        //                                                        item.FabricPackingId,
        //                                                        false
        //                                                        );

        //            modelItem.DyeingPrintingStockOpnameId = model.Id;

        //            result += await _stockOpnameProductionOrderRepository.InsertAsync(modelItem);

        //        }

        //    }

        //    return result;
        //}

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

        

        public List<BarcodeInfoViewModel> GetMonitoringScan(long productionOrderId, string barcode, string documentNo, string grade, string userFilter)
        {
            var query = _stockOpnameProductionOrderRepository.ReadAll().Where(x => x.IsStockOpname == true);

            //var query = _stockOpnameProductionOrderRepository.GetDbSet().Where(x => x.IsStockOpname == true);

            if (!string.IsNullOrEmpty(documentNo))
            {
                query = query.Where(s => s.DocumentNo.Contains(documentNo));
            }

            if (!string.IsNullOrEmpty(barcode))
            {
                query = query.Where(s => s.ProductPackingCode.Contains(barcode));
            }

            if (!string.IsNullOrEmpty(grade))
            {
                query = query.Where(s => s.Grade == grade);
            }
            if (productionOrderId != 0)
            {
                query = query.Where(s => s.ProductionOrderId == productionOrderId);
            }
            if (!string.IsNullOrEmpty(userFilter))
            {
                query = query.Where(s => s.CreatedBy.Contains(userFilter));
            }


            //var barcodeList = query.Select(x => new NewBarcodeInfo()
            //{
            //    productionOrderNo = x.ProductionOrderNo,
            //    grade = x.Grade,
            //    productPackingCodes = x.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
            //    PackagingQty = x.PackagingQty,
            //    PackagingLength = x.PackagingLength,
            //    Balance = x.Balance,
            //    DocumentNo = x.DocumentNo,
            //    CreatedBy = x.CreatedBy

            //}).OrderBy(x => x.CreatedUtc);

            _barcodes = new List<BarcodeInfoViewModel>();
            var result = query.Select(x => new StockOpnameWarehouseProductionOrderViewModel()
            {
                ProductionOrderNo = x.ProductionOrderNo,
                Grade = x.Grade,
                ProductPackingCodes = x.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
                PackagingQty = x.PackagingQty,
                PackagingLength = x.PackagingLength,
                Balance = x.Balance,
                DocumentNo = x.DocumentNo,
                CreatedBy = x.CreatedBy

            }).OrderBy(x => x.DocumentNo).ToList();


            //foreach (var data in result) {

            //    foreach (var packingCode in data.ProductPackingCodes) {
            //        var barcodeInfo = new BarcodeInfo()
            //        {

            //            OrderNo = data.ProductionOrderNo,
            //            PackingCode = packingCode,
            //            PackingLength = data.PackagingLength,
            //            Balance = data.Balance,
            //            CreatedBy = data.CreatedBy,

            //            UOMSKU = data.UomUnit,
            //            DocumentNo = data.DocumentNo,
            //            Grade = data.Grade
            //        };
            //        _barcodes.Add(barcodeInfo);
            //    }
            //}

            foreach (var data in result)
            {

                foreach (var packingCode in data.ProductPackingCodes)
                {
                    var barcodeInfo = new BarcodeInfoViewModel()
                    {

                        OrderNo = data.ProductionOrderNo,
                        PackingCode = packingCode,
                        PackingLength = data.PackagingLength,
                        Balance = data.Balance,
                        CreatedBy = data.CreatedBy,
                        PackagingQty = 1,

                        UOMSKU = data.UomUnit,
                        DocumentNo = data.DocumentNo,
                        Grade = data.Grade
                    };
                    _barcodes.Add(barcodeInfo);
                }


            }



           




            return _barcodes ;
        }

        public MemoryStream GenerateExcelMonitoringScan(long productionOrderId, string barcode, string documentNo, string grade, string userFilter)
        {

            var query = GetMonitoringScan(productionOrderId, barcode, documentNo, grade, userFilter);

            var indexNumber = 1;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO.", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "BARCODE", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY PACKING", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY PER ROLL", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "JALUR", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "USER", DataType = typeof(string) });

            decimal qtyRoll = 0;
            double qtyBalance = 0;
            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", 0, 0, 0, "", "");
            }
            else
            {
                
                foreach (var item in query)
                {
                    //var dataIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                    //var dataOut = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateOut.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                    //qtyRoll += item.PackagingQty;
                    qtyBalance += item.Balance;
                    dt.Rows.Add(indexNumber,
                                item.OrderNo,
                                item.PackingCode ,
                                item.PackagingQty,
                               item.PackingLength,
                                item.Balance,
                                item.DocumentNo,
                                item.CreatedBy
                                );
                    indexNumber++;
                }
            }

            ExcelPackage package = new ExcelPackage();
            #region Header
            var sheet = package.Workbook.Worksheets.Add("Bon Keluar Aval");

            //sheet.Cells[1, 1].Value = "TANGGAL";
            //sheet.Cells[1, 2].Value = model.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

            //sheet.Cells[2, 1].Value = "NO. BON";
            //sheet.Cells[2, 2].Value = model.BonNo;
            //sheet.Cells[2, 2, 2, 3].Merge = true;

            var row = 3;
            var merge = 4;

            sheet.Cells[row, 1].Value = "NO.";
            sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 1, merge, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 1, merge, 1].Merge = true;

            sheet.Cells[row, 2].Value = "NO.SPP";
            sheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 2, merge, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 2, merge, 2].Merge = true;

            sheet.Cells[row, 3].Value = "BARCODE";
            sheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 3, merge, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 3, merge, 3].Merge = true;

            sheet.Cells[row, 4].Value = "QTY ROLL";
            sheet.Cells[row, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 4, merge, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 4, merge, 4].Merge = true;

            sheet.Cells[row, 5].Value = "QTY PER ROLL";
            sheet.Cells[row, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 5, merge, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 5, merge, 5].Merge = true;

            sheet.Cells[row, 6].Value = "QUANTITY";
            sheet.Cells[row, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 6, merge, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 6, merge, 6].Merge = true;

            sheet.Cells[row, 7].Value = "JALUR";
            sheet.Cells[row, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 7, merge, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 7, merge, 7].Merge = true;

            sheet.Cells[row, 8].Value = "USER";
            sheet.Cells[row, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 8, merge, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 8, merge, 8].Merge = true;
            #endregion

            //var a = query.Count();
            
            var a = query.Count();
            sheet.Cells[$"A{5 + a}"].Value = "T O T A L  . . . . . . . . . . . . . . .";
            sheet.Cells[$"A{5 + a}:C{6 + a}"].Merge = true;
            sheet.Cells[$"A{5 + a}:C{6 + a}"].Style.Font.Bold = true;
            sheet.Cells[$"A{5 + a}:C{6 + a}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{5 + a}:C{6 + a}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            sheet.Cells[$"D{5 + a}"].Value = qtyRoll;
            sheet.Cells[$"F{5 + a}"].Value = qtyBalance;
            //sheet.Cells[$"K{6 + a}"].Value = CorrQtyTotal;
            //sheet.Cells[$"M{6 + a}"].Value = ExpendQtyTotal;
            //sheet.Cells[$"O{6 + a}"].Value = EndingQtyTotal;

            int tableRowStart = 5;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        public List<StockOpnameWarehouseProductionOrderViewModel> getDatabyCode(string itemData)
        {
            var stockOpnameQuery = _stockOpnameProductionOrderRepository.ReadAll().Where(entity => entity.ProductPackingCode == itemData);

            var stockOpnameMutationQuery = _stockOpnameMutationItemsRepository.ReadAll().Where(entity => entity.ProductPackingCode == itemData);
            var resultOpname = stockOpnameQuery.GroupBy(d => new { d.ProductPackingCode }).Select(e => new StockOpnameWarehouseProductionOrderViewModel()
            {

                Balance = e.Sum(s => s.Balance),
                Color = e.First().Color,
                Construction = e.First().Construction,
                Grade = e.First().Grade,
                Motif = e.First().Motif,
                PackagingQty = e.Sum(s => s.PackagingQty),
                PackagingLength = e.First().PackagingLength,
                PackagingType = e.First().PackagingType,
                PackagingUnit = e.First().PackagingUnit,
                ProductionOrder = new ProductionOrder
                {
                    Id = e.First().ProductionOrderId,
                    No = e.First().ProductionOrderNo,
                    Type = e.First().ProductionOrderType,
                    OrderQuantity = e.First().ProductionOrderOrderQuantity
                },
                ProcessTypeId = e.First().ProcessTypeId,
                ProcessTypeName = e.First().ProcessTypeName,
                Unit = e.First().Unit,
                UomUnit = e.First().UomUnit,
                ProductSKUId = e.First().ProductSKUId,
                FabricSKUId = e.First().FabricSKUId,
                ProductSKUCode = e.First().ProductSKUCode,
                ProductPackingId = e.First().ProductPackingId,
                FabricPackingId = e.First().FabricPackingId,
                ProductPackingCode = e.Key.ProductPackingCode,
                ProductionOrderNo = e.First().ProductionOrderNo,
                Material = new Material { 
                    Id = e.First().MaterialId,
                    Name = e.First().MaterialName
                },
                Track = new Track { 
                    Id = e.First().TrackId,
                    Type = e.First().TrackType,
                    Name = e.First().TrackName,
                    Box = e.First().TrackBox
                }
             
            }).ToList();

            var resultMutation = stockOpnameMutationQuery.GroupBy(d => new { d.ProductPackingCode }).Select(e => new StockOpnameWarehouseProductionOrderViewModel()
            {

                //Balance = 0,
                //Color = "",
                //Construction = "",
                //Grade = "",
                //Motif = "",
                PackagingQty = e.Sum(s => s.PackagingQty)*-1,
                //PackagingLength = "",
                //PackagingType = e.First().PackagingType,
                //PackagingUnit = e.First().PackagingUnit,
                //ProductionOrder = new ProductionOrder
                //{
                //    Id = e.First().ProductionOrderId,
                //    No = e.First().ProductionOrderNo,
                //    Type = e.First().ProductionOrderType,
                //    OrderQuantity = e.First().ProductionOrderOrderQuantity
                //},
                //ProcessTypeId = e.First().ProcessTypeId,
                //ProcessTypeName = e.First().ProcessTypeName,
                //Unit = e.First().Unit,
                //UomUnit = e.First().UomUnit,
                //ProductSKUId = e.First().ProductSKUId,
                //FabricSKUId = e.First().FabricSKUId,
                //ProductSKUCode = e.First().ProductSKUCode,
                //ProductPackingId = e.First().ProductPackingId,
                //FabricPackingId = e.First().FabricPackingId,
                ProductPackingCode = e.Key.ProductPackingCode,
                //Track = new Track { 
                //    Id = 0,
                //    Name = "",
                //    Type = "",
                //}
                //ProductionOrderNo = e.First().ProductionOrderNo,
                //Material = new Material
                //{
                //    Id = e.First().MaterialId,
                //    Name = e.First().MaterialName
                //},

            }).ToList();
            var joinResult = resultOpname.Union(resultMutation);

            var result = joinResult.GroupBy(d => new { d.ProductPackingCode }).Select(e => new StockOpnameWarehouseProductionOrderViewModel()
            {
                Balance = e.Sum(s => s.Balance),
                Color = e.First().Color,
                Construction = e.First().Construction,
                Grade = e.First().Grade,
                Motif = e.First().Motif,
                PackagingQty = e.Sum(s => s.PackagingQty),
                PackagingLength = e.First().PackagingLength,
                PackagingType = e.First().PackagingType,
                PackagingUnit = e.First().PackagingUnit,
                ProductionOrder = new ProductionOrder
                {
                    Id = e.First().ProductionOrder.Id,
                    No = e.First().ProductionOrder.No,
                    Type = e.First().ProductionOrder.Type,
                    OrderQuantity = e.First().ProductionOrder.OrderQuantity
                },
                ProcessTypeId = e.First().ProcessTypeId,
                ProcessTypeName = e.First().ProcessTypeName,
                Unit = e.First().Unit,
                UomUnit = e.First().UomUnit,
                ProductSKUId = e.First().ProductSKUId,
                FabricSKUId = e.First().FabricSKUId,
                ProductSKUCode = e.First().ProductSKUCode,
                ProductPackingId = e.First().ProductPackingId,
                FabricPackingId = e.First().FabricPackingId,
                ProductPackingCode = e.Key.ProductPackingCode,
                ProductionOrderNo = e.First().ProductionOrderNo,
                Material = new Material
                {
                    Id = e.First().Material.Id,
                    Name = e.First().Material.Name
                },
                Track = new Track
                {
                    Id = e.First().Track.Id,
                    Type = e.First().Track.Type,
                    Name = e.First().Track.Name,
                    Box = e.First().Track.Box
                }
            }).ToList();

            return result;


        }

        private IEnumerable<ReportSOViewModel> GetDataIN(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, string barcode, int track, string queryType, int offset)
        {
            IQueryable<DyeingPrintingStockOpnameProductionOrderModel> stockOpnameItemsQuery;

            if (queryType == "Mutation")
            {
                stockOpnameItemsQuery = _stockOpnameProductionOrderRepository.ReadAll().Where(s =>
            s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date);
            }
            else {
                stockOpnameItemsQuery = _stockOpnameProductionOrderRepository.ReadAll().Where(s =>
            s.CreatedUtc.AddHours(7).Date < dateFrom.Date);
            }
            

            if (productionOrderId != 0)
            {
                stockOpnameItemsQuery = stockOpnameItemsQuery.Where(s => s.ProductionOrderId == productionOrderId);
            }

            if (!string.IsNullOrEmpty(barcode))
            {
                stockOpnameItemsQuery = stockOpnameItemsQuery.Where(s => s.ProductPackingCode.Contains(barcode));
            }

            if (track != 0)
            {
                stockOpnameItemsQuery = stockOpnameItemsQuery.Where(s => s.TrackId == track);
            }

            var result = stockOpnameItemsQuery.GroupBy( s => new { s.ProductionOrderId, s.ProductPackingCode, s.Grade, s.PackagingUnit, /*s.TrackId*/}).Select( d => new ReportSOViewModel()
            { 
                ProductionOrderId = d.Key.ProductionOrderId,
                ProductionOrderNo = d.First().ProductionOrderNo,
                ProductPackingCode = d.Key.ProductPackingCode,
                ProcessTypeName = d.First().ProcessTypeName,
                PackagingUnit = d.Key.PackagingUnit,
                Grade = d.First().Grade,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Motif = d.First().Motif,
                BuyerName = d.First().Buyer,
                
                //TrackId = d.First().TrackId,
                //TrackName = d.First().TrackName +"-"+ d.First().TrackType,
                SaldoBegin = 0,
                InQty = d.Sum( s => s.Balance),
                OutQty = 0,
                AdjOutQty = 0,
                Total = d.Sum( s=> s.Balance),


            });

            return result;
        }

        private IEnumerable<ReportSOViewModel> GetDataOUT(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, string barcode, int track, string queryType, int offset)
        {
            IQueryable<DyeingPrintingStockOpnameMutationItemModel> stockOpnameMutationItemsQuery;

            if (queryType == "Mutation")
            {
                stockOpnameMutationItemsQuery = _stockOpnameMutationItemsRepository.ReadAll().Where(s =>
            s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date && s.TypeOut == DyeingPrintingArea.SO );
            }
            else {
                stockOpnameMutationItemsQuery = _stockOpnameMutationItemsRepository.ReadAll().Where(s =>
            s.CreatedUtc.AddHours(7).Date < dateFrom.Date && s.TypeOut == DyeingPrintingArea.SO);
            }
            

            if (productionOrderId != 0)
            {
                stockOpnameMutationItemsQuery = stockOpnameMutationItemsQuery.Where(s => s.ProductionOrderId == productionOrderId);
            }

            if (!string.IsNullOrEmpty(barcode))
            {
                stockOpnameMutationItemsQuery = stockOpnameMutationItemsQuery.Where(s => s.ProductPackingCode.Contains(barcode));
            }

            if (track != 0)
            {
                stockOpnameMutationItemsQuery = stockOpnameMutationItemsQuery.Where(s => s.TrackId == track);
            }

            var result = stockOpnameMutationItemsQuery.GroupBy(s => new { s.ProductionOrderId, s.ProductPackingCode, s.Grade, s.PackagingUnit, /*s.TrackId*/ }).Select(d => new ReportSOViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                ProductionOrderNo = d.First().ProductionOrderNo,
                ProductPackingCode = d.Key.ProductPackingCode,
                ProcessTypeName = d.First().ProcessTypeName,
                PackagingUnit = d.Key.PackagingUnit,
                Grade = d.First().Grade,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Motif = d.First().Motif,
                
                //TrackId = d.Key.TrackId,
                //TrackName = d.First().TrackName + " - " + d.First().TrackType,
                SaldoBegin = 0,
                InQty = 0,
                OutQty = d.Sum(s => s.Balance),
                AdjOutQty = 0,
                Total = d.Sum(s => s.Balance) * -1,


            });

            return result;
        }
        private IEnumerable<ReportSOViewModel> GetDataAdjOut(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, string barcode, int track, string queryType, int offset)
        {
            IQueryable<DyeingPrintingStockOpnameMutationItemModel> stockOpnameMutationItemsQuery;

            if (queryType == "Mutation")
            {
                stockOpnameMutationItemsQuery = _stockOpnameMutationItemsRepository.ReadAll().Where(s =>
            s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date && s.TypeOut == DyeingPrintingArea.ADJ_OUT);
            }
            else
            {
                stockOpnameMutationItemsQuery = _stockOpnameMutationItemsRepository.ReadAll().Where(s =>
            s.CreatedUtc.AddHours(7).Date < dateFrom.Date && s.TypeOut == DyeingPrintingArea.ADJ_OUT);
            }


            if (productionOrderId != 0)
            {
                stockOpnameMutationItemsQuery = stockOpnameMutationItemsQuery.Where(s => s.ProductionOrderId == productionOrderId);
            }

            if (!string.IsNullOrEmpty(barcode))
            {
                stockOpnameMutationItemsQuery = stockOpnameMutationItemsQuery.Where(s => s.ProductPackingCode.Contains(barcode));
            }

            if (track != 0)
            {
                stockOpnameMutationItemsQuery = stockOpnameMutationItemsQuery.Where(s => s.TrackId == track);
            }

            var result = stockOpnameMutationItemsQuery.GroupBy(s => new { s.ProductionOrderId, s.ProductPackingCode, s.Grade, s.PackagingUnit, /*s.TrackId*/ }).Select(d => new ReportSOViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                ProductionOrderNo = d.First().ProductionOrderNo,
                ProductPackingCode = d.Key.ProductPackingCode,
                ProcessTypeName = d.First().ProcessTypeName,
                PackagingUnit = d.Key.PackagingUnit,
                Grade = d.First().Grade,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Motif = d.First().Motif,

                //TrackId = d.Key.TrackId,
                //TrackName = d.First().TrackName + " - " + d.First().TrackType,
                SaldoBegin = 0,
                InQty = 0,
                OutQty = 0,
                AdjOutQty = d.Sum(s => s.Balance),
                Total = d.Sum(s => s.Balance) * -1,


            });

            return result;
        }
        private IEnumerable<ReportSOViewModel> GetDataBegin(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, string barcode, int track, string queryType, int offset)
        {

            var dataIN = GetDataIN(dateFrom, dateTo, productionOrderId, barcode, track, queryType, offset);
            var dataOut = GetDataOUT(dateFrom, dateTo, productionOrderId, barcode, track, queryType, offset);
            var dataAdjOut = GetDataAdjOut(dateFrom, dateTo, productionOrderId, barcode, track, queryType, offset);

            var queyJoin = dataIN.Concat(dataOut).Concat(dataAdjOut);

            var result = queyJoin.GroupBy(s => new { s.ProductionOrderId, s.ProductPackingCode, s.Grade, s.PackagingUnit, /*s.TrackId*/ }).Select(d => new ReportSOViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                ProductionOrderNo = d.First().ProductionOrderNo,
                ProductPackingCode = d.Key.ProductPackingCode,
                ProcessTypeName = d.First().ProcessTypeName,
                PackagingUnit = d.Key.PackagingUnit,
                Grade = d.First().Grade,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Motif = d.First().Motif,
                BuyerName = d.First().BuyerName,
                //TrackId = d.Key.TrackId,
                //TrackName = d.First().TrackName,
                SaldoBegin = d.Sum(s => s.InQty) - d.Sum(s => s.OutQty) - d.Sum( s => s.AdjOutQty),
                InQty = 0,
                OutQty = 0,
                AdjOutQty=0,
                Total = d.Sum(s => s.InQty) - d.Sum(s => s.OutQty) - d.Sum(s => s.AdjOutQty),
            });


            return result;
        }


        public List<ReportSOViewModel> GetReportDataSO(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, string barcode, int track, int offset )
        {
            var dataIN = GetDataIN(dateFrom, dateTo, productionOrderId, barcode, track, "Mutation", offset);
            var dataOut = GetDataOUT(dateFrom, dateTo, productionOrderId, barcode, track, "Mutation", offset);
            var dataAdjOut = GetDataAdjOut(dateFrom, dateTo, productionOrderId, barcode, track, "Mutation", offset);
            var dataBegin = GetDataBegin(dateFrom, dateTo, productionOrderId, barcode, track, "Begin", offset);


            var queryJoin = dataIN.Concat(dataOut).Concat(dataAdjOut).Concat(dataBegin);

            var result = queryJoin.GroupBy(s => new { s.ProductionOrderId, s.ProductPackingCode, s.Grade, s.PackagingUnit, /*s.TrackId*/ }).Select(d => new ReportSOViewModel() 
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                ProductionOrderNo = d.First().ProductionOrderNo,
                ProductPackingCode = d.Key.ProductPackingCode,
                ProcessTypeName = d.First().ProcessTypeName,
                PackagingUnit = d.Key.PackagingUnit,
                Grade = d.First().Grade,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Motif = d.First().Motif,
                BuyerName = d.First().BuyerName,
                //TrackId = d.Key.TrackId,
                //TrackName = d.First().TrackName,
                SaldoBegin = d.Sum(s => s.SaldoBegin),
                InQty = d.Sum(s=> s.InQty),
                OutQty = d.Sum(s => s.OutQty),
                AdjOutQty = d.Sum( s=> s.AdjOutQty),
                Total = d.Sum(s => s.Total),

            }).ToList();

            return result;
        }

        public MemoryStream GenerateExcel (DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, string barcode, int track, int offset)
        {
            var data = GetReportDataSO(dateFrom, dateTo, productionOrderId, barcode, track, offset);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Barcode", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Saldo Awal", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Masuk SO", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keluar", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Adj Keluar", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Total", DataType = typeof(double) });
            

            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", 0, 0, 0, 0, 0 );
            }
            else
            {
                double saldoBegin = 0;
                double inQty = 0;
                double outQty = 0;
                double adjOutQty = 0;
                double total = 0;
                foreach (var item in data)
                {
                   // var sldbegin = item.SaldoBegin;
                    //saldoBegin =+ item.SaldoBegin;
                    dt.Rows.Add(item.ProductionOrderNo, item.BuyerName, item.Construction, item.Color, item.Motif, item.Grade,item.PackagingUnit, item.ProductPackingCode,
                        item.SaldoBegin, item.InQty, item.OutQty, item.AdjOutQty, item.Total);
                    
                    saldoBegin += item.SaldoBegin;
                    inQty += item.InQty;
                    outQty += item.OutQty;
                    adjOutQty += item.AdjOutQty;
                    total += item.Total;
                }

               dt.Rows.Add("", "", "", "", "", "", "", "", saldoBegin, inQty, outQty, adjOutQty, total);
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, string.Format("Laporan Stock {0}", "SO")) }, true);

        }

        public List<ReportSOViewModel> GetMonitoringSO(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int track, int offset)
        {

            IQueryable<DyeingPrintingStockOpnameModel> stockOpnameQuery;
            IQueryable<DyeingPrintingStockOpnameProductionOrderModel> stockOpnameItemsQuery;
            if (dateFrom == DateTimeOffset.MinValue && dateTo == DateTimeOffset.MinValue)
            {
                //stockOpnameQuery = _stockOpnameRepository.ReadAll();
                stockOpnameItemsQuery = _stockOpnameProductionOrderRepository.ReadAll();
            }
            else
            {
                //stockOpnameQuery = _stockOpnameRepository.ReadAll().Where(s =>
                //                    s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date);
                stockOpnameItemsQuery = _stockOpnameProductionOrderRepository.ReadAll().Where(s =>
                                        s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date);
            }
            
            


            if (productionOrderId != 0)
            {
                stockOpnameItemsQuery = stockOpnameItemsQuery.Where(s => s.ProductionOrderId == productionOrderId);
            }

            if (track != 0)
            {
                stockOpnameItemsQuery = stockOpnameItemsQuery.Where(s => s.TrackId == track);
            }

            //var query = (from a in stockOpnameQuery
            //             join b in stockOpnameItemsQuery on a.Id equals b.DyeingPrintingStockOpnameId
            //              select new ReportSOViewModel()
            //              {
            //                  ProductionOrderId = b.ProductionOrderId,
            //                  ProductionOrderNo = b.ProductionOrderNo,
            //                  ProductPackingCode = b.ProductPackingCode,
            //                  ProcessTypeName = b.ProcessTypeName,
            //                  PackagingUnit = b.PackagingUnit,
            //                  Grade = b.Grade,
            //                  Color = b.Color,
            //                  Construction = b.Construction,
            //                  Motif = b.Motif,
            //                  TrackId = b.TrackId,
            //                  TrackName = b.TrackType +" - "+ b.TrackName,
            //                  BonNo = a.BonNo,
            //                  DateIn = a.CreatedUtc.AddHours(7),
            //                  PackagingQty = b.PackagingQty,
            //                  PackingLength = b.PackagingLength,
            //                  InQty = (double)b.PackagingQty * b.PackagingLength
            //              }).ToList();
            //var result = query.GroupBy(s => new { s.ProductPackingCode, s.TrackId }).Select( d => new ReportSOViewModel() 
            //{
            //    ProductionOrderId = d.First().ProductionOrderId,
            //    ProductionOrderNo = d.First().ProductionOrderNo,
            //    ProductPackingCode = d.First().ProductPackingCode,
            //    ProcessTypeName = d.First().ProcessTypeName,
            //    PackagingUnit = d.First().PackagingUnit,
            //    Grade = d.First().Grade,
            //    Color = d.First().Color,
            //    //TrackId = d.First().TrackId,
            //    //TrackName = d.First().TrackName +"-"+ d.First().TrackType,
            //    TrackName = d.First().TrackName,
            //    BonNo = d.First().BonNo,
            //    DateIn = d.First().DateIn,
            //    PackagingQty = d.Sum( a => a.PackagingQty),
            //    PackingLength = d.First().PackingLength,
            //    InQty = d.Sum( a => a.InQty)
            //}).OrderBy(o => o.TrackId).ThenBy( o => o.ProductionOrderId).ToList();

            var query = (from  b in stockOpnameItemsQuery 
                         select new ReportSOViewModel()
                         {
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
                             TrackName = b.TrackType + " - " + b.TrackName,
                             DateIn = b.CreatedUtc.AddHours(7),
                             PackagingQty = b.PackagingQty,
                             PackingLength = b.PackagingLength,
                             InQty = (double)b.PackagingQty * b.PackagingLength
                         }).ToList();
            var result = query.GroupBy(s => new { s.ProductPackingCode, s.TrackId }).Select(d => new ReportSOViewModel()
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
                //TrackId = d.First().TrackId,
                //TrackName = d.First().TrackName +"-"+ d.First().TrackType,
                TrackName = d.First().TrackName,
                BonNo = d.First().BonNo,
                DateIn = d.First().DateIn,
                PackagingQty = d.Sum(a => a.PackagingQty),
                PackingLength = d.First().PackingLength,
                InQty = d.Sum(a => a.InQty)
            }).OrderBy(o => o.TrackId).ThenBy(o => o.ProductionOrderId).ToList();


            return result;

        }
        public MemoryStream GenerateExcelMonitoring(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int track, int offset)
        {
            var data = GetMonitoringSO(dateFrom, dateTo, productionOrderId, track, offset);
            DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Barcode", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jalur/Rak", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Pack", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Total", DataType = typeof(double) });


            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", 0, "", 0);
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
                    //dt.Rows.Add(item.BonNo, item.ProductionOrderNo, dateIn, item.ProductPackingCode, item.PackagingUnit,
                    //    item.Grade, item.Color, item.TrackName, item.PackagingQty, item.PackingLength, item.InQty);
                    dt.Rows.Add(item.ProductionOrderNo, dateIn, item.ProductPackingCode, item.Construction, item.Color, item.Motif,
                        item.Grade, item.TrackName,  item.PackagingUnit, item.PackagingQty, item.PackingLength, item.InQty);

                    packagingQty += item.PackagingQty;
                    total += item.InQty;
                }

                dt.Rows.Add("", "", "", "", "", "", "", "", "", packagingQty, "", total);
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, string.Format("Laporan Stock {0}", "SO")) }, true);

        }


        public class BarcodeInfoViewModel
        {
            public string PackingCode { get; set; }
            public string MaterialName { get; set; }
            public string MaterialConstructionName { get; set; }
            public string YarnMaterialName { get; set; }
            public double PackingLength { get; set; }
            public string PackingType { get; set; }
            public string Color { get; set; }
            public string OrderNo { get; set; }
            public string UOMSKU { get; set; }
            public string DocumentNo { get; set; }
            public string Grade { get; set; }
            public double Balance { get; set; }
            public string CreatedBy { get; set; }
            public decimal PackagingQty { get; set; }
        }

        public class NewBarcodeInfo
        {
            public NewBarcodeInfo()
            {
                productPackingCodes = new List<string>();
            }

            public List<string> productPackingCodes { get; set; }

            public double productPackingLength { get; set; }
            public string productPackingType { get; set; }
            public string color { get; set; }
            public string uomUnit { get; set; }
            public string productionOrderNo { get; set; }
            public string documentNo { get; set; }
            public string grade { get; set; }

        }
    }
}