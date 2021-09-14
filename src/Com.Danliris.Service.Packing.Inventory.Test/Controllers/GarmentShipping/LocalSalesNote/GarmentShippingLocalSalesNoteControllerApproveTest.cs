using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingLocalSalesNote;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.LocalSalesNote
{
    public class GarmentShippingLocalSalesNoteControllerApproveTest : GarmentShippingLocalSalesNoteControllerTest
    {
        [Fact]
        public async Task ApproveFinance_Ok()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ApproveFinance(It.IsAny<int>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;
            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);

            var response = await controller.PutApproveFinance(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task ApproveFinance_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ApproveFinance(It.IsAny<int>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;
            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);
            var response = await controller.PutApproveFinance(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task ApproveShipping_Ok()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ApproveShipping(It.IsAny<int>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;
            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);

            var response = await controller.PutApproveShipping(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task ApproveShipping_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            serviceMock
                .Setup(s => s.ApproveShipping(It.IsAny<int>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;
            var localcoverletterServiceMock = new Mock<IGarmentLocalCoverLetterService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, localcoverletterServiceMock.Object, identityProvider, validateService);
            var response = await controller.PutApproveShipping(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
