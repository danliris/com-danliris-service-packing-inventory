using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DTOs
{
    public class IdAndCodeDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            IdAndCodeDto dto = new IdAndCodeDto(1,"code");
           
            Assert.Equal("code", dto.Code);
            Assert.Equal(1, dto.Id);
        }
    }
}
