using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByMarketing;
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

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.Monitoring
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/monitoring/garment-omzet-by-marketing")]
    [Authorize]
    public class GarmenOmzetMonthlyByMarketingController : ControllerBase
    {
        private string ApiVersion = "1.0.0";
        private readonly IGarmentOmzetMonthlyByMarketingService _service;
        private readonly IIdentityProvider _identityProvider;

        public GarmenOmzetMonthlyByMarketingController(IGarmentOmzetMonthlyByMarketingService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        [HttpGet]
        public IActionResult GetReport(string marketingName, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order = "{}")
         {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];
            try
            {
                var data = _service.GetReportData(marketingName, dateFrom, dateTo, offset);

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
        public IActionResult GetXls(string marketingName, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
          
                var xls = _service.GenerateExcel(marketingName, dateFrom, dateTo, offset);

                string filename = String.Format("Garment Omzet Per Marketing - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

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
