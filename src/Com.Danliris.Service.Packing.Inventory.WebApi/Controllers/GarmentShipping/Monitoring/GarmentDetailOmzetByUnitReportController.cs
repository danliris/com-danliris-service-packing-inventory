using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCMTSalesReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDetailOmzetByUnitReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.Monitoring
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/report/detail-omzet-by-unit")]
    [Authorize]
    public class GarmentDetailOmzetByUnitReportController : ControllerBase
    {
        //private string ApiVersion = "1.0.0";
        private readonly IGarmentDetailOmzetByUnitReportService _service;
        private readonly IIdentityProvider _identityProvider;

        public GarmentDetailOmzetByUnitReportController(IGarmentDetailOmzetByUnitReportService service, IIdentityProvider identityProvider)
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
        public IActionResult GetReport(string unit, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];
            try
            {

                VerifyUser();
                var data = _service.GetReportData(unit, dateFrom, dateTo, offset);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Data.Count },
                        { "total", data.Total },
                    };

                return Ok(new
                {
                    data = data.Data,
                    info
                });
            }
            
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("download")]
        public IActionResult GetXls(string unit, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : Convert.ToDateTime(dateFrom);
                DateTime DateTo = dateTo == null ? DateTime.Now : Convert.ToDateTime(dateTo);

                var xls = _service.GenerateExcel(unit, dateFrom, dateTo, offset);

                string filename = String.Format("Report Rincian Omzet Konfeksi - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

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
