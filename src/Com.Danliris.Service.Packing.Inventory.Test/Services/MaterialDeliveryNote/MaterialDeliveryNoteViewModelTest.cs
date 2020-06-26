using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteViewModelTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            MaterialDeliveryNoteViewModel viewModel = new MaterialDeliveryNoteViewModel();

            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_ItemsMaterialDeliveryNoteWeaving_moreThan1()
        {
            MaterialDeliveryNoteViewModel viewModel = new MaterialDeliveryNoteViewModel();

            viewModel.Items = new List<ItemsViewModel>()
            {
                new ItemsViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
