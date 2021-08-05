using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.AmendLetterOfCredit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.AmendLetterOfCredit
{
    public class GarmentAmendLetterOfCreditValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentAmendLetterOfCreditViewModel viewModel = new GarmentAmendLetterOfCreditViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

    }
}
