using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricPackingIdCodeDtoTest
    {
        [Fact]
        public void Should_Success_Intantiate()
        {
            FabricPackingIdCodeDto dto = new FabricPackingIdCodeDto()
            {
                FabricPackingId =1,
                ProductPackingCode ="code",
                ProductPackingId =1
            };

            Assert.Equal(1, dto.FabricPackingId);
            Assert.Equal("code", dto.ProductPackingCode);
            Assert.Equal(1, dto.ProductPackingId);
        }
    }
}
