using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingAreaInput.Transit
{
    public class PreTransitIndexViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            PreTransitIndexViewModel index = new PreTransitIndexViewModel()
            {
                Area = "Area",
                BonNo = "BonNo",
                Date = DateTimeOffset.Now,
                DestinationArea = "DestinationArea",
                Id = 1,
                HasNextAreaDocument = true,
                Shift = "Shift",
                PreTransitProductionOrders = new List<OutputPreTransitProductionOrderViewModel>()
                {
                    new OutputPreTransitProductionOrderViewModel()
                }
            };

            Assert.Equal(1, index.Id);
            Assert.Equal("Area", index.Area);
            Assert.Equal("BonNo", index.BonNo);
            Assert.Equal("DestinationArea", index.DestinationArea);
            Assert.Equal("Shift", index.Shift);
            Assert.True(DateTimeOffset.MinValue < index.Date);
            Assert.True(index.HasNextAreaDocument);
            Assert.True(0 < index.PreTransitProductionOrders.Count());
        }
    }
}
