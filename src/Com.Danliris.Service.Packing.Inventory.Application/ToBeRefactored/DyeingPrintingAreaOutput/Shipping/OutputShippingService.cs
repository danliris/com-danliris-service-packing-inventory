using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingService : IOutputShippingService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _productionOrderRepository;

        private const string TYPE = "OUT";

        private const string IM = "IM";
        private const string TR = "TR";
        private const string PC = "PC";
        private const string GJ = "GJ";
        private const string GA = "GA";
        private const string SP = "SP";
        private const string PJ = "PJ";

        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";
        private const string PENJUALAN = "PENJUALAN";
        private const string BUYER = "BUYER";

        private const string BuyerId = "BuyerId";

        public OutputShippingService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private OutputShippingViewModel MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputShippingViewModel()
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
                Shift = model.Shift,
                DestinationArea = model.DestinationArea,
                HasNextAreaDocument = model.HasNextAreaDocument,
                Group = model.Group,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = model.DeliveryOrderSalesId,
                    No = model.DeliveryOrderSalesNo
                },
                ShippingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputShippingProductionOrderViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Buyer = s.Buyer,
                    BuyerId = s.BuyerId,
                    CartNo = s.CartNo,
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Grade = s.Grade,
                    Remark = s.Remark,
                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    Packing = s.PackagingUnit,
                    QtyPacking = s.PackagingQty,
                    PackingType = s.PackagingType,
                    HasNextAreaDocument = s.HasNextAreaDocument,
                    Motif = s.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        Type = s.ProductionOrderType,
                        OrderQuantity = s.ProductionOrderOrderQuantity
                    },
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = s.DeliveryOrderSalesId,
                        No = s.DeliveryOrderSalesNo
                    },
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    DeliveryNote = s.DeliveryNote,
                    Qty = s.Balance
                }).ToList()
            };


            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}.{3}", SP, PJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));


        }

        public async Task<int> Create(OutputShippingViewModel viewModel)
        {
            int result = 0;

            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == SHIPPING && s.DestinationArea == viewModel.DestinationArea
                && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING
                    && s.DestinationArea == PENJUALAN && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);

                if (viewModel.DestinationArea == PENJUALAN)
                {
                    model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group,
                        viewModel.DeliveryOrder.Id, viewModel.DeliveryOrder.No,
                        viewModel.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                       s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId)).ToList());
                }
                else
                {
                    model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, false, viewModel.DestinationArea, viewModel.Group,
                        viewModel.DeliveryOrder.Id, viewModel.DeliveryOrder.No,
                        viewModel.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId)).ToList());
                }


                result = await _repository.InsertAsync(model);
                foreach (var item in viewModel.ShippingProductionOrders)
                {
                    if (viewModel.DestinationArea == PENJUALAN)
                    {

                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, true);
                    }
                    else
                    {
                        result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.Id, true);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.InputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty);

                    result += await _movementRepository.InsertAsync(movementModel);
                    if (previousSummary == null)
                    {

                        result += await _summaryRepository.InsertAsync(summaryModel);
                    }
                    else
                    {

                        result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    }
                }
            }
            else
            {
                foreach (var item in viewModel.ShippingProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.DeliveryOrder.Id, item.DeliveryOrder.No,
                        item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.Buyer, item.Construction,
                       item.Unit, item.Color, item.Motif, item.Grade, item.UomUnit, item.DeliveryNote, item.Qty, item.Id, item.Packing, item.PackingType, item.QtyPacking, item.BuyerId);
                    modelItem.DyeingPrintingAreaOutputId = model.Id;


                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.InputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty);

                    result += await _productionOrderRepository.InsertAsync(modelItem);
                    if (viewModel.DestinationArea == PENJUALAN)
                    {

                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, true);
                    }
                    else
                    {
                        result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.Id, true);
                    }
                    result += await _movementRepository.InsertAsync(movementModel);
                    if (previousSummary == null)
                    {

                        result += await _summaryRepository.InsertAsync(summaryModel);
                    }
                    else
                    {

                        result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    }
                }
            }



            return result;
        }

        public async Task<MemoryStream> GenerateExcel(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            var query = model.DyeingPrintingAreaOutputProductionOrders;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. DO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Ket", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY Packing", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", 0, 0, "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.DeliveryOrderSalesNo, item.ProductionOrderNo, item.Buyer, item.Construction, "", item.Color, item.Motif, item.Grade, "", item.Remark, 0, 0,
                        item.UomUnit, "");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Shipping Area Dyeing Printing") }, true);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == SHIPPING && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                DestinationArea = s.DestinationArea,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo
                },
                HasNextAreaDocument = s.HasNextAreaDocument,
                ShippingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputShippingProductionOrderViewModel()
                {
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo
                    },
                    BuyerId = d.BuyerId,
                    Buyer = d.Buyer,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    Motif = d.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType
                    },
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    Remark = d.Remark,
                    PackingType = d.PackagingType,
                    QtyPacking = d.PackagingQty,
                    Packing = d.PackagingUnit,
                    UomUnit = d.UomUnit,
                    DeliveryNote = d.DeliveryNote,
                    DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId,
                    Qty = d.Balance,
                    InputId = d.DyeingPrintingAreaOutputId

                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<OutputShippingViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputShippingViewModel vm = MapToViewModel(model);

            return vm;
        }

        public List<InputShippingProductionOrderViewModel> GetInputShippingProductionOrdersByDeliveryOrder(long deliveryOrderId)
        {
            var productionOrders = _inputProductionOrderRepository.ReadAll();

            productionOrders = productionOrders.Where(s => s.Area == SHIPPING && !s.HasOutputDocument && s.DeliveryOrderSalesId == deliveryOrderId).OrderByDescending(s => s.LastModifiedUtc);
            var data = productionOrders.Select(d => new InputShippingProductionOrderViewModel()
            {
                Buyer = d.Buyer,
                BuyerId = d.BuyerId,
                CartNo = d.CartNo,
                Color = d.Color,
                Construction = d.Construction,
                HasOutputDocument = d.HasOutputDocument,
                Motif = d.Motif,
                ProductionOrder = new ProductionOrder()
                {
                    Id = d.ProductionOrderId,
                    No = d.ProductionOrderNo,
                    Type = d.ProductionOrderType,
                    OrderQuantity = d.ProductionOrderOrderQuantity,
                },
                Grade = d.Grade,
                Id = d.Id,
                Unit = d.Unit,
                UomUnit = d.UomUnit,
                InputId = d.DyeingPrintingAreaInputId,
                QtyPacking = d.PackagingQty,
                PackingType = d.PackagingType,
                Packing = d.PackagingUnit,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = d.DeliveryOrderSalesId,
                    No = d.DeliveryOrderSalesNo
                },
                Qty = d.Balance

            });

            return data.ToList();
        }

        public ListResult<IndexViewModel> ReadForSales(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == SHIPPING && s.DestinationArea == PENJUALAN);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            var parentFilterDictionary = FilterDictionary.Where(s => s.Key != BuyerId).ToDictionary(s => s.Key, s => s.Value);
            object buyerData;
            int buyerId = 0;
            if (FilterDictionary.TryGetValue(BuyerId, out buyerData))
            {
                buyerId = Convert.ToInt32(buyerData);
            }
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, parentFilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Where(s => s.DyeingPrintingAreaOutputProductionOrders.Any(d => d.BuyerId == buyerId)).Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                DestinationArea = s.DestinationArea,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo
                },
                HasNextAreaDocument = s.HasNextAreaDocument,
                ShippingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Where(d => d.BuyerId == buyerId).Select(d => new OutputShippingProductionOrderViewModel()
                {
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo
                    },
                    BuyerId = d.BuyerId,
                    Buyer = d.Buyer,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    Motif = d.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType
                    },
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    Remark = d.Remark,
                    PackingType = d.PackagingType,
                    QtyPacking = d.PackagingQty,
                    Packing = d.PackagingUnit,
                    UomUnit = d.UomUnit,
                    DeliveryNote = d.DeliveryNote,
                    DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId,
                    Qty = d.Balance,
                    InputId = d.DyeingPrintingAreaOutputId

                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        //public List<OutputShippingViewModel> GetOutputShippingProductionOrdersByBon(int shippingOutputId)
        //{
        //    var productionOrders = _productionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
        //       .Where(s => s.Area == SHIPPING && s.DyeingPrintingAreaOutputId == shippingOutputId && s.DestinationArea == PENJUALAN
        //                && !s.HasNextAreaDocument);
        //    var data = productionOrders.Select(d => new OutputShippingProductionOrderViewModel()
        //    {
        //        Buyer = d.Buyer,
        //        CartNo = d.CartNo,
        //        Color = d.Color,
        //        Construction = d.Construction,
        //        Motif = d.Motif,
        //        ProductionOrder = new ProductionOrder()
        //        {
        //            Id = d.ProductionOrderId,
        //            No = d.ProductionOrderNo,
        //            Type = d.ProductionOrderType,
        //            OrderQuantity = d.ProductionOrderOrderQuantity,
        //        },
        //        Grade = d.Grade,
        //        Id = d.Id,
        //        Unit = d.Unit,
        //        UomUnit = d.UomUnit,
        //        DeliveryOrder = new DeliveryOrderSales()
        //        {
        //            Id = d.DeliveryOrderSalesId,
        //            No = d.DeliveryOrderSalesNo
        //        },
        //        Qty = d.Balance,
        //        Remark = d.Remark,
        //        DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId

        //    });

        //    return data.ToList();
        //}
    }
}
