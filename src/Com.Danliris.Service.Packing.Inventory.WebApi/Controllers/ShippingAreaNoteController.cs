using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Shipping;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/shipping-area-note")]
    [Authorize]
    public class ShippingAreaNoteController : ControllerBase
    {
        private readonly IShippingAreaNoteService _service;
        private readonly IIdentityProvider _identityProvider;

        public ShippingAreaNoteController(IShippingAreaNoteService service, IIdentityProvider identityProvider)
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
        public IActionResult GetShippingAreaNote(DateTimeOffset? searchDate, string mutation, string zone)
        {
            try
            {
                VerifyUser();
                int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GetReport(searchDate, mutation, zone, clientTimeZoneOffset);

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

        //[HttpGet("xls")]
        //public IActionResult GetAvalAreaNoteExcel(DateTimeOffset? searchDate, string mutation, string zone)
        //{
        //    try
        //    {
        //        VerifyUser();
        //        byte[] xlsInBytes;
        //        int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        //        var Result = _service.GenerateExcel(searchDate, mutation, zone, clientTimeZoneOffset);
        //        string filename = "Aval Area Note Dyeing/Printing.xlsx";
        //        xlsInBytes = Result.ToArray();
        //        var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        //        return file;
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
    }
}
