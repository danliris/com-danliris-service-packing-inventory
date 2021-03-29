using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.GarmentShippingCostStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureControllerTest
    {
		protected GarmentShippingCostStructureController GetController(IGarmentShippingCostStructureService service, IIdentityProvider identityProvider, IValidateService validateService)
		{
			var claimPrincipal = new Mock<ClaimsPrincipal>();
			var claims = new Claim[]
			{
				new Claim("username", "unittestusername")
			};
			claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

			var controller = new GarmentShippingCostStructureController(service, identityProvider, validateService)
			{
				ControllerContext = new ControllerContext()
				{
					HttpContext = new DefaultHttpContext()
					{
						User = claimPrincipal.Object

					}
				}
			};
			controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
			controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";
			controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

			return controller;
		}

		public virtual GarmentShippingCostStructureViewModel ViewModel
		{
			get
			{
				return new GarmentShippingCostStructureViewModel();
			}
		}

		protected ServiceValidationException GetServiceValidationExeption()
		{
			Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
			List<ValidationResult> validationResults = new List<ValidationResult>()
			{
				new ValidationResult("message",new string[1]{ "A" }),
				new ValidationResult("{}",new string[1]{ "B" })
			};
			System.ComponentModel.DataAnnotations.ValidationContext validationContext = new ValidationContext(ViewModel, serviceProvider.Object, null);
			return new ServiceValidationException(validationContext, validationResults);
		}

		protected int GetStatusCode(IActionResult response)
		{
			return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
		}
	}
}
