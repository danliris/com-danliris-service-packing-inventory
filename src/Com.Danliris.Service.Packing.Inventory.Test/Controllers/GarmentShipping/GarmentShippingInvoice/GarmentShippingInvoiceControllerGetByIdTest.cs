using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceControllerGetByIdTest : GarmentShippingInvoiceControllerTest
	{
		[Fact]
		public async Task GetById_Ok()
		{
			var serviceMock = new Mock<IGarmentShippingInvoiceService>();
			serviceMock
				.Setup(s => s.ReadById(It.IsAny<int>()))
				.ReturnsAsync(new GarmentShippingInvoiceViewModel());
			var service = serviceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var validateServiceMock = new Mock<IValidateService>();
			var validateService = validateServiceMock.Object;

			var controller = GetController(service, identityProvider, validateService);
			var response = await controller.GetById(1);

			Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
		}

		[Fact]
		public async Task GetById_Exception_InternalServerError()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingInvoiceService>();
			serviceMock
				.Setup(s => s.ReadById(It.IsAny<int>()))
				.Throws(new Exception());
			var service = serviceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var validateServiceMock = new Mock<IValidateService>();
			var validateService = validateServiceMock.Object;

			var controller = GetController(service, identityProvider, validateService);
			var response = await controller.GetById(1);

			Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
		}
	}
}