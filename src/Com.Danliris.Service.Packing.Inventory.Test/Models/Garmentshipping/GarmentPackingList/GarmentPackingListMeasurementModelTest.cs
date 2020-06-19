using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListMeasurementModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentPackingListMeasurementModel model = new GarmentPackingListMeasurementModel();
            Assert.NotNull(model);
        }
    }
}
