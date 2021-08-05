using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Application;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.Master
{
    [Produces("application/json")]
    [Route("v1/master/product-skus")]
    [Authorize]
    public class ProductSKUController : ControllerBase
    {
        private readonly IProductSKUService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public ProductSKUController(IServiceProvider serviceProvider)
        {
            _service = serviceProvider.GetService<IProductSKUService>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _validateService = serviceProvider.GetService<IValidateService>();

        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormDto form)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(form);
                var result = await _service.Create(form);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {

                var data = await _service.GetById(id);

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

        [HttpGet]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.Delete(id);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] FormDto form)
        {
            try
            {
                VerifyUser();

                var data = await _service.GetById(id);
                if (data == null)
                    return NotFound();

                _validateService.Validate(form);
                await _service.Update(id, form);

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
