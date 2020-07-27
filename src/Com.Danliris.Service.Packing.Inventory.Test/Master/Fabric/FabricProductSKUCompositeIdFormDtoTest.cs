using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricProductSKUCompositeIdFormDtoTest
    {
        [Fact]
        public void Validate_default()
        {
            FabricProductSKUCompositeIdFormDto dto = new FabricProductSKUCompositeIdFormDto();
            dto.ColorWayId = 1;
            dto.ConstructionTypeId = 1;
            dto.GradeId = 1;
            dto.ProcessTypeId = 1;
            dto.UOMId = 1;
            dto.WarpThreadId = 1;
            dto.WeftThreadId = 1;
            dto.WidthId = 1;
            dto.WovenTypeId = 1;
          
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
