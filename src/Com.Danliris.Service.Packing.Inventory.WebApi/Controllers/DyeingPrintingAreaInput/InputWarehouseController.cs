using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Reject;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaInput
{
    [Produces("application/json")]
    [Route("v1/input-warehouse")]
    [Authorize]
    public class InputWarehouseController : ControllerBase
    {
        private readonly IInputWarehouseService _service;
        private readonly IIdentityProvider _identityProvider;

        public InputWarehouseController(IInputWarehouseService service, IIdentityProvider identityProvider)
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
        public async Task<IActionResult> Post([FromBody] InputWarehouseCreateViewModel viewModel)
        {
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
                VerifyUser();
                var result = await _service.Create(viewModel);

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
        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody]InputWarehouseCreateViewModel viewModel)
        {
            try
            {
                VerifyUser();
                var data = await _service.Update(id,viewModel);
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
                var data = await _service.Delete(id);
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

        [HttpGet("output-production-orders")]
        public IActionResult GetProductionOrders()
        {
            try
            {

                var data = _service.GetOutputPreWarehouseProductionOrders();
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

        [HttpPost("reject")]
        public async Task<IActionResult> Reject([FromBody] RejectedInputWarehouseViewModel viewModel)
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
                var result = await _service.Reject(viewModel);

                return Created("/", result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet("xls")]
        public IActionResult GetExcelAll([FromHeader(Name = "x-timezone-offset")] string timezone, [FromQuery] DateTimeOffset? dateFrom = null, [FromQuery] DateTimeOffset? dateTo = null)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var Result = _service.GenerateExcelAll(dateFrom, dateTo, clientTimeZoneOffset);
                string filename = "Penerimaan Gudang Jadi Dyeing/Printing.xlsx";
                xlsInBytes = Result.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}