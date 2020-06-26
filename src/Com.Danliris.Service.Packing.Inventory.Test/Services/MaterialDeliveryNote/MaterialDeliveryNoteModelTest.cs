using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
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
                DoNumberId =1,
                DONumber = "DONumber",
                FONumber = "FONumber",
                ReceiverId =1,
                ReceiverCode = "ReceiverCode",
                ReceiverName = "ReceiverName",
                Remark = "Remark",
                SCNumberId = 1,
                SCNumber = "SCNumber",
                SenderId =1,
                SenderCode= "SenderCode",
                SenderName = "SenderName",
                StorageId =1,
                StorageCode= "StorageCode",
                StorageName= "StorageName",
                Items = item
            };
            Assert.Equal(1, model.Id);
            Assert.Equal("BonCode", model.BonCode);
            Assert.Equal("Code", model.Code);
            Assert.Equal(date, model.DateFrom);
            Assert.Equal(date, model.DateTo);
            Assert.Equal(date, model.DateSJ);

            Assert.Equal(1, model.DoNumberId);
            Assert.Equal("DONumber", model.DONumber);

            Assert.Equal("FONumber", model.FONumber);

            Assert.Equal(1, model.ReceiverId);
            Assert.Equal("Receiver", model.ReceiverCode);
            Assert.Equal("Receiver", model.ReceiverName);

            Assert.Equal("Remark", model.Remark);

            Assert.Equal(1, model.SCNumberId);
            Assert.Equal("SCNumber", model.SCNumber);

            Assert.Equal(1, model.SenderId);
            Assert.Equal("Sender", model.SenderCode);
            Assert.Equal("Sender", model.SenderName);

            Assert.Equal(1, model.StorageId);
            Assert.Equal("StorageNumber", model.StorageCode);
            Assert.Equal("StorageNumber", model.StorageName);

            Assert.Equal(item, model.Items);
        }
        }
}
