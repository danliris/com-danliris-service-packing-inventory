using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingPackingList;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentPackingList
{
    public class GarmentShippingPackingListControllerSetRevisedShippingTest : GarmentShippingPackingListControllerTest
    {
        [Fact]
        public async Task Set_RevisedShipping_Success()
        {
            var serviceMock = new Mock<IGarmentShippingPackingListService>();
            serviceMock
                .Setup(s => s.SetStatus(It.IsAny<int>(), It.IsAny<GarmentShippingPackingListStatusEnum>(), It.IsAny<string>()))
                .Verifiable();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.SetRevisedShipping(It.IsAny<int>(), "Alasan");

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Set_RevisedShipping_Exception_BadRequest()
        {
            var serviceMock = new Mock<IGarmentShippingPackingListService>();
            serviceMock
                .Setup(s => s.SetStatus(It.IsAny<int>(), It.IsAny<GarmentShippingPackingListStatusEnum>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.SetRevisedShipping(It.IsAny<int>(), It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Set_RevisedShipping_Exception_InternalServerError()
        {
            var serviceMock = new Mock<IGarmentShippingPackingListService>();
            serviceMock
                .Setup(s => s.SetStatus(It.IsAny<int>(), It.IsAny<GarmentShippingPackingListStatusEnum>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.SetRevisedShipping(It.IsAny<int>(), "Alasan");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
