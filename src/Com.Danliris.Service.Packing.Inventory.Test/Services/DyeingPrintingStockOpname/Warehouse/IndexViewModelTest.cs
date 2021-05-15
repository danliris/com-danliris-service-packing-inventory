using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingStockOpname.Warehouse
{
   public class IndexViewModelTest
    {
        [Fact]
        public void should_SUccess_Instantiate()
        {
            var date = DateTimeOffset.Now;
            IndexViewModel viewModel = new IndexViewModel()
            {
                Area = DyeingPrintingArea.GUDANGJADI,
                BonNo="1",
                Date= date,
                Id=1,
                Type= DyeingPrintingArea.STOCK_OPNAME
            };

            Assert.Equal(DyeingPrintingArea.GUDANGJADI, viewModel.Area);
            Assert.Equal("1", viewModel.BonNo);
            Assert.Equal(1, viewModel.Id);
            Assert.Equal(date, viewModel.Date);
            Assert.Equal(DyeingPrintingArea.STOCK_OPNAME, viewModel.Type);
           
        }
    }
}
