using System;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.CoverLetter
{
    public class IndexViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            IndexViewModel index = new IndexViewModel()
            {
                address = "address",
                attn = "attn",
                bookingDate = DateTimeOffset.Now,
                date = DateTimeOffset.Now,
                id = 1,
                invoiceNo = "invoiceNo",
                name = "name",
                orderName = "orderName",
                phone = "099"
                
            };

            Assert.Equal("address", index.address);
            Assert.Equal("attn", index.attn);
            Assert.Equal("invoiceNo", index.invoiceNo);
            Assert.Equal("name", index.name);
            Assert.Equal("orderName", index.orderName);
            Assert.Equal("099", index.phone);
            Assert.True(DateTimeOffset.MinValue <index.bookingDate);
            Assert.True(DateTimeOffset.MinValue < index.date);
        }
    }
}
