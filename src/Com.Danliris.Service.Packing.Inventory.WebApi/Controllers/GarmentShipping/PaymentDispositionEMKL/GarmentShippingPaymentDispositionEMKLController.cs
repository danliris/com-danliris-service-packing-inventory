using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition.PaymentDispositionEMKLs;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.PaymentDispositionEMKL
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/payment-dispositions/EMKL")]
    [Authorize]
    public class GarmentShippingPaymentDispositionEMKLController : ControllerBase
    {
        private readonly IGarmentShippingPaymentEMKLDispositionService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;
        private readonly IGarmentShippingInvoiceService _invoiceService;
        private readonly IGarmentPackingListService _packingListService;

        public GarmentShippingPaymentDispositionEMKLController(IGarmentShippingPaymentEMKLDispositionService service, IIdentityProvider identityProvider, IValidateService validateService, IGarmentShippingInvoiceService invoiceService, IGarmentPackingListService packingListService)
        {
            _service = service;
            _identityProvider = identityProvider;
            _validateService = validateService;
            _invoiceService = invoiceService;
            _packingListService = packingListService;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GarmentShippingPaymentDispositionEMKLViewModel viewModel)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(viewModel);
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
        public IActionResult Get([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}", [FromQuery] string filter = "{}")
        {
            try
            {
                var data = _service.Read(page, size, filter, order, keyword);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Data.Count },
                        { "total", data.Total },
                        { "order", order },
                        { "page", page },
                        { "size", size }
                    };

                return Ok(new
                {
                    data = data.Data,
                    info
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] GarmentShippingPaymentDispositionEMKLViewModel viewModel)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(viewModel);
                var result = await _service.Update(id, viewModel);

                return Ok(result);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.Delete(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                var exception = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(exception);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var model = await _service.ReadById(Id);

                if (model == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Not Found");
                }
                else
                {
                    List<GarmentShippingInvoiceViewModel> invoices = new List<GarmentShippingInvoiceViewModel>();
                    List<GarmentPackingListViewModel> packingLists = new List<GarmentPackingListViewModel>();
                    foreach (var invoiceItem in model.invoiceDetails)
                    {
                        GarmentShippingInvoiceViewModel invoice = await _invoiceService.ReadById(invoiceItem.invoiceId);
                        GarmentPackingListViewModel pl = await _packingListService.ReadByInvoiceNo(invoiceItem.invoiceNo);
                        invoices.Add(invoice);
                        packingLists.Add(pl);
                    }


                    var PdfTemplate = new GarmentShippingPaymentDispositionEMKLPDFTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(model, invoices, timeoffsset);

                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = model.dispositionNo + ".pdf"
                    };



                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
