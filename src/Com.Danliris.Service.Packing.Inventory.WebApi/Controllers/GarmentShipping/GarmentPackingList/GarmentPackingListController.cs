﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.GarmentPackingList
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/packing-lists")]
    [Authorize]
    public class GarmentPackingListController : ControllerBase
    {
        private readonly IGarmentPackingListService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public GarmentPackingListController(IGarmentPackingListService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            _service = service;
            _identityProvider = identityProvider;
            _validateService = validateService;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GarmentPackingListViewModel viewModel)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(viewModel);
                var result = await _service.Create(viewModel);

                return Created("/", new { data = result });
            }
            catch (ServiceValidationException ex)
            {
                var Result = new
                {
                    error = ResultFormatter.Fail(ex),
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data does not pass validation"
                };

                return new BadRequestObjectResult(Result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
		[HttpGet("loaderSample")]
		public IActionResult GetPLSample([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}", [FromQuery] string filter = "{}")
		{
			try
			{
				var data = _service.ReadPLSample(page, size, filter, order, keyword);
				var info = new Dictionary<string, object>
					{
						{ "count", data.Data.Count },
						{ "total", data.Total },
						{ "order", order },
						{ "page", page },
						{ "size", size }
					};

				return Ok(new
				{
					data = data.Data,
					info
				});


			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
		[HttpGet("loaderSampleRO")]
		public IActionResult GetPLSampleRO([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}", [FromQuery] string filter = "{}")
		{
			try
			{
				var data = _service.ReadPLSampleRO( page,  size,  filter,  order,  keyword);

				return Ok(new
				{
					data = data
				});


			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
		[HttpGet("loaderSampleStyle/{roNo}")]
		public IActionResult GetPLSampleStyle(string roNo)
		{
			try
			{
				var data = _service.ReadPLSampleStyle(roNo);

				return Ok(new
				{
					data = data
				});


			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
		[HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var accept = Request.Headers["Accept"];
                if (accept == "application/pdf")
                {
                    VerifyUser();
                    var result = await _service.ReadPdfById(id);

                    return File(result.Data.ToArray(), "application/pdf", result.FileName);
                }
                else if (accept == "application/xls")
                {
                    VerifyUser();
                    var result = await _service.ReadExcelById(id);

                    return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.FileName);
                }

                var data = await _service.ReadById(id);

                return Ok(new
                {
                    data
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/wh")]
        public async Task<IActionResult> GetWHById([FromRoute] int id)
        {
            try
            {
                var accept = Request.Headers["Accept"];
                if (accept == "application/pdf")
                {
                    VerifyUser();
                    var result = await _service.ReadWHPdfById(id);

                    return File(result.Data.ToArray(), "application/pdf", result.FileName);
                }
                else if (accept == "application/xls")
                {
                    VerifyUser();
                    var result = await _service.ReadExcelById(id);

                    return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.FileName);
                }

                var data = await _service.ReadById(id);

                return Ok(new
                {
                    data
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/wh-section-d")]
        public async Task<IActionResult> GetWHByIdSectionD([FromRoute] int id)
        {
            try
            {
                var accept = Request.Headers["Accept"];
                //if (accept == "application/pdf")
                {
                    VerifyUser();
                    var result = await _service.ReadWHSectionDPdfById(id);

                    return File(result.Data.ToArray(), "application/pdf", result.FileName);
                }
                //else if (accept == "application/xls")
                //{
                //    VerifyUser();
                //    var result = await _service.ReadExcelById(id);

                //    return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.FileName);
                //}

                //var data = await _service.ReadById(id);

                //return Ok(new
                //{
                //    data
                //});
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/order-no")]
        public async Task<IActionResult> GetByOrderNo([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.ReadPdfByOrderNo(id);

                return File(result.Data.ToArray(), "application/pdf", result.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/order-no-wh")]
        public async Task<IActionResult> GetByOrderNoWH([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.ReadWHPdfByOrderNo(id);

                return File(result.Data.ToArray(), "application/pdf", result.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/order-no-wh-section-d")]
        public async Task<IActionResult> GetByOrderNoWHSectionD([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.ReadWHSectionDPdfByOrderNo(id);

                return File(result.Data.ToArray(), "application/pdf", result.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("not-used")]
        public IActionResult GetNotUsed([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string order = "{}", [FromQuery] string filter = "{}")
        {
            try
            {
                var data = _service.ReadNotUsed(page, size, filter, order, keyword);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Data.Count },
                        { "total", data.Total },
                        { "order", order },
                        { "page", page },
                        { "size", size }
                    };

                return Ok(new
                {
                    data = data.Data,
                    info
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("not-used-cost-structure")]
        public IActionResult GetNotUsedCostStructure([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string order = "{}", [FromQuery] string filter = "{}")
        {
            try
            {
                var data = _service.ReadNotUsedCostStructure(page, size, filter, order, keyword);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Data.Count },
                        { "total", data.Total },
                        { "order", order },
                        { "page", page },
                        { "size", size }
                    };

                return Ok(new
                {
                    data = data.Data,
                    info
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("delivered-sample")]
        public IActionResult GetDeliveredSample([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string order = "{}", [FromQuery] string filter = "{}", string select = "{}")
        {
            try
            {
                var data = _service.ReadSampleDelivered(page, size, filter, order, keyword, select);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Data.Count },
                        { "total", data.Total },
                        { "order", order },
                        { "page", page },
                        { "size", size }
                    };

                return Ok(new
                {
                    data = data.Data,
                    info
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string order = "{}", [FromQuery] string filter = "{}")
        {
            try
            {
                var data = _service.Read(page, size, filter, order, keyword);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Data.Count },
                        { "total", data.Total },
                        { "order", order },
                        { "page", page },
                        { "size", size }
                    };

                return Ok(new
                {
                    data = data.Data,
                    info
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] GarmentPackingListViewModel viewModel)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(viewModel);
                var result = await _service.Update(id, viewModel);

                return Ok(result);
            }
            catch (ServiceValidationException ex)
            {
                var Result = new
                {
                    error = ResultFormatter.Fail(ex),
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data does not pass validation"
                };

                return new BadRequestObjectResult(Result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

		

		[HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.Delete(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("post")]
        public async Task<IActionResult> SetPost([FromBody] List<int> ids)
        {
            try
            {
                VerifyUser();
                await _service.SetPost(ids);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("unpost/{id}")]
        public async Task<IActionResult> SetUnpost([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                await _service.SetUnpost(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
		[HttpPut("unpostDelivered/{id}")]
		public async Task<IActionResult> SetUnpostDelivered([FromRoute] int id)
		{
			try
			{
				VerifyUser();
				await _service.SetUnpostDelivered(id);

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}

		}
		[HttpPut("cancel/{id}")]
        public async Task<IActionResult> SetCancel([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();
                if (string.IsNullOrWhiteSpace(reason) || string.IsNullOrEmpty(reason))
                {
                    var Result = new
                    {
                        error = "Alasan harus diisi.",
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Data does not pass validation"
                    };

                    return new BadRequestObjectResult(Result);
                }
                await _service.SetStatus(id, GarmentPackingListStatusEnum.CANCELED, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("reject-md/{id}")]
        public async Task<IActionResult> SetRejectMd([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();

                if (string.IsNullOrWhiteSpace(reason) || string.IsNullOrEmpty(reason))
                {
                    var Result = new
                    {
                        error = "Alasan harus diisi.",
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Data does not pass validation"
                    };

                    return new BadRequestObjectResult(Result);
                }

                await _service.SetStatus(id, GarmentPackingListStatusEnum.REJECTED_MD, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("approve-md/{id}")]
        public async Task<IActionResult> ApproveMd([FromRoute] int id, [FromBody] GarmentPackingListMerchandiserViewModel viewModel)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(viewModel);
                await _service.SetApproveMd(id, viewModel);

                return Ok();
            }
            catch (ServiceValidationException ex)
            {
                var Result = new
                {
                    error = ResultFormatter.Fail(ex),
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data does not pass validation"
                };

                return new BadRequestObjectResult(Result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("revise-md/{id}")]
        public async Task<IActionResult> SetRevisedMd([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();

                if (string.IsNullOrWhiteSpace(reason) || string.IsNullOrEmpty(reason))
                {
                    var Result = new
                    {
                        error = "Alasan harus diisi.",
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Data does not pass validation"
                    };

                    return new BadRequestObjectResult(Result);
                }

                await _service.SetStatus(id, GarmentPackingListStatusEnum.REVISED_MD, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("approve-shipping/{id}")]
        public async Task<IActionResult> ApproveShipping([FromRoute] int id, [FromBody] GarmentPackingListShippingViewModel viewModel)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(viewModel);
                await _service.SetApproveShipping(id, viewModel);

                return Ok();
            }
            catch (ServiceValidationException ex)
            {
                var Result = new
                {
                    error = ResultFormatter.Fail(ex),
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data does not pass validation"
                };

                return new BadRequestObjectResult(Result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
		 
		[HttpPut("revise-to-md/{id}")]
		public async Task<IActionResult> SetRevisedToMd([FromRoute] int id, [FromBody] string reason)
		{
			try
			{
				VerifyUser();

				if (string.IsNullOrWhiteSpace(reason) || string.IsNullOrEmpty(reason))
				{
					var Result = new
					{
						error = "Alasan harus diisi.",
						apiVersion = "1.0.0",
						statusCode = HttpStatusCode.BadRequest,
						message = "Data does not pass validation"
					};

					return new BadRequestObjectResult(Result);
				}

				await _service.SetStatus(id, GarmentPackingListStatusEnum.REVISED_TO_MD, reason);

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}

		}

		[HttpPut("reject-shipping-unit/{id}")]
        public async Task<IActionResult> SetRejectShippingToUnit([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();

                if (string.IsNullOrWhiteSpace(reason) || string.IsNullOrEmpty(reason))
                {
                    var Result = new
                    {
                        error = "Alasan harus diisi.",
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Data does not pass validation"
                    };

                    return new BadRequestObjectResult(Result);
                }

                await _service.SetStatus(id, GarmentPackingListStatusEnum.REJECTED_SHIPPING_UNIT, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("reject-shipping-md/{id}")]
        public async Task<IActionResult> SetRejectShippingToMd([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();

                if (string.IsNullOrWhiteSpace(reason) || string.IsNullOrEmpty(reason))
                {
                    var Result = new
                    {
                        error = "Alasan harus diisi.",
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Data does not pass validation"
                    };

                    return new BadRequestObjectResult(Result);
                }

                await _service.SetStatus(id, GarmentPackingListStatusEnum.REJECTED_SHIPPING_MD, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("revise-shipping/{id}")]
        public async Task<IActionResult> SetRevisedShipping([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();

                if (string.IsNullOrWhiteSpace(reason) || string.IsNullOrEmpty(reason))
                {
                    var Result = new
                    {
                        error = "Alasan harus diisi.",
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Data does not pass validation"
                    };

                    return new BadRequestObjectResult(Result);
                }

                await _service.SetStatus(id, GarmentPackingListStatusEnum.REVISED_SHIPPING, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("setIsGarmentSampleExpenditureGood")]
        public async Task<IActionResult> SetIsGarmentSampleExpenditureGood([FromBody] GarmentPackingListShippingViewModel viewModel)
        {
            try
            {
                VerifyUser();
                await _service.SetSampleExpenditureGood(viewModel.InvoiceNo, viewModel.IsSampleExpenditureGood);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("delivered")]
        public async Task<IActionResult> DeliveredSample([FromBody] List<int> ids)
        {
            try
            {
                VerifyUser();
                await _service.SetSampleDelivered(ids);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
