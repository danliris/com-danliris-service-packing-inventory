using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricSKUIndexInfoTest
    {
        [Fact]
        public void Should_Success_Intantiate()
        {
            FabricSKUIndexInfo dto = new FabricSKUIndexInfo()
            {
                Code = "Code",
                Construction = "Construction",
                Grade="A",
                ProcessType = "ProcessType",
                UOM = "UOM",
                Warp = "Warp",
                Weft = "Weft",
                Width = "Width",
                WovenType = "WovenType",
                YarnType = "YarnType"
            };

            Assert.Equal("Construction", dto.Construction);
            Assert.Equal("Code", dto.Code);
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
