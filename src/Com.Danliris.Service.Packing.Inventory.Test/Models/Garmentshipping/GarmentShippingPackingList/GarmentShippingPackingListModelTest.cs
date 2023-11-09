using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentShippingPackingListModel model = new GarmentShippingPackingListModel();
            model.SetIsShipping(true, "asd", "asd");
            Assert.Equal(true, model.IsShipping);
            Assert.NotNull(model);
        }
    }
}
