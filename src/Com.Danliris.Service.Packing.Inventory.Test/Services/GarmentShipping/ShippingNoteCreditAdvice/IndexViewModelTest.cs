using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.ShippingNoteCreditAdvice
{
   public class IndexViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            IndexViewModel index = new IndexViewModel()
            {
               amount=1,
               paidAmount=1,
               bankAccountName = "bankAccountName",
               buyerName = "buyerName",
               date =DateTimeOffset.Now,
               id=1,
               noteNo = "noteNo"
            };

            Assert.Equal(1, index.amount);
            Assert.Equal(1, index.paidAmount);
            Assert.Equal("bankAccountName", index.bankAccountName);
            Assert.Equal("buyerName", index.buyerName);
            Assert.Equal("noteNo", index.noteNo);
            Assert.Equal(1, index.id);
            Assert.True(DateTimeOffset.MinValue < index.date);
        
        }
    }
}
