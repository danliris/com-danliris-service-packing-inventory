using Com.Danliris.Service.Packing.Inventory.Application.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.helper
{
   public class YearCodeTest
    {
        [Fact]
        public void should_success_instantiate()
        {
            YearCode yearCode = new YearCode(2019, "code");
            Assert.Equal("code", yearCode.Code);
            Assert.Equal(2019, yearCode.Year);
        }
    }
}
