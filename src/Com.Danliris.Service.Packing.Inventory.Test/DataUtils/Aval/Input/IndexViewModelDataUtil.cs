using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using System;
using System.Diagnostics;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Input
{
    public class IndexViewModelDataUtil
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
                    Group = "A"
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
        public void Should_ValidatorGroup_Success()
        {
            var dataUtil = IndexViewModel;
            Assert.NotNull(dataUtil.Group);
        }
    }
}
