using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceValidationTest
	{
		private GarmentShippingInvoiceViewModel ViewModel
		{
			get
			{
				return new GarmentShippingInvoiceViewModel
				{
					InvoiceNo = null,
					InvoiceDate = DateTimeOffset.MinValue,
					
					FabricType = null,
					Items = new List<GarmentShippingInvoiceItemViewModel>
					{
						new GarmentShippingInvoiceItemViewModel
						{
							RONo = null,
							SCNo = null,
							BuyerBrand = new BuyerBrand
							{
								Id = 0,
								Name = null
							},
							Comodity = new Comodity
							{
								Id = 0,
								Code = null,
								Name = null
							},
							ComodityDesc = null,
							Quantity = 0,
							Uom = new UnitOfMeasurement
							{
								Id = 0,
								Unit = null
							},
							PriceRO = 0,
							Price = 0,
							Amount = 0,
							CurrencyCode = null,
							Unit = new Unit
							{
								Id = 0,
								Code = null,
								Name = null
							},
							CMTPrice=0

						}
					},
					
					GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>
					{
						new GarmentShippingInvoiceAdjustmentViewModel
						{
							AdjustmentDescription = "aaa",
							AdjustmentValue =0
							 
						},
						new GarmentShippingInvoiceAdjustmentViewModel
						{
							AdjustmentDescription = "",
							AdjustmentValue =1

						}
					},
					TotalAmount = 0,
					Say = "",
					Memo = ""
				};
			}
		}
		[Fact]
		public void Validate_DefaultValue()
		{
			GarmentShippingInvoiceViewModel viewModel = ViewModel;

			var result = viewModel.Validate(null);
			Assert.NotEmpty(result.ToList());
		}

		[Fact]
		public void Validate_DateMoreThanToday()
		{
			GarmentShippingInvoiceViewModel viewModel = ViewModel;
			viewModel.InvoiceDate = DateTimeOffset.Now.AddDays(1);
			viewModel.Items = new List<GarmentShippingInvoiceItemViewModel>();
			viewModel.GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>();

			var result = viewModel.Validate(null);
			Assert.NotEmpty(result.ToList());
		}

		[Fact]
		public void Validate_ItemsDefaultValue()
		{
			GarmentShippingInvoiceViewModel viewModel = ViewModel;
			viewModel.Items = new List<GarmentShippingInvoiceItemViewModel>
			{
				new GarmentShippingInvoiceItemViewModel()
			};

			var result = viewModel.Validate(null);
			Assert.NotEmpty(result.ToList());
		}

		[Fact]
		public void Validate_DetailsDefaultValue()
		{
			GarmentShippingInvoiceViewModel viewModel = ViewModel;
			viewModel.Items = new List<GarmentShippingInvoiceItemViewModel>
			{
				new GarmentShippingInvoiceItemViewModel
				{
					Price =0,
					Quantity=0,
					ComodityDesc=""
					 
				}
			};

			var result = viewModel.Validate(null);
			Assert.NotEmpty(result.ToList());
		}

		[Fact]
		public void Validate_AdjustmentDescriptionValue()
		{
			GarmentShippingInvoiceViewModel viewModel = ViewModel;
			viewModel.GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>
			{
				new GarmentShippingInvoiceAdjustmentViewModel
				{
					AdjustmentDescription = "",
					AdjustmentValue = 1
				}
			};

			var result = viewModel.Validate(null);
			Assert.NotEmpty(result.ToList());
		}
		[Fact]
		public void Validate_AdjustmentValue()
		{
			GarmentShippingInvoiceViewModel viewModel = ViewModel;
			viewModel.GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>
			{
				new GarmentShippingInvoiceAdjustmentViewModel
				{
					AdjustmentDescription = "adjustment",
					AdjustmentValue = 0
				}
			};

			var result = viewModel.Validate(null);
			Assert.NotEmpty(result.ToList());
		}
	}
}
