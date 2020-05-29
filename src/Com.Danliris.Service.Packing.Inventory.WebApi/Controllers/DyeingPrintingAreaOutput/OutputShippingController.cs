using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaOutput
{
    [Produces("application/json")]
    [Route("v1/output-shipping")]
    [Authorize]
    public class OutputShippingController : ControllerBase
    {
        private readonly IOutputShippingService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService ValidateService;

        public OutputShippingController(IOutputShippingService service, IIdentityProvider identityProvider, IValidateService validateService)
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutputShippingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var excpetion = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(excpetion);
            }
            try
            {
                VerifyUser();
                ValidateService.Validate(viewModel);
                var result = await _service.Create(viewModel);

                return Created("/", result);
            }
            catch (ServiceValidationException ex)
            {
                var Result = new
                {
                    error = ResultFormatter.Fail(ex),
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data does not pass validation"
                };

                return new BadRequestObjectResult(Result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {

                var data = await _service.ReadById(id);
                return Ok(new
                {
                    data
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string order = "{}",
            [FromQuery] string filter = "{}")
        {
            try
            {

                var data = _service.Read(page, size, filter, order, keyword);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpGet("xls/{id}")]
        public async Task<IActionResult> GetExcel(int id)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = await _service.GenerateExcel(id);
                string filename = "Shipping Area Note Dyeing/Printing.xlsx";
                xlsInBytes = Result.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("input-production-orders/{id}")]
        public IActionResult GetProductionOrders(long id)
        {
            try
            {

                var data = _service.GetInputShippingProductionOrdersByDeliveryOrder(id);
                return Ok(new
                {
                    data
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpGet("sales-loader")]
        public IActionResult GetForSales([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string order = "{}",
           [FromQuery] string filter = "{}")
        {
            try
            {

                var data = _service.ReadForSales(page, size, filter, order, keyword);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpPut("sales-invoice/{id}")]
        public async Task<IActionResult> UpdateHasSalesInvoice([FromRoute] int id, [FromBody] OutputShippingUpdateSalesInvoiceViewModel salesInvoice)
        {
            if (!ModelState.IsValid)
            {
                var excpetion = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(excpetion);
            }
            try
            {
                VerifyUser();
                var result = await _service.UpdateHasSalesInvoice(id, salesInvoice);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> GetPdfById([FromRoute] int id, [FromHeader(Name = "x-timezone-offset")] string timezone)
        {
            try
            {
                var model = await _service.ReadById(id);
                if (model == null)
                {
                    var Result = new
                    {
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.NotFound,
                        message = "Not Found"
                    };
                    return NotFound(Result);
                }
                else
                {
                    int timeoffsset = Convert.ToInt32(timezone);
                    var pdfTemplate = new OutputShippingPdfTemplate(model, timeoffsset);
                    var stream = pdfTemplate.GeneratePdfTemplate();
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = string.Format("{0}.pdf", model.BonNo)
                    };
                }
            }
            catch (Exception e)
            {
                var Result = new
                {
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.InternalServerError,
                    message = e.Message
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, Result);
            }
        }

        //[HttpGet("output-production-orders/{id}")]
        //public IActionResult GetProductionOrdersByBon(int id)
        //{
        //    try
        //    {

        //        var data = _service.GetOutputShippingProductionOrdersByBon(id);
        //        return Ok(new
        //        {
        //            data
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

        //    }
        //}
    }
}
