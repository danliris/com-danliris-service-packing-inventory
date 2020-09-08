using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionControllerGetest : GarmentShippingInstructionControllerTest
    {
        [Fact]
        public void Get_Ok()
        {
            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<GarmentShippingInstructionViewModel>(new List<GarmentShippingInstructionViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;
            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListServiceMock.Object, invoiceServiceMock.Object);
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            var service = serviceMock.Object;
            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService, packingListServiceMock.Object, invoiceServiceMock.Object);
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
