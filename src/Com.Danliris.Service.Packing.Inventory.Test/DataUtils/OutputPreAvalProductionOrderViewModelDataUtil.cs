using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class OutputPreAvalProductionOrderViewModelDataUtil
    {
        private OutputPreAvalProductionOrderViewModel OutputPreAvalProductionOrderViewModel
        {
            get
            {
                return new OutputPreAvalProductionOrderViewModel()
                {
                    Id = 1,
                    ProductionOrder = new ProductionOrder()
                    {
                        Code = "SLD",
                        Id = 63,
                        Type = "SOLID",
                        No = "F/2020/0010"
                    },
                    CartNo = "5-11",
                    Buyer = "ERWAN KURNIADI",
                    Construction = "Greige Test Dyeing Printing / TWILL 3/1. 104 x 52 / 100",
                    Unit = "DYEING",
                    Color = "RED",
                    Motif = "Mega",
                    UomUnit = "MTR",
                    Remark = "RE",
                    Grade = "A",
                    Status = "OK",
                    Balance = 1,
                    PackingInstruction = "Instruction",
                    AvalConnectionLength = 100,
                    AvalALength = 110,
                    AvalBLength = 120
                };
            }
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorCartNo_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.CartNo);
        }

        [Fact]
        public void Should_ValidatorBuyer_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.Buyer);
        }

        [Fact]
        public void Should_ValidatorConstruction_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.Construction);
        }

        [Fact]
        public void Should_ValidatorUnit_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.Unit);
        }

        [Fact]
        public void Should_ValidatorColor_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.Color);
        }

        [Fact]
        public void Should_ValidatorMotif_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.Motif);
        }

        [Fact]
        public void Should_ValidatorUomUnit_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.UomUnit);
        }

        [Fact]
        public void Should_ValidatorRemark_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.Remark);
        }

        [Fact]
        public void Should_ValidatorGrade_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.Grade);
        }

        [Fact]
        public void Should_ValidatorStatus_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.Status);
        }

        [Fact]
        public void Should_ValidatorBalance_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.Balance);
        }

        [Fact]
        public void Should_ValidatorPackingInstruction_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotNull(dataUtil.PackingInstruction);
        }

        [Fact]
        public void Should_ValidatorAvalConnectionLength_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.AvalConnectionLength);
        }

        [Fact]
        public void Should_ValidatorAvalALength_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.AvalALength);
        }

        [Fact]
        public void Should_ValidatorAvalBLength_Success()
        {
            var dataUtil = OutputPreAvalProductionOrderViewModel;
            Assert.NotEqual(0, dataUtil.AvalBLength);
        }
    }
}
