using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListItemModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentShippingPackingListItemModel model = new GarmentShippingPackingListItemModel();
            Assert.NotNull(model);
        }
    }
}
