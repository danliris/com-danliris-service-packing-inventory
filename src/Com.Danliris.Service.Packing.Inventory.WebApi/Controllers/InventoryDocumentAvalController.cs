using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentAval;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/inventory-document-aval")]
    [Authorize]
    public class InventoryDocumentAvalController : ControllerBase
    {
        private readonly IInventoryDocumentAvalService _service;
        private readonly IIdentityProvider _identityProvider;

        public InventoryDocumentAvalController(IInventoryDocumentAvalService service, IIdentityProvider identityProvider)
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

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromRoute] int id, [FromBody] InventoryDocumentAvalViewModel viewModel)
        {
            VerifyUser();
            if (!ModelState.IsValid)
            {
                var exception = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(exception);
            }

            try
            {
                await _service.Create(id, viewModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put([FromRoute] int id, [FromBody] InventoryDocumentAvalViewModel viewModel)
        //{
        //    VerifyUser();
        //    if (!ModelState.IsValid)
        //    {
        //        var exception = new
        //        {
        //            error = ResultFormatter.FormatErrorMessage(ModelState)
        //        };
        //        return new BadRequestObjectResult(exception);
        //    }

        //    try
        //    {
        //        await _service.Update(id, viewModel);

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

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
        public IActionResult Get([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}",
            [FromQuery] string filter = "{}")
        {
            try
            {

                var data = _service.Read(page, size, filter, order, keyword);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }
    }
}
