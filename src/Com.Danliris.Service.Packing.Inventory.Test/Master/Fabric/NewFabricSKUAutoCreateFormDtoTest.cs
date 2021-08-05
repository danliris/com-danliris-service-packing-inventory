using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
   public class NewFabricSKUAutoCreateFormDtoTest
    {
        [Fact]
        public void Should_Success_Intantiate()
        {
            NewFabricSKUAutoCreateFormDto dto = new NewFabricSKUAutoCreateFormDto()
            {
                ProcessType = "ProcessType",
                ProductionOrderNumber = "ProductionOrderNumber",
                Grade = "Grade"
            };

            Assert.Equal("ProductionOrderNumber", dto.ProductionOrderNumber);
            Assert.Equal("ProcessType", dto.ProcessType);
            Assert.Equal("Grade", dto.Grade);
        }
    }
}
