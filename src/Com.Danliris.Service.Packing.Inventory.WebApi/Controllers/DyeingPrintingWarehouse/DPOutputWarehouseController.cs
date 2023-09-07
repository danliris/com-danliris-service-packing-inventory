using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.OUT;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.OUT.ViewModel;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingWarehouse
{
    [Produces("application/json")]
    [Route("v1/dp-output-warehouse")]
    [Authorize]
    public class DPOutputWarehouseController : ControllerBase
    {
        private readonly IDPWarehouseOutService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService ValidateService;

        public DPOutputWarehouseController(IDPWarehouseOutService service, IIdentityProvider identityProvider, IValidateService validateService)
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
        public IActionResult GetBarcode(string itemData, int trackId)
        {


            try
            {

                //var data = _service.getDatabyCode(itemData, trackId);
                var data = _service.ListOutputWarehouse(itemData, trackId);
                //var model = mapper.Map<List<InventoryViewModel>>(data);

                return Ok(new
                {
                    data = data
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DPWarehouseOutputCreateViewModel viewModel)
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
                var result = new
                {
                    error = ex.Message,
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.InternalServerError,
                    message = ex.Message
                };


                return StatusCode((int)HttpStatusCode.InternalServerError, result);
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
        [HttpGet("monitoring")]
        public IActionResult GetMonitoring([FromQuery] DateTimeOffset dateFrom, [FromQuery] DateTimeOffset dateTo, [FromQuery] int productionOrderId, [FromQuery] int track)
        {
            try
            {
                VerifyUser();
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var data = _service.GetMonitoring(dateFrom, dateTo, productionOrderId, track, offset);
                return Ok(new
                {
                    //apiVersion = ApiVersion,
                    data = data,
                    info = new { count = data.Count(), total = data.Count() }, 
                    total = data.Count()
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("monitoring-xls")]
        public IActionResult GetMonitoringXls([FromQuery] DateTimeOffset dateFrom, [FromQuery] DateTimeOffset dateTo, [FromQuery] int productionOrderId, [FromQuery] int track)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GenerateExcelMonitoring(dateFrom, dateTo, productionOrderId, track, offset);
                string filename = "";

                if (dateFrom == DateTimeOffset.MinValue && dateTo == DateTimeOffset.MinValue)
                {
                    filename = $"Monitoring Pengeluaran Gudang Barang Jadi.xlsx";
                }
                else
                {
                    filename = $"Monitoring Pengeluaran Gudang Barang Jadi {dateFrom.ToString("yyyy MM dd")} - {dateTo.ToString("yyyy MM dd")}.xlsx";
                }
                xlsInBytes = Result.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
