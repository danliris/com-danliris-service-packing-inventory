using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.ProductPacking
{
    public class ProductPackingIndexInfoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            ProductPackingIndexInfo indexInfo = new ProductPackingIndexInfo()
            {
                Code = "Code",
                LastModifiedUtc =DateTime.Now,
                UOMUnit = "UOMUnit",
                PackingSize = 1,
                Id =1,
                ProductSKUName = "ProductSKUName"
            };
            Assert.Equal("Code", indexInfo.Code);
            Assert.Equal("UOMUnit", indexInfo.UOMUnit);
            Assert.Equal(1, indexInfo.PackingSize);
            Assert.Equal(1, indexInfo.Id);
            Assert.Equal("ProductSKUName", indexInfo.ProductSKUName);
        }
    }
}
