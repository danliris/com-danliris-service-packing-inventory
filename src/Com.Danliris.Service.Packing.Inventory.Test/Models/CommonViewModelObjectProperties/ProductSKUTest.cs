using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.CommonViewModelObjectProperties
{
   public class ProductSKUTest
    {
        [Fact]
        public void should_Success_Instantiate_ProductSKU()
        {

            ProductSKU productSKU = new ProductSKU()
            {
                Id=1,
                Code="Code",
                Name ="Name"
            };

            Assert.Equal(1, productSKU.Id);
            Assert.Equal("Code", productSKU.Code);
            Assert.Equal("Name", productSKU.Name);
        }
    }
}
