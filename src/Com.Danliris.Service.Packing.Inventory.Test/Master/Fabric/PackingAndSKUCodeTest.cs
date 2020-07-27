using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class PackingAndSKUCodeTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            PackingAndSKUCode packingAndSKUCode = new PackingAndSKUCode("packingCode", "skuCode");
            Assert.Equal("packingCode", packingAndSKUCode.PackingCode);
            Assert.Equal("skuCode", packingAndSKUCode.SKUCode);
        }
        }
}
