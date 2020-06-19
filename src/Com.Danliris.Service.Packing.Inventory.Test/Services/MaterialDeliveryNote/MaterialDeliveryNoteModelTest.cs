using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var date = DateTimeOffset.Now;
            var item = new List<ItemsViewModel>()
            {
                new ItemsViewModel()
            };
            MaterialDeliveryNoteModel model = new MaterialDeliveryNoteModel()
            {
                Id =1,
                BonCode = "BonCode",
                Code = "Code",
                DateFrom = date,
                DateSJ =date,
                DateTo =date,
                DONumber = "DONumber",
                FONumber = "FONumber",
                Receiver = "Receiver",
                Remark = "Remark",
                SCNumber = "SCNumber",
                Sender = "Sender",
                StorageNumber = "StorageNumber",
                Items = item
            };
            Assert.Equal(1, model.Id);
            Assert.Equal("BonCode", model.BonCode);
            Assert.Equal("Code", model.Code);
            Assert.Equal(date, model.DateFrom);
            Assert.Equal(date, model.DateTo);
            Assert.Equal(date, model.DateSJ);
            Assert.Equal("DONumber", model.DONumber);
            Assert.Equal("FONumber", model.FONumber);
            Assert.Equal("Receiver", model.Receiver);
            Assert.Equal("Remark", model.Remark);
            Assert.Equal("SCNumber", model.SCNumber);
            Assert.Equal("Sender", model.Sender);
            Assert.Equal("StorageNumber", model.StorageNumber);
            Assert.Equal(item, model.Items);
        }
        }
}
