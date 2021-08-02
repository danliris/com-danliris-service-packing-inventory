using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Danliris.Service.Packing.Inventory.Application.Master.UOM;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.UOM
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
