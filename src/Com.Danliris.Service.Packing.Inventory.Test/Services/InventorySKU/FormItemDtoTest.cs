using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.InventorySKU
{
    public class FormItemDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            FormItemDto packingAndSKUCode = new FormItemDto()
            {
                ProductSKUId = 1,
                UOMId = 1,
                Quantity = 1,
                Remark = "Remark"
            };
            Assert.Equal(1, packingAndSKUCode.ProductSKUId);
            Assert.Equal(1, packingAndSKUCode.UOMId);
            Assert.Equal(1, packingAndSKUCode.Quantity);
            Assert.Equal("Remark", packingAndSKUCode.Remark);
        }
    }
}
