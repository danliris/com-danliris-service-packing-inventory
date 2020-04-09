using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;
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

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/quality-control/defect")]
    [Authorize]
    public class FabricQualityControlController : ControllerBase
    {
        private readonly IFabricQualityControlService _service;
        private readonly IIdentityProvider _identityProvider;

        public FabricQualityControlController(IFabricQualityControlService service, IIdentityProvider identityProvider)
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
        public async Task<IActionResult> Post([FromBody] FabricQualityControlViewModel viewModel)
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
                var result = await _service.Create(viewModel);

                return Created("/", result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }



        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] FabricQualityControlViewModel viewModel)
        {
            VerifyUser();
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
                await _service.Update(id, viewModel);

                return NoContent();
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
                await _service.Delete(id);

                return NoContent();
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
        public IActionResult Get([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}",
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
                    var PdfTemplate = new FabricQualityControlPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(model, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = model.Code + ".pdf"
                    };

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("reports")]
        public IActionResult GetReport(DateTime? dateFrom = null, DateTime? dateTo = null, string code = null, int dyeingPrintingAreMovementId = -1, string productionOrderType = null, string productionOrderNo = null, string shiftIm = null, int page = 1, int size = 25)
        {
            try
            {
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                //int offSet = 7;
                var data = _service.GetReport(page, size, code, dyeingPrintingAreMovementId, productionOrderType, productionOrderNo, shiftIm, dateFrom, dateTo, offSet);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("reports/downloads/xls")]
        public IActionResult GetXls(DateTime? dateFrom = null, DateTime? dateTo = null, string code = null, int kanbanCode = -1, string productionOrderType = null, string productionOrderNo = null, string shiftIm = null)
        {
            try
            {
                byte[] xlsInBytes;
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var xls = _service.GenerateExcel(code, kanbanCode, productionOrderType, productionOrderNo, shiftIm, dateFrom, dateTo, offSet);

                string fileName = "";
                if (dateFrom == null && dateTo == null)
                    fileName = string.Format("Laporan Pemeriksaan Kain");
                else if (dateFrom != null && dateTo == null)
                    fileName = string.Format("Laporan Pemeriksaan Kain {0}", dateFrom.Value.ToString("dd/MM/yyyy"));
                else if (dateFrom == null && dateTo != null)
                    fileName = string.Format("Laporan Pemeriksaan Kain {0}", dateTo.GetValueOrDefault().ToString("dd/MM/yyyy"));
                else
                    fileName = string.Format("Laporan Pemeriksaan Kain {0} - {1}", dateFrom.GetValueOrDefault().ToString("dd/MM/yyyy"), dateTo.Value.ToString("dd/MM/yyyy"));
                xlsInBytes = xls.ToArray();

                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                return file;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("production-order-report")]
        public IActionResult GetForSPP(string no)
        {
            try
            {
                var data = _service.GetForSPP(no);
                return Ok(new
                {
                    data,
                    info = new
                    {
                        data.Count
                    }
                });

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
