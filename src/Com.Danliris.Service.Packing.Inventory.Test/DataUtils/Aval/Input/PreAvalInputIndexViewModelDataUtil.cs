using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Input
{
    public class PreAvalInputIndexViewModelDataUtil
    {
        private PreAvalIndexViewModel PreAvalIndexViewModel
        {
            get
            {
                return new PreAvalIndexViewModel()
                {
                    Id = 1,
                    Area = "INSPECTION MATERIAL",
                    BonNo = "IM.GA.20.001",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "GUDANG AVAL",
                    HasNextAreaDocument = false,
                    Shift = "PAGI",
                    Group = "A",
                    PreAvalProductionOrders = new List<OutputPreAvalProductionOrderViewModel>()
                    {
                        new OutputPreAvalProductionOrderViewModel()
                        {
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            CartNo = "1",
                            Buyer = "s",
                            Construction = "sd",
                            Unit = "s",
                            Color = "red",
                            Motif = "sd",
                            UomUnit = "d",
                            Remark = "RE",
                            Grade = "s",
                            Status = "s",
                            Balance = 1,
                            PackingInstruction = "d",
                            AvalConnectionLength = 100,
                            AvalALength = 110,
                            AvalBLength = 120
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorArea_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Assert.NotNull(dataUtil.Area);
        }

        [Fact]
        public void Should_ValidatorBonNo_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Assert.NotNull(dataUtil.BonNo);
        }

        [Fact]
        public void Should_ValidatorDate_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Debug.Assert((dataUtil.Date - DateTimeOffset.UtcNow) < TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Should_ValidatorDestinationArea_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Assert.NotNull(dataUtil.DestinationArea);
        }

        [Fact]
        public void Should_ValidatorHasNextAreaDocument_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Assert.False(dataUtil.HasNextAreaDocument);
        }

        [Fact]
        public void Should_ValidatorShift_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Assert.NotNull(dataUtil.Shift);
        }

        [Fact]
        public void Should_ValidatorGroup_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Assert.NotNull(dataUtil.Group);
        }

        [Fact]
        public void Should_ValidatorPreAvalProductionOrders_Success()
        {
            var dataUtil = PreAvalIndexViewModel;
            Assert.NotEmpty(dataUtil.PreAvalProductionOrders);
        }
    }
}
