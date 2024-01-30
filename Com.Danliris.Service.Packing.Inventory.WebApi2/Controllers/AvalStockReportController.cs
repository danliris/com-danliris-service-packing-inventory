using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalStockReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/aval-stock-report")]
    [Authorize]
    public class AvalStockReportController : ControllerBase
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IAvalStockReportService _service;

        public AvalStockReportController(IAvalStockReportService service, IIdentityProvider identityProvider)
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

        [HttpGet]
        public IActionResult Get([FromQuery] DateTimeOffset? searchDate, [FromHeader(Name = "x-timezone-offset")] string timezone)
        {
            try
            {
                VerifyUser();
                if (!searchDate.HasValue)
                {
                    throw new Exception("Tanggal Harus Diisi");
                }
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var data = _service.GetReportData(searchDate.GetValueOrDefault(), clientTimeZoneOffset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet("xls/")]
        public IActionResult GetExcel([FromQuery] DateTimeOffset? searchDate, [FromHeader(Name = "x-timezone-offset")] string timezone)
        {
            try
            {
                VerifyUser();
                if (!searchDate.HasValue)
                {
                    throw new Exception("Tanggal Harus Diisi");
                }
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var Result = _service.GenerateExcel(searchDate.GetValueOrDefault(), clientTimeZoneOffset);
                string filename = $"Stock Aval - {searchDate.GetValueOrDefault().ToString("dd MMMM yyyy")}.xlsx";
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
