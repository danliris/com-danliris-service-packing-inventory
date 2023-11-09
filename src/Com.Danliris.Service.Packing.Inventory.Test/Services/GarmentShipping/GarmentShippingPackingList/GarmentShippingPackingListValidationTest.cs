using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListValidationTest
    {
        private GarmentShippingPackingListViewModel ViewModel
        {
            get
            {
                return new GarmentShippingPackingListViewModel
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
                    Items = new List<GarmentShippingPackingListItemViewModel>
                    {
                        new GarmentShippingPackingListItemViewModel
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
                            Details = new List<GarmentShippingPackingListDetailViewModel>
                            {
                                new GarmentShippingPackingListDetailViewModel
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
                                    Sizes = new List<GarmentShippingPackingListDetailSizeViewModel>
                                    {
                                        new GarmentShippingPackingListDetailSizeViewModel
                                        {
                                            Size = new SizeViewModel
                                            {
                                                Id = 0,
                                                Size = null,
                                                SizeIdx = 0,
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
                    Measurements = new List<GarmentShippingPackingListMeasurementViewModel>
                    {
                        new GarmentShippingPackingListMeasurementViewModel
                        {
                            Length = 0,
                            Width = 0,
                            Height = 0,
                            CartonsQuantity = 0
                        }
                    },
                    ShippingMark = null,
                    SideMark = null,
                    Remark = null,
                    StatusActivities = new List<GarmentShippingPackingListStatusActivityViewModel>
                    {
                        new GarmentShippingPackingListStatusActivityViewModel
                        {
                            Id = 1,
                            CreatedAgent = "",
                            CreatedBy = "",
                            CreatedDate = DateTimeOffset.Now,
                            Remark = "",
                            Status = ""
                        }
                    }
                };
            }
        }

        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingPackingListViewModel viewModel = ViewModel;
            foreach (var activity in viewModel.StatusActivities)
            {
                activity.Id = activity.Id;
                activity.CreatedDate = activity.CreatedDate;
                activity.CreatedBy = activity.CreatedBy;
                activity.CreatedAgent = activity.CreatedAgent;
                activity.Remark = activity.Remark;
                activity.Status = activity.Status;
            }

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_DateMoreThanToday()
        {
            GarmentShippingPackingListViewModel viewModel = ViewModel;
            viewModel.Date = DateTimeOffset.Now.AddDays(1);
            viewModel.Items = new List<GarmentShippingPackingListItemViewModel>();
            viewModel.Measurements = new List<GarmentShippingPackingListMeasurementViewModel>();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingPackingListViewModel viewModel = ViewModel;
            viewModel.Items = new List<GarmentShippingPackingListItemViewModel>
            {
                new GarmentShippingPackingListItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_DetailsDefaultValue()
        {
            GarmentShippingPackingListViewModel viewModel = ViewModel;
            viewModel.Items = new List<GarmentShippingPackingListItemViewModel>
            {
                new GarmentShippingPackingListItemViewModel
                {
                    Details = new List<GarmentShippingPackingListDetailViewModel>
                    {
                        new GarmentShippingPackingListDetailViewModel()
                    }
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_QuantityDifferent()
        {
            GarmentShippingPackingListViewModel viewModel = ViewModel;

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

        [Fact]
        public void Validate_MeasurementsDefaultValue()
        {
            GarmentShippingPackingListViewModel viewModel = ViewModel;
            viewModel.Measurements = new List<GarmentShippingPackingListMeasurementViewModel>
            {
                new GarmentShippingPackingListMeasurementViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
