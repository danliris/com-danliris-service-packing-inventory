using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var item = new List<Application.ItemsMaterialDeliveryNoteWeavingViewModel>
            {
                new Application.ItemsMaterialDeliveryNoteWeavingViewModel()
            };
            var date = DateTimeOffset.Now;
            MaterialDeliveryNoteWeavingModel model = new MaterialDeliveryNoteWeavingModel()
            {
                BuyerCode = "BuyerCode",
                BuyerId =1,
                BuyerName = "BuyerName",
                Code = "Code",
                DateSJ =date,
                DoSalesNumber = "DoSalesNumber",
                DoSalesNumberId =1,
                NumberOut = "NumberOut",
                Remark = "Remark",
                SendTo = "SendTo",
                StorageCode = "StorageCode",
                StorageId =1,
                StorageName = "StorageName",
                UnitId =1,
                UnitLength ="1",
                UnitName = "UnitName",
                UnitPacking = "UnitPacking",
                ItemsMaterialDeliveryNoteWeaving = item
            };


            Assert.Equal("BuyerCode", model.BuyerCode);
            Assert.Equal(1, model.BuyerId);
            Assert.Equal("BuyerName", model.BuyerName);
            Assert.Equal("Code", model.Code);
            Assert.Equal(date, model.DateSJ);
            Assert.Equal("DoSalesNumber", model.DoSalesNumber);
            Assert.Equal(1, model.DoSalesNumberId);
            Assert.Equal("NumberOut", model.NumberOut);
            Assert.Equal("Remark", model.Remark);
            Assert.Equal("SendTo", model.SendTo);
            Assert.Equal("StorageCode", model.StorageCode);
            Assert.Equal(1, model.StorageId);
            Assert.Equal("StorageName", model.StorageName);
            Assert.Equal(1, model.UnitId);
            Assert.Equal("1", model.UnitLength);
            Assert.Equal("UnitName", model.UnitName);
            Assert.Equal("UnitPacking", model.UnitPacking);
            Assert.Equal(item, model.ItemsMaterialDeliveryNoteWeaving);
        }
    }
}
