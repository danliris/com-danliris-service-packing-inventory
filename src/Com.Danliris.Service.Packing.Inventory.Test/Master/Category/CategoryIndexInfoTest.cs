using Com.Danliris.Service.Packing.Inventory.Application.Master.Category;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Category
{
    public class CategoryIndexInfoTest
    {
        [Fact]
        public void Should_Success_Intantiate()
        {
            CategoryIndexInfo indexInfo = new CategoryIndexInfo()
            {
                Code = "Code",
                Id = 1,
            };
            Assert.Equal("Code", indexInfo.Code);
            Assert.Equal(1, indexInfo.Id);
        }
        }
}
