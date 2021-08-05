using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingStockOpname.Warehouse
{
  public  class StockOpnameWarehouseProductionOrderViewModelTest
    {
        [Fact]
        public void should_SUccess_Instantiate()
        {
            var date = DateTimeOffset.Now;
            var grade =  new GradeProduct();
            var uom = new UnitOfMeasurement();
            var viewModel = new StockOpnameWarehouseProductionOrderViewModel()
            {
                Id=1,
                BalanceRemains=1,
                DeliveryOrderSalesId =1,
                GradeProduct = grade,
                QtyOrder=1,
                PackagingLength =1,
                PreviousBalance =1,
                PreviousQtyPacking =1,
                ProductionOrderNo ="1",
                MtrLength =1,
                YdsLength =1,
                ProcessTypeId =1,
                ProcessTypeName= "ProcessTypeName",
                YarnMaterialId=1,
                YarnMaterialName = "YarnMaterialName",
                Uom= uom,
                InputId=1
            };

            Assert.Equal(1, viewModel.BalanceRemains);
            Assert.Equal(1, viewModel.DeliveryOrderSalesId);
            Assert.Equal(1, viewModel.Id);
            Assert.False(viewModel.IsSave);
            Assert.Equal(1, viewModel.InputId);
            Assert.Equal(grade, viewModel.GradeProduct);
            Assert.Equal(1, viewModel.PreviousBalance);
            Assert.Equal(1, viewModel.PreviousQtyPacking);
            Assert.Equal("1", viewModel.ProductionOrderNo);
            Assert.Equal(1, viewModel.MtrLength);
            Assert.Equal(1, viewModel.PackagingLength);
            Assert.Equal(1, viewModel.YdsLength);
            Assert.Equal(1, viewModel.QtyOrder);
            Assert.Equal(1, viewModel.ProcessTypeId);
            Assert.Equal("ProcessTypeName", viewModel.ProcessTypeName);
            Assert.Equal(uom, viewModel.Uom);
            Assert.Equal(1, viewModel.YarnMaterialId);
            Assert.Equal("YarnMaterialName", viewModel.YarnMaterialName);

        }
    }
}
