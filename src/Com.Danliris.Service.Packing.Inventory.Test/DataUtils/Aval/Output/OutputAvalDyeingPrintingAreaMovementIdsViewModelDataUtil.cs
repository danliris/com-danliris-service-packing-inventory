using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Aval.Output
{
    public class OutputAvalDyeingPrintingAreaMovementIdsViewModelDataUtil
    {
        private OutputAvalDyeingPrintingAreaMovementIdsViewModel OutputAvalDyeingPrintingAreaMovementIdsViewModel
        {
            get
            {
                return new OutputAvalDyeingPrintingAreaMovementIdsViewModel()
                {
                    DyeingPrintingAreaMovementId = 12,
                    AvalItemId = 60
                };
            }
        }

        [Fact]
        public void Should_ValidatorDyeingPrintingAreaMovementId_Success()
        {
            var dataUtil = OutputAvalDyeingPrintingAreaMovementIdsViewModel;
            Assert.NotEqual(0, dataUtil.DyeingPrintingAreaMovementId);
        }

        [Fact]
        public void Should_ValidatorAvalItemId_Success()
        {
            var dataUtil = OutputAvalDyeingPrintingAreaMovementIdsViewModel;
            Assert.NotEqual(0, dataUtil.AvalItemId);
        }
    }
}
