using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Utilities
{
    public class DocumentCodeGeneratorTest
    {
        [Fact]
        public void Should_Success_Intantiate_When_yearNowCode_IsEqual_yearCode()
        {
                var monthNowCode = DateTime.Now.Month.ToString().PadLeft(2, '0');
                var latestcode = monthNowCode + monthNowCode;
                var codeGenerator = DocumentCodeGenerator.ProductSKU(latestcode);
                Assert.NotEmpty(codeGenerator);
        }

        [Fact]
        public void Should_Success_Intantiate_When_yearNowCode_NotEqual_yearCode()
        {
            var monthNowCode = DateTime.Now.Month.ToString().PadLeft(2, '0');
            var latestcode = "00" + monthNowCode;
            var codeGenerator = DocumentCodeGenerator.ProductSKU(latestcode);
            Assert.NotEmpty(codeGenerator);
        }


        [Fact]
        public void Should_Success_ProductSKU_When_latestCodeIsEmpty()
        {

            var latestcode = "";
            var codeGenerator = DocumentCodeGenerator.ProductSKU(latestcode);
            Assert.NotEmpty(codeGenerator);
        }

        
    }
}
