using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricSKUAutoCreateFormDtoTest
    {
        [Fact]
        public void Should_Success_Intantiate()
        {
            FabricSKUAutoCreateFormDto dto = new FabricSKUAutoCreateFormDto()
            {
               Construction = "Construction",
               Grade ="A",
               ProcessType = "ProcessType",
               UOM = "UOM",
               Warp = "Warp",
               Weft = "Weft",
               Width = "Width",
               WovenType = "WovenType",
               YarnType = "YarnType"
            };

            Assert.Equal("A", dto.Grade);
            Assert.Equal("Construction", dto.Construction);
            Assert.Equal("A", dto.Grade);
            Assert.Equal("ProcessType", dto.ProcessType);
            Assert.Equal("UOM", dto.UOM);
            Assert.Equal("Warp", dto.Warp);
            Assert.Equal("Weft", dto.Weft);
            Assert.Equal("Width", dto.Width);
            Assert.Equal("WovenType", dto.WovenType);
            Assert.Equal("YarnType", dto.YarnType);
        }
    }
}
