using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteControllerDeleteTest : GarmentShippingLocalSalesNoteControllerTest
    {
        [Fact]
        public async Task Delete_Ok()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;
            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);

            var response = await controller.Delete(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Delete_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;
            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);
            var response = await controller.Delete(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
