using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
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
            var products = new List<Data.Models.Product.ProductSKUModel>()
            {
                new Data.Models.Product.ProductSKUModel()
            };

            var uoms = new List<Data.Models.Product.UnitOfMeasurementModel>()
            {
                new Data.Models.Product.UnitOfMeasurementModel()
            };
            var categories = new List<Data.Models.Product.CategoryModel>()
            {
                new Data.Models.Product.CategoryModel()
            };
            DocumentDto dto = new DocumentDto(document, items, products, uoms, categories);

            Assert.Equal("DocumentNo", dto.DocumentNo);
            Assert.Equal("ReferenceNo", dto.ReferenceNo);
            Assert.Equal("ReferenceType", dto.ReferenceType);
            Assert.Equal("DocumentNo", dto.DocumentNo);
            Assert.Equal("Type", dto.Type);
            Assert.Equal("Remark", dto.Remark);
           
        }
        }
}
