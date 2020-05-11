using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Input
{
    public class InputAvalDyeingPrintingAreaMovementIdsViewModelDataUtil
    {
        private InputAvalDyeingPrintingAreaMovementIdsViewModel InputAvalDyeingPrintingAreaMovementIdsViewModel
        {
            get
            {
                return new InputAvalDyeingPrintingAreaMovementIdsViewModel()
                {
                    DyeingPrintingAreaMovementId = 51,
                    ProductionOrderIds = new List<int>()
                            {
                                123,
                                124
                            }
                };
            }
        }

        [Fact]
        public void Should_ValidatorDyeingPrintingAreaMovementId_Success()
        {
            var dataUtil = InputAvalDyeingPrintingAreaMovementIdsViewModel;
            Assert.NotEqual(0, dataUtil.DyeingPrintingAreaMovementId);
        }

        [Fact]
        public void Should_ValidatorProductionOrderIds_Success()
        {
            var dataUtil = InputAvalDyeingPrintingAreaMovementIdsViewModel;
            Assert.NotEmpty(dataUtil.ProductionOrderIds);
        }
    }
}
