using Com.Danliris.Service.Packing.Inventory.Application.Master.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Category
{
    public class FormDtoTest
    {
        [Fact]
        public void Validate_default()
        {
            FormDto dto = new FormDto();
            var result = dto.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
