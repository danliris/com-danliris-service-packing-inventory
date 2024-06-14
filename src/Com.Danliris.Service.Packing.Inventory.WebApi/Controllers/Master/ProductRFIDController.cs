using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductRFID;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application;
using System.Net;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.Master
{
    [Produces("application/json")]
    [Route("v1/master/product-rfids")]
    [Authorize]
    public class ProductRFIDController : ControllerBase
    {
        private readonly IProductRFIDService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public ProductRFIDController(IServiceProvider serviceProvider)
        {
            _service = serviceProvider.GetService<IProductRFIDService>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _validateService = serviceProvider.GetService<IValidateService>();

        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
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
    }
}
