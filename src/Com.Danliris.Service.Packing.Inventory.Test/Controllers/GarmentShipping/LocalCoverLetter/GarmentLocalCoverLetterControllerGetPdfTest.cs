using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentLocalCoverLetter
{
    public class GarmentLocalCoverLetterControllerGetPdfTest : GarmentLocalCoverLetterControllerTest
    {
        private GarmentLocalCoverLetterViewModel viewModel
        {
            get
            {
                return new GarmentLocalCoverLetterViewModel()
                {
                    noteNo="jshda",
                    shippingStaff = new ShippingStaff
                    {
                        name = "asljhdal",
                        id = 2
                    },
                    buyer = new Buyer
                    {
                        Name = "jshd",
                        Id = 1,
                        Address = "asdh",
                    },
                    driver = "ashdajh",
                    truck = "ahdjsh",
                    plateNumber = "jaksj",
                    date=DateTimeOffset.Now,
                    localCoverLetterNo="kajshd",
                    remark="sldk",
                };
            }
        }
        private GarmentShippingLocalSalesNoteViewModel salesNoteViewModel
        {
            get
            {
                return new GarmentShippingLocalSalesNoteViewModel()
                {
                    noteNo = "jshdaj",
                    buyer = new Buyer
                    {
                        Name = "sajd",
                        Id = 1,
                    },
                    date = DateTimeOffset.Now,
                    useVat = true,
                    tempo = 6,
                    remark = "lsjhdalsdh",
                    items = new List<GarmentShippingLocalSalesNoteItemViewModel>()
                    {
                        new GarmentShippingLocalSalesNoteItemViewModel
                        {
                            quantity=12.1,
                            uom=new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="asjd"
                            },
                            packageQuantity=12,
                            product=new ProductViewModel
                            {
                                name="aksd",
                            },
                            packageUom= new UnitOfMeasurement
                            {
                                Unit="ksljd"
                            },
                            price=54.1
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
            var serviceMock = new Mock<IGarmentLocalCoverLetterService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModel);
            var service = serviceMock.Object;

            var salesNoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            salesNoteServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(salesNoteViewModel);
            salesNoteServiceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var salesNoteService = salesNoteServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, salesNoteService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            var serviceMock = new Mock<IGarmentLocalCoverLetterService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var salesNoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            salesNoteServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(salesNoteViewModel);
            salesNoteServiceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var salesNoteService = salesNoteServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, salesNoteService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            var serviceMock = new Mock<IGarmentLocalCoverLetterService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentLocalCoverLetterViewModel));
            var service = serviceMock.Object;

            var salesNoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            salesNoteServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(salesNoteViewModel);
            salesNoteServiceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var salesNoteService = salesNoteServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, salesNoteService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentLocalCoverLetterService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var salesNoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            salesNoteServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(salesNoteViewModel);
            salesNoteServiceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var salesNoteService = salesNoteServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, salesNoteService, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
    }
}
