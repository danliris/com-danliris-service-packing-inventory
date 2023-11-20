using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.LogHistory
{
    [Produces("application/json")]
    [Route("v1/log-history")]
    [Authorize]
    public class LogHistoryController : ControllerBase
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly ILogHistoryService _service;

        public LogHistoryController(ILogHistoryService service, IIdentityProvider identityProvider)
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
        public async Task<IActionResult> Get(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                VerifyUser();

                var result = await _service.GetData(dateFrom, dateTo);

                return Ok(new
                {
                    data = result,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
