using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/dyeing-printing-product")]
    [Authorize]
    public class DyeingPrintingProductController : ControllerBase
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IDyeingPrintingProductService _service;

        public DyeingPrintingProductController(IDyeingPrintingProductService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpGet("packing")]
        public IActionResult Get([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string order = "{}",
            [FromQuery] string filter = "{}")
        {
            try
            {

                var data = _service.GetDataProductPacking(page, size, filter, order, keyword);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpPut("packing/status/{id}")]
        public async Task<IActionResult> UpdatePrintingStatusProductPacking([FromRoute] int id, [FromBody] bool hasProductPacking)
        {
            try
            {
                VerifyUser();
                var result = await _service.UpdatePrintingStatusProductPacking(id, hasProductPacking);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
