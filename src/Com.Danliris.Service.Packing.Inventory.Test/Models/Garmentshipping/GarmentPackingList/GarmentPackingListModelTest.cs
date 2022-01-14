using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentPackingListModel model = new GarmentPackingListModel();
            model.SetIsShipping(true, "asd", "asd");
            Assert.Equal(true, model.IsShipping);
            Assert.NotNull(model);
        }
    }
}
