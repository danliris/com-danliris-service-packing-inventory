
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Http;
using System.Threading;
using OfficeOpenXml;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.OmzetCorrectionService;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.OmzetCorrectionsModel;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DownPayment
{
    [Produces("application/json")]
    [Route("v1/omzet-correction")]
    [Authorize]
    public class OmzetCorrectionController : Controller
    {
        private readonly IIdentityProvider _identityService;
        private readonly IValidateService _validateService;
        private readonly IOmzetCorrectionService _service;
        private readonly ConverterChecker converterChecker = new ConverterChecker();

        public OmzetCorrectionController(IServiceProvider serviceProvider)
        {
            _identityService = serviceProvider.GetService<IIdentityProvider>();
            _validateService = serviceProvider.GetService<IValidateService>();
            _service = serviceProvider.GetService<IOmzetCorrectionService>();
        }

        protected void VerifyUser()
        {
            _identityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityService.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        //[HttpGet("pdf/{Id}")]
        //public async Task<IActionResult> GetDownPaymentPDF([FromRoute] int Id)
        //{
        //    try
        //    {
        //        var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
        //        int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        //        DownPaymentModel model = await _service.ReadByIdAsync(Id);

        //        if (model == null)
        //        {
        //            Dictionary<string, object> Result =
        //                new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE)
        //                .Fail();
        //            return NotFound(Result);
        //        }
        //        else
        //        {
        //            DownPaymentViewModel viewModel = _mapper.Map<DownPaymentViewModel>(model);

        //            DownPaymentPDFTemplate PdfTemplate = new DownPaymentPDFTemplate();
        //            MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
        //            return new FileStreamResult(stream, "application/pdf")
        //            {
        //                //FileDownloadName = "Kuitansi - " + viewModel.Buyer.Name + ".pdf"
        //                FileDownloadName = "Kuitansi -.pdf"
        //            };
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Dictionary<string, object> Result =
        //            new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
        //            .Fail();
        //        return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
        //    }
        //}

        [HttpGet]
        public IActionResult Get(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")] List<string> select = null, string keyword = null, string filter = "{}")
        {
            try
            {
                var queryResult = _service.Read(page, size, order, select, keyword, filter);

                return Ok(queryResult);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] DownPaymentViewModel viewModel)
        //{
        //    try
        //    {
        //        VerifyUser();
        //        _validateService.Validate(viewModel);

        //        var model = _mapper.Map<DownPaymentModel>(viewModel);
        //        await _service.CreateAsync(model);

        //        var result = new ResultFormatter(ApiVersion, General.CREATED_STATUS_CODE, General.OK_MESSAGE).Ok();

        //        return Created(String.Concat(Request.Path, "/", 0), result);
        //    }
        //    catch (ServiceValidationException e)
        //    {
        //        var result = new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE).Fail(e);
        //        return BadRequest(result);
        //    }
        //    catch (Exception e)
        //    {
        //        var result =
        //            new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
        //            .Fail();
        //        return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, result);
        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById([FromRoute] int id)
        //{
        //    try
        //    {
        //        var model = await _service.ReadByIdAsync(id);

        //        if (model == null)
        //        {
        //            var result = new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE).Fail();
        //            return NotFound(result);
        //        }
        //        else
        //        {
        //            var viewModel = _mapper.Map<DownPaymentViewModel>(model);
        //            var result = new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE).Ok<DownPaymentViewModel>(_mapper, viewModel);
        //            return Ok(result);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var result = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message).Fail();
        //        return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, result);
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put([FromRoute] int id, [FromBody] DownPaymentViewModel viewModel)
        //{
        //    try
        //    {
        //        VerifyUser();
        //        _validateService.Validate(viewModel);

        //        if (id != viewModel.Id)
        //        {
        //            var result = new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE).Fail();
        //            return BadRequest(result);
        //        }

        //        var model = _mapper.Map<DownPaymentModel>(viewModel);

        //        await _service.UpdateAsync(id, model);

        //        return NoContent();
        //    }
        //    catch (ServiceValidationException e)
        //    {
        //        var result = new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE).Fail(e);
        //        return BadRequest(result);
        //    }
        //    catch (Exception e)
        //    {
        //        var result = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message).Fail();
        //        return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, result);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    try
        //    {
        //        VerifyUser();

        //        await _service.DeleteAsync(id);

        //        return NoContent();
        //    }
        //    catch (Exception e)
        //    {
        //        var result = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message).Fail();
        //        return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, result);
        //    }
        //}

        [HttpPost("upload")]
        public async Task<IActionResult> Post(IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                VerifyUser();

                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (file.Length > 0 && fileExtension.Equals(".xlsx"))
                {
                    var downPaymentList = await MapExcel(file, cancellationToken);
                    //Tuple<bool, List<ReportDto>> Validated = _employeeService.UploadValidate(employeeList);
                    //if (Validated.Item1)
                    //{
                    var result = await _service.UploadExcelAsync(downPaymentList);

                    return Created("", result);
                    //}
                    //else
                    //{
                    //    var stream = GenerateExcelLogError(Validated.Item2);

                    //    var filename = "Error Log - Upload Karyawan.xlsx";
                    //    var bytes = stream.ToArray();
                    //return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                    //}
                }
                else
                {
                    throw new Exception("File not valid!");
                }
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
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        private async Task<List<OmzetCorrectionModel>> MapExcel(IFormFile file, CancellationToken cancellationToken)
        {
            var result = new List<OmzetCorrectionModel>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        var data = new OmzetCorrectionModel()
                        {
                            MemoNo = converterChecker.GeneratePureString(worksheet.Cells[row, 2]),
                            Remark = converterChecker.GeneratePureString(worksheet.Cells[row, 3]),
                            InvoiceNo = converterChecker.GeneratePureString(worksheet.Cells[row, 4]),
                            BuyerCode = converterChecker.GeneratePureString(worksheet.Cells[row, 5]),
                            Amount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 6]).Value,
                            Kurs = converterChecker.GeneratePureDouble(worksheet.Cells[row, 7]).Value,
                            TotalAmount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 8]).Value,
                            Month = converterChecker.GenerateValueInt(worksheet.Cells[row, 3])
                        };

                        result.Add(data);
                    }
                }

            }
            return result;
        }
    }
}
