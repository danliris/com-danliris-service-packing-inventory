﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionReport;
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
    [Route("v1/garment-shipping/monitoring/garment-payment-disposition")]
    [Authorize]
    public class GarmentPaymentDispositionReportController : ControllerBase
    {
        private string ApiVersion = "1.0.0";
        private readonly IGarmentPaymentDispositionReportService _service;
        private readonly IIdentityProvider _identityProvider;

        public GarmentPaymentDispositionReportController(IGarmentPaymentDispositionReportService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        [HttpGet]
        public IActionResult GetReport(string paymentType, string unit, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];
            try
            {
                var data = _service.GetReportData(paymentType, unit, dateFrom, dateTo, offset);

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
        public IActionResult GetXls(string paymentType, string unit, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : Convert.ToDateTime(dateFrom);
                DateTime DateTo = dateTo == null ? DateTime.Now : Convert.ToDateTime(dateTo);

                var xls = _service.GenerateExcel(paymentType, unit, dateFrom, dateTo, offset);

                string filename = String.Format("Report Disposisi Pembayaran - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

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
