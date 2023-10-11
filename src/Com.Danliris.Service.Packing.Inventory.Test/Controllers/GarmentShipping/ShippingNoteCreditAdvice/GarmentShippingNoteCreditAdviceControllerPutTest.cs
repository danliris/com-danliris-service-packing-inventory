using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceControllerPutTest : GarmentShippingNoteCreditAdviceControllerTest
    {
        [Fact]
        public async Task Put_Ok()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingNoteCreditAdviceService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentShippingNoteCreditAdviceViewModel>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingNoteCreditAdviceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_ValidationException_BadRequest()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingNoteCreditAdviceService>();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingNoteCreditAdviceViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingNoteCreditAdviceService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentShippingNoteCreditAdviceViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingNoteCreditAdviceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
