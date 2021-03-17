using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureControllerPutTest : GarmentShippingCostStructureControllerTest
    {
		[Fact]
		public async Task Put_Ok()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingCostStructureService>();
			serviceMock
				.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentShippingCostStructureViewModel>()))
				.ReturnsAsync(1);
			var service = serviceMock.Object;

			var validateServiceMock = new Mock<IValidateService>();
			validateServiceMock
				.Setup(s => s.Validate(It.IsAny<GarmentShippingCostStructureViewModel>()))
				.Verifiable();
			var validateService = validateServiceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var controller = GetController(service, identityProvider, validateService);

			var response = await controller.Put(dataUtil.Id, dataUtil);

			Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
		}

		[Fact]
		public async Task Put_ValidationException_BadRequest()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingCostStructureService>();
			var service = serviceMock.Object;


			var validateServiceMock = new Mock<IValidateService>();
			validateServiceMock
				.Setup(s => s.Validate(It.IsAny<GarmentShippingCostStructureViewModel>()))
				.Throws(GetServiceValidationExeption());
			var validateService = validateServiceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var controller = GetController(service, identityProvider, validateService);
			var response = await controller.Put(dataUtil.Id, dataUtil);

			Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
		}

		[Fact]
		public async Task Put_Exception_InternalServerError()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingCostStructureService>();
			serviceMock
				.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentShippingCostStructureViewModel>()))
				.ThrowsAsync(new Exception());
			var service = serviceMock.Object;

			var validateServiceMock = new Mock<IValidateService>();
			validateServiceMock
				.Setup(s => s.Validate(It.IsAny<GarmentShippingCostStructureViewModel>()))
				.Verifiable();
			var validateService = validateServiceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var controller = GetController(service, identityProvider, validateService);
			var response = await controller.Put(dataUtil.Id, dataUtil);

			Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
		}
	}
}
