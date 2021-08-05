using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricPackingAutoCreateFormDtoTest
    {
        [Fact]
        public void Should_Success_Intantiate()
        {
            FabricPackingAutoCreateFormDto dto = new FabricPackingAutoCreateFormDto()
            {
                FabricSKUId=1,
                PackingType = "PackingType",
                Quantity=1
            };

            Assert.Equal(1, dto.FabricSKUId);
            Assert.Equal("PackingType", dto.PackingType);
            Assert.Equal(1, dto.Quantity);
        }
    }
}
