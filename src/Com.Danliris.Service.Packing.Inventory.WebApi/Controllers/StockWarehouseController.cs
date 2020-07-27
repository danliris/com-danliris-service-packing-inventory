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
        public IActionResult Get([FromQuery] DateTimeOffset dateFrom, [FromQuery] DateTimeOffset dateTo, [FromQuery] string zona, [FromHeader(Name = "x-timezone-offset")] string timezone,
            [FromQuery] string unit = null, [FromQuery] string packingType = null, [FromQuery] string construction = null, [FromQuery] string buyer = null, [FromQuery] long productionOrderId = 0)
        {
            try
            {
                VerifyUser();
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var data = _service.GetReportData(dateFrom, dateTo, zona, clientTimeZoneOffset, unit, packingType, construction, buyer, productionOrderId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet("xls/")]
        public IActionResult GetExcel([FromQuery] DateTimeOffset dateFrom, [FromQuery] DateTimeOffset dateTo, [FromQuery] string zona, [FromHeader(Name = "x-timezone-offset")] string timezone,
            [FromQuery] string unit = null, [FromQuery] string packingType = null, [FromQuery] string construction = null, [FromQuery] string buyer = null, [FromQuery] long productionOrderId = 0)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var Result = _service.GenerateExcel(dateFrom, dateTo, zona, clientTimeZoneOffset, unit, packingType, construction, buyer, productionOrderId);
                string filename = $"Stock {dateFrom.ToString("yyyy MM dd")} - {dateTo.ToString("yyyy MM dd")}.xlsx";
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