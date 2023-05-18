using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DTOs
{
    public class ProductSKUDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            ProductSKUDto dto = new ProductSKUDto(new ProductSKUModel("code", "name", 1, 1, "description", true) { LastModifiedUtc =DateTime.Now}, new UnitOfMeasurementModel("unit"), new CategoryModel("name","code"));
            Assert.Equal("name", dto.Name);
            Assert.Equal("code", dto.Code);
            Assert.Equal("description", dto.Description);
            Assert.Equal(0, dto.Id);
            Assert.True(DateTime.MinValue < dto.LasModifiedUtc);
            Assert.NotNull(dto.UOM);
            Assert.NotNull(dto.Category);
            Assert.Equal(0, dto.UOMId);
            Assert.Equal("unit", dto.UOMUnit);
        }
        }
}
