using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.QcToWarehouseReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingReport
{
    [Produces("application/json")]
    [Route("v1/dyeing-printing/report/qc-towarehouse-reports")]
    [Authorize]
    public class QcToWarehouseReportController : ControllerBase
    {
        private string ApiVersion = "1.0.0";
        private readonly IQcToWarehouseReportService _service;
        private readonly IIdentityProvider _identityProvider;

        public QcToWarehouseReportController(IQcToWarehouseReportService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        [HttpGet]
        public IActionResult GetReport([FromQuery] string bonNo, [FromQuery] string orderNo, [FromQuery] DateTime startdate, [FromQuery] DateTime finishdate)
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];
            try
            {
                var data = _service.GetReportData(bonNo, orderNo, startdate,finishdate, offset);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data,
                    message = General.OK_MESSAGE,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("download")]
        public IActionResult GetXls([FromQuery] string bonNo, [FromQuery] string orderNo, [FromQuery] DateTime startdate, [FromQuery] DateTime finishdate)
        {
            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var xls = _service.GenerateExcel(bonNo, orderNo, startdate, finishdate, offset);

                string filename = String.Format("Laporan Penyerahan Produksi - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

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
