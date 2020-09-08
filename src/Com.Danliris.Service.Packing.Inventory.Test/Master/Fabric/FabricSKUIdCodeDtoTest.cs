using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
   public class FabricSKUIdCodeDtoTest
    {
        [Fact]
        public void Should_Success_Intantiate()
        {
            FabricSKUIdCodeDto dto = new FabricSKUIdCodeDto()
            {
                FabricSKUId =1,
                ProductSKUCode ="code",
                ProductSKUId=1
            };

            Assert.Equal(1, dto.FabricSKUId);
            Assert.Equal("code", dto.ProductSKUCode);
            Assert.Equal(1, dto.ProductSKUId);
        }
    }
}
