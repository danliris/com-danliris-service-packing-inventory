using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Input
{
    public class InputAvalItemViewModelDataUtil
    {
        private InputAvalItemViewModel InputAvalItemViewModel
        {
            get
            {
                return new InputAvalItemViewModel()
                {
                    AvalType = "SAMBUNGAN",
                    AvalCartNo = "5-11",
                    AvalUomUnit = "KRG",
                    AvalQuantity = 5,
                    AvalQuantityKg = 10,
                    HasOutputDocument = false,
                    IsChecked = false
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = InputAvalItemViewModel;
            var validator = new InputAvalItemValidator();
            var result = validator.Validate(dataUtil);
            Assert.Equal(0, result.Errors.Count);
        }
    }
}
