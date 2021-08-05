using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceControllerGetShippingInvoicePackingList : GarmentShippingInvoiceControllerTest
    {
        [Fact]
        public async Task GetExportSalesDebtor()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentShippingInvoiceViewModel());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentPackingListViewModel());
            var packingListService = packingListServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            var response = controller.GetExportSalesDebtor(1, 1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        [Fact]
        public async Task GetExportSalesDebtorsNow()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentShippingInvoiceViewModel());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentPackingListViewModel());
            var packingListService = packingListServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            var response = controller.GetExportSalesDebtorNow(1, 1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        [Fact]
        public void Should_Error_GetExportSalesDebtorsNow()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock
                .Setup(s => s.ReadShippingPackingListNow(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentPackingListViewModel());
            var packingListService = packingListServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            var response = controller.GetExportSalesDebtorNow(1, 1);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Error_GetExportSalesDebtors()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock
                .Setup(s => s.ReadShippingPackingList(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentPackingListViewModel());
            var packingListService = packingListServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            var response = controller.GetExportSalesDebtor(1, 1);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}