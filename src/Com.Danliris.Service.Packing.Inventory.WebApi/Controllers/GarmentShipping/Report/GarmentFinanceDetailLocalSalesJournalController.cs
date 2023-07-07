using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.Report
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/report/garment-finance-detail-local-sales-journals")]
    [Authorize]
    public class GarmentFinanceDetailLocalSalesJournalController : ControllerBase
    {
        private string ApiVersion = "1.0.0";
        private readonly IGarmentFinanceDetailLocalSalesJournalService _service;
        private readonly IIdentityProvider _identityProvider;

        public GarmentFinanceDetailLocalSalesJournalController(IGarmentFinanceDetailLocalSalesJournalService service, IIdentityProvider identityProvider)
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

        //[HttpGet]
        //public IActionResult GetReport([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        //{
        //    int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        //    //string accept = Request.Headers["Accept"];

        //    VerifyUser();

        //    try
        //    {
        //        var data = _service.GetReportData(dateFrom, dateTo, offset);

        //        return Ok(new
        //        {
        //            apiVersion = ApiVersion,
        //            data = data,
        //            message = General.OK_MESSAGE,
        //            statusCode = General.OK_STATUS_CODE
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet("download")]
        public IActionResult GetXls([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            VerifyUser();

            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var xls = _service.GenerateExcel(dateFrom, dateTo, offset);

                string filename = String.Format("Detail Jurnal Penjualan Lokal - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

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
