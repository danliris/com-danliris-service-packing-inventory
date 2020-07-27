using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Inventory
{
    public class ProductPackingInventoryDocumentModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var Date = DateTimeOffset.Now;
            ProductPackingInventoryDocumentModel model = new ProductPackingInventoryDocumentModel("documentNo", Date, "referenceNo", "referenceType", 1, "storageName", "storageCode", "inventoryType", "remark");
            Assert.Equal("documentNo", model.DocumentNo);
            Assert.Equal(Date, model.Date);
            Assert.Equal("referenceNo", model.ReferenceNo);
            Assert.Equal("remark", model.Remark);
            Assert.Equal("referenceType", model.ReferenceType);
            Assert.Equal(1, model.StorageId);
            Assert.Equal("storageName", model.StorageName);
            Assert.Equal("storageCode", model.StorageCode);
            Assert.Equal("inventoryType", model.InventoryType);
        }
        }
}
