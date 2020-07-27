using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.InventorySKU
{
    public class FormDtoTest
    {
        [Fact]
        public void Validate_default()
        {
            FormDto dto = new FormDto()
            {
                Items = new List<FormItemDto>()
            };
           
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
