using Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingLocalSalesNote;
using System;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.LocalSalesNote
{
    public class GarmentShippingLocalSalesNoteControllerGetLocalSalesDebtorNowTest : GarmentShippingLocalSalesNoteControllerTest
    {
        [Fact]
        public async Task GetLocalSalesDebtorNow_Ok()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentShippingLocalSalesNoteViewModel());
            var service = serviceMock.Object;
            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);
            var response = controller.GetLocalSalesDebtorNow(1, 1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetLocalSalesDebtorNow_Exception_InternalServerError()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ReadShippingLocalSalesNoteListNow(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;
            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);
            var response = controller.GetLocalSalesDebtorNow(1,1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
