using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Warehouse.Output
{
    public class OutputWarehouseViewModelDataUtil
    {
        private OutputWarehouseViewModel OutputWarehousesViewModel
        {
            get
            {
                return new OutputWarehouseViewModel()
                {
                    Id = 1,
                    Area = "GUDANG JADI",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "SHIPPING",
                    HasNextAreaDocument = false,
                    Shift = "PAGI",
                    InputWarehouseId = 12,
                    Group = "A",
                    WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
                    {
                        new OutputWarehouseProductionOrderViewModel()
                        {
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            CartNo = "5-11",
                            PackingInstruction = "a",
                            Construction = "a",
                            Unit = "a",
                            Buyer = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 50,
                            PreviousBalance = 100,
                            InputId = 2,
                            ProductionOrderNo = "asd",
                            HasNextAreaDocument = false,
                            Material = "a",
                            MtrLength = 10,
                            YdsLength = 10,
                            Quantity = 10,
                            PackagingType = "s",
                            PackagingUnit = "a",
                            PackagingQty = 10,
                            QtyOrder = 10
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            var validator = new OutputWarehouseValidator();
            var result = validator.Validate(dataUtil);
            Assert.Equal(0, result.Errors.Count);
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorArea_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.NotNull(dataUtil.Area);
        }

        [Fact]
        public void Should_ValidatorBonNo_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.NotNull(dataUtil.BonNo);
        }

        [Fact]
        public void Should_ValidatorDate_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Debug.Assert((dataUtil.Date - DateTimeOffset.UtcNow) < TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Should_ValidatorDestinationArea_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.NotNull(dataUtil.DestinationArea);
        }

        [Fact]
        public void Should_ValidatorHasNextAreaDocument_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.False(dataUtil.HasNextAreaDocument);
        }

        [Fact]
        public void Should_ValidatorShift_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.NotNull(dataUtil.Shift);
        }

        [Fact]
        public void Should_ValidatorInputWarehouseId_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.NotEqual(0, dataUtil.InputWarehouseId);
        }

        [Fact]
        public void Should_ValidatorGroup_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.NotNull(dataUtil.Group);
        }

        [Fact]
        public void Should_ValidatorWarehousesProductionOrders_Success()
        {
            var dataUtil = OutputWarehousesViewModel;
            Assert.NotEmpty(dataUtil.WarehousesProductionOrders);
        }
    }
}
