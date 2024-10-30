
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;

using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CMT;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.CMT;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.AR.CMT
{
    [Produces("application/json")]
    [Route("v1/cmt")]
    [Authorize]
    public class AR_CMTController : Controller
    {
        private readonly IIdentityProvider _identityService;
        private readonly IValidateService _validateService;
        private readonly ICMTService _service;
        private const string ApiVersion = "1.0";
        private readonly ConverterChecker converterChecker = new ConverterChecker();

        public AR_CMTController(IServiceProvider serviceProvider)
        {
            _identityService = serviceProvider.GetService<IIdentityProvider>();
            _validateService = serviceProvider.GetService<IValidateService>();
            _service = serviceProvider.GetService<ICMTService>();
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

        private async Task<List<CMTModel>> MapExcel(IFormFile file, CancellationToken cancellationToken)
        {
            var result = new List<CMTModel>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 7; row <= rowCount; row++)
                    {
                        var data = new CMTModel()
                        {
                            InvoiceNo = converterChecker.GeneratePureString(worksheet.Cells[row, 2]),
                            TruckingDate = converterChecker.GeneratePureDateTime(worksheet.Cells[row, 3]).Value,
                            PEBDate = converterChecker.GeneratePureDateTime(worksheet.Cells[row, 4]).Value,
                            ExpenditureGoodNo = converterChecker.GeneratePureString(worksheet.Cells[row, 5]),
                            RONo = converterChecker.GeneratePureString(worksheet.Cells[row, 6]),
                            Quantity = converterChecker.GeneratePureDouble(worksheet.Cells[row, 7]).Value,
                            Amount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 8]).Value,
                            Kurs = converterChecker.GeneratePureDouble(worksheet.Cells[row, 9]).Value,
                            TotalAmount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 10]).Value,
                            Month = converterChecker.GenerateValueInt(worksheet.Cells[row, 1]),
                        };

                        result.Add(data);
                    }
                }

            }
            return result;
        }
    }
}
