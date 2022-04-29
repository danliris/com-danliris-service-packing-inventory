﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListControllerGetTest : GarmentPackingListControllerTest
    {
        [Fact]
        public void Get_Ok()
        {
            var serviceMock = new Mock<IGarmentPackingListService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<GarmentPackingListViewModel>(new List<GarmentPackingListViewModel>(), 1, 1, 1));
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
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListService>();
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

		[Fact]
		public void GetPLSampleRO_Ok()
		{
			var serviceMock = new Mock<IGarmentPackingListService>();
			serviceMock
				.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new ListResult<GarmentPackingListViewModel>(new List<GarmentPackingListViewModel>(), 1, 1, 1));
			var service = serviceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var validateServiceMock = new Mock<IValidateService>();
			var validateService = validateServiceMock.Object;

			var controller = GetController(service, identityProvider, validateService);
			var response = controller.GetPLSampleRO();

			Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
		}

		[Fact]
		public void GetPLSampleStyle_Ok()
		{
			var serviceMock = new Mock<IGarmentPackingListService>();
			serviceMock
				.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new ListResult<GarmentPackingListViewModel>(new List<GarmentPackingListViewModel>(), 1, 1, 1));
			var service = serviceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var validateServiceMock = new Mock<IValidateService>();
			var validateService = validateServiceMock.Object;

			var controller = GetController(service, identityProvider, validateService);
			var response = controller.GetPLSampleStyle(It.IsAny<string>());

			Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
		}
	}
}
