using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteService : IGarmentShippingLocalPriceCorrectionNoteService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IGarmentShippingLocalPriceCorrectionNoteRepository _repository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentShippingLocalPriceCorrectionNoteService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _repository = serviceProvider.GetService<IGarmentShippingLocalPriceCorrectionNoteRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private GarmentShippingLocalPriceCorrectionNoteViewModel MapToViewModel(GarmentShippingLocalPriceCorrectionNoteModel model)
        {
            GarmentShippingLocalPriceCorrectionNoteViewModel viewModel = new GarmentShippingLocalPriceCorrectionNoteViewModel
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

                correctionNoteNo = model.CorrectionNoteNo,
                correctionDate = model.CorrectionDate,
                salesNote = new GarmentShippingLocalSalesNoteViewModel
                {
                    Active = model.SalesNote.Active,
                    Id = model.SalesNote.Id,
                    CreatedAgent = model.SalesNote.CreatedAgent,
                    CreatedBy = model.SalesNote.CreatedBy,
                    CreatedUtc = model.SalesNote.CreatedUtc,
                    DeletedAgent = model.SalesNote.DeletedAgent,
                    DeletedBy = model.SalesNote.DeletedBy,
                    DeletedUtc = model.SalesNote.DeletedUtc,
                    IsDeleted = model.SalesNote.IsDeleted,
                    LastModifiedAgent = model.SalesNote.LastModifiedAgent,
                    LastModifiedBy = model.SalesNote.LastModifiedBy,
                    LastModifiedUtc = model.SalesNote.LastModifiedUtc,

                    noteNo = model.SalesNote.NoteNo,
                    date = model.SalesNote.Date,
                    transactionType = new TransactionType
                    {
                        id = model.SalesNote.TransactionTypeId,
                        code = model.SalesNote.TransactionTypeCode,
                        name = model.SalesNote.TransactionTypeName
                    },
                    buyer = new Buyer
                    {
                        Id = model.SalesNote.BuyerId,
                        Code = model.SalesNote.BuyerCode,
                        Name = model.SalesNote.BuyerName,
                        npwp = model.SalesNote.BuyerNPWP
                    },
                    tempo = model.SalesNote.Tempo,
                    dispositionNo = model.SalesNote.DispositionNo,
                    useVat = model.SalesNote.UseVat,
                    remark = model.SalesNote.Remark,
                    isUsed = model.SalesNote.IsUsed,
                },
                remark = model.Remark,

                items = (model.Items ?? new List<GarmentShippingLocalPriceCorrectionNoteItemModel>()).Select(i => new GarmentShippingLocalPriceCorrectionNoteItemViewModel
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

                    isChecked = true,
                    priceCorrection = i.PriceCorrection,
                    salesNoteItem = new GarmentShippingLocalSalesNoteItemViewModel
                    {
                        Active = i.SalesNoteItem.Active,
                        Id = i.SalesNoteItem.Id,
                        CreatedAgent = i.SalesNoteItem.CreatedAgent,
                        CreatedBy = i.SalesNoteItem.CreatedBy,
                        CreatedUtc = i.SalesNoteItem.CreatedUtc,
                        DeletedAgent = i.SalesNoteItem.DeletedAgent,
                        DeletedBy = i.SalesNoteItem.DeletedBy,
                        DeletedUtc = i.SalesNoteItem.DeletedUtc,
                        IsDeleted = i.SalesNoteItem.IsDeleted,
                        LastModifiedAgent = i.SalesNoteItem.LastModifiedAgent,
                        LastModifiedBy = i.SalesNoteItem.LastModifiedBy,
                        LastModifiedUtc = i.SalesNoteItem.LastModifiedUtc,

                        product = new ProductViewModel
                        {
                            id = i.SalesNoteItem.ProductId,
                            code = i.SalesNoteItem.ProductCode,
                            name = i.SalesNoteItem.ProductName
                        },
                        quantity = i.SalesNoteItem.Quantity,
                        uom = new UnitOfMeasurement
                        {
                            Id = i.SalesNoteItem.UomId,
                            Unit = i.SalesNoteItem.UomUnit
                        },
                        price = i.SalesNoteItem.Price
                    }

                }).ToList()
            };

            return viewModel;
        }

        private GarmentShippingLocalPriceCorrectionNoteModel MapToModel(GarmentShippingLocalPriceCorrectionNoteViewModel vm)
        {
            var items = (vm.items ?? new List<GarmentShippingLocalPriceCorrectionNoteItemViewModel>())
                .Where(w => w.isChecked)
                .Select(i =>
                {
                    i.salesNoteItem = i.salesNoteItem ?? new GarmentShippingLocalSalesNoteItemViewModel();
                    return new GarmentShippingLocalPriceCorrectionNoteItemModel(i.salesNoteItem.Id, null, i.priceCorrection) { Id = i.Id };
                }).ToList();

            vm.salesNote = vm.salesNote ?? new GarmentShippingLocalSalesNoteViewModel();
            return new GarmentShippingLocalPriceCorrectionNoteModel(GenerateNo(), vm.correctionDate.GetValueOrDefault(), vm.salesNote.Id, null, vm.remark, items) { Id = vm.Id };
        }

        private string GenerateNo()
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"{year}/NKH/";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.CorrectionNoteNo.StartsWith(prefix))
                .OrderByDescending(o => o.CorrectionNoteNo)
                .Select(s => int.Parse(s.CorrectionNoteNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingLocalPriceCorrectionNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "CorrectionNoteNo", "SalesNote.NoteNo", "SalesNote.BuyerCode", "SalesNote.BuyerName", "SalesNote.DispositionNo"
            };
            query = QueryHelper<GarmentShippingLocalPriceCorrectionNoteModel>.Search(query, SearchAttributes, keyword, ignoreDot: true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalPriceCorrectionNoteModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalPriceCorrectionNoteModel>.Order(query, OrderDictionary, ignoreDot: true);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    id = model.Id,
                    correctionNoteNo = model.CorrectionNoteNo,
                    correctionDate = model.CorrectionDate,
                    salesNote = new SalesNote
                    {
                        id = model.SalesNote.Id,
                        noteNo = model.SalesNote.NoteNo,
                        buyer = new Buyer
                        {
                            Code = model.SalesNote.BuyerCode,
                            Name = model.SalesNote.BuyerName
                        },
                        date = model.SalesNote.Date,
                        tempo = model.SalesNote.Tempo,
                        dispositionNo = model.SalesNote.DispositionNo
                    }
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingLocalPriceCorrectionNoteViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<ExcelResult> ReadPdfById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);
            var buyer = await GetBuyer(viewModel.salesNote.buyer.Id);

            var PdfTemplate = new GarmentShippingLocalPriceCorrectionNotePdfTemplate();

            var stream = PdfTemplate.GeneratePdfTemplate(viewModel, buyer, _identityProvider.TimezoneOffset);

            return new ExcelResult(stream, "Nota Koreksi " + data.CorrectionNoteNo + ".pdf");
        }

        async Task<Buyer> GetBuyer(int id)
        {
            string buyerUri = "master/garment-leftover-warehouse-buyers";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.GetAsync($"{APIEndpoint.Core}{buyerUri}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                Buyer viewModel = JsonConvert.DeserializeObject<Buyer>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return new Buyer();
            }
        }
    }
}
