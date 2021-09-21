using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingLocalSalesNote;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.LocalSalesNote
{
    public class GarmentShippingLocalSalesNoteControllerGetForFinanceTest : GarmentShippingLocalSalesNoteControllerTest
    {
        [Fact]
        public void Get_Ok()
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
            var response = controller.GetSalesNoteFinanceReport(It.IsAny<string>(),1, 1, It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        [Fact]
        public void Should_Error_GetPLForDebtorCardNow()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ReadSalesNoteForFinance(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);
            var response = controller.GetSalesNoteFinanceReport(It.IsAny<string>(), 1, 1, It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        
    }
}
