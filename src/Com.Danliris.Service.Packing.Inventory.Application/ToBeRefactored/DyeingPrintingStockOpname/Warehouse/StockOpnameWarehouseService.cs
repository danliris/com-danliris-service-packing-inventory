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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{


    public class StockOpnameWarehouseService : IStockOpnameWarehouseService
    {

        private readonly IDyeingPrintingStockOpnameRepository _stockOpnameRepository;
        private readonly IDyeingPrintingStockOpnameProductionOrderRepository _outputProductionOrderRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;

        public StockOpnameWarehouseService(IServiceProvider serviceProvider)
        {
            _stockOpnameRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameProductionOrderRepository>();
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
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
                                                                                        s.Date.Date == viewModel.Date.Date &&
                                                                                        s.Type == DyeingPrintingArea.GUDANGJADI);
            //viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.Where(s => s.IsSave).ToList();
            viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.ToList();
            if (model == null)
            {
                int totalCurrentYearData = _stockOpnameRepository.ReadAllIgnoreQueryFilter()
                                                            .Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                        s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.STOCK_OPNAME);

                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.Area);

                model = new DyeingPrintingStockOpnameModel(viewModel.Area, bonNo, viewModel.Date, viewModel.Type,
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
                                                                s.UomUnit
                                                                )).ToList());


                result = await _stockOpnameRepository.InsertAsync(model);
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
                                                                item.UomUnit
                                                                );

                    modelItem.DyeingPrintingStockOpnameId = model.Id;

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);

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




        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _stockOpnameRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);


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
                Type = s.Type == null || s.Type == DyeingPrintingArea.STOCK_OPNAME ? DyeingPrintingArea.STOCK_OPNAME : DyeingPrintingArea.STOCK_OPNAME,
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
                s.UomUnit
                     )

                {
                    Id = s.Id
                }).ToList());


            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            Dictionary<int, decimal> dictQtyPacking = new Dictionary<int, decimal>();
            foreach (var item in dbModel.DyeingPrintingStockOpnameProductionOrders)
            {
                var lclModel = model.DyeingPrintingStockOpnameProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;
                    var diffQtyPacking = lclModel.PackagingQty - item.PackagingQty;
                    dictBalance.Add(lclModel.Id, diffBalance);
                    dictQtyPacking.Add(lclModel.Id, diffQtyPacking);
                }
            }

            var deletedData = dbModel.DyeingPrintingStockOpnameProductionOrders.Where(s => !model.DyeingPrintingStockOpnameProductionOrders.Any(d => d.Id == s.Id)).ToList();

            // result = await _outputRepository.UpdateAdjustmentData(id, model, dbModel);
            if(dbModel != null)
            {
                result = await _stockOpnameRepository.UpdateAsync(id, model);
            }
          

            return result;
        }

        private async Task<StockOpnameWarehouseViewModel> MapToViewModel(DyeingPrintingStockOpnameModel model)
        {
            var vm = new StockOpnameWarehouseViewModel();
            if (model.Type == null || model.Type == DyeingPrintingArea.STOCK_OPNAME)
            {
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
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.YarnMaterialId,
                            Name = s.YarnMaterialName
                        },
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = s.ProcessTypeId,
                            Name = s.ProcessTypeName
                        },
                        UomUnit = s.UomUnit,
                        Uom =new UnitOfMeasurement()
                        {
                            Unit= s.PackagingUnit
                        },
                        PackagingQty = s.PackagingQty,
                        PackagingType = s.PackagingType,
                        PackagingUnit = s.PackagingUnit,
                        ProductionOrderNo = s.ProductionOrderNo,
                        QtyOrder = s.ProductionOrderOrderQuantity,
                        DocumentNo = s.DocumentNo,
                        Quantity = s.PackagingLength,
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
            }
            else
            {
                vm = new StockOpnameWarehouseViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
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
                    Type = DyeingPrintingArea.STOCK_OPNAME,
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
                        DocumentNo = s.DocumentNo,
                        Quantity = s.PackagingLength,
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
                        GradeProduct=new GradeProduct { 
                        Type =s.Grade
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
                        Uom =new UnitOfMeasurement()
                        {
                            Unit=s.PackagingUnit
                        },
                        UomUnit = s.UomUnit,
                        PackagingQty = s.PackagingQty,
                        PackagingType = s.PackagingType,
                        PackagingUnit = s.PackagingUnit,
                        ProductionOrderNo = s.ProductionOrderNo,
                        QtyOrder = s.ProductionOrderOrderQuantity,
                       
                        //DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                    }).ToList()
                };

                //foreach (var item in vm.WarehousesProductionOrders)
                //{
                //    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                //    if (inputData != null)
                //    {
                //        item.BalanceRemains = inputData.BalanceRemains + (item.Balance * -1);
                //    }
                //}

            }


            return vm;
        }

        public Task<List<StockOpnameWarehouseViewModel>> GetSppByBonNo(int bonId)
        {
            throw new NotImplementedException();
        }

        //public Task<List<StockOpnameWarehouseViewModel>> GetSppByBonNo(int bonId)
        //{
        //    var query = _outputProductionOrderRepository.ReadAll()
        //                                                .Join(_outputRepository.ReadAll().Where(x => x.Id == bonId),
        //                                                spp => spp.DyeingPrintingStockOpnameId,
        //                                                bon => bon.Id,
        //                                                (spp, bon) => spp)
        //                                                .OrderByDescending(s => s.LastModifiedUtc)
        //                                                .Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);

        //    //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

        //    var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new StockOpnameWarehouseViewModel()
        //    {
        //        ProductionOrderId = s.Key.ProductionOrderId,
        //        ProductionOrderNo = s.Key.ProductionOrderNo,
        //        ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
        //        ProductionOrderType = s.Key.ProductionOrderType,
        //        ProductionOrderItems = s.Select(p => new InputSppWarehouseItemListViewModel()
        //        {
        //            MaterialConstruction = new MaterialConstruction()
        //            {
        //                Id = p.MaterialConstructionId,
        //                Name = p.MaterialConstructionName
        //            },
        //            MaterialProduct = new Material()
        //            {
        //                Id = p.MaterialId,
        //                Name = p.MaterialName
        //            },
        //            YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
        //            {
        //                Id = p.YarnMaterialId,
        //                Name = p.YarnMaterialName
        //            },
        //            ProcessType = new CommonViewModelObjectProperties.ProcessType()
        //            {
        //                Id = p.ProcessTypeId,
        //                Name = p.ProcessTypeName
        //            },
        //            MaterialWidth = p.MaterialWidth,
        //            FinishWidth = p.FinishWidth,
        //            Id = p.Id,
        //            ProductionOrder = new ProductionOrder()
        //            {
        //                Id = s.Key.ProductionOrderId,
        //                No = s.Key.ProductionOrderNo,
        //                Type = s.Key.ProductionOrderType,
        //                OrderQuantity = s.Key.ProductionOrderOrderQuantity
        //            },
        //            CartNo = p.CartNo,
        //            Buyer = p.Buyer,
        //            BuyerId = p.BuyerId,
        //            Construction = p.Construction,
        //            Unit = p.Unit,
        //            Color = p.Color,
        //            Motif = p.Motif,
        //            UomUnit = p.UomUnit,
        //            Remark = p.Remark,
        //            //InputId = p.DyeingPrintingAreaOutputId,
        //            Grade = p.Grade,
        //            Status = p.Status,
        //            Balance = p.Balance,
        //            PackingInstruction = p.PackingInstruction,
        //            PackagingType = p.PackagingType,
        //            PackagingQty = p.PackagingQty,
        //            PackagingUnit = p.PackagingUnit,
        //            AvalALength = p.AvalALength,
        //            AvalBLength = p.AvalBLength,
        //            AvalConnectionLength = p.AvalConnectionLength,
        //            DeliveryOrderSalesId = p.DeliveryOrderSalesId,
        //            DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
        //            AvalType = p.AvalType,
        //            AvalCartNo = p.AvalCartNo,
        //            AvalQuantityKg = p.AvalQuantityKg,
        //            //Description = p.Description,
        //            //DeliveryNote = p.DeliveryNote,
        //            Area = p.Area,
        //            //DestinationArea = p.DestinationArea,
        //            HasOutputDocument = p.HasNextAreaDocument,
        //            HasNextAreaDocument = p.HasNextAreaDocument,
        //            //DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
        //            Qty = p.PackagingLength,
        //            Quantity = p.PackagingLength,
        //            ProductSKUId = p.ProductSKUId,
        //            FabricSKUId = p.FabricSKUId,
        //            ProductSKUCode = p.ProductSKUCode,
        //            HasPrintingProductSKU = p.HasPrintingProductSKU,
        //            ProductPackingId = p.ProductPackingId,
        //            FabricPackingId = p.FabricPackingId,
        //            ProductPackingCode = p.ProductPackingCode,
        //            HasPrintingProductPacking = p.HasPrintingProductPacking,
        //            DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId
        //        }).ToList()

        //    }).ToList();
        //    foreach (var item in data)
        //    {
        //        foreach (var detail in item.ProductionOrderItems)
        //        {
        //            var inputData = await _inputProductionOrderRepository.ReadByIdAsync(detail.DyeingPrintingAreaInputProductionOrderId);
        //            if (inputData != null)
        //            {
        //                detail.BalanceRemains = inputData.BalanceRemains + detail.Balance;
        //                detail.PreviousBalance = detail.BalanceRemains;
        //            }
        //        }
        //    }
        //    return data;

        //}
    }
}
