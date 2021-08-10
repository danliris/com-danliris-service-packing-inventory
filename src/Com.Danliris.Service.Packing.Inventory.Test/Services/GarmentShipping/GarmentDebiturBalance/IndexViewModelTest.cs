using System;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDebiturBalance;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentDebiturBalance
{
    public class IndexViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            IndexViewModel index = new IndexViewModel()
            {
                buyerAgentId = 1,
                buyerAgentCode = "buyerAgentCode",
                buyerAgentName = "buyerAgentName",
                balanceDate = DateTimeOffset.Now,
                id = 1,
                balanceAmount  = 0,
            };

            Assert.Equal("buyerAgentCode", index.buyerAgentCode);
            Assert.Equal("buyerAgentName", index.buyerAgentName);
            Assert.Equal(0, index.balanceAmount);
            Assert.True(DateTimeOffset.MinValue <index.balanceDate);         
        }
    }
}
