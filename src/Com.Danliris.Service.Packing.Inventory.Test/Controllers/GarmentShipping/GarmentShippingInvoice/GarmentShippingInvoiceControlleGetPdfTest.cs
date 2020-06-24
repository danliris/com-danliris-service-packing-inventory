using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Xunit;
using System.Threading.Tasks;
using Moq;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceControlleGetPdfTest : GarmentShippingInvoiceControllerTest
    {
        private GarmentShippingInvoiceViewModel ViewModel
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "no",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId=1,
                    ShippingPer="aa",
                    From="aa",
                    To="aa",
                    
                    Items= new List<GarmentShippingInvoiceItemViewModel> ()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aa",
                            Quantity=1002,
                            Amount=(decimal)12222.01,
                            Price=1332,
                            RONo="roNo",
                            Uom= new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="aa"
                            }
                        }
                    }
                };
            }
        }

        private GarmentPackingListViewModel packingListVM
        {
            get
            {
                return new GarmentPackingListViewModel()
                {
                    Remark="aa",
                    SideMark="aa",
                    ShippingMark="aa",
                    GrossWeight=12,
                    NettWeight=12,
                    Measurements= new List<GarmentPackingListMeasurementViewModel>()
                    {
                        new GarmentPackingListMeasurementViewModel
                        {
                            Width=1,
                            Height=1,
                            CartonsQuantity=1,
                            Length=1,
                            
                        }
                    }
                };
            }
        }

        private Buyer buyerVm
        {
            get
            {
                return new Buyer()
                {
                    Id=1,
                    Name="aa",
                    Code="aa",
                    Country="aa",
                    Address="aa",
                };
            }
        }

        private BankAccount bankVm
        {
            get
            {
                return new BankAccount()
                {
                   id=1,
                   accountName="aa",
                   Currency=new Currency
                   {
                       Id=1,
                       Code="aa",
                   },
                   AccountNumber="21231",
                   bankAddress="asdasda",
                   swiftCode="jskha"
                };
            }
        }

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_LongAmount()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            ViewModel.Items.First().Amount = (decimal)123456789012345612.007;
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentShippingInvoiceViewModel));
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
    }
}
