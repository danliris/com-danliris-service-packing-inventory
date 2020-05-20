using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Warehouse.Input
{
    public class InputWarehousesViewModelDataUtil
    {
        private InputWarehouseViewModel InputWarehouseViewModel
        {
            get
            {
                return new InputWarehouseViewModel()
                {
                    Id = 1,
                    Area = "GUDANG JADI",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    //OutputId = 10,
                    Group = "A",
                    MappedWarehousesProductionOrders = new List<InputWarehouseProductionOrderViewModel>()
                    {
                        new InputWarehouseProductionOrderViewModel()
                        {
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            ProductionOrderNo = "asd",
                            CartNo = "5-11",
                            PackingInstruction = "a",
                            Construction = "a",
                            Unit = "a",
                            Buyer = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Balance = 10,
                            HasOutputDocument = true,
                            IsChecked = true,
                            Grade = "a",
                            Remark = "a",
                            Status = "a",
                            //Material = "a",
                            //MtrLength = 10,
                            //YdsLength = 10,
                            //Quantity = 10,
                            PackagingType = "s",
                            PackagingUnit = "a",
                            PackagingQty = 10,
                            QtyOrder = 10,
                            OutputId = 1,
                            //InputId = 2
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = InputWarehouseViewModel;
            var validator = new InputWarehouseValidator();
            var result = validator.Validate(dataUtil);
            Assert.Equal(0, result.Errors.Count);
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = InputWarehouseViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorArea_Success()
        {
            var dataUtil = InputWarehouseViewModel;
            Assert.NotNull(dataUtil.Area);
        }

        [Fact]
        public void Should_ValidatorBonNo_Success()
        {
            var dataUtil = InputWarehouseViewModel;
            Assert.NotNull(dataUtil.BonNo);
        }

        [Fact]
        public void Should_ValidatorDate_Success()
        {
            var dataUtil = InputWarehouseViewModel;
            Debug.Assert((dataUtil.Date - DateTimeOffset.UtcNow) < TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Should_ValidatorShift_Success()
        {
            var dataUtil = InputWarehouseViewModel;
            Assert.NotNull(dataUtil.Shift);
        }

        //[Fact]
        //public void Should_ValidatorOutputId_Success()
        //{
        //    var dataUtil = InputWarehouseViewModel;
        //    Assert.NotEqual(0, dataUtil.OutputId);
        //}

        [Fact]
        public void Should_ValidatorGroup_Success()
        {
            var dataUtil = InputWarehouseViewModel;
            Assert.NotNull(dataUtil.Group);
        }

        [Fact]
        public void Should_ValidatorWarehousesProductionOrders_Success()
        {
            var dataUtil = InputWarehouseViewModel;
            Assert.NotEmpty(dataUtil.MappedWarehousesProductionOrders);
        }
    }
}
