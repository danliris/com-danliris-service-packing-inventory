using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/stock-warehouse")]
    [Authorize]
    public class StockWarehouseController : Controller
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IStockWarehouseService _service;

        public StockWarehouseController(IStockWarehouseService service, IIdentityProvider identityProvider)
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
        public IActionResult Get([FromQuery] DateTimeOffset dateReport, [FromQuery] string zona, [FromHeader(Name = "x-timezone-offset")] string timezone,
            [FromQuery] string unit = null, [FromQuery] string packingType = null, [FromQuery] string construction = null, [FromQuery] string buyer = null, [FromQuery] long productionOrderId = 0)
        {
            try
            {
                VerifyUser();
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var data = _service.GetReportData(dateReport, zona, clientTimeZoneOffset, unit, packingType, construction, buyer, productionOrderId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet("xls/")]
        public IActionResult GetExcel([FromQuery] DateTimeOffset dateReport, [FromQuery] string zona, [FromHeader(Name = "x-timezone-offset")] string timezone,
            [FromQuery] string unit = null, [FromQuery] string packingType = null, [FromQuery] string construction = null, [FromQuery] string buyer = null, [FromQuery] long productionOrderId = 0)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var Result = _service.GenerateExcel(dateReport, zona, clientTimeZoneOffset, unit, packingType, construction, buyer, productionOrderId);
                string filename = $"Stock {dateReport.ToString("yyyy MM dd")}.xlsx";
                xlsInBytes = Result.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("packing")]
        public IActionResult GetPackingData([FromQuery] DateTimeOffset dateReport, [FromQuery] string zona, [FromHeader(Name = "x-timezone-offset")] string timezone,
            [FromQuery] string unit, [FromQuery] string packingType, [FromQuery] string construction, [FromQuery] string buyer, [FromQuery] long productionOrderId,
            [FromQuery] string grade)
        {
            try
            {
                VerifyUser();
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var data = _service.GetPackingData(dateReport, zona, clientTimeZoneOffset, unit, packingType, construction, buyer, productionOrderId, grade);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}