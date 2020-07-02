using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOControllerGetPdfTest : GarmentShippingExportSalesDOControllerTest
    {
        private GarmentShippingExportSalesDOViewModel viewModel
        {
            get
            {
                return new GarmentShippingExportSalesDOViewModel()
                {
                    buyerAgent=new Buyer
                    {
                        Id=1,
                        Name="aa"
                    },
                    date=DateTimeOffset.Now,
                    exportSalesDONo="aa",
                    invoiceNo="aa",
                    unit=new Unit
                    {
                        Name="sd",
                        Code="sdj"
                    },
                    to="asdla",
                    items= new List<GarmentShippingExportSalesDOItemViewModel>()
                    {
                        new GarmentShippingExportSalesDOItemViewModel
                        {
                            quantity=1,
                            cartonQuantity=1,
                            comodity=new Comodity
                            {
                                Name="kjs",
                            },
                            grossWeight=1,
                            nettWeight=1,
                            uom=new UnitOfMeasurement
                            {
                                Unit="kjshd",
                            },
                            volume=1,
                            description="asjhd",
                        }
                    }
                };
            }
        }

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingExportSalesDOService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModel);

            var service = serviceMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingExportSalesDOViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingExportSalesDOService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingExportSalesDOViewModel>()))
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
            var serviceMock = new Mock<IGarmentShippingExportSalesDOService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentShippingExportSalesDOViewModel));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingExportSalesDOViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingExportSalesDOService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingExportSalesDOViewModel>()))
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
