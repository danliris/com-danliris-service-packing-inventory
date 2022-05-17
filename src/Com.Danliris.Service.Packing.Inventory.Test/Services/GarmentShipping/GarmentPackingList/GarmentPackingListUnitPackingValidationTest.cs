using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListUnitPackingValidationTest
    {
        private GarmentPackingListUnitPackingViewModel ViewModel
        {
            get
            {
                return new GarmentPackingListUnitPackingViewModel
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
                                    Length = 0,
                                    Width = 0,
                                    Height = 0,
                                    CartonsQuantity = 0,
                                    Sizes = new List<GarmentPackingListDetailSizeViewModel>
                                    {
                                        new GarmentPackingListDetailSizeViewModel
                                        {
                                            Size = new SizeViewModel
                                            {
                                                Id = 0,
                                                Size = null,
                                                SizeIdx = 0
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
                    Remark = null,
                    Status = GarmentPackingListStatusEnum.DRAFT_APPROVED_SHIPPING.ToString()
                };
            }
        }

        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentPackingListUnitPackingViewModel viewModel = ViewModel;

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_Items_Empty()
        {
            GarmentPackingListUnitPackingViewModel viewModel = ViewModel;
            viewModel.Mode = Mode.CREATE;
            viewModel.Items = null;

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
