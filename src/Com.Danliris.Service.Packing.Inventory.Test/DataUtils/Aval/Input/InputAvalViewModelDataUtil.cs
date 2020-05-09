using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Input
{
    public class InputAvalViewModelDataUtil
    {
        private InputAvalViewModel InputAvalViewModel
        {
            get
            {
                return new InputAvalViewModel()
                {
                    Id = 1,
                    Area = "GUDANG AVAL",
                    Date = DateTimeOffset.UtcNow,
                    BonNo = "IM.GA.20.001",
                    Shift = "PAGI",
                    Group = "A",
                    AvalItems = new List<InputAvalItemViewModel>()
                    {
                        new InputAvalItemViewModel()
                        {
                            AvalType = "SAMBUNGAN",
                            AvalCartNo = "5-11",
                            AvalUomUnit = "KRG",
                            AvalQuantity = 5,
                            AvalQuantityKg = 10,
                            HasOutputDocument = false,
                            IsChecked = false
                        }
                    },
                    DyeingPrintingMovementIds = new List<InputAvalDyeingPrintingAreaMovementIdsViewModel>()
                    {
                        new InputAvalDyeingPrintingAreaMovementIdsViewModel()
                        {
                            DyeingPrintingAreaMovementId = 51,
                            ProductionOrderIds = new List<int>()
                            {
                                123,
                                124
                            }
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = InputAvalViewModel;
            var validator = new InputAvalValidator();
            var result = validator.Validate(dataUtil);
            Assert.Equal(0, result.Errors.Count);
        }

        [Fact]
        public void Should_ValidatorBonNo_Success()
        {
            var dataUtil = InputAvalViewModel;
            Assert.NotNull(dataUtil.BonNo);
        }
    }
}
