using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;


namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/inspection-documents")]
    [Authorize]
    public class InspectionDocumentController : Controller
    {
        private readonly IInspectionDocumentReportService _service;
        private readonly IIdentityProvider _identityProvider;

        public InspectionDocumentController(IInspectionDocumentReportService service, IIdentityProvider identityProvider)
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
        public IActionResult GetAll(
            [FromQuery]DateTimeOffset? dateReport,
            [FromQuery] string group="",
            [FromQuery] string mutasi="",
            [FromQuery] string zona="",
            [FromQuery] string keterangan="")
        {
            try
            {
                VerifyUser();
                int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GetReport(dateReport,group,mutasi,zona,keterangan,clientTimeZoneOffset);
                //if (Result == null)
                //{
                //    return StatusCode((int)HttpStatusCode.NoContent, "{'Message' : 'Null Result Occured'}");
                //}

                //if (Result.Count == 0)
                //{
                    //return StatusCode((int)HttpStatusCode.NoContent, "{'Message': 'Report Data Not Found for the filter'}");
                //}

                return Ok(new
                {
                    data = Result
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("xls")]
        public IActionResult GetDataByExcel(
            [FromQuery]DateTimeOffset? dateReport,
            [FromQuery] string group = "",
            [FromQuery] string mutasi = "",
            [FromQuery] string zona = "",
            [FromQuery] string keterangan = "")
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GenerateExcel(dateReport,group,mutasi,zona,keterangan,clientTimeZoneOffset);
                string filename = "BON IM.xlsx";
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