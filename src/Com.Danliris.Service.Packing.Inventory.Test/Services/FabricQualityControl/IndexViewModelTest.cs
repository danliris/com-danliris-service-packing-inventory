using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.FabricQualityControl
{
 public   class IndexViewModelTest
    {
        [Fact]
        public void should_SUccess_Instantiate()
        {
            IndexViewModel viewModel = new IndexViewModel()
            {
               CartNo = "CartNo",
               Code = "Code",
               DateIm=DateTimeOffset.Now,
               Id =1,
               InspectionMaterialBonNo = "InspectionMaterialBonNo",
               IsUsed =true,
               MachineNoIm = "MachineNoIm",
               OperatorIm = "OperatorIm",
               ProductionOrderNo = "ProductionOrderNo",
               ProductionOrderType = "ProductionOrderType",
               ShiftIm = "ShiftIm"
            };

            Assert.Equal("CartNo", viewModel.CartNo);
            Assert.Equal("Code", viewModel.Code);
            Assert.True(DateTimeOffset.MinValue < viewModel.DateIm);
            Assert.Equal(1, viewModel.Id);
            Assert.Equal("InspectionMaterialBonNo", viewModel.InspectionMaterialBonNo);
            Assert.True(viewModel.IsUsed);
            Assert.Equal("MachineNoIm", viewModel.MachineNoIm);
            Assert.Equal("OperatorIm", viewModel.OperatorIm);
            Assert.Equal("ProductionOrderNo", viewModel.ProductionOrderNo);
            Assert.Equal("ProductionOrderType", viewModel.ProductionOrderType);
            Assert.Equal("ShiftIm", viewModel.ShiftIm);
        }
    }
}
