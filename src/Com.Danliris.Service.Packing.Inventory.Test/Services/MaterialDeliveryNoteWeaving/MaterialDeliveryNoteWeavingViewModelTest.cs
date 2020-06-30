using Com.Danliris.Service.Packing.Inventory.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingViewModelTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            MaterialDeliveryNoteWeavingViewModel viewModel = new MaterialDeliveryNoteWeavingViewModel();

            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_ItemsMaterialDeliveryNoteWeaving_moreThan1()
        {
            MaterialDeliveryNoteWeavingViewModel viewModel = new MaterialDeliveryNoteWeavingViewModel();

            viewModel.ItemsMaterialDeliveryNoteWeaving = new List<ItemsMaterialDeliveryNoteWeavingViewModel>()
            {
                new ItemsMaterialDeliveryNoteWeavingViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
