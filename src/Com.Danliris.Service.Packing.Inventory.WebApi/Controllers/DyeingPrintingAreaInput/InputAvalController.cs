using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaInput
{
    [Produces("application/json")]
    [Route("v1/input-aval")]
    [Authorize]
    public class InputAvalController : ControllerBase
    {
        private readonly IInputAvalService _service;
        private readonly IIdentityProvider _identityProvider;

        public InputAvalController(IInputAvalService service, IIdentityProvider identityProvider)
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
        public async Task<IActionResult> Post([FromBody] InputAvalViewModel viewModel)
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

        [HttpPost("reject")]
        public async Task<IActionResult> Reject([FromBody] InputAvalViewModel viewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    var excpetion = new
            //    {
            //        error = ResultFormatter.FormatErrorMessage(ModelState)
            //    };
            //    return new BadRequestObjectResult(excpetion);
            //}
            try
            {
                VerifyUser();
                var result = await _service.Reject(viewModel);

                return Created("/", result);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var data = await _service.Delete(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpGet("pre-aval")]
        public IActionResult GetPreAval([FromQuery] DateTimeOffset searchDate,
                                        [FromQuery] string searchShift,
                                        [FromQuery] string searchGroup,
                                        [FromQuery] string keyword = null,
                                        [FromQuery] int page = 1,
                                        [FromQuery] int size = 25,
                                        [FromQuery] string order = "{}",
                                        [FromQuery] string filter = "{}")
        {
            var data = _service.ReadOutputPreAval(searchDate, searchShift, searchGroup, page, size, filter, order, keyword);
            if (data == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else
            {
                return Ok(data);
            }
        }
        [HttpGet("pre-aval/all")]
        public IActionResult GetPreAvalAll(
                                        [FromQuery] string keyword = null,
                                        [FromQuery] int page = 1,
                                        [FromQuery] int size = 25,
                                        [FromQuery] string order = "{}",
                                        [FromQuery] string filter = "{}")
        {
            try
            {
                var data = _service.ReadAllOutputPreAval(page, size, filter, order, keyword);
                if (data == null)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
                else
                {
                    return Ok(data);
                }
            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("xls")]
        public IActionResult GetExcelAll([FromHeader(Name = "x-timezone-offset")] string timezone, [FromQuery] DateTimeOffset? dateFrom = null, [FromQuery] DateTimeOffset? dateTo = null)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int clientTimeZoneOffset = Convert.ToInt32(timezone);
                var Result = _service.GenerateExcel(dateFrom, dateTo, clientTimeZoneOffset);
                string filename = "Penerimaan Area Gudang Aval Dyeing/Printing.xlsx";

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
