using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionControllerGetPdfTest : GarmentShippingInstructionControllerTest
    {
        private GarmentShippingInstructionViewModel viewModel
        {
            get
            {
                return new GarmentShippingInstructionViewModel()
                {
                    InvoiceId=1,
                    InvoiceNo="no",
                    Phone="11231321",
                    EMKL=new EMKL
                    {
                        Id=1,
                        Name="sdlka",
                        Code="jsln"
                    },
                    ATTN="ajs",
                    BankAccountId=1,
                    BankAccountName="salk",
                    BuyerAgent=new Buyer
                    {
                        Id=1,
                        Name="ska",
                        Code="sfasmn"
                    },
                    BuyerAgentAddress="askdan.ans",
                    CartonNo="55",
                    CC="sna.",
                    Date=DateTimeOffset.Now,
                    LadingDate=DateTimeOffset.Now,
                    Fax="0880980",
                    FeederVessel="dans.n",
                    Freight="skfaknf",
                    ShippingStaffName="adsadlkasjk"
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

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModel);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service,  identityProvider, validateService, packingListService,invoiceService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
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
            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentShippingInstructionViewModel));
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
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
            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
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
