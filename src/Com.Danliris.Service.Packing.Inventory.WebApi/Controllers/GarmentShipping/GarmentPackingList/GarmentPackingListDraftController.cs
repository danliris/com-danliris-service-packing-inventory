using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.GarmentPackingList
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/packing-lists/draft")]
    [Authorize]
    public class GarmentPackingListDraftController : ControllerBase
    {
        private readonly IGarmentPackingListDraftService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public GarmentPackingListDraftController(IGarmentPackingListDraftService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            _service = service;
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
        public async Task<IActionResult> Post([FromBody] GarmentPackingListDraftViewModel viewModel)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(viewModel);
                var result = await _service.Create(viewModel);

                return Created("/", new { data = result });
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

        [HttpPost("copy")]
        public async Task<IActionResult> PostCopy([FromBody] GarmentPackingListDraftCopyViewModel viewModel)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(viewModel);
                var result = await _service.Create(viewModel);

                return Created("/", new { data = result });
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] GarmentPackingListDraftViewModel viewModel)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var accept = Request.Headers["Accept"];
                if (accept == "application/pdf")
                {
                    VerifyUser();
                    var result = await _service.ReadPdfById(id);

                    return File(result.Data.ToArray(), "application/pdf", result.FileName);
                }
                else if (accept == "application/xls")
                {
                    VerifyUser();
                    var result = await _service.ReadExcelById(id);

                    return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.FileName);
                }

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

        [HttpGet("{id}/carton")]
        public async Task<IActionResult> GetPdfFilterCarton([FromRoute] int id)
        {
            try
            {
                var accept = Request.Headers["Accept"];
                if (accept == "application/pdf")
                {
                    VerifyUser();
                    var result = await _service.ReadPdfFilterCarton(id);

                    return File(result.Data.ToArray(), "application/pdf", result.FileName);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("post-booking/{id}")]
        public async Task<IActionResult> PostBooking(int id)
        {
            try
            {
                VerifyUser();
                await _service.SetStatus(id, GarmentPackingListStatusEnum.DRAFT_POSTED);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("unpost-booking/{id}")]
        public async Task<IActionResult> UnpostBooking(int id)
        {
            try
            {
                VerifyUser();
                await _service.SetStatus(id, GarmentPackingListStatusEnum.DRAFT);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> SetCancel([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();
                await _service.SetStatus(id, GarmentPackingListStatusEnum.DRAFT_CANCELED, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("approve-md/{id}")]
        public async Task<IActionResult> SetApproveMd([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                await _service.SetStatus(id, GarmentPackingListStatusEnum.DRAFT_APPROVED_MD);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("reject-md/{id}")]
        public async Task<IActionResult> SetRejectMd([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();
                await _service.SetStatus(id, GarmentPackingListStatusEnum.DRAFT_REJECTED_MD, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("approve-shipping/{id}")]
        public async Task<IActionResult> SetApproveShipping([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                await _service.SetStatus(id, GarmentPackingListStatusEnum.DRAFT_APPROVED_SHIPPING);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("reject-shipping/{id}")]
        public async Task<IActionResult> SetRejectShipping([FromRoute] int id, [FromBody] string reason)
        {
            try
            {
                VerifyUser();
                await _service.SetStatus(id, GarmentPackingListStatusEnum.DRAFT_REJECTED_SHIPPING, reason);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("post-packing-list/{id}")]
        public async Task<IActionResult> PostPackingList(int id)
        {
            try
            {
                VerifyUser();

                var packingListViewModel = await _service.ReadById(id);
                var packingListViewModelSerialized = JsonConvert.SerializeObject(packingListViewModel);
                var packingListUnitPackingViewModel = JsonConvert.DeserializeObject<GarmentPackingListUnitPackingViewModel>(packingListViewModelSerialized);
                foreach (var item in packingListUnitPackingViewModel.Items)
                {
                    item.Section = packingListUnitPackingViewModel.Section;
                }
                _validateService.Validate(packingListUnitPackingViewModel);

                await _service.SetStatus(id, GarmentPackingListStatusEnum.POSTED);

                return Ok();
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

        [HttpPut("unpost-packing-list/{id}")]
        public async Task<IActionResult> UnpostPackingList(int id)
        {
            try
            {
                VerifyUser();
                await _service.SetStatus(id, GarmentPackingListStatusEnum.DRAFT_APPROVED_SHIPPING);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
