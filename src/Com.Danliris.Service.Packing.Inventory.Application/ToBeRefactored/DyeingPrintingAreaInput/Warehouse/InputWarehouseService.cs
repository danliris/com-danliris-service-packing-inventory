using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Detail;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.List;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Reject;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.PreOutputWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using OfficeOpenXml.Style;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse
{
    public class InputWarehouseService : IInputWarehouseService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private readonly IDyeingPrintingAreaReferenceRepository _areaReferenceRepository;
        public List<BarcodeInfoViewModel2> _barcodes;

        public InputWarehouseService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _areaReferenceRepository = serviceProvider.GetService<IDyeingPrintingAreaReferenceRepository>();
        }

        //Get All (List)
        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI &&
            //                                             s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument && d.Balance > 0));
            var query = _inputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);

            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
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

        //Get By Id
        private InputWarehouseDetailViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputWarehouseDetailViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                BonNo = model.BonNo,
                Group = model.Group,
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
                WarehousesProductionOrders = model.DyeingPrintingAreaInputProductionOrders.GroupBy(item => item.ProductionOrderId).Select(item => new InputWarehouseProductionOrderDetailViewModel()
                {
                    ProductionOrderId = item.Key,
                    ProductionOrderNo = item.First().ProductionOrderNo,
                    ProductionOrderType = item.First().ProductionOrderType,
                    ProductionOrderOrderQuantity = item.First().ProductionOrderOrderQuantity,

                    ProductionOrderItems = item.Select(s => new ProductionOrderItemListDetailViewModel()
                    {
                        Active = s.Active,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Id = s.Id,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        LastModifiedUtc = s.LastModifiedUtc,

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
                        MaterialProduct = new Material()
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
                        CartNo = s.CartNo,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        Construction = s.Construction,
                        Unit = s.Unit,
                        Color = s.Color,
                        Motif = s.Motif,
                        UomUnit = s.UomUnit,
                        Remark = s.Remark,
                        Grade = s.Grade,
                        Status = s.Status,
                        Balance = s.Balance,
                        InputQuantity = s.InputQuantity,
                        InputPackagingQty = s.InputPackagingQty,
                        PackingInstruction = s.PackingInstruction,
                        PackagingType = s.PackagingType,
                        PackagingQty = s.PackagingQty,
                        PackagingUnit = s.PackagingUnit,
                        AvalALength = s.AvalALength,
                        AvalBLength = s.AvalBLength,
                        AvalConnectionLength = s.AvalConnectionLength,
                        DeliveryOrderSalesId = s.DeliveryOrderSalesId,
                        DeliveryOrderSalesNo = s.DeliveryOrderSalesNo,
                        AvalType = s.AvalType,
                        AvalCartNo = s.AvalCartNo,
                        AvalQuantityKg = s.AvalQuantityKg,
                        Area = s.Area,
                        HasOutputDocument = s.HasOutputDocument,
                        DyeingPrintingAreaInputId = s.DyeingPrintingAreaInputId,
                        Qty = s.PackagingLength,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        PreviousOutputPackagingQty = s.InputPackagingQty,

                    }).Distinct(new PackingComparer()).ToList()
                }).ToList()
            };


            return vm;
        }

        //Get By Id
        private InputWarehouseDetailViewModel MapToViewModelBon(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputWarehouseDetailViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                BonNo = model.BonNo,
                Group = model.Group,
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
                WarehousesProductionOrders = model.DyeingPrintingAreaInputProductionOrders.GroupBy(item => item.ProductionOrderId).Select(item => new InputWarehouseProductionOrderDetailViewModel()
                {
                    ProductionOrderId = item.Key,
                    ProductionOrderNo = item.First().ProductionOrderNo,
                    ProductionOrderType = item.First().ProductionOrderType,
                    ProductionOrderOrderQuantity = item.First().ProductionOrderOrderQuantity,

                    ProductionOrderItems = item.GroupBy(r => new { r.ProductionOrderId, r.Grade }).Select(s => new ProductionOrderItemListDetailViewModel()
                    {
                        Active = s.First().Active,
                        CreatedAgent = s.First().CreatedAgent,
                        CreatedBy = s.First().CreatedBy,
                        CreatedUtc = s.First().CreatedUtc,
                        DeletedAgent = s.First().DeletedAgent,
                        DeletedBy = s.First().DeletedBy,
                        DeletedUtc = s.First().DeletedUtc,
                        Id = s.First().Id,
                        IsDeleted = s.First().IsDeleted,
                        LastModifiedAgent = s.First().LastModifiedAgent,
                        LastModifiedBy = s.First().LastModifiedBy,
                        LastModifiedUtc = s.First().LastModifiedUtc,

                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.Key.ProductionOrderId,
                            No = s.First().ProductionOrderNo,
                            OrderQuantity = s.First().ProductionOrderOrderQuantity,
                            Type = s.First().ProductionOrderType
                        },
                        MaterialWidth = s.First().MaterialWidth,
                        MaterialOrigin = s.First().MaterialOrigin,
                        FinishWidth = s.First().FinishWidth,
                        MaterialProduct = new Material()
                        {
                            Id = s.First().MaterialId,
                            Name = s.First().MaterialName
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Name = s.First().MaterialConstructionName,
                            Id = s.First().MaterialConstructionId
                        },
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
                        CartNo = s.First().CartNo,
                        Buyer = s.First().Buyer,
                        BuyerId = s.First().BuyerId,
                        Construction = s.First().Construction,
                        Unit = s.First().Unit,
                        Color = s.First().Color,
                        Motif = s.First().Motif,
                        UomUnit = s.First().UomUnit,
                        Remark = s.First().Remark,
                        Grade = s.Key.Grade,
                        Status = s.First().Status,
                        Balance = s.Sum(d => d.Balance),
                        InputQuantity = s.Sum(d => d.InputQuantity),
                        InputPackagingQty = s.Sum(d => d.InputPackagingQty),
                        PackingInstruction = s.First().PackingInstruction,
                        PackagingType = s.First().PackagingType,
                        PackagingQty = s.Sum(d => d.PackagingQty),
                        PackagingUnit = s.First().PackagingUnit,
                        AvalALength = s.First().AvalALength,
                        AvalBLength = s.First().AvalBLength,
                        AvalConnectionLength = s.First().AvalConnectionLength,
                        DeliveryOrderSalesId = s.First().DeliveryOrderSalesId,
                        DeliveryOrderSalesNo = s.First().DeliveryOrderSalesNo,
                        AvalType = s.First().AvalType,
                        AvalCartNo = s.First().AvalCartNo,
                        AvalQuantityKg = s.First().AvalQuantityKg,
                        Area = s.First().Area,
                        HasOutputDocument = s.First().HasOutputDocument,
                        DyeingPrintingAreaInputId = s.First().DyeingPrintingAreaInputId,
                        Qty = s.First().PackagingLength,
                        ProductSKUId = s.First().ProductSKUId,
                        FabricSKUId = s.First().FabricSKUId,
                        ProductSKUCode = s.First().ProductSKUCode,
                        HasPrintingProductSKU = s.First().HasPrintingProductSKU,
                        ProductPackingId = s.First().ProductPackingId,
                        FabricPackingId = s.First().FabricPackingId,
                        ProductPackingCode = s.First().ProductPackingCode,
                        HasPrintingProductPacking = s.First().HasPrintingProductPacking,
                        PreviousOutputPackagingQty = s.Sum(d => d.InputPackagingQty),

                    }).Distinct(new PackingComparer()).ToList()
                }).ToList()
            };


            return vm;
        }

        public async Task<InputWarehouseDetailViewModel> ReadById(int id)
        {
            var model = await _inputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            InputWarehouseDetailViewModel vm = MapToViewModel(model);

            return vm;
        }

        public async Task<InputWarehouseDetailViewModel> ReadByIdBon(int id)
        {
            var model = await _inputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            InputWarehouseDetailViewModel vm = MapToViewModelBon(model);

            return vm;
        }

        //Create - Generate Bon No
        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        //Create
        public async Task<int> Create(InputWarehouseCreateViewModel viewModel)
        {
            int result = 0;

            var model = _inputRepository.GetDbSet().Include(s => s.DyeingPrintingAreaInputProductionOrders)
                                                   .FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                        s.Date.AddHours(7).ToString("dd/MM/YYYY").Equals(viewModel.Date.AddHours(7).ToString("dd/MM/YYYY")) &&
                                                                        s.Shift == viewModel.Shift &&
                                                                        s.Group == viewModel.Group);

            var dateData = viewModel.Date;
            var ids = _inputRepository.GetDbSet().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI).Select(x => x.Id).ToList();
            //var errorResult = new List<ValidationResult>();
            //foreach (var item in viewModel.MappedWarehousesProductionOrders)
            //{
            //    var splitedCode = item.ProductPackingCode.Split(",");
            //    foreach (var code in splitedCode)
            //    {
            //        var latestDataOnIn = _inputProductionOrderRepository.GetDbSet().OrderByDescending(o => o.DateIn).FirstOrDefault(x =>
            //            x.Area == DyeingPrintingArea.GUDANGJADI &&
            //            x.ProductPackingCode.Contains(code)
            //        );

            //        if (latestDataOnIn != null)
            //        {
            //            var latestDataOnOut = _outputProductionOrderRepository.GetDbSet()
            //                .OrderByDescending(o => o.CreatedUtc)
            //                .FirstOrDefault(x =>
            //                    x.ProductPackingCode.Contains(code) &&
            //                    x.CreatedUtc > latestDataOnIn.CreatedUtc
            //                );

            //            if (latestDataOnOut == null)
            //            {
            //                //errorResult.Add(new ValidationResult("Kode " + code + " belum keluar", new List<string> { "Kode" }));
            //            }
            //        }
            //    }
            //}

            //if (errorResult.Count > 0)
            //{
            //    var validationContext = new ValidationContext(viewModel, _serviceProvider, null);
            //    throw new ServiceValidationException(validationContext, errorResult);
            //}
            //else
            //{
            if (model != null)
            {
                result = await UpdateExistingWarehouse(viewModel, model.Id, model.BonNo);
            }
            else
            {
                result = await InsertNewWarehouse(viewModel);
            }

            //}
            return result;

            // if (model != null)
            // {
            // var listOfInId = model.DyeingPrintingAreaInputProductionOrders.Select(x => x.Id).ToList();
            // var outModel = _outputProductionOrderRepository.GetDbSet().Where(x => listOfInId.Contains(x.DyeingPrintingAreaInputProductionOrderId)).ToList();

            // var outModelCode = outModel.Select(x => x.ProductPackingCode).ToList();

            // var outCode = new List<string>();
            // foreach (var item in outModelCode)
            // {
            //     var splitedCode = item.Split(",");
            //     foreach (var code in splitedCode)
            //     {
            //         outCode.Add(code);
            //     }
            // }

            // // Cek sudah masuk
            // var itemNotAvailable = new List<string>();
            // // var notAvaiableInInput = false;
            // foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            // {
            //     var splitedCode = item.ProductPackingCode.Split(",");
            //     foreach(var code in splitedCode) {
            //         itemNotAvailable.Add(code);
            //     }
            // }

            // foreach (var item in viewModel.MappedWarehousesProductionOrders)
            // {
            //     var splitedCode = item.ProductPackingCode.Split(",");
            //     if(splitedCode.Any(el => itemNotAvailable.Contains(el))) {
            //         var notAvailableCode = splitedCode.Where(x => itemNotAvailable.Any(y => x.Equals(y))).ToList();
            //         throw new Exception("Kode " + String.Join(",", notAvailableCode) + " tidak available");
            //     }
            // }

            // Cek belum keluar
            // foreach (var item in viewModel.MappedWarehousesProductionOrders)
            // {
            //     var splitedCode = item.ProductPackingCode.Split(",");
            //     foreach (var code in splitedCode)
            //     {
            //         var codeAvailableInInput = splitedCode.Any(el => itemNotAvailable.Contains(el));
            //         if(!outCode.Contains(code) && codeAvailableInInput) {
            //             var notAvailableCode = splitedCode.Where(x => !outCode.Any(y => x.Equals(y))).ToList();
            //             throw new Exception("Kode " + String.Join(",", notAvailableCode) + " belum keluar");
            //         } else if (codeAvailableInInput){
            //             var notAvailableCode = splitedCode.Where(x => itemNotAvailable.Any(y => x.Equals(y))).ToList();
            //             throw new Exception("Kode " + String.Join(",", notAvailableCode) + " tidak available");
            //         }
            //     }
            // }

            //     result = await UpdateExistingWarehouse(viewModel, model.Id, model.BonNo);
            // }
            // else
            // {
            //     result = await InsertNewWarehouse(viewModel);
            // }

            // return result;
        }

        //Create - Insert New Warehouse
        public async Task<int> InsertNewWarehouse(InputWarehouseCreateViewModel viewModel)
        {
            int result = 0;

            int totalCurrentYearData = _inputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                              s.CreatedUtc.Year == viewModel.Date.Year);

            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);

            //Mapping ViewModel to DyeingPrintingAreaInputModel
            var model = new DyeingPrintingAreaInputModel(viewModel.Date,
                                                         viewModel.Area,
                                                         viewModel.Shift,
                                                         bonNo,
                                                         viewModel.Group,
                                                         viewModel.MappedWarehousesProductionOrders.Select(s => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area,
                                                             s.ProductionOrder.Id,
                                                             s.ProductionOrder.No,
                                                             s.ProductionOrder.Type,
                                                             s.PackingInstruction,
                                                             s.CartNo,
                                                             s.Buyer,
                                                             s.Construction,
                                                             s.Unit,
                                                             s.Color,
                                                             s.Motif,
                                                             s.UomUnit,
                                                             s.Qty * (double)s.InputPackagingQty,
                                                             false,
                                                             s.PackagingUnit,
                                                             s.PackagingType,
                                                             s.InputPackagingQty,
                                                             s.Grade,
                                                             s.ProductionOrder.OrderQuantity,
                                                             s.BuyerId,
                                                             s.Id,
                                                             s.Remark,
                                                             s.Qty * (double)s.InputPackagingQty,
                                                             s.MaterialProduct.Id,
                                                             s.MaterialProduct.Name,
                                                             s.MaterialConstruction.Id,
                                                             s.MaterialConstruction.Name,
                                                             s.MaterialWidth,
                                                             s.ProcessType.Id,
                                                             s.ProcessType.Name,
                                                             s.YarnMaterial.Id,
                                                             s.YarnMaterial.Name,
                                                             s.ProductSKUId,
                                                             s.FabricSKUId,
                                                             s.ProductSKUCode,
                                                             s.HasPrintingProductSKU,
                                                             s.ProductPackingId,
                                                             s.FabricPackingId,
                                                             s.ProductPackingCode,
                                                             s.HasPrintingProductPacking,
                                                             s.Qty,
                                                             s.Qty * (double)s.InputPackagingQty,
                                                             s.InputPackagingQty, // InputPackaging Qty
                                                             s.FinishWidth,
                                                             viewModel.Date,
                                                             s.InventoryType,
                                                             s.MaterialOrigin,
                                                             s.PackingCodeToCreate
                                                             ))
                                                         .ToList());

            foreach (var item in viewModel.MappedWarehousesProductionOrders)
            {
                // If kode sudah ada di in dia gabisa kurang quantity
                //var splitedCode = item.ProductPackingCode.Split(",");
                //foreach (var code in splitedCode)
                //{
                //    if (!_inputProductionOrderRepository.CheckIfHasInInput(code))
                //    {
                //        result += await _outputProductionOrderRepository.UpdateOutputBalancePackingQtyFromInput(item.Id, 1);
                //    }
                //}
            }

            //Insert to Input Repository
            result = await _inputRepository.InsertAsync(model);

            foreach (var item in viewModel.MappedWarehousesProductionOrders)
            {
                var itemModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == item.Id);
                if (item.Area == DyeingPrintingArea.PACKING)
                {
                    var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                    var packingData = JsonConvert.DeserializeObject<List<PackingData>>(item.PrevSppInJson);
                    foreach (var packing in packingData)
                    {
                        packing.Balance = (double)item.InputPackagingQty * item.Qty;
                    }

                    outputData.UpdateBalance(item.PackingCodeToCreate.Split(',').ToList());
                    result += await _inputProductionOrderRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                }
                else
                {
                    var balance = (double)item.InputPackagingQty * item.Qty;
                    result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, balance, item.InputPackagingQty);
                }

                var inputQuantity = item.Qty * (double)item.InputPackagingQty;
                if (inputQuantity == item.InputQuantity)
                    result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.Id, true, DyeingPrintingArea.TERIMA);

                //Mapping to DyeingPrintingAreaMovementModel
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No, item.CartNo,
                    item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, (double)item.InputPackagingQty * item.Qty, itemModel.Id, item.ProductionOrder.Type, item.Grade, null,
                    item.PackagingType, item.PackingCodeToCreate.Split(',').Count(), item.PackagingUnit, item.Qty, item.InventoryType);


                //Insert to Movement Repository
                result += await _movementRepository.InsertAsync(movementModel);

                var areaReference = new DyeingPrintingAreaReferenceModel("IN", itemModel.Id, itemModel.DyeingPrintingAreaOutputProductionOrderId);
                await _areaReferenceRepository.InsertAsync(areaReference);
            }

            //Update from Output Production Order (Child) Flag for HasNextAreaDocument == True
            List<int> listOfOutputProductionOrderIds = viewModel.MappedWarehousesProductionOrders.Select(o => o.Id).ToList();
            foreach (var outputProductionOrderId in listOfOutputProductionOrderIds)
            {
                result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(outputProductionOrderId, true, DyeingPrintingArea.TERIMA);
            }

            return result;
        }

        //Create - Update Existing Warehouse
        public async Task<int> UpdateExistingWarehouse(InputWarehouseCreateViewModel viewModel, int dyeingPrintingAreaInputId, string bonNo)
        {
            int result = 0;

            foreach (var productionOrder in viewModel.MappedWarehousesProductionOrders)
            {
                var inputQuantity = productionOrder.Qty * (double)productionOrder.InputPackagingQty;
                //Mapping to DyeingPrintingAreaInputProductionOrderModel
                var productionOrderModel = new DyeingPrintingAreaInputProductionOrderModel(
                    viewModel.Area,
                    productionOrder.ProductionOrder.Id,
                    productionOrder.ProductionOrder.No,
                    productionOrder.ProductionOrder.Type,
                    productionOrder.PackingInstruction,
                    productionOrder.CartNo,
                    productionOrder.Buyer,
                    productionOrder.Construction,
                    productionOrder.Unit,
                    productionOrder.Color,
                    productionOrder.Motif,
                    productionOrder.UomUnit,
                    inputQuantity,
                    false,
                    productionOrder.PackagingUnit,
                    productionOrder.PackagingType,
                    productionOrder.InputPackagingQty,
                    productionOrder.Grade,
                    productionOrder.ProductionOrder.OrderQuantity,
                    productionOrder.BuyerId,
                    productionOrder.Id,
                    productionOrder.Remark,
                    inputQuantity,
                    productionOrder.MaterialProduct.Id,
                    productionOrder.MaterialProduct.Name,
                    productionOrder.MaterialConstruction.Id,
                    productionOrder.MaterialConstruction.Name,
                    productionOrder.MaterialWidth,
                    productionOrder.ProcessType.Id,
                    productionOrder.ProcessType.Name,
                    productionOrder.YarnMaterial.Id,
                    productionOrder.YarnMaterial.Name,
                    productionOrder.ProductSKUId,
                    productionOrder.FabricSKUId,
                    productionOrder.ProductSKUCode,
                    productionOrder.HasPrintingProductSKU,
                    productionOrder.ProductPackingId,
                    productionOrder.FabricPackingId,
                    productionOrder.ProductPackingCode,
                    productionOrder.HasPrintingProductPacking,
                    productionOrder.Qty,
                    inputQuantity,
                    productionOrder.InputPackagingQty,
                    productionOrder.FinishWidth,
                    viewModel.Date,
                    productionOrder.InventoryType,
                    productionOrder.MaterialOrigin,
                    productionOrder.PackingCodeToCreate
                    )

                {
                    DyeingPrintingAreaInputId = dyeingPrintingAreaInputId,
                };


                // If kode sudah ada di in dia gabisa kurang quantity
                //var splitedCode = productionOrder.ProductPackingCode.Split(",");
                //foreach (var item in splitedCode)
                //{
                //    if (!_inputProductionOrderRepository.CheckIfHasInInput(item))
                //    {
                //        result += await _outputProductionOrderRepository.UpdateOutputBalancePackingQtyFromInput(productionOrder.Id, 1);
                //    }
                //}

                //Insert to Input Production Order Repository
                result += await _inputProductionOrderRepository.InsertAsync(productionOrderModel);



                if (productionOrder.Area == DyeingPrintingArea.PACKING)
                {
                    var outputData = await _outputProductionOrderRepository.ReadByIdAsync(productionOrder.Id);
                    var packingData = JsonConvert.DeserializeObject<List<PackingData>>(productionOrder.PrevSppInJson);
                    outputData.UpdateBalance(productionOrder.PackingCodeToCreate.Split(',').ToList());
                    result += await _inputProductionOrderRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                }
                else
                {
                    result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(productionOrder.DyeingPrintingAreaInputProductionOrderId, productionOrder.InputQuantity, productionOrder.InputPackagingQty);
                }

                if (inputQuantity == productionOrder.InputQuantity)
                    result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(productionOrder.Id, true, DyeingPrintingArea.TERIMA);

                //Mapping to DyeingPrintingAreaMovementModel
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, productionOrder.MaterialOrigin, viewModel.Area, DyeingPrintingArea.IN, dyeingPrintingAreaInputId, bonNo, productionOrder.ProductionOrder.Id,
                    productionOrder.ProductionOrder.No, productionOrder.CartNo, productionOrder.Buyer, productionOrder.Construction, productionOrder.Unit, productionOrder.Color,
                    productionOrder.Motif, productionOrder.UomUnit, inputQuantity, productionOrderModel.Id, productionOrder.ProductionOrder.Type, productionOrder.Grade,
                    null, productionOrder.PackagingType, productionOrder.PackingCodeToCreate.Split(',').Count(), productionOrder.PackagingUnit, productionOrder.Qty, productionOrder.InventoryType);

                //Insert to Movement Repository
                result += await _movementRepository.InsertAsync(movementModel);

                //result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(productionOrder.DyeingPrintingAreaInputProductionOrderId, productionOrder.InputQuantity, productionOrder.InputPackagingQty);
            }

            //Update from Output Production Order (Child) Flag for HasNextAreaDocument == True
            //List<int> listOfOutputProductionOrderIds = viewModel.MappedWarehousesProductionOrders.Select(o => o.Id).ToList();
            //foreach (var outputProductionOrderId in listOfOutputProductionOrderIds)
            //{
            //    result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(outputProductionOrderId, true, DyeingPrintingArea.TERIMA);
            //}

            return result;
        }

        //Get Output Pre Warehouse Input
        public List<OutputPreWarehouseViewModel> GetOutputPreWarehouseProductionOrders()
        {
            var query = _outputProductionOrderRepository.ReadAll()
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.DestinationArea == DyeingPrintingArea.GUDANGJADI &&
                                                                    !s.HasNextAreaDocument);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new OutputPreWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p => new OutputPreWarehouseItemListViewModel()
                {

                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
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
                    OutputId = p.DyeingPrintingAreaOutputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    InputQuantity = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = (decimal)p.PackagingQuantityBalance,
                    InputPackagingQty = (decimal)p.PackagingQuantityBalance,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                    DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    Description = p.Description,
                    DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    DestinationArea = p.DestinationArea,
                    HasNextAreaDocument = p.HasNextAreaDocument,
                    DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingLength,
                    ProductSKUId = p.ProductSKUId,
                    FabricSKUId = p.FabricSKUId,
                    ProductSKUCode = p.ProductSKUCode,
                    HasPrintingProductSKU = p.HasPrintingProductSKU,
                    ProductPackingId = p.ProductPackingId,
                    FabricPackingId = p.FabricPackingId,
                    ProductPackingCode = p.ProductPackingCode,
                    ProductPackingCodeCreated = p.ProductPackingCodeCreated,
                    HasPrintingProductPacking = p.HasPrintingProductPacking,
                    PreviousOutputPackagingQty = p.PackagingQty,
                    PrevSppInJson = p.PrevSppInJson,
                    InventoryType = p.InventoryType
                }).ToList()

            });

            return data.ToList();
        }

        //Reject - Generate Bon No
        private string GenerateBonNoReject(int totalPreviousData, DateTimeOffset date, string area)
        {
            if (area == DyeingPrintingArea.PACKING)
            {

                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == DyeingPrintingArea.INSPECTIONMATERIAL)
            {

                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == DyeingPrintingArea.SHIPPING)
            {
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        //Reject
        public async Task<int> Reject(RejectedInputWarehouseViewModel viewModel)
        {
            int result = 0;

            var groupedProductionOrders = viewModel.MappedWarehousesProductionOrders.GroupBy(s => s.Area);
            foreach (var item in groupedProductionOrders)
            {
                var model = _inputRepository.GetDbSet().AsNoTracking()
                                .FirstOrDefault(s => s.Area == item.Key &&
                                                     s.Date.Date == viewModel.Date.Date &&
                                                     s.Shift == viewModel.Shift);

                if (model == null)
                {
                    int totalCurrentYearData = _inputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == item.Key &&
                                                                                                      s.CreatedUtc.Year == viewModel.Date.Year);
                    string bonNo = GenerateBonNoReject(totalCurrentYearData + 1, viewModel.Date, item.Key);

                    model = new DyeingPrintingAreaInputModel(viewModel.Date,
                                                             item.Key,
                                                             viewModel.Shift,
                                                             bonNo,
                                                             viewModel.Group,
                                                             viewModel.MappedWarehousesProductionOrders.Select(s =>
                                                                new DyeingPrintingAreaInputProductionOrderModel(s.ProductionOrder.Id,
                                                                                                                s.ProductionOrder.No,
                                                                                                                s.CartNo,
                                                                                                                s.Buyer,
                                                                                                                s.Construction,
                                                                                                                s.Unit,
                                                                                                                s.Color,
                                                                                                                s.Motif,
                                                                                                                s.UomUnit,
                                                                                                                s.InputQuantity,
                                                                                                                false,
                                                                                                                s.PackingInstruction,
                                                                                                                s.ProductionOrder.Type,
                                                                                                                s.ProductionOrder.OrderQuantity,
                                                                                                                s.Remark,
                                                                                                                s.Grade,
                                                                                                                s.Status,
                                                                                                                s.AvalALength,
                                                                                                                s.AvalBLength,
                                                                                                                s.AvalConnectionLength,
                                                                                                                s.AvalType,
                                                                                                                s.AvalCartNo,
                                                                                                                s.AvalQuantityKg,
                                                                                                                s.DeliveryOrderSalesId,
                                                                                                                s.DeliveryOrderSalesNo,
                                                                                                                s.PackagingUnit,
                                                                                                                s.PackagingType,
                                                                                                                s.InputPackagingQty,
                                                                                                                item.Key,
                                                                                                                s.InputQuantity,
                                                                                                                s.Id,
                                                                                                                s.BuyerId,
                                                                                                                s.MaterialProduct.Id,
                                                                                                                s.MaterialProduct.Name,
                                                                                                                s.MaterialConstruction.Id,
                                                                                                                s.MaterialConstruction.Name,
                                                                                                                s.MaterialWidth,
                                                                                                                s.ProcessType.Id,
                                                                                                                s.ProcessType.Name,
                                                                                                                s.YarnMaterial.Id,
                                                                                                                s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                                                                                                                s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId,
                                                                                                                s.ProductPackingCode, s.HasPrintingProductPacking, s.Qty,
                                                                                                                s.InputQuantity, s.InputPackagingQty, s.FinishWidth, s.MaterialOrigin)).ToList());

                    result = await _inputRepository.InsertAsync(model);

                    //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);

                    result += await _outputProductionOrderRepository.UpdateFromInputAsync(item.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TOLAK);

                    foreach (var detail in item)
                    {
                        var itemModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == detail.Id);
                        //result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputPackagingQty);
                        if (detail.Area == DyeingPrintingArea.PACKING)
                        {
                            //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                            var packingData = JsonConvert.DeserializeObject<List<PackingData>>(detail.PrevSppInJson);
                            result += await _inputProductionOrderRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                        }
                        else
                        {

                            result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputPackagingQty);
                        }
                        var movementModel = new DyeingPrintingAreaMovementModel();
                        if (item.Key == DyeingPrintingArea.PACKING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type, detail.Grade);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.SHIPPING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type,
                                detail.Grade, null, detail.PackagingType, detail.InputPackagingQty, detail.PackagingUnit, detail.Qty);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.TRANSIT)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type, null, detail.Remark);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                    }
                }
                else
                {
                    foreach (var detail in item)
                    {
                        var modelItem = new DyeingPrintingAreaInputProductionOrderModel(detail.ProductionOrder.Id,
                                                                                        detail.ProductionOrder.No,
                                                                                        detail.CartNo,
                                                                                        detail.Buyer,
                                                                                        detail.Construction,
                                                                                        detail.Unit,
                                                                                        detail.Color,
                                                                                        detail.Motif,
                                                                                        detail.UomUnit,
                                                                                        detail.InputQuantity,
                                                                                        false,
                                                                                        detail.PackingInstruction,
                                                                                        detail.ProductionOrder.Type,
                                                                                        detail.ProductionOrder.OrderQuantity,
                                                                                        detail.Remark,
                                                                                        detail.Grade,
                                                                                        detail.Status,
                                                                                        detail.AvalALength,
                                                                                        detail.AvalBLength,
                                                                                        detail.AvalConnectionLength,
                                                                                        detail.AvalType,
                                                                                        detail.AvalCartNo,
                                                                                        detail.AvalQuantityKg,
                                                                                        detail.DeliveryOrderSalesId,
                                                                                        detail.DeliveryOrderSalesNo,
                                                                                        detail.PackagingUnit,
                                                                                        detail.PackagingType,
                                                                                        detail.InputPackagingQty,
                                                                                        item.Key,
                                                                                        detail.InputQuantity,
                                                                                        detail.Id,
                                                                                        detail.BuyerId,
                                                                                        detail.MaterialProduct.Id,
                                                                                        detail.MaterialProduct.Name,
                                                                                        detail.MaterialConstruction.Id,
                                                                                        detail.MaterialConstruction.Name,
                                                                                        detail.MaterialWidth,
                                                                                        detail.ProcessType.Id,
                                                                                        detail.ProcessType.Name,
                                                                                        detail.YarnMaterial.Id,
                                                                                        detail.YarnMaterial.Name, detail.ProductSKUId, detail.FabricSKUId, detail.ProductSKUCode,
                                                                                        detail.HasPrintingProductSKU, detail.ProductPackingId, detail.FabricPackingId,
                                                                                        detail.ProductPackingCode, detail.HasPrintingProductPacking, detail.Qty,
                                                                                        detail.InputQuantity, detail.InputPackagingQty, detail.FinishWidth, detail.MaterialOrigin);

                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        result += await _inputProductionOrderRepository.InsertAsync(modelItem);
                        if (detail.Area == DyeingPrintingArea.PACKING)
                        {
                            //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                            var packingData = JsonConvert.DeserializeObject<List<PackingData>>(detail.PrevSppInJson);
                            result += await _inputProductionOrderRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                        }
                        else
                        {

                            result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputPackagingQty);
                        }
                        //result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputPackagingQty);
                        var movementModel = new DyeingPrintingAreaMovementModel();
                        if (item.Key == DyeingPrintingArea.PACKING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type, detail.Grade);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.SHIPPING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type,
                                detail.Grade, null, detail.PackagingType, detail.InputPackagingQty, detail.PackagingUnit, detail.Qty);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.TRANSIT)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type, null, detail.Remark);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }

                    }
                    result += await _outputProductionOrderRepository.UpdateFromInputAsync(item.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TOLAK);
                }
            }

            return result;
        }

        public async Task<int> Delete(int bonId)
        {
            var result = 0;
            var bonInput = _inputRepository.ReadAll().Where(x => x.Id == bonId && x.DyeingPrintingAreaInputProductionOrders.Any());

            foreach (var bon in bonInput)
            {
                var hasSPPwithOutput = bon.DyeingPrintingAreaInputProductionOrders.Where(x => x.HasOutputDocument);
                if (hasSPPwithOutput.Count() > 0)
                {
                    throw new Exception("Bon Sudah Berada di Packing Keluar");
                }

                var sppInput = bon.DyeingPrintingAreaInputProductionOrders;// 2355, 2356, 2357
                //var sppDeleted = sppInput.Where(x => viewModel.MappedWarehousesProductionOrders.Any(s => !sppInputIds.Contains(s.Id)));
                foreach (var spp in sppInput)
                {
                    var prevOutput = _outputProductionOrderRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    foreach (var prevOut in prevOutput)
                    {
                        IQueryable<DyeingPrintingAreaInputProductionOrderModel> prevInput;
                        if (prevOut.Area == DyeingPrintingArea.PACKING)
                        {
                            //prevINput masih salah, karena prevsppInJSON salah
                            var packingData = JsonConvert.DeserializeObject<List<PackingData>>(prevOut.PrevSppInJson);
                            prevInput = _inputProductionOrderRepository.ReadAll().Where(x => packingData.Any(e => e.Id == x.Id));
                        }
                        else
                        {

                            prevInput = _inputProductionOrderRepository.ReadAll().Where(x => x.Id == prevOut.DyeingPrintingAreaInputProductionOrderId);
                        }
                        foreach (var prevIn in prevInput)
                        {
                            //tidak mengubah row database
                            var newBalance = prevIn.Balance + prevOut.Balance;
                            prevIn.SetBalance(newBalance, "UPDATEWAREHOUSE", "SERVICE");
                        }

                        prevOut.SetHasNextAreaDocument(false, "UPDATEWAREHOUSE", "SERVICE");
                        prevOut.SetNextAreaInputStatus(null, "UPDATEWAREHOUSE", "SERVICE");

                        prevOut.SetBalance(prevOut.Balance + spp.Balance, "UPDATEWAREHOUSE", "SERVICE");
                        prevOut.SetPackagingQty(prevOut.PackagingQty + spp.PackagingQty, "UPDATEWAREHOUSE", "SERVICE");
                        result += await _outputProductionOrderRepository.UpdateAsync(prevOut.Id, prevOut);
                    }
                    result += await _inputProductionOrderRepository.DeleteAsync(spp.Id);

                    var movementModel = new DyeingPrintingAreaMovementModel(bon.Date, spp.MaterialOrigin, bon.Area, DyeingPrintingArea.IN, bon.Id, bon.BonNo, spp.ProductionOrderId, spp.ProductionOrderNo,
                               spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance * -1, spp.Id, spp.ProductionOrderType, spp.Grade,
                               null, spp.PackagingType, spp.PackagingQty * -1, spp.PackagingUnit, spp.PackagingLength);
                    result += await _movementRepository.InsertAsync(movementModel);
                }

                //delete entire packing bon and spp using model
                result += await _inputRepository.DeleteAsync(bonId);
            }
            return result;

            //var result = 0;
            ////get bon data and check if it has document output
            //var modelBon = _inputRepository.ReadAll().Where(x => x.Id == bonId && x.DyeingPrintingAreaInputProductionOrders.Any()).FirstOrDefault();
            //if (modelBon != null)
            //{
            //    var hasSPPwithOutput = modelBon.DyeingPrintingAreaInputProductionOrders.Where(x => x.HasOutputDocument);
            //    if (hasSPPwithOutput.Count() > 0)
            //    {
            //        throw new Exception("Bon Sudah Berada di Packing Keluar");
            //    }
            //    else
            //    {
            //        //get prev bon id using first spp modelBon and search bonId
            //        var firstSppBonModel = modelBon.DyeingPrintingAreaInputProductionOrders.FirstOrDefault();
            //        int sppIdPrevOutput = firstSppBonModel == null ? 0 : firstSppBonModel.DyeingPrintingAreaOutputProductionOrderId;
            //        var sppPrevOutput = _outputProductionOrderRepository.ReadAll().Where(s => s.Id == sppIdPrevOutput).FirstOrDefault();
            //        int bonIdPrevOutput = sppPrevOutput == null ? 0 : sppPrevOutput.DyeingPrintingAreaOutputId;
            //        var bonPrevOutput = _outputRepository.ReadAll().Where(x =>
            //                                                            x.DyeingPrintingAreaOutputProductionOrders.Any() &&
            //                                                            x.Id == bonIdPrevOutput
            //                                                            );
            //        //get prev bon input using input spp id in prev bon out and search bonId
            //        int sppIdPrevInput = sppPrevOutput == null ? 0 : sppPrevOutput.DyeingPrintingAreaInputProductionOrderId;
            //        var sppPrevInput = _inputProductionOrderRepository.ReadAll().FirstOrDefault(x => x.Id == sppIdPrevInput);
            //        int bonIdPrevInput = sppPrevInput == null ? 0 : sppPrevInput.DyeingPrintingAreaInputId;
            //        var bonPrevInput = _inputRepository.ReadAll().Where(x =>
            //                                                x.DyeingPrintingAreaInputProductionOrders.Any() &&
            //                                                x.Id == bonIdPrevInput
            //                                                );


            //        //delete entire packing bon and spp using model
            //        result += await _inputRepository.DeleteAsync(bonId);

            //        foreach (var item in modelBon.DyeingPrintingAreaInputProductionOrders)
            //        {
            //            var movementModel = new DyeingPrintingAreaMovementModel(modelBon.Date, item.MaterialOrigin, modelBon.Area, DyeingPrintingArea.IN, modelBon.Id, modelBon.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
            //                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade,
            //                    null, item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength);
            //            result += await _movementRepository.InsertAsync(movementModel);
            //        }

            //        //activate bon prev hasNextAreaDocument == false;
            //        foreach (var bon in bonPrevOutput)
            //        {
            //            bon.SetHasNextAreaDocument(false, "WAREHOUSESERVICE", "SERVICE");
            //            //activate spp prev from bon
            //            foreach (var spp in bon.DyeingPrintingAreaOutputProductionOrders)
            //            {
            //                spp.SetHasNextAreaDocument(false, "WAREHOUSESERVICE", "SERVICE");
            //                spp.SetNextAreaInputStatus(null, "WAREHOUSESERVICE", "SERVICE");
            //                //update balance input spp from prev spp
            //                IQueryable<DyeingPrintingAreaInputProductionOrderModel> inputSpp;
            //                if (spp.Area == DyeingPrintingArea.PACKING)
            //                {
            //                    var packingData = JsonConvert.DeserializeObject<List<PackingData>>(spp.PrevSppInJson);
            //                    inputSpp = _inputProductionOrderRepository.ReadAll().Where(x => packingData.Any(e => e.Id == x.Id));
            //                }
            //                else
            //                {

            //                    inputSpp = _inputProductionOrderRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaInputProductionOrderId);
            //                }
            //                //var inputSpp = _inputProductionOrderRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaInputProductionOrderId);
            //                foreach (var modifInputSpp in inputSpp)
            //                {
            //                    var newBalance = modifInputSpp.Balance + spp.Balance;
            //                    modifInputSpp.SetBalance(newBalance, "WAREHOUSESERVICE", "SERVICE");

            //                    modifInputSpp.SetHasOutputDocument(false, "WAREHOUSESERVICE", "SERVICE");
            //                    result += await _inputProductionOrderRepository.UpdateAsync(modifInputSpp.Id, modifInputSpp);
            //                }


            //            }
            //            result += await _outputRepository.UpdateAsync(bon.Id, bon);
            //            //result += await _outputRepository.DeleteAsync(bon.Id);
            //        }
            //    }
            //}

            //return result;
        }

        public async Task<int> Update(int bonId, InputWarehouseCreateViewModel viewModel)// 2355, 2356
        {
            var result = 0;
            var bonInput = _inputRepository.ReadAll().Where(x => x.Id == bonId && x.DyeingPrintingAreaInputProductionOrders.Any());
            foreach (var bon in bonInput)
            {
                var sppInput = bon.DyeingPrintingAreaInputProductionOrders;// 2355, 2356, 2357

                var sentedId = viewModel.MappedWarehousesProductionOrders.Select(x => x.Id).ToList(); // 1, 2 -> false, false, true
                var sppDeleted = sppInput.Where(x => !sentedId.Contains(x.Id));

                //var sppDeleted = sppInput.Where(x => viewModel.MappedWarehousesProductionOrders.Any(s => !sppInputIds.Contains(s.Id)));
                foreach (var spp in sppDeleted)
                {
                    var prevOutput = _outputProductionOrderRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    foreach (var prevOut in prevOutput)
                    {
                        IQueryable<DyeingPrintingAreaInputProductionOrderModel> prevInput;
                        if (prevOut.Area == DyeingPrintingArea.PACKING)
                        {
                            //prevINput masih salah, karena prevsppInJSON salah
                            var packingData = JsonConvert.DeserializeObject<List<PackingData>>(prevOut.PrevSppInJson);
                            prevInput = _inputProductionOrderRepository.ReadAll().Where(x => packingData.Any(e => e.Id == x.Id));
                        }
                        else
                        {

                            prevInput = _inputProductionOrderRepository.ReadAll().Where(x => x.Id == prevOut.DyeingPrintingAreaInputProductionOrderId);
                        }
                        foreach (var prevIn in prevInput)
                        {
                            //tidak mengubah row database
                            var newBalance = prevIn.Balance + prevOut.Balance;
                            prevIn.SetBalance(newBalance, "UPDATEWAREHOUSE", "SERVICE");
                        }

                        if (sppInput.Count() == sppDeleted.Count())
                        {
                            prevOut.SetHasNextAreaDocument(false, "UPDATEWAREHOUSE", "SERVICE");
                            prevOut.SetNextAreaInputStatus(null, "UPDATEWAREHOUSE", "SERVICE");
                        }
                        prevOut.SetBalance(prevOut.Balance + spp.Balance, "UPDATEWAREHOUSE", "SERVICE");
                        prevOut.SetPackagingQty(prevOut.PackagingQty + spp.PackagingQty, "UPDATEWAREHOUSE", "SERVICE");
                        result += await _outputProductionOrderRepository.UpdateAsync(prevOut.Id, prevOut);
                    }
                    result += await _inputProductionOrderRepository.DeleteAsync(spp.Id);

                    var movementModel = new DyeingPrintingAreaMovementModel(bon.Date, spp.MaterialOrigin, bon.Area, DyeingPrintingArea.IN, bon.Id, bon.BonNo, spp.ProductionOrderId, spp.ProductionOrderNo,
                               spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance * -1, spp.Id, spp.ProductionOrderType, spp.Grade,
                               null, spp.PackagingType, spp.PackagingQty * -1, spp.PackagingUnit, spp.PackagingLength);
                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            return result;
        }

        public MemoryStream GenerateExcelAll(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, string type, int offSet)
        {
            //var warehouseData = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var warehouseData = _inputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);


            if (dateFrom.HasValue && dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }

            warehouseData = warehouseData.OrderBy(s => s.BonNo);
            //var model = await _repository.ReadByIdAsync(id);
            //var modelWhere = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var modelAll = warehouseData.Select(s =>
                new
                {
                    SppList = s.DyeingPrintingAreaInputProductionOrders.Select(d => new
                    {
                        BonNo = s.BonNo,
                        NoSPP = d.ProductionOrderNo,
                        QtyOrder = d.ProductionOrderOrderQuantity,
                        Material = d.Construction,
                        MaterialOrigin = d.MaterialOrigin,
                        Unit = d.Unit,
                        Buyer = d.Buyer,
                        Warna = d.Color,
                        Motif = d.Motif,
                        Jenis = d.PackagingType,
                        Grade = d.Grade,
                        QtyPack = d.InputPackagingQty,
                        Pack = d.PackagingUnit,
                        Qty = d.InputQuantity,
                        SAT = d.UomUnit,
                        DateIn = d.DateIn,

                    })
                });

            if (type == "BON")
            {
                modelAll = modelAll.Select(s => new
                {
                    SppList = s.SppList.GroupBy(r => new { r.NoSPP, r.Grade }).Select(d => new
                    {
                        BonNo = d.First().BonNo,
                        NoSPP = d.Key.NoSPP,
                        QtyOrder = d.First().QtyOrder,
                        Material = d.First().Material,
                        MaterialOrigin = d.First().MaterialOrigin,
                        Unit = d.First().Unit,
                        Buyer = d.First().Buyer,
                        Warna = d.First().Warna,
                        Motif = d.First().Motif,

                        Jenis = d.First().Jenis,
                        Grade = d.First().Grade,
                        QtyPack = d.Sum(x => x.QtyPack),
                        Pack = d.First().Pack,
                        Qty = d.Sum(x => x.Qty),

                        SAT = d.First().SAT,
                        DateIn = d.First().DateIn,
                    })
                });
            }
            else
            {
                modelAll = modelAll.Select(s => new
                {
                    SppList = s.SppList.Select(d => new
                    {
                        BonNo = d.BonNo,
                        NoSPP = d.NoSPP,
                        QtyOrder = d.QtyOrder,
                        Material = d.Material,
                        MaterialOrigin = d.MaterialOrigin,
                        Unit = d.Unit,
                        Buyer = d.Buyer,
                        Warna = d.Warna,
                        Motif = d.Motif,

                        Jenis = d.Jenis,
                        Grade = d.Grade,
                        QtyPack = d.QtyPack,
                        Pack = d.Pack,
                        Qty = d.Qty,

                        SAT = d.SAT,
                        DateIn = d.DateIn,
                    })
                });
            }

            //var model = modelAll.First();
            //var query = model.DyeingPrintingAreaOutputProductionOrders;
            //var query = modelAll.SelectMany(s => s.SppList);
            var query = modelAll.Select(s => s.SppList);


            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"BonNo","NO BON" },
                {"NoSPP","NO SP" },
                {"DateIn","Tanggal Masuk" },
                {"QtyOrder","QTY ORDER" },
                {"Material","MATERIAL"},
                {"MaterialOrigin", "ASAL MATERIAL" },
                {"Unit","UNIT"},
                {"Buyer","BUYER"},
                {"Warna","WARNA"},
                {"Motif","MOTIF"},
                {"Jenis","JENIS"},
                {"Grade","GRADE"},
                {"QtyPack","QTY Pack"},
                {"Pack","PACK"},
                {"Qty","QTY" },
                {"SAT","SAT" },
            };
            var listClass = query.ToList().FirstOrDefault().GetType().GetProperties();
            #endregion
            #region Assign Column
            foreach (var prop in mappedClass.Select((item, index) => new { Index = index, Items = item }))
            {
                string fieldName = prop.Items.Value;
                dt.Columns.Add(new DataColumn() { ColumnName = fieldName, DataType = typeof(string) });
            }
            #endregion
            #region Assign Data
            foreach (var items in query)
            {
                foreach (var item in items)
                {
                    List<string> data = new List<string>();
                    foreach (DataColumn column in dt.Columns)
                    {
                        var searchMappedClass = mappedClass.Where(x => x.Value == column.ColumnName && column.ColumnName != "Menyerahkan" && column.ColumnName != "Menerima");
                        string valueClass = "";
                        //if (searchMappedClass != null && searchMappedClass != null && searchMappedClass.FirstOrDefault().Key != null)
                        //{
                        var searchProperty = item.GetType().GetProperty(searchMappedClass.FirstOrDefault().Key);
                        var searchValue = searchProperty.GetValue(item, null);
                        if (searchProperty.Name.Equals("DateIn"))
                        {
                            var date = DateTimeOffset.Parse(searchValue.ToString());
                            valueClass = date.Equals(DateTimeOffset.MinValue) ? "" : date.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                        }
                        else
                        {
                            valueClass = searchValue == null ? "" : searchValue.ToString();
                        }
                        data.Add(valueClass);
                    }
                    dt.Rows.Add(data.ToArray());
                }
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("PENERIMAAN GUDANG JADI");

            int startHeaderColumn = 1;
            int endHeaderColumn = mappedClass.Count;

            sheet.Cells[1, 1, 1, endHeaderColumn].Style.Font.Bold = true;


            sheet.Cells[1, startHeaderColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn, 1, endHeaderColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[1, startHeaderColumn, 1, endHeaderColumn].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aqua);

            foreach (DataColumn column in dt.Columns)
            {

                sheet.Cells[1, startHeaderColumn].Value = column.ColumnName;
                sheet.Cells[1, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[1, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                startHeaderColumn++;
            }
            #endregion

            #region Insert Data To Excel
            int tableRowStart = 2;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            #endregion

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        public MemoryStream GenerateExcelAllBarcode(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var warehouseData = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var warehouseData = _inputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);


            if (dateFrom.HasValue && dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }

            warehouseData = warehouseData.OrderBy(s => s.BonNo);

            //var countData = warehouseData.Count();
            //var model = await _repository.ReadByIdAsync(id);
            //var modelWhere = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            _barcodes = new List<BarcodeInfoViewModel2>();
            var modelAll = warehouseData.Select(s =>
                new
                {
                    SppList = s.DyeingPrintingAreaInputProductionOrders.Select(d => new
                    {
                        BonNo = s.BonNo,
                        ProductionOrderNo = d.ProductionOrderNo,
                        Construction = d.Construction,
                        Unit = d.Unit,
                        Color = d.Color,
                        Motif = d.Motif,
                        ProductPackingCodes = d.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
                        PackagingType = d.PackagingType,
                        Grade = d.Grade,
                        InputPackagingQty = d.InputPackagingQty,
                        PackagingUnit = d.PackagingUnit,
                        InputQuantity = d.InputQuantity,
                        UomUnit = d.UomUnit,
                        DateIn = d.DateIn,
                        PackingLength = d.PackagingLength

                    })
                });

            foreach (var data in modelAll.Select( x => x.SppList).SingleOrDefault())
            {

                foreach (var packingCode in data.ProductPackingCodes)
                {
                    var barcodeInfo = new BarcodeInfoViewModel2()
                    {
                        BonNo = data.BonNo,
                        OrderNo = data.ProductionOrderNo,
                        Construction =data.Construction,
                        Unit = data.Unit,
                        Color = data.Color,
                        PackingCode = packingCode,
                        PackingLength = data.PackingLength,
                        PackagingQty = 1,
                        Grade = data.Grade,
                    };
                    _barcodes.Add(barcodeInfo);
                }


            }


            //var model = modelAll.First();
            //var query = model.DyeingPrintingAreaOutputProductionOrders;
            //var query = modelAll.SelectMany(s => s.SppList);
            var query = _barcodes;


            var indexNumber = 1;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO.", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "BON NO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KONSTRUKSI", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "UNIT", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "COLOR", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY PACKING", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "BARCODE", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "GRADE", DataType = typeof(string) });

            decimal qtyRoll = 0;
            double qtyBalance = 0;
            //if (warehouseData.Count() == 0)
            //{
            //    dt.Rows.Add(0, "", "", "", "", "", 0, 0);
            //}
            //else
            //{

                foreach (var item in query)
                {
                    //var dataIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                    //var dataOut = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateOut.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                    qtyRoll += item.PackagingQty;
                    qtyBalance += item.PackingLength;
                    dt.Rows.Add(indexNumber,
                                item.BonNo,
                                item.OrderNo,
                                item.Construction,
                                item.Unit,
                                item.Color,
                                item.PackagingQty,
                                item.PackingLength,
                                item.PackingCode,
                                item.Grade
                                );
                    indexNumber++;
                }
            //}

            ExcelPackage package = new ExcelPackage();
            #region Header
            var sheet = package.Workbook.Worksheets.Add("Barcode List");

            ////sheet.Cells[1, 1].Value = "TANGGAL";
            ////sheet.Cells[1, 2].Value = model.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

            ////sheet.Cells[2, 1].Value = "NO. BON";
            ////sheet.Cells[2, 2].Value = model.BonNo;
            ////sheet.Cells[2, 2, 2, 3].Merge = true;

            var row = 1;
            var merge = 2;

            sheet.Cells[row, 1].Value = "NO.";
            sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 1, merge, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 1, merge, 1].Merge = true;

            sheet.Cells[row, 2].Value = "BON NO";
            sheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 2, merge, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 2, merge, 2].Merge = true;

            sheet.Cells[row, 3].Value = "NO.SPP";
            sheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 3, merge, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 3, merge, 3].Merge = true;

            sheet.Cells[row, 4].Value = "KONSTRUKSI";
            sheet.Cells[row, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 4, merge, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 4, merge, 4].Merge = true;

            sheet.Cells[row, 5].Value = "UNIT";
            sheet.Cells[row, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 5, merge, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 5, merge, 5].Merge = true;

            sheet.Cells[row, 6].Value = "WARNA";
            sheet.Cells[row, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 6, merge, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 6, merge, 6].Merge = true;

            sheet.Cells[row, 7].Value = "QUANTITY  ROLL";
            sheet.Cells[row, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 7, merge, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 7, merge, 7].Merge = true;

            sheet.Cells[row, 8].Value = "QUANTITY";
            sheet.Cells[row, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 8, merge, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 8, merge, 8].Merge = true;

            sheet.Cells[row, 9].Value = "BARCODE";
            sheet.Cells[row, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 9, merge, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 9, merge, 9].Merge = true;

            sheet.Cells[row, 10].Value = "GRADE";
            sheet.Cells[row, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[row, 10, merge, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[row, 10, merge, 10].Merge = true;
            #endregion

            // var a = query.Count();

            var a = query.Count();
            sheet.Cells[$"A{3 + a}"].Value = "T O T A L  . . . . . . . . . . . . . . .";
            sheet.Cells[$"A{3 + a}:F{4 + a}"].Merge = true;
            sheet.Cells[$"A{3 + a}:F{4 + a}"].Style.Font.Bold = true;
            sheet.Cells[$"A{3 + a}:F{4 + a}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{3 + a}:F{4 + a}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            sheet.Cells[$"G{3 + a}"].Value = qtyRoll;
            sheet.Cells[$"H{3 + a}"].Value = qtyBalance;
            //sheet.Cells[$"K{6 + a}"].Value = CorrQtyTotal;
            //sheet.Cells[$"M{6 + a}"].Value = ExpendQtyTotal;
            //sheet.Cells[$"O{6 + a}"].Value = EndingQtyTotal;

            int tableRowStart = 3;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        //    public OutputPreWarehouseItemListViewModel GetOutputPreWarehouseProductionOrdersByCode(string packingCode)
        //    {
        //        var query = _outputProductionOrderRepository.ReadAll()
        //                                                    .OrderByDescending(s => s.LastModifiedUtc)
        //                                                    .Where(s => s.DestinationArea == DyeingPrintingArea.GUDANGJADI &&
        //                                                                s.Balance > 0).Select(p => new OutputPreWarehouseItemListViewModel()
        //                                                                {

        //                                                                    Id = p.Id,
        //                                                                    ProductionOrder = new ProductionOrder()
        //                                                                    {
        //                                                                        Id = p.ProductionOrderId,
        //                                                                        No = p.ProductionOrderNo,
        //                                                                        Type = p.ProductionOrderType,
        //                                                                        OrderQuantity = p.ProductionOrderOrderQuantity
        //                                                                    },
        //                                                                    MaterialWidth = p.MaterialWidth,
        //                                                                    MaterialOrigin = p.MaterialOrigin,
        //                                                                    FinishWidth = p.FinishWidth,
        //                                                                    MaterialConstruction = new MaterialConstruction()
        //                                                                    {
        //                                                                        Id = p.MaterialConstructionId,
        //                                                                        Name = p.MaterialConstructionName
        //                                                                    },
        //                                                                    MaterialProduct = new Material()
        //                                                                    {
        //                                                                        Id = p.MaterialId,
        //                                                                        Name = p.MaterialName
        //                                                                    },
        //                                                                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
        //                                                                    {
        //                                                                        Id = p.ProcessTypeId,
        //                                                                        Name = p.ProcessTypeName
        //                                                                    },
        //                                                                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
        //                                                                    {
        //                                                                        Id = p.YarnMaterialId,
        //                                                                        Name = p.YarnMaterialName
        //                                                                    },
        //                                                                    CartNo = p.CartNo,
        //                                                                    Buyer = p.Buyer,
        //                                                                    BuyerId = p.BuyerId,
        //                                                                    Construction = p.Construction,
        //                                                                    Unit = p.Unit,
        //                                                                    Color = p.Color,
        //                                                                    Motif = p.Motif,
        //                                                                    UomUnit = p.UomUnit,
        //                                                                    Remark = p.Remark,
        //                                                                    OutputId = p.DyeingPrintingAreaOutputId,
        //                                                                    Grade = p.Grade,
        //                                                                    Status = p.Status,
        //                                                                    Balance = p.Balance,
        //                                                                    InputQuantity = p.Balance,
        //                                                                    PackingInstruction = p.PackingInstruction,
        //                                                                    PackagingType = p.PackagingType,
        //                                                                    PackagingQty = p.PackagingQty,
        //                                                                    InputPackagingQty = p.PackagingQty,
        //                                                                    PackagingUnit = p.PackagingUnit,
        //                                                                    AvalALength = p.AvalALength,
        //                                                                    AvalBLength = p.AvalBLength,
        //                                                                    AvalConnectionLength = p.AvalConnectionLength,
        //                                                                    DeliveryOrderSalesId = p.DeliveryOrderSalesId,
        //                                                                    DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
        //                                                                    AvalType = p.AvalType,
        //                                                                    AvalCartNo = p.AvalCartNo,
        //                                                                    AvalQuantityKg = p.AvalQuantityKg,
        //                                                                    Description = p.Description,
        //                                                                    DeliveryNote = p.DeliveryNote,
        //                                                                    Area = p.Area,
        //                                                                    DestinationArea = p.DestinationArea,
        //                                                                    HasNextAreaDocument = p.HasNextAreaDocument,
        //                                                                    DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
        //                                                                    Qty = p.PackagingLength,
        //                                                                    ProductSKUId = p.ProductSKUId,
        //                                                                    FabricSKUId = p.FabricSKUId,
        //                                                                    ProductSKUCode = p.ProductSKUCode,
        //                                                                    HasPrintingProductSKU = p.HasPrintingProductSKU,
        //                                                                    ProductPackingId = p.ProductPackingId,
        //                                                                    FabricPackingId = p.FabricPackingId,
        //                                                                    ProductPackingCode = p.ProductPackingCode,
        //                                                                    HasPrintingProductPacking = p.HasPrintingProductPacking,
        //                                                                    PreviousOutputPackagingQty = p.PackagingQty,
        //                                                                    PrevSppInJson = p.PrevSppInJson
        //                                                                });

        //        return query.FirstOrDefault(entity => entity.ProductPackingCode.Contains(packingCode));
        //    }
        //}

        public OutputPreWarehouseItemListViewModel GetOutputPreWarehouseProductionOrdersByCode(string packingCode)
        {
            var query = _outputProductionOrderRepository.ReadAll()
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.DestinationArea == DyeingPrintingArea.GUDANGJADI &&
                                                                    s.Balance > 0 && !s.HasNextAreaDocument).Select(p => new OutputPreWarehouseItemListViewModel()
                                                                    {

                                                                        Id = p.Id,
                                                                        ProductionOrder = new ProductionOrder()
                                                                        {
                                                                            Id = p.ProductionOrderId,
                                                                            No = p.ProductionOrderNo,
                                                                            Type = p.ProductionOrderType,
                                                                            OrderQuantity = p.ProductionOrderOrderQuantity
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
                                                                        OutputId = p.DyeingPrintingAreaOutputId,
                                                                        Grade = p.Grade,
                                                                        Status = p.Status,
                                                                        Balance = p.Balance,
                                                                        InputQuantity = p.Balance,
                                                                        PackingInstruction = p.PackingInstruction,
                                                                        PackagingType = p.PackagingType,
                                                                        PackagingQty = p.PackagingQty,
                                                                        InputPackagingQty = p.PackagingQty,
                                                                        PackagingUnit = p.PackagingUnit,
                                                                        AvalALength = p.AvalALength,
                                                                        AvalBLength = p.AvalBLength,
                                                                        AvalConnectionLength = p.AvalConnectionLength,
                                                                        DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                                                                        DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                                                                        AvalType = p.AvalType,
                                                                        AvalCartNo = p.AvalCartNo,
                                                                        AvalQuantityKg = p.AvalQuantityKg,
                                                                        Description = p.Description,
                                                                        DeliveryNote = p.DeliveryNote,
                                                                        Area = p.Area,
                                                                        DestinationArea = p.DestinationArea,
                                                                        HasNextAreaDocument = p.HasNextAreaDocument,
                                                                        DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                                                                        Qty = p.PackagingLength,
                                                                        ProductSKUId = p.ProductSKUId,
                                                                        FabricSKUId = p.FabricSKUId,
                                                                        ProductSKUCode = p.ProductSKUCode,
                                                                        HasPrintingProductSKU = p.HasPrintingProductSKU,
                                                                        ProductPackingId = p.ProductPackingId,
                                                                        FabricPackingId = p.FabricPackingId,
                                                                        ProductPackingCode = p.ProductPackingCode,
                                                                        HasPrintingProductPacking = p.HasPrintingProductPacking,
                                                                        PreviousOutputPackagingQty = p.PackagingQty,
                                                                        PrevSppInJson = p.PrevSppInJson
                                                                    });

            return query.FirstOrDefault(entity => entity.ProductPackingCode.Contains(packingCode));
        }

        public string GetValidationMessage(string packingCode)
        {
            var input = _inputProductionOrderRepository.ReadAll().Where(entity => entity.ProductPackingCode.Contains(packingCode)).OrderByDescending(entity => entity.CreatedUtc).FirstOrDefault();
            if (input == null)
            {
                var output = _outputProductionOrderRepository.ReadAll().Where(entity => entity.ProductPackingCode.Contains(packingCode) && entity.DestinationArea == DyeingPrintingArea.GUDANGJADI &&
                                                                    entity.Balance > 0).OrderByDescending(entity => entity.CreatedUtc).FirstOrDefault();
                if (output == null)
                {
                    //if (input.CreatedUtc > output.CreatedUtc)
                    return "Kode Packing Tidak Dapat Diterima";
                }
            }
            else
            {
                var output = _outputProductionOrderRepository.ReadAll().Where(entity => entity.ProductPackingCode.Contains(packingCode) && entity.DestinationArea == DyeingPrintingArea.GUDANGJADI &&
                                                                    entity.Balance > 0).OrderByDescending(entity => entity.CreatedUtc).FirstOrDefault();

                if (output == null)
                {
                    if (input.CreatedUtc > output.CreatedUtc)
                        return "Kode Packing Masih di Gudang";
                }
            }

            return "";
        }
    }

    public class PackingComparer : IEqualityComparer<ProductionOrderItemListDetailViewModel>
    {
        public bool Equals(ProductionOrderItemListDetailViewModel x, ProductionOrderItemListDetailViewModel y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ProductionOrderItemListDetailViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class BarcodeInfoViewModel2
    {
        public string BonNo { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string PackingCode { get; set; }
        public double PackingLength { get; set; }
        public string Color { get; set; }
        public string OrderNo { get; set; }
        public string Grade { get; set; }
        public decimal PackagingQty { get; set; }
    }
}