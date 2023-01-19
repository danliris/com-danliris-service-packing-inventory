
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
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

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.GarmentShippingInvoice
{
	[Produces("application/json")]
	[Route("v1/garment-shipping/invoices")]
	[Authorize]
	public class GarmentShippingInvoiceController : ControllerBase
	{
		private readonly IGarmentShippingInvoiceService _service;
		private readonly IGarmentPackingListService _packingListService;
		private readonly IIdentityProvider _identityProvider;
		private readonly IValidateService _validateService;
		public GarmentShippingInvoiceController(IGarmentShippingInvoiceService service, IGarmentPackingListService packingListService, IIdentityProvider identityProvider, IValidateService validateService)
		{
			_service = service;
			_identityProvider = identityProvider;
			_validateService = validateService;
			_packingListService = packingListService;

		}

		protected void VerifyUser()
		{
			_identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
			_identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
			_identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] GarmentShippingInvoiceViewModel viewModel)
		{
			try
			{
				VerifyUser();
				_validateService.Validate(viewModel);
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
		public IActionResult Get([FromQuery] string keyword = null, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery]string order = "{}", [FromQuery] string filter = "{}")
		{
			try
			{
				var data = _service.Read(page, size, filter, order, keyword);
				var info = new Dictionary<string, object>
					{
						{ "count", data.Data.Count },
						{ "total", data.Total },
						{ "order", order },
						{ "page", page },
						{ "size", size }
					};

				return Ok(new
				{
					data = data.Data,
					info
				});


			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put([FromRoute] int id, [FromBody] GarmentShippingInvoiceViewModel viewModel)
		{
			try
			{
				VerifyUser();
				_validateService.Validate(viewModel);
				var result = await _service.Update(id, viewModel);

				return Ok(result);
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

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			try
			{
				VerifyUser();
				var result = await _service.Delete(id);

				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}

		}

		[HttpGet("pdf/{Id}/{type}")]
		public async Task<IActionResult> GetPDF([FromRoute] int Id, [FromRoute] string type)
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
					Buyer buyer = _service.GetBuyer(model.BuyerAgent.Id);
					BankAccount bank = _service.GetBank(model.BankAccountId);
					GarmentPackingListViewModel pl = await _packingListService.ReadById(model.PackingListId);
					if (type == "fob")
					{
						var PdfTemplate = new GarmentShippingInvoicePdfTemplate();
						MemoryStream stream = PdfTemplate.GeneratePdfTemplate(model, buyer, bank, pl, timeoffsset);

						return new FileStreamResult(stream, "application/pdf")
						{
							FileDownloadName = model.InvoiceNo + "-Invoice" + ".pdf"
						};
					}
					else
					{
						var PdfTemplate = new GarmentShippingInvoiceCMTPdfTemplate();
						MemoryStream stream = PdfTemplate.GeneratePdfTemplate(model, buyer, bank, pl, timeoffsset);

						return new FileStreamResult(stream, "application/pdf")
						{
							FileDownloadName = model.InvoiceNo + "-CMT" + ".pdf"
						};
					}

				}
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpGet("xls/{Id}/{type}")]
		public async Task<IActionResult> GetXLS([FromRoute] int Id, [FromRoute] string type)
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
				var indexAcceptXls = Request.Headers["Accept"].ToList().IndexOf("application/xls");
				int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
				var model = await _service.ReadById(Id);

				if (model == null)
				{
					return StatusCode((int)HttpStatusCode.NotFound, "Not Found");
				}
				else
				{
					if (type == "fob")
					{
						VerifyUser();
						var result = await _service.ReadInvoiceExcelById(model.Id);

						return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.FileName);
					}
					else
					{
						{
							VerifyUser();
							var result = await _service.ReadInvoiceCMTExcelById(model.Id);

							return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.FileName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpGet("whpdf/{Id}/{type}")]
		public async Task<IActionResult> GetWHPDF([FromRoute] int Id, [FromRoute] string type)
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
					Buyer buyer = _service.GetBuyer(model.BuyerAgent.Id);
					BankAccount bank = _service.GetBank(model.BankAccountId);
					GarmentPackingListViewModel pl = await _packingListService.ReadById(model.PackingListId);
					if (type == "fob")
					{
						var PdfTemplate = new GarmentShippingInvoiceWithHeaderPdfTemplate();
						MemoryStream stream = PdfTemplate.GeneratePdfTemplate(model, buyer, bank, pl, timeoffsset);

						return new FileStreamResult(stream, "application/pdf")
						{
							FileDownloadName = model.InvoiceNo + "-Invoice" + ".pdf"
						};
					}
					else
					{
						var PdfTemplate = new GarmentShippingInvoiceCMTWithHeaderPdfTemplate();
						MemoryStream stream = PdfTemplate.GeneratePdfTemplate(model, buyer, bank, pl, timeoffsset);

						return new FileStreamResult(stream, "application/pdf")
						{
							FileDownloadName = model.InvoiceNo + "-CMT" + ".pdf"
						};
					}

				}
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpGet("exportSalesDebtor")]
		public IActionResult GetExportSalesDebtor(int month, int year)
		{
			try
			{
				var data = _service.ReadShippingPackingList(month, year);

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
		[HttpGet("exportSalesDebtorNow")]
		public IActionResult GetExportSalesDebtorNow(int month, int year)
		{
			try
			{
				var data = _service.ReadShippingPackingListNow(month, year);

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

		[HttpGet("packing-list-for-debtor-card")]
		public IActionResult GetPLForDebtorCard(int month, int year, string buyer)
		{
			try
			{
				var data = _service.ReadShippingPackingListForDebtorCard(month, year, buyer);

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

		[HttpGet("packing-list-for-debtor-card-now")]
		public IActionResult GetPLForDebtorCardNow(int month, int year, string buyer)
		{
			try
			{
				var data = _service.ReadShippingPackingListForDebtorCardNow(month, year, buyer);

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

		[HttpGet("packingListById/{id}")]
		public IActionResult GetShippingInvoiceByPLId([FromRoute] int id)
		{
			try
			{
				ShippingPackingListViewModel data = _service.ReadShippingPackingListById(id);

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
	}
}