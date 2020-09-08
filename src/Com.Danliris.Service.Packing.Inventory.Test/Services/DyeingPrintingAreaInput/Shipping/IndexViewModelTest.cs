using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingAreaInput.Shipping
{
  public  class IndexViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            //  var 
            IndexViewModel index = new IndexViewModel()
            {
                Area = "Area",
                BonNo = "BonNo",
                Date = DateTimeOffset.Now,
                Group = "Group",
                Id = 1,
                ShippingProductionOrders = new List<InputShippingProductionOrderViewModel>()
                {
                  new InputShippingProductionOrderViewModel()
                },
                Shift = "Shift",

            };

            Assert.Equal("Area", index.Area);
            Assert.Equal("BonNo", index.BonNo);
            Assert.Equal("Group", index.Group);
            Assert.Equal(1, index.Id);
            Assert.True(0 < index.ShippingProductionOrders.Count());
            Assert.Equal("Shift", index.Shift);
            Assert.True(DateTimeOffset.MinValue < index.Date);
        }
    }
}
