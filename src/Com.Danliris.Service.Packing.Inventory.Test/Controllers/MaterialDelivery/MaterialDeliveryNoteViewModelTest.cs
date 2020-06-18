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
        public void Validate_When_Items_More_Than1()
        {
            MaterialDeliveryNoteViewModel viewModel = new MaterialDeliveryNoteViewModel();
            viewModel.DateTo = default(DateTimeOffset);
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


        [Fact]
        public void Validate_When_DateFrom_MoreThan_DateTo()
        {
            MaterialDeliveryNoteViewModel viewModel = new MaterialDeliveryNoteViewModel();
            viewModel.DateTo = DateTimeOffset.Now.AddDays(-1);
            viewModel.DateFrom = DateTimeOffset.Now.AddDays(1);
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

        [Fact]
        public void Validate_When_DateSJ_Empty()
        {
            MaterialDeliveryNoteViewModel viewModel = new MaterialDeliveryNoteViewModel();
            viewModel.DateSJ = default(DateTimeOffset);
            

            var defaultValidationResult = viewModel.Validate(null);
            Assert.True(defaultValidationResult.Count() > 0);
        }

    }
}
