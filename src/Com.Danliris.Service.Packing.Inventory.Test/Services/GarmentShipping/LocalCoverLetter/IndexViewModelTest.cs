using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.LocalCoverLetter
{
    public class IndexViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            IndexViewModel index = new IndexViewModel()
            {
                date = DateTimeOffset.Now,
                id = 1,
                buyer = new Buyer(),
                localCoverLetterNo = "localCoverLetterNo",
                noteNo = "noteNo"
            };

            Assert.NotNull(index.buyer);
            Assert.Equal(1, index.id);
            Assert.Equal("localCoverLetterNo", index.localCoverLetterNo);
            Assert.Equal("noteNo", index.noteNo);
            Assert.True(DateTimeOffset.MinValue < index.date);

        }
    }
}
