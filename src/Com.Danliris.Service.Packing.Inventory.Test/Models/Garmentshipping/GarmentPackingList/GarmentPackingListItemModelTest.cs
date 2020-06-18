using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListItemModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentPackingListItemModel model = new GarmentPackingListItemModel();
            Assert.NotNull(model);
        }
    }
}
