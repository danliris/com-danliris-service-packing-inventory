using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListDetailModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentShippingPackingListDetailModel model = new GarmentShippingPackingListDetailModel();
            model.SetGrossWeight(1, "asd", "asd");
            model.SetNetWeight(1, "asd", "asd");
            model.SetNetNetWeight(1, "asd", "asd");
            Assert.Equal(1, model.GrossWeight);
            Assert.Equal(1, model.NetWeight);
            Assert.Equal(1, model.NetNetWeight);
            Assert.NotNull(model);
        }
    }
}
