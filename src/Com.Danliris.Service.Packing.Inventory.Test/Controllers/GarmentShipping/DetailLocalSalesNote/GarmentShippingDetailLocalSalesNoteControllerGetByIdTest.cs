using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingDetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteControllerGetByIdTest : GarmentShippingDetailLocalSalesNoteControllerTest
    {
        [Fact]
        public async Task GetById_Ok()
        {
            var serviceMock = new Mock<IGarmentShippingDetailLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentShippingDetailLocalSalesNoteViewModel());
            var service = serviceMock.Object;
            var localsalesnoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, localsalesnoteServiceMock.Object, identityProvider, validateService);
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingDetailLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;
            var localsalesnoteServiceMock = new Mock<IGarmentShippingLocalSalesNoteService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, localsalesnoteServiceMock.Object, identityProvider, validateService);
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
