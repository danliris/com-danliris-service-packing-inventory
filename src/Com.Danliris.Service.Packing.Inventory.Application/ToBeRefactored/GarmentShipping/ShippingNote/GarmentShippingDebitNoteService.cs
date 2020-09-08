using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote
{
    public class GarmentShippingDebitNoteService : IGarmentShippingDebitNoteService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IGarmentShippingNoteRepository _repository;

        public GarmentShippingDebitNoteService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _repository = serviceProvider.GetService<IGarmentShippingNoteRepository>();
        }

        private GarmentShippingDebitNoteViewModel MapToViewModel(GarmentShippingNoteModel model)
        {
            GarmentShippingDebitNoteViewModel viewModel = new GarmentShippingDebitNoteViewModel
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

                noteNo = model.NoteNo,
                date = model.Date,
                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName
                },
                bank = new BankAccount
                {
                    id = model.BankId,
                    bankName = model.BankName,
                    Currency = new Currency
                    {
                        Code = model.BankCurrencyCode
                    }
                },
                totalAmount = model.TotalAmount,

                items = (model.Items ?? new List<GarmentShippingNoteItemModel>()).Select(i => new GarmentShippingNoteItemViewModel
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

                    description = i.Description,
                    currency = new Currency
                    {
                        Id = i.CurrencyId,
                        Code = i.CurrencyCode
                    },
                    amount = i.Amount
                }).ToList()
            };

            return viewModel;
        }

        private GarmentShippingNoteModel MapToModel(GarmentShippingDebitNoteViewModel viewModel)
        {
            var items = (viewModel.items ?? new List<GarmentShippingNoteItemViewModel>()).Select(i =>
            {
                i.currency = i.currency ?? new Currency();
                return new GarmentShippingNoteItemModel(i.description, i.currency.Id.GetValueOrDefault(), i.currency.Code, i.amount) { Id = i.Id };
            }).ToList();

            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.bank = viewModel.bank ?? new BankAccount { Currency = new Currency() };
            viewModel.bank.Currency = viewModel.bank.Currency ?? new Currency();
            GarmentShippingNoteModel model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.ND, GenerateNo(), viewModel.date.GetValueOrDefault(), viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.bank.id, viewModel.bank.bankName, viewModel.bank.Currency.Code, viewModel.totalAmount, items);

            return model;
        }

        private string GenerateNo()
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"{year}ND";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.NoteNo.StartsWith(prefix))
                .OrderByDescending(o => o.NoteNo)
                .Select(s => int.Parse(s.NoteNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D4")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingDebitNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingDebitNoteViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(w => w.NoteType == GarmentShippingNoteTypeEnum.ND);
            List<string> SearchAttributes = new List<string>()
            {
                "NoteNo", "BuyerCode", "BuyerName"
            };
            query = QueryHelper<GarmentShippingNoteModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingNoteModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingNoteModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingDebitNoteViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingDebitNoteViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingDebitNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            return await _repository.UpdateAsync(id, model);
        }

        public async Task<ExcelResult> ReadPdfById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);
            viewModel.buyer = await GetBuyer(viewModel.buyer.Id);
            viewModel.bank = await GetBank(viewModel.bank.id);

            var PdfTemplate = new GarmentShippingDebitNotePdfTemplate();

            var stream = PdfTemplate.GeneratePdfTemplate(viewModel);

            return new ExcelResult(stream, "Nota Debet " + data.NoteNo + ".pdf");
        }

        async Task<Buyer> GetBuyer(int id)
        {
            string buyerUri = "master/garment-buyers";
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

        async Task<BankAccount> GetBank(int id)
        {
            string bankUri = "master/account-banks";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.GetAsync($"{APIEndpoint.Core}{bankUri}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                BankAccount viewModel = JsonConvert.DeserializeObject<BankAccount>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return new BankAccount
                {
                    Currency = new Currency()
                };
            }
        }

    }
}
