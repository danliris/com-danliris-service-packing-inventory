using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WeftType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WeftType.WeftTypeService;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.Master
{
    [Produces("application/json")]
    [Route("v1/master/weft-type")]
    [Authorize]
    public class WeftTypeController : ControllerBase
    {
        private readonly IWeftTypeService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService ValidateService;
        private readonly string ContentType = "text/csv";
        private readonly string FileName = string.Concat("Error Log - Jenis Pakan ", DateTime.UtcNow.ToString("dd MMM yyyy"), ".csv");


        public WeftTypeController(IWeftTypeService service, IIdentityProvider identityProvider, IValidateService validateService)
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeftTypeViewModel viewModel)
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
                ValidateService.Validate(viewModel);
                var result = await _service.Create(viewModel);

                return Created(HttpContext.Request.Path, result);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                await _service.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                var error = new
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    error = ex.Message
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] WeftTypeViewModel viewModel)
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
                VerifyUser();
                ValidateService.Validate(viewModel);
                await _service.Update(id, viewModel);

                return NoContent();
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
                var error = new
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    error = ex.Message
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                VerifyUser();
                if (file == null)
                {
                    var Result = new
                    {
                        error = "No File Uploaded",
                        apiVersion = "1.0.0",
                        statusCode = HttpStatusCode.NotFound,
                        message = "No File Uploaded"
                    };

                    return NotFound(Result);
                }
                else
                {
                    StreamReader Reader = new StreamReader(file.OpenReadStream());
                    List<string> FileHeader = new List<string>(Reader.ReadLine().Split(","));
                    var ValidHeader = _service.ValidateHeader(FileHeader);

                    if (ValidHeader)
                    {
                        Reader.DiscardBufferedData();
                        Reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        Reader.BaseStream.Position = 0;

                        CsvReader Csv = new CsvReader(Reader, CultureInfo.InvariantCulture);
                        Csv.Configuration.IgnoreQuotes = false;
                        Csv.Configuration.Delimiter = ",";
                        Csv.Configuration.RegisterClassMap<WeftTypeMap>();
                        Csv.Configuration.HeaderValidated = null;

                        List<WeftTypeViewModel> Data = Csv.GetRecords<WeftTypeViewModel>().ToList();

                        Tuple<bool, List<object>> Validated = _service.UploadValidate(Data);

                        Reader.Close();

                        if (Validated.Item1) /* If Data Valid */
                        {
                            var result = await _service.Upload(Data);
                            return Created(HttpContext.Request.Path, result);
                        }
                        else
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                                {
                                    using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                                    {
                                        csvWriter.WriteRecords(Validated.Item2);
                                    }
                                }
                                
                                return File(memoryStream.ToArray(), ContentType, FileName);
                            }
                        }
                    }
                    else
                    {
                        var Result = new
                        {
                            error = "Header is not valid",
                            apiVersion = "1.0.0",
                            statusCode = HttpStatusCode.BadRequest,
                            message = "Header is not valid"
                        };
                        return BadRequest(Result);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = new
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    error = ex.Message
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }
        }

        [HttpGet("template/download")]
        public IActionResult DownloadTemplate()
        {
            try
            {
                byte[] csvInBytes;
                var csv = _service.DownloadTemplate();

                string fileName = "Jenis Pakan.csv";

                csvInBytes = csv.ToArray();

                var file = File(csvInBytes, ContentType, fileName);
                return file;
            }
            catch (Exception ex)
            {
                var error = new
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    error = ex.Message
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }
        }
    }
}
