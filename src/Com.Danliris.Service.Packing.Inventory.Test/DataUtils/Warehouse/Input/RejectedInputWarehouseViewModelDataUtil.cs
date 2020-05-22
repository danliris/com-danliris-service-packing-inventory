using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Reject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Warehouse.Input
{
    public class RejectedInputWarehouseViewModelDataUtil
    {
        private RejectedInputWarehouseViewModel RejectedInputWarehouseViewModel
        {
            get
            {
                return new RejectedInputWarehouseViewModel
                {
                    Id = 1,
                    Area = "GUDANG JADI",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    OutputId = 2,
                    Group = "A",
                    WarehousesProductionOrders = new List<RejectedInputWarehouseProductionOrderViewModel>()
                    {
                        new RejectedInputWarehouseProductionOrderViewModel()
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
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = RejectedInputWarehouseViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorArea_Success()
        {
            var dataUtil = RejectedInputWarehouseViewModel;
            Assert.NotNull(dataUtil.Area);
        }

        [Fact]
        public void Should_ValidatorBonNo_Success()
        {
            var dataUtil = RejectedInputWarehouseViewModel;
            Assert.NotNull(dataUtil.BonNo);
        }

        [Fact]
        public void Should_ValidatorDate_Success()
        {
            var dataUtil = RejectedInputWarehouseViewModel;
            Debug.Assert((dataUtil.Date - DateTimeOffset.UtcNow) < TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Should_ValidatorShift_Success()
        {
            var dataUtil = RejectedInputWarehouseViewModel;
            Assert.NotNull(dataUtil.Shift);
        }

        [Fact]
        public void Should_ValidatorOutputId_Success()
        {
            var dataUtil = RejectedInputWarehouseViewModel;
            Assert.NotEqual(0, dataUtil.OutputId);
        }

        [Fact]
        public void Should_ValidatorGroup_Success()
        {
            var dataUtil = RejectedInputWarehouseViewModel;
            Assert.NotNull(dataUtil.Group);
        }

        [Fact]
        public void Should_ValidatorWarehousesProductionOrders_Success()
        {
            var dataUtil = RejectedInputWarehouseViewModel;
            Assert.NotEmpty(dataUtil.WarehousesProductionOrders);
        }
    }
}
