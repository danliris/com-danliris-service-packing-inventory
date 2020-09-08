using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DTOs
{
    public class CategoryDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            CategoryDto dto = new CategoryDto(new CategoryModel("name","code"));
            Assert.Equal("name", dto.Name);
            Assert.Equal("code", dto.Code);
            Assert.Equal(0, dto.Id);
        }
        }
}
