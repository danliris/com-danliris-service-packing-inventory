using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureValidationTest
    {
		private GarmentShippingCostStructureViewModel ViewModel
		{
			get
			{
				return new GarmentShippingCostStructureViewModel
				{
					InvoiceNo = null,
					Date = DateTimeOffset.MinValue,
					FabricType = null,
					Destination = null,
					HsCode = null,
					FabricTypeId = 0,
					Comodity = new Comodity
                    {
						Id = 1,
						Code = "code",
						Name = "name"
                    },
					Amount = 0,
				};
			}
		}

		[Fact]
		public void Validate_DefaultValue()
		{
			GarmentShippingCostStructureViewModel viewModel = ViewModel;

			var result = viewModel.Validate(null);
			Assert.NotEmpty(result.ToList());
		}
	}
}
