using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricProductPackingCompositeStringFormDtoTest
    {
        [Fact]
        public void Validate_default()
        {
            FabricProductPackingCompositeStringFormDto dto = new FabricProductPackingCompositeStringFormDto();
            dto.PackingSize = 1;
            dto.SKUCode = "";
            dto.PackingUOM ="";
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
