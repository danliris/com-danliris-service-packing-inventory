using Com.Danliris.Service.Packing.Inventory.Application.Master.UOM;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.UOM
{
    public class UOMIndexInfoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            UOMIndexInfo indexInfo = new UOMIndexInfo()
            {
                Id = 1
            };

            Assert.Equal(1, indexInfo.Id);
        }
        }
}
