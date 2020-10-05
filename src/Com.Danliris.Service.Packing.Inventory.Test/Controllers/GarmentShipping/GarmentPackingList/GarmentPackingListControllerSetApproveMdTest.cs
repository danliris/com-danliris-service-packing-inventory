using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListControllerSetApproveMdTest : GarmentPackingListControllerTest
    {
        [Fact]
        public async Task Set_ApproveMd_Success()
        {
            var serviceMock = new Mock<IGarmentPackingListService>();
            serviceMock
                .Setup(s => s.SetApproveMd(It.IsAny<int>(), It.IsAny<GarmentPackingListViewModel>()))
                .Verifiable();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.ApproveMd(It.IsAny<int>(), new GarmentPackingListMerchandiserViewModel());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Set_ApproveMd_Exception_BadRequest()
        {
            var serviceMock = new Mock<IGarmentPackingListService>();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.ApproveMd(It.IsAny<int>(), new GarmentPackingListMerchandiserViewModel());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Set_ApproveMd_Exception_InternalServerError()
        {
            var serviceMock = new Mock<IGarmentPackingListService>();
            serviceMock
                .Setup(s => s.SetApproveMd(It.IsAny<int>(), It.IsAny<GarmentPackingListViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.ApproveMd(It.IsAny<int>(), new GarmentPackingListMerchandiserViewModel());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
