using Com.Danliris.Service.Packing.Inventory.Application.Product;
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
    [Route("v1/product-packing-skus")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IIdentityProvider _identityProvider;

        public ProductController(IProductService service, IIdentityProvider identityProvider)
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

        [HttpPost("retreive-qrcode-info")]
        public async Task<IActionResult> Post([FromBody] CreateProductPackAndSKUViewModel viewModel)
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

            var barcodeInfo = await _service.CreateProductPackAndSKU(viewModel);

            return Created("/", new
            {
                barcodeInfo
            });
        }
    }
}
