using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.MaterialDelivery
{
    public class MaterialDeliveryNoteViewModelTest
    {
        [Fact]
        public void Validate_Default()
        {
            MaterialDeliveryNoteViewModel defaultViewModel = new MaterialDeliveryNoteViewModel();

            var defaultValidationResult = defaultViewModel.Validate(null);
            Assert.True(defaultValidationResult.Count() > 0);
        }

        [Fact]
        public void Validate_Default_When_Items_More_Than1()
        {
            MaterialDeliveryNoteViewModel viewModel = new MaterialDeliveryNoteViewModel();
            viewModel.DateTo = DateTimeOffset.Now;
            viewModel.Items = new List<ItemsViewModel>()
            {
                new ItemsViewModel()
                {
                    WeightBruto =0,
                    WeightBale =0,
                    WeightCone =0,
                    WeightDOS =0
                }
            };


            var defaultValidationResult = viewModel.Validate(null);
            Assert.True(defaultValidationResult.Count() > 0);
        }

    }
}
