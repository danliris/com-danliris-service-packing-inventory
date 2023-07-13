using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
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
        public async Task<IActionResult> Post([FromBody] DPInputWarehouseCreateViewModel viewModel)
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
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPost("reject")]
        public async Task<IActionResult> Reject([FromBody] DPInputWarehouseCreateViewModel viewModel)
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
                var result = await _service.Reject(viewModel);

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

        [HttpGet("monitoring")]
        public IActionResult GetMonitoring([FromQuery] DateTimeOffset dateFrom, [FromQuery] DateTimeOffset dateTo, [FromQuery] int productionOrderId)
        {
            try
            {
                VerifyUser();
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var data = _service.GetMonitoring(dateFrom, dateTo, productionOrderId, offset);
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

        [HttpGet("monitoring-xls")]
        public IActionResult GetMonitoringXls([FromQuery] DateTimeOffset dateFrom, [FromQuery] DateTimeOffset dateTo, [FromQuery] int productionOrderId, [FromQuery] int track)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GenerateExcelMonitoring(dateFrom, dateTo, productionOrderId, offset);
                string filename = "";

                if (dateFrom == DateTimeOffset.MinValue && dateTo == DateTimeOffset.MinValue)
                {
                    filename = $"Monitoring Penerimaan Gudang Barang Jadi.xlsx";
                }
                else
                {
                    filename = $"Monitoring Penerimaan Gudang Barang Jadi {dateFrom.ToString("yyyy MM dd")} - {dateTo.ToString("yyyy MM dd")}.xlsx";
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

        [HttpGet("preinput")]
        public IActionResult GetMonitoringPreInput( [FromQuery] int productionOrderId, [FromQuery] string productPackingCode)
        {
            try
            {
                VerifyUser();
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var data = _service.GetMonitoringPreInput( productionOrderId, productPackingCode);
                return Ok(new
                {
                    //apiVersion = ApiVersion,
                    data = data,
                    info = new { count = data.Count(), total = data.Count() },
                    total = data.Count(),
                    message = General.OK_MESSAGE,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("preinput-xls")]
        public IActionResult GetMonitoringPreInputXls([FromQuery] int productionOrderId, [FromQuery] string productPackingCode)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var Result = _service.GenerateExcelPreInput( productionOrderId, productPackingCode);
                string filename = "";

                //if (dateFrom == DateTimeOffset.MinValue && dateTo == DateTimeOffset.MinValue)
                //{
                    filename = $"Monitoring SPP Belum Di Terima Gudang Barang Jadi.xlsx";
                //}
                //else
                //{
                //    filename = $"Monitoring Penerimaan Gudang Barang Jadi {dateFrom.ToString("yyyy MM dd")} - {dateTo.ToString("yyyy MM dd")}.xlsx";
                //}
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
