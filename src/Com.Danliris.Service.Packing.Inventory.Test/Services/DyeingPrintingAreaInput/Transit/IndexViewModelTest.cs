using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingAreaInput.Transit
{
   public class IndexViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            IndexViewModel index = new IndexViewModel()
            {
                Area = "Area",
                BonNo = "BonNo",
                Date = DateTimeOffset.Now,
                Group = "Group",
                Id = 1,
                Shift = "Shift",
                TransitProductionOrders = new List<InputTransitProductionOrderViewModel>()
                {
                    new InputTransitProductionOrderViewModel()
                }
            };

            Assert.Equal(1, index.Id);
            Assert.Equal("Area", index.Area);
            Assert.Equal("BonNo", index.BonNo);
            Assert.Equal("Group", index.Group);
            Assert.Equal("Shift", index.Shift);
            Assert.True(DateTimeOffset.MinValue < index.Date);
            Assert.True(0 < index.TransitProductionOrders.Count());
        }
    }
}
