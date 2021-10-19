using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDraftPackingListItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListItemValidationTest
    {
        private GarmentDraftPackingListItemViewModels ViewModel
        {
            get
            {
                return new GarmentDraftPackingListItemViewModels
                {
                    
                    Items = new List<GarmentDraftPackingListItemViewModel>
                    {
                        new GarmentDraftPackingListItemViewModel
                        {
                            RONo = null,
                            SCNo = null,
                            BuyerBrand = new Buyer
                            {
                                Id = 0,
                                Code = null,
                                Name = null
                            },
                            Comodity = new Comodity
                            {
                                Id = 0,
                                Code = null,
                                Name = null
                            },
                            ComodityDescription = null,
                            Quantity = 0,
                            Uom = new UnitOfMeasurement
                            {
                                Id = 0,
                                Unit = null
                            },
                            PriceRO = 0,
                            Price = 0,
                            Amount = 0,
                            Valas = null,
                            Unit = new Unit
                            {
                                Id = 0,
                                Code = null,
                                Name = null
                            },
                            Article = null,
                            OrderNo = null,
                            Description = null,
                            Details = new List<GarmentDraftPackingListDetailViewModel>
                            {
                                new GarmentDraftPackingListDetailViewModel
                                {
                                    Carton1 = 0,
                                    Carton2 = 0,
                                    Colour = null,
                                    CartonQuantity = 0,
                                    QuantityPCS = 0,
                                    TotalQuantity = 0,
                                    Length = 0,
                                    Width = 0,
                                    Height = 0,
                                    CartonsQuantity = 0,
                                    Sizes = new List<GarmentDraftPackingListDetailSizeViewModel>
                                    {
                                        new GarmentDraftPackingListDetailSizeViewModel
                                        {
                                            Size = new SizeViewModel
                                            {
                                                Id = 0,
                                                Size = null
                                            },
                                            Quantity = 0
                                        }
                                    }
                                }
                            },
                            AVG_GW = 0,
                            AVG_NW = 0,
                        }
                    },
                };
            }
        }

        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentDraftPackingListItemViewModels viewModel = ViewModel;

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }


        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentDraftPackingListItemViewModels viewModel = ViewModel;
            viewModel.Items = new List<GarmentDraftPackingListItemViewModel>
            {
                new GarmentDraftPackingListItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_DetailsDefaultValue()
        {
            GarmentDraftPackingListItemViewModels viewModel = ViewModel;
            viewModel.Items = new List<GarmentDraftPackingListItemViewModel>
            {
                new GarmentDraftPackingListItemViewModel
                {
                    Details = new List<GarmentDraftPackingListDetailViewModel>
                    {
                        new GarmentDraftPackingListDetailViewModel()
                    }
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_QuantityDifferent()
        {
            GarmentDraftPackingListItemViewModels viewModel = ViewModel;

            foreach (var item in viewModel.Items)
            {
                foreach (var detail in item.Details)
                {
                    foreach (var size in detail.Sizes)
                    {
                        size.Quantity = 1;
                    }
                    detail.QuantityPCS = 2;
                    detail.CartonQuantity = 3;
                }
                item.Quantity = 4;
            }

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
