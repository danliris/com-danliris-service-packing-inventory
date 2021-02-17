using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListDetailModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentPackingListDetailModel model = new GarmentPackingListDetailModel();
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
