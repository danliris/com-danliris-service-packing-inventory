
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
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CashInBank;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.Azure.Amqp.Framing;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.CashInBank;


namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.AR.CashInBank
{
    [Produces("application/json")]
    [Route("v1/cash-in-bank")]
    [Authorize]
    public class CashInBankController : Controller
    {
        private readonly IIdentityProvider _identityService;
        private readonly IValidateService _validateService;
        private readonly ICashInBankService _service;
        private const string ApiVersion = "1.0";
        private readonly ConverterChecker converterChecker = new ConverterChecker();

        public CashInBankController(IServiceProvider serviceProvider)
        {
            _identityService = serviceProvider.GetService<IIdentityProvider>();
            _validateService = serviceProvider.GetService<IValidateService>();
            _service = serviceProvider.GetService<ICashInBankService>();
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

        private async Task<List<CashInBankModel>> MapExcel(IFormFile file, CancellationToken cancellationToken)
        {
            var result = new List<CashInBankModel>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 11; row <= rowCount; row++)
                    {
                        var data = new CashInBankModel()
                        {
                            ReceiptDate = converterChecker.GeneratePureDateTime(worksheet.Cells[row, 2]),
                            Month = converterChecker.GenerateValueInt(worksheet.Cells[row, 3]),
                            ReceiptNo = converterChecker.GeneratePureString(worksheet.Cells[row, 4]),
                            BuyerCode = converterChecker.GeneratePureString(worksheet.Cells[row, 5]),

                            //Terima
                            ReceiptAmount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 6]).Value,
                            ReceiptKurs = converterChecker.GeneratePureDouble(worksheet.Cells[row, 7]).Value,
                            ReceiptTotalAmount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 8]).Value,

                            //Piutang
                            InvoiceNo = converterChecker.GeneratePureString(worksheet.Cells[row, 9]),
                            //Jumlah Cair
                            LiquidAmount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 10]).Value,
                            LiquidTotalAmount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 11]).Value,
                            //Saldo Buku
                            BookBalanceKurs = converterChecker.GeneratePureDouble(worksheet.Cells[row, 12]).Value,
                            BookBalanceTotalAmount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 13]).Value,
                            //Selisih Kurs
                            DifferenceKurs = converterChecker.GeneratePureDouble(worksheet.Cells[row, 14]).Value,

                            //Biaya/Penghasilan Lain2
                            COA = converterChecker.GeneratePureString(worksheet.Cells[row, 15]),
                            UnitCode = converterChecker.GeneratePureString(worksheet.Cells[row, 16]),
                            Remark = converterChecker.GeneratePureString(worksheet.Cells[row, 17]),
                            SupportingDocument = converterChecker.GeneratePureString(worksheet.Cells[row, 18]),
                            Amount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 19]).Value,
                            TotalAmount = converterChecker.GeneratePureDouble(worksheet.Cells[row, 20]).Value,
    
                        };

                        result.Add(data);
                    }
                }

            }
            return result;
        }
    }
}
