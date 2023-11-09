using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListControllerSetApproveShippingTest : GarmentShippingPackingListControllerTest
    {
        [Fact]
        public async Task Set_ApproveShipping_Success()
        {
            var serviceMock = new Mock<IGarmentShippingPackingListService>();
            serviceMock
                .Setup(s => s.SetApproveShipping(It.IsAny<int>(), It.IsAny<GarmentShippingPackingListViewModel>()))
                .Verifiable();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.ApproveShipping(It.IsAny<int>(), new GarmentShippingPackingListShippingViewModel());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Set_ApproveShipping_Exception_BadRequest()
        {
            var serviceMock = new Mock<IGarmentShippingPackingListService>();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPackingListViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.ApproveShipping(It.IsAny<int>(), new GarmentShippingPackingListShippingViewModel());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Set_ApproveShipping_Exception_InternalServerError()
        {
            var serviceMock = new Mock<IGarmentShippingPackingListService>();
            serviceMock
                .Setup(s => s.SetApproveShipping(It.IsAny<int>(), It.IsAny<GarmentShippingPackingListViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.ApproveShipping(It.IsAny<int>(), new GarmentShippingPackingListShippingViewModel());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
