using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionControllerPutTest : GarmentShippingInstructionControllerTest
    {
        [Fact]
        public async Task Put_Ok()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentShippingInstructionViewModel>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInstructionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListServiceMock.Object, invoiceServiceMock.Object);

            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_ValidationException_BadRequest()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            var service = serviceMock.Object;
            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInstructionViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListServiceMock.Object, invoiceServiceMock.Object);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentShippingInstructionViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;
            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInstructionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListServiceMock.Object, invoiceServiceMock.Object);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
