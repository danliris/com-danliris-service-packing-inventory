using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DTOs
{
  public  class ProductPackingDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            ProductPackingDto dto = new ProductPackingDto(new ProductPackingModel(1, 1, 1, "code", "name", "description") { LastModifiedUtc=DateTime.Now},new ProductSKUModel(),new UnitOfMeasurementModel(),new UnitOfMeasurementModel(),new CategoryModel());
            Assert.Equal("name", dto.Name);
            Assert.Equal("code", dto.Code);
            Assert.Equal(0, dto.Id);
            Assert.True(DateTime.MinValue < dto.LasModifiedUtc);
            Assert.NotNull(dto.ProductSKU);
            Assert.NotNull(dto.UOM);
        }
    }
}

