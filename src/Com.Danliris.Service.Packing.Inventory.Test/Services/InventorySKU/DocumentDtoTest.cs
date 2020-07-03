using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.InventorySKU
{
    public class DocumentDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var date = DateTimeOffset.Now;
            ProductSKUInventoryDocumentModel document = new ProductSKUInventoryDocumentModel("DocumentNo", date, "ReferenceNo", "ReferenceType",1, "StorageName", "StorageCode", "Type","Remark");

            var items = new List<ProductSKUInventoryMovementModel>()
            {
                new ProductSKUInventoryMovementModel()
            };
            var products = new List<ProductSKUModel>()
            {
                new ProductSKUModel()
            };

            var uoms = new List<UnitOfMeasurementModel>()
            {
                new UnitOfMeasurementModel()
            };
            var categories = new List<CategoryModel>()
            {
                new CategoryModel()
            };
            DocumentDto dto = new DocumentDto(document, items, products, uoms, categories);

            Assert.Equal("DocumentNo", dto.DocumentNo);
            Assert.Equal("ReferenceNo", dto.ReferenceNo);
            Assert.Equal("ReferenceType", dto.ReferenceType);
            Assert.Equal("DocumentNo", dto.DocumentNo);
            Assert.Equal("Type", dto.Type);
            Assert.Equal("Remark", dto.Remark);
            Assert.True(DateTimeOffset.MinValue < dto.Date);
            Assert.NotNull(dto.Storage);
            Assert.NotNull(dto.Items);
        }
        }
}
