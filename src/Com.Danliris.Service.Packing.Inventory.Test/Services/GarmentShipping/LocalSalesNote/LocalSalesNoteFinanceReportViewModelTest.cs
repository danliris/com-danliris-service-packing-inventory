using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesNote;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.LocalSalesNote
{
    public class LocalSalesNoteFinanceReportViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            LocalSalesNoteFinanceReportViewModel model = new LocalSalesNoteFinanceReportViewModel {
                Amount=1,
                BuyerCode="code",
                BuyerName="name",
                Date=DateTimeOffset.Now,
                SalesNoteId=1,
                SalesNoteNo="no"
            };
            
            Assert.NotNull(model);
        }
    }
}
