using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.ProductSKU
{
    public class FormDtoTest
    {
        [Fact]
        public void Validate_default()
        {
            FormDto dto = new FormDto();
            dto.UOMId = 0;
            dto.CategoryId = 0;
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_code()
        {
            FormDto dto = new FormDto();
            dto.UOMId = 0;
            dto.Code = "Code";
            dto.CategoryId = 0;
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
