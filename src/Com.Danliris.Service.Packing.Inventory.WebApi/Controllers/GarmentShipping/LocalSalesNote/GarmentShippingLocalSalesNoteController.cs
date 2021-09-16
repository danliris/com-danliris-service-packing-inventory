using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
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

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.DebitNote
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/local-sales-notes")]
    [Authorize]
    public class GarmentShippingLocalSalesNoteController : ControllerBase
    {
        private readonly IGarmentShippingLocalSalesNoteService _service;
        private readonly IGarmentLocalCoverLetterService _clservice;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public GarmentShippingLocalSalesNoteController(IGarmentShippingLocalSalesNoteService service, IGarmentLocalCoverLetterService clservice, IIdentityProvider identityProvider, IValidateService validateService)
        {
            _service = service;
            _clservice = clservice;
            _identityProvider = identityProvider;
            _validateService = validateService;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GarmentShippingLocalSalesNoteViewModel viewModel)
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
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] GarmentShippingLocalSalesNoteViewModel viewModel)
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

        [HttpPut("approve-shipping/{id}")]
        public async Task<IActionResult> PutApproveShipping([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.ApproveShipping(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("approve-finance/{id}")]
        public async Task<IActionResult> PutApproveFinance([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var result = await _service.ApproveFinance(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("reject-shipping/{id}")]
        public async Task<IActionResult> PutRejectShipping([FromRoute] int id, [FromBody] GarmentShippingLocalSalesNoteViewModel viewModel)
        {
            try
            {
                VerifyUser();
                var result = await _service.RejectedShipping(id, viewModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("reject-finance/{id}")]
        public async Task<IActionResult> PutRejectFinance([FromRoute] int id, [FromBody] GarmentShippingLocalSalesNoteViewModel viewModel)
        {
            try
            {
                VerifyUser();
                var result = await _service.RejectedFinance(id, viewModel);

                return Ok(result);
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
                VerifyUser();
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var model = await _service.ReadById(Id);

                if (model == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Not Found");
                }
                else
                {
                    Buyer buyer = _service.GetBuyer(model.buyer.Id);
                    GarmentLocalCoverLetterViewModel cl = await _clservice.ReadByLocalSalesNoteId(model.Id);
                    var PdfTemplate = new GarmentShippingLocalSalesNotePdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(model, cl, buyer, timeoffsset);

                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = model.noteNo + ".pdf"
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
