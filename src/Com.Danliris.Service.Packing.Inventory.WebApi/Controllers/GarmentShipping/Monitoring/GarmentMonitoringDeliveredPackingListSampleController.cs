using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.Monitoring
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/delivered-packing-list")]
    [Authorize]

    public class GarmentMonitoringDeliveredPackingListSampleController : ControllerBase
    {
        private string ApiVersion = "1.0.0";
        private readonly IGarmentMonitoringDeliveredPackingListSample _service;
        private readonly IIdentityProvider _identityProvider;

        public GarmentMonitoringDeliveredPackingListSampleController(IGarmentMonitoringDeliveredPackingListSample service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        [HttpGet]
        public IActionResult GetReport(string invoiceNo, string paymentTerm, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];
            try
            {
                var data = _service.GetReportData(invoiceNo, paymentTerm, dateFrom, dateTo,offset);

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
        public IActionResult GetXls(string invoiceNo, string paymentTerm, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            try
            {

                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var dateFromString = dateFrom == null ? "" : $" from {(dateFrom.Value.ToString("dd MMM yyyy", new CultureInfo("id-ID")))}";
                var dateToString = dateTo == null ? "" : $" to {(dateTo.Value.ToString("dd MMM yyyy", new CultureInfo("id-ID")))}";
                var invoinceNoo = string.IsNullOrWhiteSpace(invoiceNo) ? "" : $" {invoiceNo}";

                var xls = _service.GenerateExcel(invoiceNo, paymentTerm, dateFrom, dateTo,offset);

                string filename = String.Format("Monitoring Delivered Packing List Sample -{0}-{1} {2}.xlsx", invoinceNoo, dateFromString, dateToString);

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
