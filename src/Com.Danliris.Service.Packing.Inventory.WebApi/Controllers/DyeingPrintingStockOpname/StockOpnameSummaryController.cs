using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingStockOpname
{
    [Produces("application/json")]
    [Route("v1/stock-opname-summary")]
    [Authorize]
    public class StockOpnameSummaryController : Controller
    {
        private readonly IStockOpnameSummaryService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService ValidateService;
        public StockOpnameSummaryController(IStockOpnameSummaryService service, IIdentityProvider identityProvider, IValidateService validateService)
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

        [HttpGet("update-track")]
        public IActionResult GetUpdateTrack([FromQuery] int productionOrderId, [FromQuery] string barcode, [FromQuery] int trackId)
        {
            try
            {
                VerifyUser();
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var data = _service.GetDataUpdateTrack(productionOrderId, barcode, trackId);
                return Ok(new
                {
                    //apiVersion = ApiVersion,
                    data = data,
                    info = new { count = data.Count(), total = data.Count() },

                    message = General.OK_MESSAGE,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdUpdateTrack([FromRoute] int id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] StockOpnameTrackViewModel viewModel)
        {
            VerifyUser();
            //if (!ModelState.IsValid)
            //{
            //    var exception = new
            //    {
            //        error = ResultFormatter.FormatErrorMessage(ModelState)
            //    };
            //    return new BadRequestObjectResult(exception);
            //}

            try
            {
                VerifyUser();
                //ValidateService.Validate(viewModel);
                await _service.Update(id, viewModel);

                return NoContent();
            }
            //catch (ServiceValidationException ex)
            //{
            //    var Result = new
            //    {
            //        error = ResultFormatter.Fail(ex),
            //        apiVersion = "1.0.0",
            //        statusCode = HttpStatusCode.BadRequest,
            //        message = "Data does not pass validation"
            //    };

            //    return new BadRequestObjectResult(Result);
            //}
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

        [HttpGet("download")]
        public IActionResult GetXls([FromQuery] int productionOrderId, [FromQuery] string barcode, [FromQuery] int trackId)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GenerateExcelMonitoring( productionOrderId, barcode, trackId);
                string filename = $"Monitoring Jalur/Rak.xlsx";
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
