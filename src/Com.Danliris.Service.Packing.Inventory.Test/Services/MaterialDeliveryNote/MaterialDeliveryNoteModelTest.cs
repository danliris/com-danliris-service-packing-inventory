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
                DONumber = new DeliveryOrderMaterialDeliveryNoteWeaving()
                {
                    Id = 1,
                    DOSalesNo = "123"
                },
                FONumber = "FONumber",
                buyer = new BuyerMaterialDeliveryNoteWeaving
                {
                    Id = 1,
                    Code = "abc123",
                    Name = "abc"
                },
                Remark = "Remark",
                salesContract = new SalesContract()
                {
                    SalesContractId = 1,
                    SalesContractNo = "SalesContractNo"
                },
                unit = new UnitMaterialDeliveryNoteWeaving
                {
                    Id = 1,
                    Code = "abc123",
                    Name = "abc"
                },
                storage = new StorageMaterialDeliveryNoteWeaving
                {
                    Id = 1,
                    Code = "abc123",
                    Name = "abc",
                    unit = "abc"
                },
                Items = item
            };
            Assert.Equal(1, model.Id);
            Assert.Equal("BonCode", model.BonCode);
            Assert.Equal("Code", model.Code);
            Assert.Equal(date, model.DateFrom);
            Assert.Equal(date, model.DateTo);
            Assert.Equal(date, model.DateSJ);

            Assert.Equal(1, model.DONumber.Id);
            Assert.Equal("DONumber", model.DONumber.DOSalesNo);

            Assert.Equal("FONumber", model.FONumber);

            Assert.Equal(1, model.buyer.Id);
            Assert.Equal("Receiver", model.buyer.Code);
            Assert.Equal("Receiver", model.buyer.Name);

            Assert.Equal("Remark", model.Remark);

            Assert.Equal(1, model.salesContract.SalesContractId);
            Assert.Equal("SCNumber", model.salesContract.SalesContractNo);

            Assert.Equal(1, model.unit.Id);
            Assert.Equal("Sender", model.unit.Code);
            Assert.Equal("Sender", model.unit.Name);

            Assert.Equal(1, model.storage.Id);
            Assert.Equal("StorageNumber", model.storage.Code);
            Assert.Equal("StorageNumber", model.storage.Name);
            Assert.Equal("StorageNumber", model.storage.unit);

            Assert.Equal(item, model.Items);
        }
        }
}
