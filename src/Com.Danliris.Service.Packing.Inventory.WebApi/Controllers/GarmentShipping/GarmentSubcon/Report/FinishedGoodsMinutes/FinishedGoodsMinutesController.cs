using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.FinishedGoodsMinutes;
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

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.GarmentSubcon.Report.FinishedGoodsMinutes
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/receipt-subcon-finished-goods-minutes")]
    [Authorize]
    public class FinishedGoodsMinutesController : ControllerBase
    {
        private readonly IFinishedGoodsMinutesService _service;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidateService _validateService;

        public FinishedGoodsMinutesController(IFinishedGoodsMinutesService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            _service = service;
            _identityProvider = identityProvider;
            _validateService = validateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string invoiceNo)
        {
            try
            {
                _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
                var data =  await _service.GetReportQuery(invoiceNo);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Count },
                    };

                return Ok(new
                {
                    data = data,
                    info
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> GetXls([FromQuery] string invoiceNo)
        {
            try
            {
                _identityProvider.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

                byte[] xlsInBytes;

                var xls = await _service.GetExcel(invoiceNo);

                string filename = String.Format("Laporan Risalah Barang Jadi - {0}.xlsx", invoiceNo);

                xlsInBytes = xls.ToArray();
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
