using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureControllerGetTest : GarmentShippingCostStructureControllerTest
    {
		[Fact]
		public void Get_Ok()
		{
			var serviceMock = new Mock<IGarmentShippingCostStructureService>();
			serviceMock
				.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new ListResult<GarmentShippingCostStructureViewModel>(new List<GarmentShippingCostStructureViewModel>(), 1, 1, 1));
			var service = serviceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var validateServiceMock = new Mock<IValidateService>();
			var validateService = validateServiceMock.Object;

			var controller = GetController(service, identityProvider, validateService);
			var response = controller.Get();

			Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
		}

		[Fact]
		public void Get_Exception_InternalServerError()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingCostStructureService>();
			serviceMock
				.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Throws(new Exception());
			var service = serviceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var validateServiceMock = new Mock<IValidateService>();
			var validateService = validateServiceMock.Object;

			var controller = GetController(service, identityProvider, validateService);
			var response = controller.Get();

			Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
		}
	}
}
