using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/sku-inventory-documents")]
    public class SKUInventoryDocumentController : Controller
    {
        public SKUInventoryDocumentController(IProductSKUService service)
        {

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(new { });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new { });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductSKUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var result = new 
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(result);
            }


            
            return Created("/", new { });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put()
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete()
        {
            return NoContent();
        }
    }

    
}