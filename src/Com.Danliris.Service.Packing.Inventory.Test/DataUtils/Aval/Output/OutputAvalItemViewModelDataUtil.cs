using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Output
{
    public class OutputAvalItemViewModelDataUtil
    {
        private OutputAvalItemViewModel OutputAvalItemViewModel
        {
            get
            {
                return new OutputAvalItemViewModel()
                {
                    AvalItemId = 122,
                    AvalType = "SAMBUNGAN",
                    AvalCartNo = "5-11",
                    AvalUomUnit = "KRG",
                    AvalQuantity = 5,
                    AvalQuantityKg = 10,
                    AvalOutQuantity = 5,
                    AvalOutSatuan = 1
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = OutputAvalItemViewModel;
            var validator = new OutputAvalItemValidator();
            var result = validator.Validate(dataUtil);
            Assert.Equal(0, result.Errors.Count);
        }

        [Fact]
        public void Should_ValidatorAvalItemId_Success()
        {
            var dataUtil = OutputAvalItemViewModel;
            Assert.NotEqual(0, dataUtil.AvalItemId);
        }

        [Fact]
        public void Should_ValidatorAvalType_Success()
        {
            var dataUtil = OutputAvalItemViewModel;
            Assert.NotNull(dataUtil.AvalType);
        }

        [Fact]
        public void Should_ValidatorAvalCartNo_Success()
        {
            var dataUtil = OutputAvalItemViewModel;
            Assert.NotNull(dataUtil.AvalCartNo);
        }

        [Fact]
        public void Should_ValidatorAvalUomUnit_Success()
        {
            var dataUtil = OutputAvalItemViewModel;
            Assert.NotNull(dataUtil.AvalUomUnit);
        }
    }
}
