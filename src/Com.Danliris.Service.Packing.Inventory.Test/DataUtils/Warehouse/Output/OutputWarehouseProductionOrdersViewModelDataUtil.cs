using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Warehouse.Output
{
    public class OutputWarehouseProductionOrdersViewModelDataUtil
    {
        private OutputWarehouseProductionOrderViewModel OutputWarehousesProductionOrdersViewModel
        {
            get
            {
                return new OutputWarehouseProductionOrderViewModel()
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
                };
            }
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorCartNo_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.CartNo);
        }

        [Fact]
        public void Should_ValidatorPackingInstruction_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.PackingInstruction);
        }

        [Fact]
        public void Should_ValidatorConstruction_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Construction);
        }

        [Fact]
        public void Should_ValidatorUnit_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Unit);
        }

        [Fact]
        public void Should_ValidatorBuyer_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Buyer);
        }

        [Fact]
        public void Should_ValidatorColor_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Color);
        }

        [Fact]
        public void Should_ValidatorMotif_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Motif);
        }

        [Fact]
        public void Should_ValidatorUomUnit_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.UomUnit);
        }

        [Fact]
        public void Should_ValidatorRemark_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Remark);
        }

        [Fact]
        public void Should_ValidatorGrade_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Grade);
        }

        [Fact]
        public void Should_ValidatorStatus_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Status);
        }

        [Fact]
        public void Should_ValidatorBalance_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.Balance);
        }

        [Fact]
        public void Should_ValidatorPreviousBalance_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.PreviousBalance);
        }

        [Fact]
        public void Should_ValidatorInputId_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.InputId);
        }

        [Fact]
        public void Should_ValidatorProductionOrderNo_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.ProductionOrderNo);
        }

        [Fact]
        public void Should_ValidatorHasNextAreaDocument_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.False(dataUtil.HasNextAreaDocument);
        }

        [Fact]
        public void Should_ValidatorMaterial_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.Material);
        }

        [Fact]
        public void Should_ValidatorMtrLength_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.MtrLength);
        }

        [Fact]
        public void Should_ValidatorYdsLength_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.YdsLength);
        }

        [Fact]
        public void Should_ValidatorQuantity_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.Quantity);
        }

        [Fact]
        public void Should_ValidatorPackagingType_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.PackagingType);
        }

        [Fact]
        public void Should_ValidatorPackagingUnit_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotNull(dataUtil.PackagingUnit);
        }

        [Fact]
        public void Should_ValidatorPackagingQty_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.PackagingQty);
        }

        [Fact]
        public void Should_ValidatorQtyOrder_Success()
        {
            var dataUtil = OutputWarehousesProductionOrdersViewModel;
            Assert.NotEqual(0, dataUtil.QtyOrder);
        }
    }
}
