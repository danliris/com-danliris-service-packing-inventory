using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.DyeingPrintingAreaMovement
{
public    class DyeingPrintingAreaOutputProductionOrderModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            DyeingPrintingAreaOutputProductionOrderModel model = new DyeingPrintingAreaOutputProductionOrderModel();

            model.SetDateIn(DateTimeOffset.Now, "", "");
        }
    }
}
