using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.CreditAdvice
{
   public class IndexViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            IndexViewModel index = new IndexViewModel()
            {
               amount=1,
               amountToBePaid =1,
               bankAccountName = "bankAccountName",
               buyerName = "bankAccountName",
               date =DateTimeOffset.Now,
               id=1,
               invoiceNo = "invoiceNo"
            };

            Assert.Equal(1, index.amount);
            Assert.Equal(1, index.amountToBePaid);
            Assert.Equal("bankAccountName", index.bankAccountName);
            Assert.Equal("invoiceNo", index.invoiceNo);
            Assert.Equal(1, index.id);
            Assert.True(DateTimeOffset.MinValue < index.date);
        
        }
    }
}
