using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DTOs
{
    public class UnitOfMeasurementDtoTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            UnitOfMeasurementDto dto = new UnitOfMeasurementDto(new UnitOfMeasurementModel());
            Assert.Equal(0, dto.Id);
        }
        }
}
