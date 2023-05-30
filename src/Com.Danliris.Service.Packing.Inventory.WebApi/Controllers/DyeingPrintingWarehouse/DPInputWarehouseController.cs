using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingWarehouse
{
    [Produces("application/json")]
    [Route("v1/dp-input-warehouse")]
    [Authorize]
    public class DPInputWarehouseController : ControllerBase
    {
        private readonly IDPWarehouseInService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService ValidateService;
        public DPInputWarehouseController(IDPWarehouseInService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            _service = service;
            _identityProvider = identityProvider;
            ValidateService = validateService;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpGet("code")]
        public IActionResult GetBarcode(string packingCode)
        {

            
            try
            {

                //var data = _service.getDatabyCode(itemData, trackId);
                var data = _service.PreInputWarehouse(packingCode);
                //var model = mapper.Map<List<InventoryViewModel>>(data);

                return Ok(new
                {
                    //apiVersion = ApiVersion,
                    data = data,
                    info = new { count = data.Count(), total = data.Count() },

                    message = General.OK_MESSAGE,
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
