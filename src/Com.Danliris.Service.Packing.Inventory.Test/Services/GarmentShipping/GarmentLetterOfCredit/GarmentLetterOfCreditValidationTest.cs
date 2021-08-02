using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LetterOfCredit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentLetterOfCredit
{
    public class GarmentLetterOfCreditValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentLetterOfCreditViewModel viewModel = new GarmentLetterOfCreditViewModel();
            viewModel.Quantity = -1;

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_Double_DocNo()
        {
            GarmentLetterOfCreditViewModel viewModel = new GarmentLetterOfCreditViewModel();
            viewModel.DocumentCreditNo = "a";
            viewModel.available = true;
            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
