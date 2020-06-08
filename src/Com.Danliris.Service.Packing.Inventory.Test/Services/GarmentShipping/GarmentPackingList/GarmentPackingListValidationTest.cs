using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListValidationTest
    {
        private GarmentPackingListViewModel ViewModel
        {
            get
            {
                return new GarmentPackingListViewModel
                {
                    InvoiceNo = null,
                    PackingListType = null,
                    InvoiceType = null,
                    Section = new Section
                    {
                        Id = 0,
                        Code = null
                    },
                    Date = null,
                    LCNo = null,
                    IssuedBy = null,
                    Destination = null,
                    TruckingDate = null,
                    ExportEstimationDate = null,
                    Omzet = false,
                    Accounting = false,
                    Items = new List<GarmentPackingListItemViewModel>
                    {
                        new GarmentPackingListItemViewModel
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
                            Details = new List<GarmentPackingListDetailViewModel>
                            {
                                new GarmentPackingListDetailViewModel
                                {
                                    Carton1 = 0,
                                    Carton2 = 0,
                                    Colour = null,
                                    CartonQuantity = 0,
                                    QuantityPCS = 0,
                                    TotalQuantity = 0,
                                    Sizes = new List<GarmentPackingListDetailSizeViewModel>
                                    {
                                        new GarmentPackingListDetailSizeViewModel
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
                    GrossWeight = 0,
                    NettWeight = 0,
                    TotalCartons = 0,
                    Measurements = new List<GarmentPackingListMeasurementViewModel>
                    {
                        new GarmentPackingListMeasurementViewModel
                        {
                            Length = 0,
                            Width = 0,
                            Height = 0,
                            CartonsQuantity = 0
                        }
                    },
                    ShippingMark = null,
                    SideMark = null,
                    Remark = null
                };
            }
        }

        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentPackingListViewModel viewModel = ViewModel;

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_DateMoreThanToday()
        {
            GarmentPackingListViewModel viewModel = ViewModel;
            viewModel.Date = DateTimeOffset.Now.AddDays(1);
            viewModel.Items = new List<GarmentPackingListItemViewModel>();
            viewModel.Measurements = new List<GarmentPackingListMeasurementViewModel>();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentPackingListViewModel viewModel = ViewModel;
            viewModel.Items = new List<GarmentPackingListItemViewModel>
            {
                new GarmentPackingListItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_DetailsDefaultValue()
        {
            GarmentPackingListViewModel viewModel = ViewModel;
            viewModel.Items = new List<GarmentPackingListItemViewModel>
            {
                new GarmentPackingListItemViewModel
                {
                    Details = new List<GarmentPackingListDetailViewModel>
                    {
                        new GarmentPackingListDetailViewModel()
                    }
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_MeasurementsDefaultValue()
        {
            GarmentPackingListViewModel viewModel = ViewModel;
            viewModel.Measurements = new List<GarmentPackingListMeasurementViewModel>
            {
                new GarmentPackingListMeasurementViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
