using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentCoverLetter
{
    public class GarmentCoverLetterControllerPostTest : GarmentCoverLetterControllerTest
    {
        [Fact]
        public async Task Post_Created()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentCoverLetterService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<GarmentCoverLetterViewModel>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListServiceMock.Object, invoiceServiceMock.Object);

            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_ValidationException_BadRequest()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentCoverLetterService>();
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentCoverLetterViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListServiceMock.Object, invoiceServiceMock.Object);
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentCoverLetterService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<GarmentCoverLetterViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListServiceMock.Object, invoiceServiceMock.Object);
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
