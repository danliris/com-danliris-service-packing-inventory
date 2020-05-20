using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Warehouse.Input
{
    public class OutputPreWarehouseIndexViewModelDataUtil
    {
        private OutputPreWarehouseIndexViewModel OutputPreWarehouseIndexViewModel
        {
            get
            {
                return new OutputPreWarehouseIndexViewModel()
                {
                    Id = 1,
                    ProductionOrderCode = "SLD",
                    ProductionOrderId = 62,
                    ProductionOrderType = "SOLID",
                    ProductionOrderNo = "F/2020/000",
                    ProductionOrderOrderQuantity = 12,
                    OutputId = 10,
                    ProductionOrderItems = new List<ProductionOrderItemListViewModel>()
                    {
                        new ProductionOrderItemListViewModel()
                        {
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
                            Description = "a",
                            DeliveryNote = "a",
                            Area = "a",
                            DestinationArea = "a",
                            HasNextAreaDocument = false,
                            DyeingPrintingAreaInputProductionOrderId = 4,
                            Qty = 100/10
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = OutputPreWarehouseIndexViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorProductionOrderId_Success()
        {
            var dataUtil = OutputPreWarehouseIndexViewModel;
            Assert.NotEqual(0, dataUtil.ProductionOrderId);
        }

        [Fact]
        public void Should_ValidatorProductionOrderCode_Success()
        {
            var dataUtil = OutputPreWarehouseIndexViewModel;
            Assert.NotNull(dataUtil.ProductionOrderCode);
        }

        [Fact]
        public void Should_ValidatorProductionOrderNo_Success()
        {
            var dataUtil = OutputPreWarehouseIndexViewModel;
            Assert.NotNull(dataUtil.ProductionOrderNo);
        }

        [Fact]
        public void Should_ValidatorProductionOrderType_Success()
        {
            var dataUtil = OutputPreWarehouseIndexViewModel;
            Assert.NotNull(dataUtil.ProductionOrderType);
        }

        [Fact]
        public void Should_ValidatorProductionOrderOrderQuantity_Success()
        {
            var dataUtil = OutputPreWarehouseIndexViewModel;
            Assert.NotEqual(0, dataUtil.ProductionOrderOrderQuantity);
        }

        [Fact]
        public void Should_ValidatorOutputId_Success()
        {
            var dataUtil = OutputPreWarehouseIndexViewModel;
            Assert.NotEqual(0, dataUtil.OutputId);
        }
    }
}
