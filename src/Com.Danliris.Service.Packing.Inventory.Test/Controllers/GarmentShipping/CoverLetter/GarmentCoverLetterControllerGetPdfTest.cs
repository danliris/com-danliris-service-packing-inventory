using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentCoverLetter
{
    public class GarmentCoverLetterControllerGetPdfTest : GarmentCoverLetterControllerTest
    {
        private GarmentCoverLetterViewModel viewModel
        {
            get
            {
                return new GarmentCoverLetterViewModel()
                {
                    invoiceId=1,
                    emkl=new EMKL
                    {
                        Id=1,
                        address="lsad",
                        Name="slkd",
                        attn="lsajd",
                        Code="sad",
                        phone="8-8"
                    },
                    bookingDate=DateTimeOffset.Now,
                    cartoonQuantity=23,
                    containerNo="ajsd",
                    date=DateTimeOffset.Now,
                    dlSeal="jsdhajsh",
                    setsQuantity=231,
                    packQuantity=908,
                    shippingSeal="asdhaljs",
                    shippingStaff=new ShippingStaff
                    {
                        name="asljhdal",
                        id=2
                    },
                    emklSeal="kjashd",
                    order=new Buyer
                    {
                        Name="jshd",
                        Id=1,
                        Address="asdh",
                    },
                    driver="ashdajh",
                    forwarder=new Forwarder
                    {
                        name="slhd"
                    },
                    freight="slhadl",
                    invoiceNo="asjhd",
                    packingListId=1,
                    truck="ahdjsh",
                    plateNumber="jaksj",
                    unit="asjhd",
                    pcsQuantity=83,
                };
            }
        }
        private GarmentShippingInvoiceViewModel invoiceVM
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
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",
                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10,
                            Amount=99999999999,
                            Price=1332,
                            CMTPrice=1,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                };
            }
        }
        private GarmentPackingListViewModel packingListVM
        {
            get
            {
                return new GarmentPackingListViewModel()
                {
                    Remark = "aa",
                    SideMark = "aa",
                    ShippingMark = "aa",
                    GrossWeight = 12,
                    NettWeight = 12,
                    Measurements = new List<GarmentPackingListMeasurementViewModel>()
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
                    Id = 1,
                    Name = "aa",
                    Code = "aa",
                    Country = "aa",
                    Address = "aa",
                };
            }
        }

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            var serviceMock = new Mock<IGarmentCoverLetterService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModel);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadByInvoiceNo(It.IsAny<string>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListService, invoiceService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            var serviceMock = new Mock<IGarmentCoverLetterService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadByInvoiceNo(It.IsAny<string>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListService, invoiceService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            var serviceMock = new Mock<IGarmentCoverLetterService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentCoverLetterViewModel));
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadByInvoiceNo(It.IsAny<string>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListService, invoiceService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentCoverLetterService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadByInvoiceNo(It.IsAny<string>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListService, invoiceService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
    }
}
