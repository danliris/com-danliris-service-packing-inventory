using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report.GarmentFinanceLocalSalesJournal;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.TraceableOut;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentMD
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/traceable-out")]
    [Authorize]
    public class GarmentFinanceLocalSalesJournalController : ControllerBase
    {
        private string ApiVersion = "1.0.0";
        private readonly ITraceableOutService _service;
        private readonly IIdentityProvider _identityProvider;

        public GarmentFinanceLocalSalesJournalController(ITraceableOutService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(string bcno,string bcType,string category)
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];
            _identityProvider.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");
            try
            {
                var data = await _service.getQuery(bcno,bcType,category);

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
        public async Task<IActionResult> GetXls(string bcno, string bcType, string category)
        {
            _identityProvider.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");
            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var xls = await _service.GetExcel(bcno, bcType, category);

                string filename = String.Format("Laporan Traceable Keluar Lokal - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

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
