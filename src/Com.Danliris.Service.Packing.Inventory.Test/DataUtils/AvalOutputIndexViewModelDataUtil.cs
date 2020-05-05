using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using System;
using System.Diagnostics;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class AvalOutputIndexViewModelDataUtil
    {
        private IndexViewModel IndexViewModel
        {
            get
            {
                return new IndexViewModel()
                {
                    Id = 1,
                    Date = DateTimeOffset.UtcNow,
                    BonNo = "IM.GA.20.001",
                    Shift = "PAGI",
                    CartNo = "5-11",
                    AvalType = "SAMBUNGAN",
                    UomUnit = "KRG",
                    Qty = 15,
                    QtyKg = 10,
                };
            }
        }

        [Fact]
        public void Should_ValidatorId_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotEqual(0, dataUtil.Id);
        }

        [Fact]
        public void Should_ValidatorDate_Success()
        {
            var dataUtil = IndexViewModel;
            Debug.Assert((dataUtil.Date - DateTimeOffset.UtcNow)<TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Should_ValidatorBonNo_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotNull(dataUtil.BonNo);
        }

        [Fact]
        public void Should_ValidatorShift_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotNull(dataUtil.Shift);
        }

        [Fact]
        public void Should_ValidatorCartNo_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotNull(dataUtil.CartNo);
        }

        [Fact]
        public void Should_ValidatorAvalType_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotNull(dataUtil.AvalType);
        }

        [Fact]
        public void Should_ValidatorUomUnit_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotNull(dataUtil.UomUnit);
        }

        [Fact]
        public void Should_ValidatorQty_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotEqual(0, dataUtil.Qty);
        }

        [Fact]
        public void Should_ValidatorQtyKg_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotEqual(0, dataUtil.QtyKg);
        }
    }
}
