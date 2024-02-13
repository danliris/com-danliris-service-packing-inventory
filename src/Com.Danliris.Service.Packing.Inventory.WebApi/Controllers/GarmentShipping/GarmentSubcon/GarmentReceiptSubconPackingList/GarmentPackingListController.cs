
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.GarmentReceiptSubconPackingList
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/receipt-subcon-packing-lists")]
    [Authorize]
    public class GarmentPackingListController : ControllerBase
    {
        private readonly IGarmentReceiptSubconPackingListService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public GarmentPackingListController(IGarmentReceiptSubconPackingListService service, IIdentityProvider identityProvider, IValidateService validateService)
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GarmentReceiptSubconPackingListViewModel viewModel)
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
                var Result = new
                {
                    error = ex.Message,
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.InternalServerError,
                    message = ex.Message
                };

                return new BadRequestObjectResult(Result);
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
                    //VerifyUser();
                    //var result = await _service.ReadPdfById(id);

                    //return File(result.Data.ToArray(), "application/pdf", result.FileName);
                }
                else if (accept == "application/xls")
                {
                    //VerifyUser();
                    //var result = await _service.ReadExcelById(id);

                    //return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.FileName);
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
                var Result = new
                {
                    error = ex.Message,
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.InternalServerError,
                    message = ex.Message
                };

                return new BadRequestObjectResult(Result);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] GarmentReceiptSubconPackingListViewModel viewModel)
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
                var Result = new
                {
                    error = ex.Message,
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.InternalServerError,
                    message = ex.Message
                };

                return new BadRequestObjectResult(Result);
            }
        }
        [HttpPut("approve")]
        public async Task<IActionResult> Approve([FromBody] UpdateIsApprovedPackingListViewModel viewModel)
        {
            try
            {
                VerifyUser();
                var result = await _service.UpdateIsApproved(viewModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var Result = new
                {
                    error = ex.Message,
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.InternalServerError,
                    message = ex.Message
                };

                return new BadRequestObjectResult(Result);
            }
        }
        [HttpGet("{id}/carton")]
        public async Task<IActionResult> GetPdfFilterCarton([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.ReadPdfFilterCarton(id);

                return File(result.Data.ToArray(), "application/pdf", result.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
