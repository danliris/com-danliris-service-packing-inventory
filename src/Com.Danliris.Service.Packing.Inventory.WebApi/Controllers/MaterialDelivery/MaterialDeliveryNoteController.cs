using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/material-delivery-note")]
    [Authorize]

    public class MaterialDeliveryNoteController : Controller
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IMaterialDeliveryNoteService _service;
        private readonly IValidateService ValidateService;

        public MaterialDeliveryNoteController(IMaterialDeliveryNoteService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            _service = service;
            _identityProvider = identityProvider;
            ValidateService = validateService;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> GetPdfById([FromRoute] int id, [FromHeader(Name = "x-timezone-offset")] string timezone)
        {
            try
            {
                var model = await _service.ReadById(id);
                if (model == null)
                {
                    var Result = new
                    {
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.NotFound,
                        message = "Not Found"
                    };
                    return NotFound(Result);
                }
                else
                {
                    int timeoffsset = Convert.ToInt32(timezone);
                    var pdfTemplate = new MaterialDeliveryNotePdfTemplate(model, timeoffsset);
                    var stream = pdfTemplate.GeneratePdfTemplate();
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = string.Format("Bon Pengiriman Barang Spinning - {0}.pdf", model.Code)
                    };
                }
            }
            catch (Exception e)
            {
                var Result = new
                {
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.InternalServerError,
                    message = e.Message
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, Result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MaterialDeliveryNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var result = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(result);
            }
            try
            {
                VerifyUser();
                ValidateService.Validate(viewModel);
                await _service.Create(viewModel);

                return Created("/", new
                {
                });
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] MaterialDeliveryNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var result = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(result);
            }
            try
            {
                VerifyUser();
                ValidateService.Validate(viewModel);
                await _service.Update(id, viewModel);

                return Created("/", new
                {
                });
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
                await _service.Delete(id);

                return NoContent();
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

        [HttpGet]
        public IActionResult GetByKeyword([FromQuery] string keyword, [FromQuery] string order = "{}", [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string filter = "{}")
        {
            var data = _service.ReadByKeyword(keyword, order, page, size, filter);
            return Ok(data);
        }

    }
}
