using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingAreaInput.Shipping
{
    public class PreShippingIndexViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            //  var 
            PreShippingIndexViewModel index = new PreShippingIndexViewModel()
            {
                Area = "Area",
                BonNo = "BonNo",
                Date = DateTimeOffset.Now,
                DestinationArea = "DestinationArea",
                HasNextAreaDocument =true,
                Id=1,
                PreShippingProductionOrders=new List<OutputPreShippingProductionOrderViewModel>()
                {
                    new OutputPreShippingProductionOrderViewModel()
                },
                Shift = "Shift",
                
            };

            Assert.Equal("Area", index.Area);
            Assert.Equal("BonNo", index.BonNo);
            Assert.Equal("DestinationArea", index.DestinationArea);
            Assert.Equal(1, index.Id);
            Assert.True(0 < index.PreShippingProductionOrders.Count());
            Assert.True(index.HasNextAreaDocument);
            Assert.Equal("Shift", index.Shift);
            Assert.True(DateTimeOffset.MinValue < index.Date);
        }
    }
}
