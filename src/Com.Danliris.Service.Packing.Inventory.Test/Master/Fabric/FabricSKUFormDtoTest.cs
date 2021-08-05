using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricSKUFormDtoTest
    {
        [Fact]
        public void validate_default()
        {
            FabricSKUFormDto dto = new FabricSKUFormDto();
            var result = dto.Validate(null);
            Assert.True(0 < result.Count());

        }
    }
}
