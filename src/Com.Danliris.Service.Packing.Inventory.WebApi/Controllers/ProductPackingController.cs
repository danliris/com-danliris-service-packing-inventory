using Com.Danliris.Service.Packing.Inventory.Application.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/product-packings")]
    [Authorize]
    public class ProductPackingController : Controller
    {
        private readonly IProductPackingService _service;
        private readonly IIdentityProvider _identityProvider;

        public ProductPackingController(IProductPackingService service, IIdentityProvider identityProvider)
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductPackingFormViewModel viewModel)
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

            await _service.Create(viewModel);

            return Created("/", new
            {
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProductPackingFormViewModel viewModel)
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

            await _service.Update(id, viewModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.Delete(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _service.ReadById(id);
            return Ok(new
            {
                data
            });
        }

        [HttpGet]
        public IActionResult GetByKeyword([FromQuery] string keyword, [FromQuery] string order, [FromQuery] int page = 1, [FromQuery] int size = 25)
        {
            var data = _service.ReadByKeyword(keyword, order, page, size);
            return Ok(data);
        }
    }
}
