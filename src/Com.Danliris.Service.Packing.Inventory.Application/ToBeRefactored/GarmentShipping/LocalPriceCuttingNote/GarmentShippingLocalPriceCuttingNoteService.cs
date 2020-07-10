using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteService : IGarmentShippingLocalPriceCuttingNoteService
    {
        private readonly IGarmentShippingLocalPriceCuttingNoteRepository _repository;
        private readonly IServiceProvider _serviceProvider;

        public GarmentShippingLocalPriceCuttingNoteService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingLocalPriceCuttingNoteRepository>();
            _serviceProvider = serviceProvider;
        }

        private GarmentShippingLocalPriceCuttingNoteViewModel MapToViewModel(GarmentShippingLocalPriceCuttingNoteModel model)
        {
            GarmentShippingLocalPriceCuttingNoteViewModel viewModel = new GarmentShippingLocalPriceCuttingNoteViewModel
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

                cuttingPriceNoteNo = model.CuttingPriceNoteNo,
                date = model.Date,
                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                },
                useVat = model.UseVat,
                remark = model.Remark,
                items = (model.Items ?? new List<GarmentShippingLocalPriceCuttingNoteItemModel>()).Select(i => new GarmentShippingLocalPriceCuttingNoteItemViewModel
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

                    salesNoteId = i.SalesNoteId,
                    salesNoteNo = i.SalesNoteNo,
                    salesAmount = i.SalesAmount,
                    cuttingAmount = i.CuttingAmount
                }).ToList()
            };

            return viewModel;
        }

        private GarmentShippingLocalPriceCuttingNoteModel MapToModel(GarmentShippingLocalPriceCuttingNoteViewModel vm)
        {
            var items = (vm.items ?? new List<GarmentShippingLocalPriceCuttingNoteItemViewModel>()).Select(i =>
            {
                return new GarmentShippingLocalPriceCuttingNoteItemModel(i.salesNoteId, i.salesNoteNo, i.salesAmount, i.cuttingAmount) { Id = i.Id };
            }).ToList();

            vm.buyer = vm.buyer ?? new Buyer();
            return new GarmentShippingLocalPriceCuttingNoteModel(GenerateNo(vm), vm.date.GetValueOrDefault(), vm.buyer.Id, vm.buyer.Code, vm.buyer.Name, vm.useVat, vm.remark, items) { Id = vm.Id };
        }

        private string GenerateNo(GarmentShippingLocalPriceCuttingNoteViewModel vm)
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"{year}/NPH/";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.CuttingPriceNoteNo.StartsWith(prefix))
                .OrderByDescending(o => o.CuttingPriceNoteNo)
                .Select(s => int.Parse(s.CuttingPriceNoteNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingLocalPriceCuttingNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingLocalPriceCuttingNoteViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "CuttingPriceNoteNo", "BuyerCode", "BuyerName"
            };
            query = QueryHelper<GarmentShippingLocalPriceCuttingNoteModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalPriceCuttingNoteModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalPriceCuttingNoteModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingLocalPriceCuttingNoteViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingLocalPriceCuttingNoteViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public Buyer GetBuyer(int id)
        {
            string buyerUri = "master/garment-leftover-warehouse-buyers";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{APIEndpoint.Core}{buyerUri}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                Buyer viewModel = JsonConvert.DeserializeObject<Buyer>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return null;
            }
        }
    }
}
