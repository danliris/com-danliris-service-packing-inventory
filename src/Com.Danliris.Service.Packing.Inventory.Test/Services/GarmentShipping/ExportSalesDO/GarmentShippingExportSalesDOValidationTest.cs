using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingExportSalesDOViewModel viewModel = new GarmentShippingExportSalesDOViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

    }
}
