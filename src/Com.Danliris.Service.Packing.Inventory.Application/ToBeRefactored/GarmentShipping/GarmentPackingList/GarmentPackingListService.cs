using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListService : IGarmentPackingListService
    {
        private readonly IGarmentPackingListRepository _packingListRepository;
        private readonly IGarmentShippingInvoiceRepository _invoiceRepository;

        public GarmentPackingListService(IServiceProvider serviceProvider)
        {
            _packingListRepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _invoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
        }

        private GarmentPackingListViewModel MapToViewModel(GarmentPackingListModel model)
        {
            var vm =  new GarmentPackingListViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,

                InvoiceNo = model.InvoiceNo,
                PackingListType = model.PackingListType,
                InvoiceType = model.InvoiceType,
                Section = new Section
                {
                    Id = model.SectionId,
                    Code = model.SectionCode
                },
                Date = model.Date,
                LCNo = model.LCNo,
                IssuedBy = model.IssuedBy,
                BuyerAgent = new Buyer
                {
                    Id = model.BuyerAgentId,
                    Code = model.BuyerAgentCode,
                    Name = model.BuyerAgentName,
                },
                Destination = model.Destination,
                TruckingDate = model.TruckingDate,
                ExportEstimationDate = model.ExportEstimationDate,
                Omzet = model.Omzet,
                Accounting = model.Accounting,
                IsUsed=model.IsUsed,
                Items = (model.Items ?? new List<GarmentPackingListItemModel>()).Select(i => new GarmentPackingListItemViewModel
                {
                    Active = i.Active,
                    Id = i.Id,
                    CreatedAgent = i.CreatedAgent,
                    CreatedBy = i.CreatedBy,
                    CreatedUtc = i.CreatedUtc,
                    DeletedAgent = i.DeletedAgent,
                    DeletedBy = i.DeletedBy,
                    DeletedUtc = i.DeletedUtc,
                    IsDeleted = i.IsDeleted,
                    LastModifiedAgent = i.LastModifiedAgent,
                    LastModifiedBy = i.LastModifiedBy,
                    LastModifiedUtc = i.LastModifiedUtc,

                    RONo = i.RONo,
                    SCNo = i.SCNo,
                    BuyerBrand = new Buyer
                    {
                        Id = i.BuyerBrandId,
                        Name = i.BuyerBrandName
                    },
                    Comodity = new Comodity
                    {
                        Id = i.ComodityId,
                        Code = i.ComodityCode,
                        Name = i.ComodityName
                    },
                    ComodityDescription = i.ComodityDescription,
                    Quantity = i.Quantity,
                    Uom = new UnitOfMeasurement
                    {
                        Id = i.UomId,
                        Unit = i.UomUnit
                    },
                    PriceRO = i.PriceRO,
                    Price = i.Price,
                    Amount = i.Amount,
                    Valas = i.Valas,
                    Unit = new Unit
                    {
                        Id = i.UnitId,
                        Code = i.UnitCode
                    },
                    Article = i.Article,
                    OrderNo = i.OrderNo,
                    Description = i.Description,

                    Details = (i.Details ?? new List<GarmentPackingListDetailModel>()).Select(d => new GarmentPackingListDetailViewModel
                    {
                        Active = d.Active,
                        Id = d.Id,
                        CreatedAgent = d.CreatedAgent,
                        CreatedBy = d.CreatedBy,
                        CreatedUtc = d.CreatedUtc,
                        DeletedAgent = d.DeletedAgent,
                        DeletedBy = d.DeletedBy,
                        DeletedUtc = d.DeletedUtc,
                        IsDeleted = d.IsDeleted,
                        LastModifiedAgent = d.LastModifiedAgent,
                        LastModifiedBy = d.LastModifiedBy,
                        LastModifiedUtc = d.LastModifiedUtc,

                        Carton1 = d.Carton1,
                        Carton2 = d.Carton2,
                        Colour = d.Colour,
                        CartonQuantity = d.CartonQuantity,
                        QuantityPCS = d.QuantityPCS,
                        TotalQuantity = d.TotalQuantity,

                        Sizes = (d.Sizes ?? new List<GarmentPackingListDetailSizeModel>()).Select(s => new GarmentPackingListDetailSizeViewModel
                        {
                            Active = s.Active,
                            Id = s.Id,
                            CreatedAgent = s.CreatedAgent,
                            CreatedBy = s.CreatedBy,
                            CreatedUtc = s.CreatedUtc,
                            DeletedAgent = s.DeletedAgent,
                            DeletedBy = s.DeletedBy,
                            DeletedUtc = s.DeletedUtc,
                            IsDeleted = s.IsDeleted,
                            LastModifiedAgent = s.LastModifiedAgent,
                            LastModifiedBy = s.LastModifiedBy,
                            LastModifiedUtc = s.LastModifiedUtc,

                            Size = new SizeViewModel
                            {
                                Id = s.SizeId,
                                Size = s.Size
                            },
                            Quantity = s.Quantity
                        }).ToList()

                    }).ToList(),

                    AVG_GW = i.AVG_GW,
                    AVG_NW = i.AVG_NW,
                }).ToList(),

                GrossWeight = model.GrossWeight,
                NettWeight = model.NettWeight,
                TotalCartons = model.TotalCartons,
                Measurements = (model.Measurements ?? new List<GarmentPackingListMeasurementModel>()).Select(m => new GarmentPackingListMeasurementViewModel
                {
                    Active = m.Active,
                    Id = m.Id,
                    CreatedAgent = m.CreatedAgent,
                    CreatedBy = m.CreatedBy,
                    CreatedUtc = m.CreatedUtc,
                    DeletedAgent = m.DeletedAgent,
                    DeletedBy = m.DeletedBy,
                    DeletedUtc = m.DeletedUtc,
                    IsDeleted = m.IsDeleted,
                    LastModifiedAgent = m.LastModifiedAgent,
                    LastModifiedBy = m.LastModifiedBy,
                    LastModifiedUtc = m.LastModifiedUtc,

                    Length = m.Length,
                    Width = m.Width,
                    Height = m.Height,
                    CartonsQuantity = m.CartonsQuantity,

                }).ToList(),

                ShippingMark = model.ShippingMark,
                SideMark = model.SideMark,
                Remark = model.Remark,
            };
            return vm;
        }

        private GarmentPackingListModel MapToModel(GarmentPackingListViewModel viewModel)
        {
            var items = (viewModel.Items ?? new List<GarmentPackingListItemViewModel>()).Select(i =>
            {
                var details = (i.Details ?? new List<GarmentPackingListDetailViewModel>()).Select(d =>
                {
                    var sizes = (d.Sizes ?? new List<GarmentPackingListDetailSizeViewModel>()).Select(s =>
                    {
                        s.Size = s.Size ?? new SizeViewModel();
                        return new GarmentPackingListDetailSizeModel(s.Size.Id, s.Size.Size, s.Quantity) { Id = s.Id };
                    }).ToList();

                    return new GarmentPackingListDetailModel(d.Carton1, d.Carton2, d.Colour, d.CartonQuantity, d.QuantityPCS, d.TotalQuantity, sizes) { Id = d.Id };
                }).ToList();

                i.BuyerBrand = i.BuyerBrand ?? new Buyer();
                i.Uom = i.Uom ?? new UnitOfMeasurement();
                i.Unit = i.Unit ?? new Unit();
                i.Comodity = i.Comodity ?? new Comodity();
                return new GarmentPackingListItemModel(i.RONo, i.SCNo, i.BuyerBrand.Id, i.BuyerBrand.Name, i.Comodity.Id, i.Comodity.Code, i.Comodity.Name, i.ComodityDescription, i.Quantity, i.Uom.Id.GetValueOrDefault(), i.Uom.Unit, i.PriceRO, i.Price, i.Amount, i.Valas, i.Unit.Id, i.Unit.Code, i.Article, i.OrderNo, i.Description, details, i.AVG_GW, i.AVG_NW)
                {
                    Id = i.Id
                };
            }).ToList();

            var measurements = (viewModel.Measurements ?? new List<GarmentPackingListMeasurementViewModel>()).Select(m => new GarmentPackingListMeasurementModel(m.Length, m.Width, m.Height, m.CartonsQuantity)
            {
                Id = m.Id
            }).ToList();

            viewModel.Section = viewModel.Section ?? new Section();
            viewModel.BuyerAgent = viewModel.BuyerAgent ?? new Buyer();
            viewModel.InvoiceNo = GenerateInvoiceNo(viewModel);
            GarmentPackingListModel garmentPackingListModel = new GarmentPackingListModel(viewModel.InvoiceNo, viewModel.PackingListType, viewModel.InvoiceType, viewModel.Section.Id, viewModel.Section.Code, viewModel.Date.GetValueOrDefault(), viewModel.LCNo, viewModel.IssuedBy, viewModel.BuyerAgent.Id, viewModel.BuyerAgent.Code, viewModel.BuyerAgent.Name, viewModel.Destination, viewModel.TruckingDate.GetValueOrDefault(), viewModel.ExportEstimationDate.GetValueOrDefault(), viewModel.Omzet, viewModel.Accounting, items, viewModel.GrossWeight, viewModel.NettWeight, viewModel.TotalCartons, measurements, viewModel.ShippingMark, viewModel.SideMark, viewModel.Remark, viewModel.IsUsed);

            return garmentPackingListModel;
        }

        public async Task<int> Create(GarmentPackingListViewModel viewModel)
        {
            GarmentPackingListModel garmentPackingListModel = MapToModel(viewModel);

            int Created = await _packingListRepository.InsertAsync(garmentPackingListModel);

            return Created;
        }

        private string GenerateInvoiceNo(GarmentPackingListViewModel viewModel)
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"{(viewModel.InvoiceType ?? "").Trim().ToUpper()}/{year}";

            var lastInvoiceNo = _packingListRepository.ReadAll().Where(w => w.InvoiceNo.StartsWith(prefix))
                .OrderByDescending(o => o.InvoiceNo)
                .Select(s => int.Parse(s.InvoiceNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D4")}";

            return invoiceNo;
        }

        public ListResult<GarmentPackingListViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _packingListRepository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "InvoiceNo", "PackingListType", "SectionCode", "Destination"
            };
            query = QueryHelper<GarmentPackingListModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentPackingListModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentPackingListModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentPackingListViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentPackingListViewModel> ReadById(int id)
        {
            var data = await _packingListRepository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

		public ListResult<GarmentPackingListViewModel> ReadNotUsed(int page, int size, string filter, string order, string keyword)
		{
			var query = _packingListRepository.ReadAll();
			List<string> SearchAttributes = new List<string>()
			{
				"InvoiceNo"
			};
			query = QueryHelper<GarmentPackingListModel>.Search(query, SearchAttributes, keyword);

			Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
			query = QueryHelper<GarmentPackingListModel>.Filter(query, FilterDictionary);

			Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
			query = QueryHelper<GarmentPackingListModel>.Order(query, OrderDictionary);

			var data = query
				.Skip((page - 1) * size)
				.Take(size)
				.Where(s=>s.IsUsed== false)
				.Select(model => MapToViewModel(model))
				.ToList();

			return new ListResult<GarmentPackingListViewModel>(data, page, size, query.Count());
		}



		public async Task<int> Update(int id, GarmentPackingListViewModel viewModel)
        {
            GarmentPackingListModel garmentPackingListModel = MapToModel(viewModel);

            return await _packingListRepository.UpdateAsync(id, garmentPackingListModel);
        }

        public async Task<int> Delete(int id)
        {
            return await _packingListRepository.DeleteAsync(id);
        }

        public async Task<ExcelResult> ReadPdfById(int id)
        {
            var data = await _packingListRepository.ReadByIdAsync(id);

            var PdfTemplate = new GarmentPackingListPdfTemplate();
            var fob = _invoiceRepository.ReadAll().Where(w => w.PackingListId == data.Id).Select(s => s.From).FirstOrDefault();

            var stream = PdfTemplate.GeneratePdfTemplate(MapToViewModel(data), fob);

            return new ExcelResult(stream, "Packing List " + data.InvoiceNo + ".pdf");
        }
    }
}
