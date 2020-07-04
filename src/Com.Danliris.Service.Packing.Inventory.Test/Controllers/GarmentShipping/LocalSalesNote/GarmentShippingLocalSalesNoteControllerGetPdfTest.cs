using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteControllerGetPdfTest : GarmentShippingLocalSalesNoteControllerTest
    {
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

        private GarmentShippingLocalSalesNoteViewModel ViewModel
        {
            get
            {
                return new GarmentShippingLocalSalesNoteViewModel()
                {
                    noteNo="jshdaj",
                    buyer = new Buyer
                    {
                        Name="sajd",
                        Id=1,
                    },
                    date=DateTimeOffset.Now,
                    useVat=true,
                    tempo=6,
                    remark="lsjhdalsdh",
                    items= new List<GarmentShippingLocalSalesNoteItemViewModel>()
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

        private GarmentShippingLocalSalesNoteViewModel ViewModelNegative
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
                            quantity=17642934623962.1,
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
                            price=-54.1
                        }
                    }
                };
            }
        }
        [Fact]
        public async Task Should_Success_GetPDF()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service,  identityProvider, validateService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_Negative()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModelNegative);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentShippingLocalSalesNoteViewModel));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service,  identityProvider, validateService);
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
    }
}
