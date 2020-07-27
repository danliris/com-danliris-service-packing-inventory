using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.InventorySKU
{
    public class DocumentIndexDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var document = new List<ProductSKUInventoryDocumentModel>()
            {
                new ProductSKUInventoryDocumentModel()
            };
            DocumentIndexDto dto = new DocumentIndexDto(document, 1, 1, 25);
            Assert.Equal(document, dto.data);
            Assert.Equal(1, dto.page);
            Assert.Equal(1, dto.size);
            Assert.Equal(25, dto.total);

        }
        }
}
