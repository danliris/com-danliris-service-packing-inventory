using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
  public  class FabricProductPackingCompositeIdFormDtoTest
    {
        [Fact]
        public void Validate_default()
        {
            FabricProductPackingCompositeIdFormDto dto = new FabricProductPackingCompositeIdFormDto();
            dto.PackingSize = 1;
            dto.PackingUOMId = 1;
            dto.SKUId = 1;
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
