using Com.Danliris.Service.Packing.Inventory.Application.ReceivingDocument;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/receiving-and-dispatch-documents")]
    public class ReceivingDispatchController : Controller
    {
        private readonly IReceivingDispatchService _service;
        private readonly IIdentityProvider _identityProvider;

        public ReceivingDispatchController(IReceivingDispatchService service, IIdentityProvider identityProvider)
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

        [HttpPost("receive")]
        public async Task<IActionResult> Receive([FromBody] CreateReceivingDispatchDocumentViewModel viewModel)
        {
            VerifyUser();
            if (!ModelState.IsValid)
            {
                var result = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(result);
            }

            await _service.Receive(viewModel);

            return Created("/", new
            {
            });
        }

        [HttpPost("dispatch")]
        public async Task<IActionResult> Dispatch([FromBody] CreateReceivingDispatchDocumentViewModel viewModel)
        {
            VerifyUser();
            if (!ModelState.IsValid)
            {
                var result = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(result);
            }

            await _service.Dispatch(viewModel);

            return Created("/", new
            {
            });
        }
    }
}
