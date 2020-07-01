using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricProductSKUCompositeStringFormDtoTest
    {
        [Fact]
        public void Validate_default()
        {
            FabricProductSKUCompositeStringFormDto dto = new FabricProductSKUCompositeStringFormDto();
            
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
