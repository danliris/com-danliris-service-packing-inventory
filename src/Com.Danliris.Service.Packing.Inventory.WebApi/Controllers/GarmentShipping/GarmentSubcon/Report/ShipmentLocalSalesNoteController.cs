using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.ShipmentLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.GarmentSubcon.Report
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/receipt-subcon/shipment-local-sales-note-report")]
    [Authorize]
    public class ShipmentLocalSalesNoteController : ControllerBase
    {
        private readonly IShipmentLocalSalesNoteService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public ShipmentLocalSalesNoteController(IShipmentLocalSalesNoteService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            _service = service;
            _identityProvider = identityProvider;
            _validateService = validateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime dateFrom,[FromQuery] DateTime dateTo)
        {
            try
            {
                _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var data = await _service.GetReportQuery(dateFrom,dateTo);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Count },
                    };

                return Ok(new
                {
                    data = data,
                    info
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> GetXls([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            try
            {
                _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                byte[] xlsInBytes;

                var xls = await _service.GetExcel(dateFrom, dateTo);

                string filename = String.Format("Laporan Shipement Per Nota Jual Lokal - {0} - {1}.xlsx", dateFrom.ToString("dd-MMM-yyyy"), dateTo.ToString("dd-MMM-yyyy"));

                xlsInBytes = xls.ToArray();
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
