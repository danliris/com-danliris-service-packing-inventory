using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingStockOpname.Warehouse
{
   public class StockOpnameWarehouseValidationTest
    {
        [Fact]
        public void Should_Have_Error_When_Validate_Default_Value()
        {
            StockOpnameWarehouseViewModel viewModel = new StockOpnameWarehouseViewModel();
            viewModel.WarehousesProductionOrders = new List<StockOpnameWarehouseProductionOrderViewModel>()
            {
                 new StockOpnameWarehouseProductionOrderViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Should_Have_Error_When_Date_LesThan_Today()
        {
            StockOpnameWarehouseViewModel viewModel = new StockOpnameWarehouseViewModel()
            {
                Date=DateTimeOffset.Now.AddDays(-1)
            };
            viewModel.WarehousesProductionOrders = new List<StockOpnameWarehouseProductionOrderViewModel>()
            {
                 new StockOpnameWarehouseProductionOrderViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

    }
}
