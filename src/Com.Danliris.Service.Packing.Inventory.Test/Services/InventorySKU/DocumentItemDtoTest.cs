using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.InventorySKU
{
    public class DocumentItemDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var item = new ProductSKUInventoryMovementModel(1, 1, 1, 1, "storageCode", "storageName", 1, "type", "remark");
            var product = new Data.Models.Product.ProductSKUModel();
            var category = new Data.Models.Product.CategoryModel();
            var uom = new Data.Models.Product.UnitOfMeasurementModel();
            DocumentItemDto dto = new DocumentItemDto(item,product,uom, category);
            
            Assert.Equal(1, dto.Quantity);
            Assert.Equal("Remark", dto.Remark);
        }
    }
}
