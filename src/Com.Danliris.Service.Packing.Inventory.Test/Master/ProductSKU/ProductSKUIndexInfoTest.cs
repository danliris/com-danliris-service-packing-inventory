using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.ProductSKU
{
    public class ProductSKUIndexInfoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var date = DateTime.Now;
            ProductSKUIndexInfo indexInfo = new ProductSKUIndexInfo()
            {
                Code = "Code",
                CategoryName = "CategoryName",
                LastModifiedUtc =date,
                Name ="Name",
                Id =1,
                UOMUnit = "UOMUnit"
            };
            Assert.Equal("Code", indexInfo.Code);
            Assert.Equal("CategoryName", indexInfo.CategoryName);
            Assert.Equal(date, indexInfo.LastModifiedUtc);
            Assert.Equal("Name", indexInfo.Name);
            Assert.Equal(1, indexInfo.Id);
            Assert.Equal("UOMUnit", indexInfo.UOMUnit);
        }
        }
}
