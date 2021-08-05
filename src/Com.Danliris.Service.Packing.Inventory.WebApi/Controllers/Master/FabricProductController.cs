using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.Master
{
    [Produces("application/json")]
    [Route("v1/master/fabric-products")]
    [Authorize]
    public class FabricProductController : Controller
    {
        private readonly IFabricPackingSKUService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public FabricProductController(IServiceProvider serviceProvider)
        {
            _service = serviceProvider.GetService<IFabricPackingSKUService>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _validateService = serviceProvider.GetService<IValidateService>();

        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpPost("sku")]
        public IActionResult Post([FromBody] FabricSKUFormDto form)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(form);
                var result = _service.CreateSKU(form);

                return Created(HttpContext.Request.Path, result);
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

        [HttpGet("sku/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {

                var data = _service.GetById(id);

                if (data == null)
                    return NotFound();

                return Ok(new
                {
                    data,
                    statusCode = HttpStatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("sku")]
        public async Task<IActionResult> Get([FromQuery] IndexQueryParam queryParam)
        {
            try
            {
                var data = await _service.GetIndex(queryParam);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpDelete("sku/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = _service.DeleteSKU(id);
                if (result <= 0)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                var error = new
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    error = ex.Message
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }
        }

        [HttpPut("sku/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] FabricSKUFormDto form)
        {
            try
            {
                VerifyUser();

                var data = _service.GetById(id);
                if (data == null)
                    return NotFound();

                _validateService.Validate(form);
                _service.UpdateSKU(id, form);

                return NoContent();
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
                var error = new
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    error = ex.Message
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }
        }
    }
}
