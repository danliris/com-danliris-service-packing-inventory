using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListDetailSizeModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentShippingPackingListDetailSizeModel model = new GarmentShippingPackingListDetailSizeModel();
            Assert.NotNull(model);
        }
        }
}
