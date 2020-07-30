using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.ExportSalesDO
{
  public  class IndexViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            IndexViewModel index = new IndexViewModel()
            {
               buyerAgent =new Buyer(),
                date = DateTimeOffset.Now,
                id = 1,
                exportSalesDONo = "exportSalesDONo",
                invoiceNo = "invoiceNo",
                to ="to",
                unit=new Unit()
            };

            Assert.NotNull( index.buyerAgent);
            Assert.NotNull(index.unit);
            Assert.Equal(1, index.id);
            Assert.Equal("exportSalesDONo", index.exportSalesDONo);
            Assert.Equal("invoiceNo", index.invoiceNo);
            Assert.Equal("to", index.to);
            Assert.True(DateTimeOffset.MinValue < index.date);

        }
    }
}
