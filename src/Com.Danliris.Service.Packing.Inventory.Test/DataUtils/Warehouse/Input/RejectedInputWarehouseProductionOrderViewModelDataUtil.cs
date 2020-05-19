using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Warehouse.Input
{
    public class RejectedInputWarehouseProductionOrderViewModelDataUtil
    {
        private RejectedInputWarehouseProductionOrderViewModel RejectedInputWarehouseProductionOrderViewModel
        {
            get
            {
                return new RejectedInputWarehouseProductionOrderViewModel()
                {
                    Id = 10,
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
                    HasOutputDocument = true,
                    IsChecked = false,
                    Area = "a",
                    InputId = 12,
                    OutputId = 10,
                    DyeingPrintingAreaInputProductionOrderId = 4
                };
            }
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorProductionOrderId_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.ProductionOrder.Id);
        }

        [Fact]
        public void Should_ValidatorProductionOrderCode_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.ProductionOrder.Code);
        }

        [Fact]
        public void Should_ValidatorProductionOrderNo_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.ProductionOrder.No);
        }

        [Fact]
        public void Should_ValidatorProductionOrderType_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.ProductionOrder.Type);
        }

        [Fact]
        public void Should_ValidatorProductionOrderOrderQuantity_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.ProductionOrder.OrderQuantity);
        }

        [Fact]
        public void Should_ValidatorCartNo_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.CartNo);
        }

        [Fact]
        public void Should_ValidatorBuyer_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Buyer);
        }

        [Fact]
        public void Should_ValidatorConstruction_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Construction);
        }

        [Fact]
        public void Should_ValidatorUnit_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Unit);
        }

        [Fact]
        public void Should_ValidatorColor_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Color);
        }

        [Fact]
        public void Should_ValidatorMotif_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Motif);
        }

        [Fact]
        public void Should_ValidatorUomUnit_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.UomUnit);
        }

        [Fact]
        public void Should_ValidatorRemark_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Remark);
        }

        [Fact]
        public void Should_ValidatorGrade_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Grade);
        }

        [Fact]
        public void Should_ValidatorStatus_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Status);
        }

        [Fact]
        public void Should_ValidatorBalance_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.Balance);
        }

        [Fact]
        public void Should_ValidatorPackingInstruction_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.PackingInstruction);
        }

        [Fact]
        public void Should_ValidatorPackagingType_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.PackagingType);
        }

        [Fact]
        public void Should_ValidatorPackagingQty_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.PackagingQty);
        }

        [Fact]
        public void Should_ValidatorPackagingUnit_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.PackagingUnit);
        }

        [Fact]
        public void Should_ValidatorAvalALength_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.AvalALength);
        }

        [Fact]
        public void Should_ValidatorAvalBLength_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.AvalBLength);
        }

        [Fact]
        public void Should_ValidatorAvalConnectionLength_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.AvalConnectionLength);
        }

        [Fact]
        public void Should_ValidatorDeliveryOrderSalesId_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.DeliveryOrderSalesId);
        }

        [Fact]
        public void Should_ValidatorDeliveryOrderSalesNo_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.DeliveryOrderSalesNo);
        }

        [Fact]
        public void Should_ValidatorAvalType_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.AvalType);
        }

        [Fact]
        public void Should_ValidatorAvalCartNo_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.AvalCartNo);
        }

        [Fact]
        public void Should_ValidatorAvalQuantityKg_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.AvalQuantityKg);
        }

        [Fact]
        public void Should_ValidatorDescription_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Description);
        }

        [Fact]
        public void Should_ValidatorDeliveryNote_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.DeliveryNote);
        }

        [Fact]
        public void Should_ValidatorHasOutputDocument_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.True(dataUtil.HasOutputDocument);
        }

        [Fact]
        public void Should_ValidatorIsChecked_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.False(dataUtil.IsChecked);
        }

        [Fact]
        public void Should_ValidatorArea_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotNull(dataUtil.Area);
        }

        [Fact]
        public void Should_ValidatorInputId_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.InputId);
        }

        [Fact]
        public void Should_ValidatorOutputId_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.OutputId);
        }

        [Fact]
        public void Should_ValidatorDyeingPrintingAreaInputProductionOrderId_Success()
        {
            var dataUtil = RejectedInputWarehouseProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.DyeingPrintingAreaInputProductionOrderId);
        }
    }
}
