using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Detail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Warehouse.Input
{
    public class InputWarehouseDetailViewModelDataUtil
    {
        private InputWarehouseDetailViewModel InputWarehouseDetailViewModel
        {
            get
            {
                return new InputWarehouseDetailViewModel()
                {
                    Id = 1,
                    Area = "GUDANG JADI",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    Group = "A",
                    WarehousesProductionOrders = new List<InputWarehouseProductionOrderDetailViewModel>()
                    {
                        new InputWarehouseProductionOrderDetailViewModel()
                        {
                            ProductionOrderId = 12,
                            ProductionOrderCode = "a",
                            ProductionOrderNo = "a123",
                            ProductionOrderType  = "qwe",
                            ProductionOrderOrderQuantity = 100,
                            ProductionOrderItems = new List<ProductionOrderItemListDetailViewModel>()
                            {
                                new ProductionOrderItemListDetailViewModel()
                                {
                                    Id = 3,
                                    ProductionOrder = new ProductionOrder()
                                    {
                                        Code = "SLD",
                                        Id = 62,
                                        Type = "SOLID",
                                        No = "F/2020/000",
                                        OrderQuantity = 12
                                    },
                                    CartNo = "9",
                                    Buyer = "ANAS",
                                    Construction = "a",
                                    Unit = "a",
                                    Color = "a",
                                    Motif = "a",
                                    UomUnit = "a",
                                    Remark = "a",
                                    Grade = "a",
                                    Status = "a",
                                    Balance = 100,
                                    PackingInstruction = "a",
                                    PackagingType = "a",
                                    PackagingQty = 10,
                                    PackagingUnit = "a",
                                    AvalALength = 10,
                                    AvalBLength = 10,
                                    AvalConnectionLength = 10,
                                    DeliveryOrderSalesId = 3,
                                    DeliveryOrderSalesNo = "a",
                                    AvalType = "a",
                                    AvalCartNo = "a",
                                    AvalQuantityKg = 5,
                                    //Description = "a",
                                    //DeliveryNote = "a",
                                    Area = "a",
                                    //DestinationArea = "a",
                                    HasOutputDocument = false,
                                    DyeingPrintingAreaInputId = 1,
                                    Qty = 100 / 10
                                }
                            }
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = InputWarehouseDetailViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorArea_Success()
        {
            var dataUtil = InputWarehouseDetailViewModel;
            Assert.NotNull(dataUtil.Area);
        }

        [Fact]
        public void Should_ValidatorBonNo_Success()
        {
            var dataUtil = InputWarehouseDetailViewModel;
            Assert.NotNull(dataUtil.BonNo);
        }

        [Fact]
        public void Should_ValidatorDate_Success()
        {
            var dataUtil = InputWarehouseDetailViewModel;
            Debug.Assert((dataUtil.Date - DateTimeOffset.UtcNow) < TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Should_ValidatorShift_Success()
        {
            var dataUtil = InputWarehouseDetailViewModel;
            Assert.NotNull(dataUtil.Shift);
        }

        [Fact]
        public void Should_ValidatorGroup_Success()
        {
            var dataUtil = InputWarehouseDetailViewModel;
            Assert.NotNull(dataUtil.Group);
        }

        [Fact]
        public void Should_ValidatorWarehousesProductionOrders_Success()
        {
            var dataUtil = InputWarehouseDetailViewModel;
            Assert.NotEmpty(dataUtil.WarehousesProductionOrders);
        }
    }
}
