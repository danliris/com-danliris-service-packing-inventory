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
    public class GarmentShippingCostStructureControllerDeleteTest : GarmentShippingCostStructureControllerTest
    {
		[Fact]
		public async Task Delete_Ok()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingCostStructureService>();
			serviceMock
				.Setup(s => s.Delete(It.IsAny<int>()))
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

			var response = await controller.Delete(dataUtil.Id);

			Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
		}

		[Fact]
		public async Task Delete_Exception_InternalServerError()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingCostStructureService>();
			serviceMock
				.Setup(s => s.Delete(It.IsAny<int>()))
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
			var response = await controller.Delete(dataUtil.Id);

			Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
		}
	}
}
