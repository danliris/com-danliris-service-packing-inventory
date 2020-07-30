using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.FabricQualityControl
{
    public class FabricQCGradeTestsViewModelTest
    {
        [Fact]
        public void should_SUccess_Instantiate()
        {
            FabricQCGradeTestsViewModel viewModel = new FabricQCGradeTestsViewModel()
            {
                Grade = "A",
                OrderNo = "OrderNo",
                OrderQuantity = 1
            };

            Assert.Equal("A" , viewModel.Grade);
            Assert.Equal("OrderNo", viewModel.OrderNo);
            Assert.Equal(1, viewModel.OrderQuantity);
        }
    }
}
