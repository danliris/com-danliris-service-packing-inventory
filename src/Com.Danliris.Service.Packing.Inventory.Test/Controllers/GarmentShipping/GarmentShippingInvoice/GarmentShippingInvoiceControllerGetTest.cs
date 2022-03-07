using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceControllerGetTest : GarmentShippingInvoiceControllerTest
	{
		[Fact]
		public void Get_Ok()
		{
			var serviceMock = new Mock<IGarmentShippingInvoiceService>();
			serviceMock
				.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new ListResult<GarmentShippingInvoiceViewModel>(new List<GarmentShippingInvoiceViewModel>(), 1, 1, 1));
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
			var response = controller.Get();

			Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
		}

		[Fact]
		public void Get_Exception_InternalServerError()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingInvoiceService>();
			serviceMock
				.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
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
			var response = controller.Get();

			Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
		}

		[Fact]
		public void GetShippingInvoiceByPackingListId()
		{
			var serviceMock = new Mock<IGarmentShippingInvoiceService>();
			serviceMock
				.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new ListResult<GarmentShippingInvoiceViewModel>(new List<GarmentShippingInvoiceViewModel>(), 1, 1, 1));
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
			var response = controller.GetShippingInvoiceByPLId(1);

			Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
		}
	}
}
