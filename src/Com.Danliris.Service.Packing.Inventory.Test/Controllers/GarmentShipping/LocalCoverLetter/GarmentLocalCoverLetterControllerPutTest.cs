using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentLocalCoverLetter
{
    public class GarmentLocalCoverLetterControllerPutTest : GarmentLocalCoverLetterControllerTest
    {
        [Fact]
        public async Task Put_Ok()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentLocalCoverLetterService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;


            var salesNoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            var salesNoteService = salesNoteServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, salesNoteService, identityProvider, validateService);

            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_ValidationException_BadRequest()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentLocalCoverLetterService>();
            var service = serviceMock.Object;

            var salesNoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            var salesNoteService = salesNoteServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, salesNoteService, identityProvider, validateService);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentLocalCoverLetterService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var salesNoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            var salesNoteService = salesNoteServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentLocalCoverLetterViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, salesNoteService, identityProvider, validateService);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
