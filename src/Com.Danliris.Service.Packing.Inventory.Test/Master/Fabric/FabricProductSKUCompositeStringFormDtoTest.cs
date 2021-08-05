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
        public void Should_Success_Instantiate()
        {
            FabricProductSKUCompositeStringFormDto dto = new FabricProductSKUCompositeStringFormDto()
            {
                ColorWay = "ColorWay",
                ConstructionType = "ConstructionType",
                Grade= "Grade",
                ProcessType = "ProcessType",
                UOM = "UOM",
                WarpThread = "WarpThread",
                WeftThread = "WeftThread",
                WovenType = "WovenType",
                Width = "Width"
            };
            Assert.Equal("WovenType", dto.WovenType);
            Assert.Equal("ColorWay", dto.ColorWay);
            Assert.Equal("ConstructionType", dto.ConstructionType);
            Assert.Equal("Grade", dto.Grade);
            Assert.Equal("ProcessType", dto.ProcessType);
            Assert.Equal("WarpThread", dto.WarpThread);
            Assert.Equal("WeftThread", dto.WeftThread);
            Assert.Equal("Width", dto.Width);
        }

        [Fact]
        public void Validate_default()
        {
            FabricProductSKUCompositeStringFormDto dto = new FabricProductSKUCompositeStringFormDto();
            
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
