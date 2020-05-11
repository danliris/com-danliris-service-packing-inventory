using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouses;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaInput
{
    [Produces("application/json")]
    [Route("v1/input-warehouses")]
    [Authorize]
    public class InputWarehousesController : ControllerBase
    {
        private readonly IInputWarehousesService _service;
        private readonly IIdentityProvider _identityProvider;

        public InputWarehousesController(IInputWarehousesService service, IIdentityProvider identityProvider)
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputWarehousesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var excpetion = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(excpetion);
            }
            try
            {
                VerifyUser();
                var result = await _service.CreateAsync(viewModel);

                return Created("/", result);
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

                var data = await _service.ReadByIdAsync(id);
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


        //[HttpGet("production-orders")]
        //public IActionResult GetProductionOrders([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}",
        //    [FromQuery] string filter = "{}")
        //{
        //    try
        //    {

        //        var data = _service.ReadProductionOrders(page, size, filter, order, keyword);
        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

        //    }
        //}

        [HttpGet("list-bon-in")]
        public IActionResult GetBonInPacking([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}",
            [FromQuery] string filter = "{}")
        {
            try
            {

                var data = _service.ReadBonOutToPack(page, size, filter, order, keyword);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpGet("pre-warehouse")]
        public IActionResult GetPreWarehouse([FromQuery] DateTimeOffset searchDate,
                                             [FromQuery] string searchShift,
                                             [FromQuery] string searchGroup,
                                             [FromQuery] string keyword = null,
                                             [FromQuery] int page = 1,
                                             [FromQuery] int size = 25,
                                             [FromQuery] string order = "{}",
                                             [FromQuery] string filter = "{}")
        {
            var data = _service.ReadOutputPreWarehouse(searchDate, searchShift, searchGroup, page, size, filter, order, keyword);
            if (data == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else
            {
                return Ok(data);
            }
        }
    }
}