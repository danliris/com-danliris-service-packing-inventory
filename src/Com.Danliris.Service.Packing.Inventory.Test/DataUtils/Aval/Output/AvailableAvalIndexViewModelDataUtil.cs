using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Output
{
    public class AvailableAvalIndexViewModelDataUtil
    {
        private AvailableAvalIndexViewModel AvailableAvalIndexViewModel
        {
            get
            {
                return new AvailableAvalIndexViewModel()
                {
                    AvalInputId = 12,
                    Date = DateTimeOffset.UtcNow,
                    Area = "GUDANG AVAL",
                    Shift = "PAGI",
                    Group = "A",
                    BonNo = "GA.20.001",
                    AvalItemId = 122,
                    AvalType = "SAMBUNGAN",
                    AvalCartNo = "5-11",
                    AvalUomUnit = "KRG",
                    AvalQuantity = 5,
                    AvalQuantityKg = 10 
                };
            }
        }

        [Fact]
        public void Should_ValidatorAvalInputId_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotEqual(0, dataUtil.AvalInputId);
        }

        [Fact]
        public void Should_ValidatorDate_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Debug.Assert((dataUtil.Date - DateTimeOffset.UtcNow) < TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Should_ValidatorArea_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotNull(dataUtil.Area);
        }

        [Fact]
        public void Should_ValidatorShift_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotNull(dataUtil.Shift);
        }

        [Fact]
        public void Should_ValidatorGroup_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotNull(dataUtil.Group);
        }

        [Fact]
        public void Should_ValidatorBonNo_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotNull(dataUtil.BonNo);
        }

        [Fact]
        public void Should_ValidatorAvalItemId_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotEqual(0, dataUtil.AvalItemId);
        }

        [Fact]
        public void Should_ValidatorAvalType_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotNull(dataUtil.AvalType);
        }

        [Fact]
        public void Should_ValidatorAvalCartNo_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotNull(dataUtil.AvalCartNo);
        }

        [Fact]
        public void Should_ValidatorAvalUomUnit_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotNull(dataUtil.AvalUomUnit);
        }

        [Fact]
        public void Should_ValidatorAvalQuantity_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotEqual(0, dataUtil.AvalQuantity);
        }

        [Fact]
        public void Should_ValidatorAvalQuantityKg_Success()
        {
            var dataUtil = AvailableAvalIndexViewModel;
            Assert.NotEqual(0, dataUtil.AvalQuantityKg);
        }
    }
}
