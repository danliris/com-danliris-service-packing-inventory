using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Output
{
    public class OutputAvalViewModelDataUtil
    {
        private OutputAvalViewModel OutputAvalViewModel
        {
            get
            {
                return new OutputAvalViewModel()
                {
                    Id = 1,
                    Area = "GUDANG AVAL",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "SHIPPING",
                    Shift = "PAGI",
                    Group = "A",
                    HasNextAreaDocument = true,
                    AvalItems = new List<OutputAvalItemViewModel>()
                    {
                        new OutputAvalItemViewModel()
                        {
                            AvalItemId = 122,
                            AvalType = "SAMBUNGAN",
                            AvalCartNo = "5-11",
                            AvalUomUnit = "KRG",
                            AvalQuantity = 5,
                            AvalQuantityKg = 10,
                            AvalOutQuantity = 5
                        }
                    },
                    DyeingPrintingMovementIds = new List<OutputAvalDyeingPrintingAreaMovementIdsViewModel>()
                    {
                        new OutputAvalDyeingPrintingAreaMovementIdsViewModel()
                        {
                            DyeingPrintingAreaMovementId = 71,
                            AvalItemId = 122
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = OutputAvalViewModel;
            var validator = new OutputAvalValidator();
            var result = validator.Validate(dataUtil);
            Assert.Equal(0, result.Errors.Count);
        }
    }
}
