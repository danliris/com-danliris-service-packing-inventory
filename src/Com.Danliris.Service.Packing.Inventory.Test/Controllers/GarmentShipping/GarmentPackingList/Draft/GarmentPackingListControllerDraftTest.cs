using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.GarmentPackingList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentPackingList.Draft
{
    public class GarmentPackingListControllerDraftTest
    {
        protected GarmentPackingListDraftController GetController(IGarmentPackingListDraftService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GarmentPackingListDraftController(service, identityProvider, validateService)
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

        protected virtual GarmentPackingListDraftViewModel GetViewModel()
        {
            return new GarmentPackingListDraftViewModel() {
                Items = new List<GarmentPackingListItemViewModel>
                {
                    new GarmentPackingListItemViewModel()
                }
            };
        }

        protected ServiceValidationException GetServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>()
            {
                new ValidationResult("message",new string[1]{ "A" }),
                new ValidationResult("{}",new string[1]{ "B" })
            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(GetViewModel(), serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public async Task Post_Created()
        {
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<GarmentPackingListDraftViewModel>()))
                .ReturnsAsync("InvoiceNo");
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_ValidationException_BadRequest()
        {
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<GarmentPackingListViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_Ok()
        {
            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentPackingListViewModel());
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
        public async Task GetPdfById_Ok()
        {
            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.ReadPdfById(It.IsAny<int>()))
                .ReturnsAsync(new MemoryStreamResult(new MemoryStream(), "FileName.pdf"));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/pdf";
            var response = await controller.GetById(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetPdfFilterCarton_Ok()
        {
            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.ReadPdfFilterCarton(It.IsAny<int>()))
                .ReturnsAsync(new MemoryStreamResult(new MemoryStream(), "FileName.pdf"));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/pdf";
            var response = await controller.GetPdfFilterCarton(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetPdfFilterCartonMD_Ok()
        {
            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.ReadPdfFilterCartonMD(It.IsAny<int>()))
                .ReturnsAsync(new MemoryStreamResult(new MemoryStream(), "FileName.pdf"));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/pdf";
            var response = await controller.GetPdfFilterCartonMD(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetById_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
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

        [Fact]
        public async Task GetXlsById_Ok()
        {
            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.ReadExcelById(It.IsAny<int>()))
                .ReturnsAsync(new MemoryStreamResult(new MemoryStream(), "FileName.xls"));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";
            var response = await controller.GetById(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Put_Ok()
        {
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentPackingListDraftViewModel>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftViewModel>()))
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
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftViewModel>()))
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
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<GarmentPackingListDraftViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        protected GarmentPackingListDraftController GetControllerSetStatus(bool error = false)
        {
            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            if (!error)
            {
                serviceMock
                    .Setup(s => s.SetStatus(It.IsAny<int>(), It.IsAny<GarmentPackingListStatusEnum>(), It.IsAny<string>()))
                    .Verifiable();
            }
            else
            {
                serviceMock
                    .Setup(s => s.SetStatus(It.IsAny<int>(), It.IsAny<GarmentPackingListStatusEnum>(), It.IsAny<string>()))
                    .Throws(new Exception());
            }
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            return GetController(service, identityProvider, validateService);
        }

        [Fact]
        public async Task PostBooking_Ok()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus();

            var response = await controller.PostBooking(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task PostBooking_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.PostBooking(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task UnpostBooking_Ok()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus();

            var response = await controller.UnpostBooking(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task UnpostBooking_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.UnpostBooking(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Cancel_Ok()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus();

            var response = await controller.SetCancel(dataUtil.Id, "Alasan");

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Cancel_Exception_BadRequest()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.SetCancel(dataUtil.Id, It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Cancel_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.SetCancel(dataUtil.Id, "Alasan");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task ApproveMd_Ok()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus();

            var response = await controller.SetApproveMd(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task ApproveMd_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.SetApproveMd(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task RejectMd_Ok()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus();

            var response = await controller.SetRejectMd(dataUtil.Id, "Alasan");

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task RejectMd_Exception_BadRequest()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.SetRejectMd(dataUtil.Id, It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task RejectMd_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.SetRejectMd(dataUtil.Id, "Alasan");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task ApproveShipping_Ok()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus();

            var response = await controller.SetApproveShipping(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task ApproveShipping_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.SetApproveShipping(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task RejectShipping_Ok()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus();

            var response = await controller.SetRejectShipping(dataUtil.Id, "Alasan");

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task RejectShipping_Exception_BadRequest()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.SetRejectShipping(dataUtil.Id, It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task RejectShipping_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.SetRejectShipping(dataUtil.Id, "Alasan");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task PostPackingList_Ok()
        {
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(dataUtil);
            serviceMock
                .Setup(s => s.SetStatus(It.IsAny<int>(), It.IsAny<GarmentPackingListStatusEnum>(), It.IsAny<string>()))
                .Verifiable();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.PostPackingList(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task PostPackingList_ValidationException_BadRequest()
        {
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(dataUtil);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.PostPackingList(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task PostPackingList_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.PostPackingList(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task UnpostPackingList_Ok()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus();

            var response = await controller.UnpostPackingList(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task UnpostPackingList_Exception_InternalServerError()
        {
            var dataUtil = GetViewModel();

            var controller = GetControllerSetStatus(true);

            var response = await controller.UnpostPackingList(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        //COPY

        protected virtual GarmentPackingListDraftCopyViewModel GetCopyViewModel()
        {
            return new GarmentPackingListDraftCopyViewModel()
            {
                Items = new List<GarmentPackingListItemViewModel>
                {
                    new GarmentPackingListItemViewModel
                        {
                            Id = 1,
                            Details = new List<GarmentPackingListDetailViewModel>()
                            {
                                new GarmentPackingListDetailViewModel
                                {
                                    Id = 1,
                                    NetWeight = 10,
                                    NetNetWeight = 0,
                                    Sizes = new List<GarmentPackingListDetailSizeViewModel>()
                                    {
                                        new GarmentPackingListDetailSizeViewModel()
                                        {
                                            Id = 1
                                        }
                                    }
                                },
                                new GarmentPackingListDetailViewModel
                                {
                                    Id = 2,
                                    NetWeight = 10,
                                    NetNetWeight = 10,
                                    Sizes = new List<GarmentPackingListDetailSizeViewModel>()
                                    {
                                        new GarmentPackingListDetailSizeViewModel()
                                        {
                                            Id = 2
                                        }
                                    }
                                },
                            }
                        }
                }
            };
        }
        [Fact]
        public async Task Post_Created_Copy()
        {
            var dataUtil = GetCopyViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<GarmentPackingListDraftCopyViewModel>()))
                .ReturnsAsync("InvoiceNo");
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftCopyViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = await controller.PostCopy(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task PostCopy_ValidationException_BadRequest()
        {
            var dataUtil = GetCopyViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftCopyViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.PostCopy(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task PostCopy_Exception_InternalServerError()
        {
            var dataUtil = GetCopyViewModel();

            var serviceMock = new Mock<IGarmentPackingListDraftService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<GarmentPackingListViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentPackingListDraftCopyViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.PostCopy(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
