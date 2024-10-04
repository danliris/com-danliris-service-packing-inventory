
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
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.DownPayment;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.DownPayment;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.AR_ReportMutation;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.AR.DownPayment
{
    [Produces("application/json")]
    [Route("v1/down-payments")]
    [Authorize]
    public class DownPaymentController : Controller
    {
        private readonly IIdentityProvider _identityService;
        private readonly IValidateService _validateService;
        private readonly IDownPaymentService _service;
        private readonly IAR_ReportMutationService _mutationService;
        private readonly ConverterChecker converterChecker = new ConverterChecker();

        public DownPaymentController(IServiceProvider serviceProvider)
        {
            _identityService = serviceProvider.GetService<IIdentityProvider>();
            _validateService = serviceProvider.GetService<IValidateService>();
            _service = serviceProvider.GetService<IDownPaymentService>();
            _mutationService = serviceProvider.GetService<IAR_ReportMutationService>();
        }

        protected void VerifyUser()
        {
            _identityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityService.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

      

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

        [HttpGet("download")]
        //public IActionResult GetXls([FromQuery] int month, [FromQuery] int year)
        public async Task<IActionResult> GetXls([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                //var xls = _service.GenerateExcel(month, year, offset);
                var xls = await _mutationService.GetExcel();

                string filename = String.Format("AR_Mutasi - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

                xlsInBytes = xls.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task<List<DownPaymentModel>> MapExcel(IFormFile file, CancellationToken cancellationToken)
        {
            var result = new List<DownPaymentModel>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 4; row <= rowCount; row++)
                    {
                        var data = new DownPaymentModel()
                        {
                            MemoNo = converterChecker.GeneratePureString(worksheet.Cells[row, 2]),
                            Remark = converterChecker.GeneratePureString(worksheet.Cells[row, 3]),
                            ReceiptNo = converterChecker.GeneratePureString(worksheet.Cells[row, 4]),
                            Date = converterChecker.GeneratePureDateTime(worksheet.Cells[row, 5]).Value,
                            OffsetDate = converterChecker.GeneratePureDateTime(worksheet.Cells[row, 6]).Value,
                            InvoiceNo = converterChecker.GeneratePureString(worksheet.Cells[row, 7]),
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
