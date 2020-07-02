using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricProductCompositeStringDtoTest
    {
        [Fact]
        public void Should_Success_Intantiate()
        {
            FabricProductCompositeStringDto dto = new FabricProductCompositeStringDto()
            {
                ColorWay = "ColorWay",
                ConstructionType = "ConstructionType",
                Grade ="A",
                PackingSize =1,
                PackingType = "PackingType",
                ProcessType = "ProcessType",
                UOM = "UOM",
                WarpThread = "WarpThread",
                WeftThread = "WeftThread",
                Width ="1",
                WovenType = "WovenType",
                
            };

            Assert.Equal("ColorWay", dto.ColorWay);
            Assert.Equal("ConstructionType", dto.ConstructionType);
            Assert.Equal("A", dto.Grade);
            Assert.Equal(1, dto.PackingSize);
            Assert.Equal("PackingType", dto.PackingType);
            Assert.Equal("ProcessType", dto.ProcessType);
            Assert.Equal("UOM", dto.UOM);
            Assert.Equal("WarpThread", dto.WarpThread);
            Assert.Equal("WeftThread", dto.WeftThread);
            Assert.Equal("1", dto.Width);
            Assert.Equal("WovenType", dto.WovenType);
        }

        [Fact]
        public void Validate_default()
        {
            FabricProductCompositeStringDto dto = new FabricProductCompositeStringDto();
            dto.PackingSize = 1;
           
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
